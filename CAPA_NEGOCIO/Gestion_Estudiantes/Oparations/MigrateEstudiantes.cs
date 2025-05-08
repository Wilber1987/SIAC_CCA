using CAPA_DATOS;
using CAPA_NEGOCIO.Util;
using DataBaseModel;
using CAPA_DATOS.Security;
using Microsoft.Extensions.Configuration;
using Renci.SshNet;
using CAPA_NEGOCIO.Services;

namespace CAPA_NEGOCIO.Oparations
{
	public class MigrateEstudiantes : TransactionalClass
	{
		private readonly SshTunnelService _sshTunnelService;

		public MigrateEstudiantes()
		{
			_sshTunnelService = new SshTunnelService(LoadConfiguration());
		}

		public async Task Migrate()
		{
			await MigrateParentesco();
			await MigrateFamilia();
			await migrateEstudiantesSiac(_sshTunnelService);
			await MigrateParientesAndUsers();
			await migrateEstudiantesReponsablesFamilia();
		}

		private IConfigurationRoot LoadConfiguration()
		{
			return new ConfigurationBuilder()
				.SetBasePath(Directory.GetCurrentDirectory())
				.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
				.Build();
		}

		public async Task<bool> MigrateParentesco()
		{
			Console.Write("-->MigrateParentesco");
			using (var client = _sshTunnelService.GetSshClient("Bellacom"))
			{
				client.Connect();
				var forwardedPort = _sshTunnelService.GetForwardedPort("Bellacom", client, 3308);
				forwardedPort.Start();

				try
				{
					var parentescos2 = new Tbl_gen_relacionfamilar();
					parentescos2.SetConnection(MySqlConnections.BellacomTest);
					var parentescosMsql2 = parentescos2.Get<Tbl_gen_relacionfamilar>();

					parentescosMsql2.ForEach(tn =>
					{
						var existingParentesco = new Parentesco()
						{
							Id = tn.idrelacionfamiliar
						}.SimpleFind<Parentesco>();

						if (existingParentesco != null)
						{
							existingParentesco.Id = tn.idrelacionfamiliar;
							existingParentesco.Sigla = tn.idtrelacionfamiliar;
							existingParentesco.Descripcion = tn.texto;
							existingParentesco.Update();
						}
						else
						{
							Parentesco newParentesco = new Parentesco
							{
								Id = tn.idrelacionfamiliar,
								Sigla = tn.idtrelacionfamiliar,
								Descripcion = tn.texto
							};
							newParentesco.Save();
						}
					});

				}
				catch (Exception ex)
				{
					LoggerServices.AddMessageError("ERROR: MigrateEstudiantes.MigrateParentesco.", ex);					
					forwardedPort.Stop();
					client.Disconnect();					
				}
				finally
				{
					if (forwardedPort.IsStarted)
					{
						forwardedPort.Stop();
					}
					
					client.Disconnect();
				}
			}
			return true;
		}

		public async Task<bool> migrateEstudiantesSiac(SshTunnelService sshService)
		{
			Console.Write("-->migrateEstudiantes");

			try
			{
				
				using (var siacSshClient = sshService.GetSshClient("Siac"))
				{
					siacSshClient.Connect();
					var siacTunnel = sshService.GetForwardedPort("Siac", siacSshClient, 3307);
					siacTunnel.Start();

					// Obtener estudiantes de SiacTestViewEstudiantesActivosSiac
					var estudiante = new ViewEstudiantesActivosSiac();
					estudiante.SetConnection(MySqlConnections.SiacTest);
					estudiante.CreateViewEstudiantesActivos();
					var EstudiantesMsql = estudiante.Get<ViewEstudiantesActivosSiac>();				

					estudiante.DestroyView("viewestudiantesactivossiac");
					Console.Write("Estudiantes encontrados: " + EstudiantesMsql.Count);
					int i = 0;
					using (var bellacomSshClient = sshService.GetSshClient("Bellacom"))
					{
						bellacomSshClient.Connect();
						var bellacomTunnel = sshService.GetForwardedPort("Bellacom", bellacomSshClient, 3308);
						bellacomTunnel.Start();

						var estudianteview = new ViewEstudiantesMigracion();
						estudianteview.SetConnection(MySqlConnections.BellacomTest);
						estudianteview.CreateView();

						foreach (var est in EstudiantesMsql)
						{
							var existingEstudiante = new Estudiantes() { Codigo = est.Codigo }.SimpleFind<Estudiantes>();
							Console.Write("migrando estudiantes: " + i.ToString()); i++;
							est.Fecha_nacimiento = DateUtil.ValidSqlDateTime(est.Fecha_nacimiento.GetValueOrDefault());
							est.Updated_at = DateUtil.ValidSqlDateTime(est.Updated_at.GetValueOrDefault());
							est.Created_at = DateUtil.ValidSqlDateTime(est.Created_at.GetValueOrDefault());
							est.Fecha_ingreso = DateUtil.ValidSqlDateTime(est.Fecha_ingreso.GetValueOrDefault());

							if (existingEstudiante != null)
							{
								buildEstudianteSiac(est, existingEstudiante, bellacomSshClient, siacSshClient, sshService);
								existingEstudiante.Update();
							}
							else
							{
								var newEstudiante = new Estudiantes();
								buildEstudianteSiac(est, newEstudiante, bellacomSshClient, siacSshClient, sshService);

								newEstudiante.Save();
							}
						}

						estudianteview.DestroyView("viewestudiantesmigracion");
						bellacomTunnel.Stop();
						bellacomSshClient.Disconnect();
					}
					
					if (siacTunnel.IsStarted)
					{
						siacTunnel.Stop();
					}
					siacSshClient.Disconnect();
				}

				return true;
			}
			catch (Exception ex)
			{
				LoggerServices.AddMessageError("ERROR: migrateEstudiantes.", ex);
				return false;
			}
		}

		private static void buildEstudianteSiac(Estudiantes est, Estudiantes? existingEstudiante, SshClient bellacomSshClient, SshClient siacSshClient, SshTunnelService sshService)
		{
			var bellacomConnection = MySqlConnections.BellacomTest;
			var estudianteView = new ViewEstudiantesMigracion();
			estudianteView.SetConnection(bellacomConnection);
			estudianteView.CreateView();
			var estudiantesView = estudianteView.Where<ViewEstudiantesMigracion>(FilterData.Equal("idtestudiante", est.Codigo)).FirstOrDefault();
			if (estudiantesView == null)
			{
				Console.WriteLine($"Estudiante con código {est.Codigo} no encontrado en la vista de migración. Registro omitido.");
				return;
			}
			
			// Obtener datos de la familia usando BellacomTest
			var familiaJoin = new Tbl_aca_familia();
			familiaJoin.SetConnection(bellacomConnection);
			var familiaDatos = familiaJoin.Where<Tbl_aca_familia>(FilterData.Equal("idfamilia", estudiantesView.Idfamilia)).FirstOrDefault();

			if (familiaDatos != null)
			{
				existingEstudiante.Id_familia = familiaDatos.Idfamilia;
			}
			

			// Asignación de datos básicos del estudiante
			existingEstudiante.Id = est.Id;
			existingEstudiante.Idbellacom = est.Idbellacom;
			existingEstudiante.Primer_nombre = est.Primer_nombre;
			existingEstudiante.Segundo_nombre = est.Segundo_nombre;
			existingEstudiante.Primer_apellido = est.Primer_apellido;
			existingEstudiante.Segundo_apellido = est.Segundo_apellido;
			existingEstudiante.Fecha_nacimiento = est.Fecha_nacimiento;
			existingEstudiante.Sexo = est.Sexo;
			existingEstudiante.Foto = est.Foto;  // Usa el operador null conditional
			existingEstudiante.Direccion = est.Direccion;
			existingEstudiante.Codigo = est.Codigo;
			existingEstudiante.Created_at = est.Created_at;
			existingEstudiante.Updated_at = est.Updated_at;
			existingEstudiante.Fecha_ingreso = estudiantesView.Fechaingreso;
			existingEstudiante.Id_cliente = est.Id_cliente;
			existingEstudiante.Codigomed = est.Codigomed;
			existingEstudiante.Saldoeamd = est.Saldoeamd;
			existingEstudiante.Id_pais = estudiantesView.Idpais;
			existingEstudiante.Id_region = estudiantesView.Idregion;
			existingEstudiante.Id_religion = estudiantesView.Idreligion;
			existingEstudiante.Vivecon = estudiantesView.Vivecon;
			existingEstudiante.Sacramento = estudiantesView.Sacramento;
			existingEstudiante.Aniosacra = estudiantesView.Aniosacra;
			existingEstudiante.Colegio_procede = estudiantesView.Colegio;


			// Verifica la foto de SIAC usando el cliente SSH pasado
			var estudianteSiac = new Estudiantes();
			estudianteSiac.SetConnection(MySqlConnections.SiacTest);
			var existeRelacion = estudianteSiac.Where<Estudiantes>(FilterData.Equal("codigo", existingEstudiante.Codigo)).FirstOrDefault();

			if (existeRelacion != null)
			{
				existingEstudiante.Foto = existeRelacion.Foto;
			}
		}

		public async Task<bool> MigrateFamilia()
		{
			Console.Write("-->MigrateFamilia");
			var rol = validateRolPariente();
			if (rol == null)
			{
				return false;
			}

			var familias = new Tbl_aca_familia();
			var fechaUltimaActualizacion = MigrateService.GetLastUpdate("FAMILIAS");

			using (var client = _sshTunnelService.GetSshClient("Bellacom"))
			{
				client.Connect();
				var forwardedPort = _sshTunnelService.GetForwardedPort("Bellacom", client, 3308);
				forwardedPort.Start();

				familias.SetConnection(MySqlConnections.BellacomTest);
				var filter = new FilterData
				{
					PropName = "fechaactualizacion",
					FilterType = ">=",
					Values = new List<string?> { fechaUltimaActualizacion.ToString()}
				};
				var familiasMsql = familias.Where<Tbl_aca_familia>(filter);

				
				try
				{
					BeginGlobalTransaction();
					familiasMsql.ForEach(tn =>
					{
						var existingFamilia = new Familias()
						{
							Id = tn.Idfamilia
						}.SimpleFind<Familias>();

						if (existingFamilia != null /*&& (existingFamilia.Fecha_ultima_notificacion != tn.Fechaultimanotificacion)*/)
						{
							if (tn.Idtfamilia == null || tn.Idtfamilia == ""){
								Console.Write("-->idtfamilia nulo para la familia: "+tn.Idfamilia);
							}
							existingFamilia.Idtfamilia = tn.Idtfamilia;
							existingFamilia.Descripcion = tn.Texto;
							existingFamilia.Estado = tn.Estatus;
							existingFamilia.Saldo = tn.Saldomd;
							existingFamilia.Actualizado = tn.Actualizado;
							existingFamilia.Aceptacion = tn.Aceptacion;
							existingFamilia.Periodo_aceptacion = tn.Periodoaceptacion;
							existingFamilia.Fecha_actualizacion = tn.Fechaactualizacion;
							existingFamilia.Fecha_ultima_notificacion = tn.Fechaultimanotificacion;
							existingFamilia.Update();
						}
						else if (existingFamilia == null)
						{
							Familias newFamilia = new Familias
							{
								Id = tn.Idfamilia,
								Idtfamilia = tn.Idtfamilia,
								Descripcion = tn.Texto,
								Estado = tn.Estatus,
								Saldo = tn.Saldomd,
								Fecha_actualizacion = tn.Fechaactualizacion,
								Fecha_ultima_notificacion = tn.Fechaultimanotificacion,
								Actualizado = tn.Actualizado,
								Aceptacion = tn.Aceptacion,
								Periodo_aceptacion = tn.Periodoaceptacion
							};

							newFamilia.Save();
						}

					});
					MigrateService.UpdateLastUpdate("FAMILIAS");
					CommitGlobalTransaction();
				}
				catch (Exception ex)
				{
					LoggerServices.AddMessageError("ERROR: MigrateParientes.MigrateFamilia.", ex);
					if (forwardedPort.IsStarted)
					{
						forwardedPort.Stop();
					}
					client.Disconnect();
				}
				finally
				{
					forwardedPort.Stop();
					client.Disconnect();
				}

			}

			return true;
		}

		public async Task<bool> MigrateParientesAndUsers()
		{
			Console.Write("-->MigrateParientesAndUsers");

			// Si no existe el rol de pariente, se debe crear para asignárselo al usuario de cada responsable.
			// Ya que se crea un usuario por cada miembro de familia que tenga el check de responsable.
			var fechaUltimaActualizacion = MigrateService.GetLastUpdate("PARIENTESUSUARIOS");
			var rolResponsable = validateRolPariente();
			if (rolResponsable == null)
			{
				return false;
			}

			// Implementación del túnel SSH
			using (var client = _sshTunnelService.GetSshClient("Bellacom"))
			{
				client.Connect();
				var forwardedPort = _sshTunnelService.GetForwardedPort("Bellacom", client, 3308);
				forwardedPort.Start();

				try
				{
					var data = new Tbl_aca_tutor();
					data.SetConnection(MySqlConnections.BellacomTest);

					var filter = new FilterData
					{
						PropName = "fechamodificacion",
						FilterType = ">=",
						Values = new List<string?> { fechaUltimaActualizacion.ToString()}
					};
					var dataMsql = data.Where<Tbl_aca_tutor>(/*filter*/);

					BeginGlobalTransaction();

					dataMsql.ForEach(static tn =>
					{
						var existing = new Parientes()
						{
							Id = tn.Idtutor
						}.SimpleFind<Parientes>();

						tn.Fechagrabacion = DateUtil.ValidSqlDateTime(tn.Fechagrabacion.GetValueOrDefault());
						tn.Fechamodificacion = DateUtil.ValidSqlDateTime(tn.Fechamodificacion.GetValueOrDefault());
						tn.Fechaactualizacion = DateUtil.ValidSqlDateTime(tn.Fechaactualizacion.GetValueOrDefault());
						tn.Fechanacimiento = DateUtil.ValidSqlDateTime(tn.Fechanacimiento.GetValueOrDefault());

						if (existing != null)
						{
							buildPariente(tn, existing);
							existing.Update();
						}
						else if (existing == null)
						{
							Parientes newPariente = new Parientes();
							buildPariente(tn, newPariente);

							if (newPariente.Responsable_Pago == true && StringUtil.IsValidEmail(tn.Email))
							{
								var rolPadreResponsable = new Security_Roles().Find<Security_Roles>(FilterData.Equal("descripcion", "PADRE_RESPONSABLE"));
								var user = (Security_Users?)new Security_Users
								{
									Nombres = tn.Nombres + " " + tn.Apellidos,
									Estado = "ACTIVO",
									Descripcion = tn.Nombres + " " + tn.Apellidos,
									Password = StringUtil.GeneratePassword(tn.Email, tn.Nombres, tn.Apellidos),
									Mail = tn.Email, //StringUtil.GenerateNickName(tn.Nombres, tn.Apellidos),
									Token = null,
									Security_Users_Roles = new List<Security_Users_Roles>
									{
										new Security_Users_Roles { Security_Role = rolPadreResponsable, Estado = "ACTIVO" }
									}
								}.Save_User(null);

								newPariente.User_id = user.Id_User;
								newPariente.Credenciales_Enviadas = false;
							}

							newPariente.Save();
						}
					});
					MigrateService.UpdateLastUpdate("PARIENTESUSUARIOS");
					CommitGlobalTransaction();
				}
				catch (Exception ex)
				{
					LoggerServices.AddMessageError("ERROR: MigrateParientesAndUsers.", ex);
					if (forwardedPort.IsStarted)
					{
						forwardedPort.Stop();
					}
					client.Disconnect();
					RollBackGlobalTransaction();
					throw;
				}
				finally
				{
					forwardedPort.Stop();
					client.Disconnect();
				}
			}

			return true;
		}

		public async Task<bool> migrateEstudiantesReponsablesFamilia()
		{
			Console.Write("-->migrateEstudiantesReponsablesFamilia");
			var familia = new Familias();
			var familiasSqlserver = familia.Get<Familias>(
			);
			try
			{
				BeginGlobalTransaction();
				familiasSqlserver.ForEach(f =>
				{
					var estudiantesFamilia = new Estudiantes().Where<Estudiantes>(FilterData.Equal("id_familia", f.Id));
					var parientesFamilia = new Parientes().Where<Parientes>(FilterData.Equal("id_familia", f.Id));

					foreach (var estudiante in estudiantesFamilia)
					{
						foreach (var pariente in parientesFamilia)
						{
							var existeRelacion = new Estudiantes_responsables_familia().Where<Estudiantes_responsables_familia>(
								FilterData.Equal("Estudiante_id", estudiante.Id),
								FilterData.Equal("Pariente_id", pariente.Id),
								FilterData.Equal("Familia_id", f.Id)
							).FirstOrDefault();

							if (existeRelacion == null)
							{
								var parentesco = new Parentesco().Where<Parentesco>(
									FilterData.Equal("Id", pariente.Id_relacion_familiar)
								).FirstOrDefault();

								var nuevaRelacion = new Estudiantes_responsables_familia
								{
									Estudiante_id = estudiante.Id,
									Pariente_id = pariente.Id,
									Familia_id = f.Id,
									Created_at = DateTime.Now,
									Updated_at = DateTime.Now,
									Parentesco_id = parentesco?.Id
								};

								nuevaRelacion.Save();
							}
						}
					}

				});

				CommitGlobalTransaction();
			}
			catch (Exception ex)
			{
				LoggerServices.AddMessageError("ERROR: migrateEstudiantesReponsablesFamilia.", ex);
				//RollBackGlobalTransaction();
				//throw;
			}
			return true;
		}
		
		public Security_Roles validateRolPariente()
		{
			try
			{
				Security_Roles? responsableRol = new Security_Roles().Find<Security_Roles>(FilterData.Equal("descripcion", "PADRE_RESPONSABLE"));
				if (responsableRol != null)
				{
					return responsableRol;
				}
				//no existe rol se inserta
				Security_Roles nuevoRol = new Security_Roles
				{
					Descripcion = "PADRE_RESPONSABLE",
					Estado = "ACT"
				};
				return (Security_Roles)nuevoRol.Save();

			}
			catch (Exception ex)
			{
				LoggerServices.AddMessageError("ADVERTENCIA: validateRolPariente - Error al verificar perfil de responsable.", ex);
				return null;
			}
		}

		private static void buildPariente(Tbl_aca_tutor tn, Parientes? existing)
		{
			existing.Id = tn.Idtutor;
			existing.Id_familia = tn.Idfamilia;
			existing.Primer_nombre = StringUtil.GetNombres(tn.Nombres)[0];
			existing.Segundo_nombre = StringUtil.GetNombres(tn.Nombres)[1];
			existing.Primer_apellido = StringUtil.GetNombres(tn.Apellidos)[0];
			existing.Segundo_apellido = StringUtil.GetNombres(tn.Apellidos)[1];
			existing.Sexo = tn.Sexo;
			existing.Identificacion = tn.Noidentificacion;
			existing.Direccion = tn.Direccion;
			existing.Lugar_trabajo = tn.Lugartrabajo;
			existing.Telefono = tn.Telefono;
			existing.Celular = tn.Celular;
			existing.Telefono_trabajo = tn.Telefonotrabajo;
			existing.Email = tn.Email;
			existing.Estado_civil_id = tn.Idestadocivil;
			existing.Id_religion = tn.Idreligion;
			existing.Id_Titulo = tn.Idtitulo;
			existing.Id_Region = tn.Idregion;
			existing.Id_Estado_Civil = tn.Idestadocivil;
			existing.Responsable_Pago = tn.Responsablepago;
			existing.Ex_Alumno = tn.Exalumno;
			existing.Fecha_Nacimiento = tn.Fechanacimiento;
			existing.Created_at = tn.Fechagrabacion;
			existing.Fecha_Modificacion = tn.Fechamodificacion;
			existing.Usuario_Grabacion = tn.Usuariograbacion;
			existing.Usuario_Edicion = tn.Usuariomodificacion;
			existing.Ejercicio = tn.Ejercicio;
			existing.Actualizado = tn.Actualizado;
			existing.No_Responsable = tn.Noresponsable;
			existing.Id_familia = tn.Idfamilia;
			existing.Id_relacion_familiar = tn.Idrelacionfamiliar;
			existing.Id_Pais = tn.Idpais;
			
		}

	}
}
