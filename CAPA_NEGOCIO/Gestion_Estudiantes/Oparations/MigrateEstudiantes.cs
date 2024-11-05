using CAPA_DATOS;
using CAPA_NEGOCIO.Util;
using DataBaseModel;
using CAPA_DATOS.Security;
using Microsoft.Extensions.Configuration;
using Renci.SshNet;

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
						}.Find<Parentesco>();

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
				catch (System.Exception ex)
				{
					LoggerServices.AddMessageError("ERROR: MigrateEstudiantes.MigrateParentesco.", ex);
					RollBackGlobalTransaction();
					forwardedPort.Stop();
					client.Disconnect();
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
							var existingEstudiante = new Estudiantes() { Id = est.Id }.Find<Estudiantes>();

							// Manejo de fechas
							est.Fecha_nacimiento = DateUtil.ValidSqlDateTime(est.Fecha_nacimiento.GetValueOrDefault());
							est.Updated_at = DateUtil.ValidSqlDateTime(est.Updated_at.GetValueOrDefault());
							est.Created_at = DateUtil.ValidSqlDateTime(est.Created_at.GetValueOrDefault());
							est.Fecha_ingreso = DateUtil.ValidSqlDateTime(est.Fecha_ingreso.GetValueOrDefault());

							if (existingEstudiante != null)
							{
								//buildEstudianteSiac(est, existingEstudiante, bellacomSshClient, sshService);
								buildEstudianteSiac(est, existingEstudiante, bellacomSshClient, siacSshClient, sshService);

								existingEstudiante.Update();
							}
							else
							{
								var newEstudiante = new Estudiantes();
								//buildEstudianteSiac(est, newEstudiante, bellacomSshClient, sshService);
								buildEstudianteSiac(est, newEstudiante, bellacomSshClient, siacSshClient, sshService);

								newEstudiante.Save();
							}
						}

						estudianteview.DestroyView("viewestudiantesmigracion");
						bellacomTunnel.Stop();
						bellacomSshClient.Disconnect();
					}

					siacTunnel.Stop();
					siacSshClient.Disconnect();
				}

				return true;
			}
			catch (System.Exception ex)
			{
				LoggerServices.AddMessageError("ERROR: migrateEstudiantes.", ex);
				return false;
			}
		}

		private static void buildEstudianteSiac(Estudiantes est, Estudiantes? existingEstudiante, SshClient bellacomSshClient, SshClient siacSshClient, SshTunnelService sshService)
		{
			// Utiliza la conexión existente para BellacomTest
			var bellacomConnection = MySqlConnections.BellacomTest;

			// Cargar la vista de estudiantes
			var estudianteView = new ViewEstudiantesMigracion();
			estudianteView.SetConnection(bellacomConnection);
			estudianteView.CreateView();
			var estudiantesView = estudianteView.Where<ViewEstudiantesMigracion>(FilterData.Equal("idtestudiante", est.Codigo)).FirstOrDefault();

			if (estudiantesView != null)
			{
				// Obtener datos de la familia usando BellacomTest
				var familiaJoin = new Tbl_aca_familia();
				familiaJoin.SetConnection(bellacomConnection);
				var familiaDatos = familiaJoin.Where<Tbl_aca_familia>(FilterData.Equal("idfamilia", estudiantesView.Idfamilia)).FirstOrDefault();

				if (familiaDatos != null)
				{
					existingEstudiante.Id_familia = familiaDatos.Idfamilia;
				}
			}

			// Asignación de datos básicos del estudiante
			existingEstudiante.Id = est.Id;
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
			existingEstudiante.Fecha_ingreso = est.Fecha_ingreso;
			existingEstudiante.Id_cliente = est.Id_cliente;
			existingEstudiante.Codigomed = est.Codigomed;
			existingEstudiante.Saldoeamd = est.Saldoeamd;

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

			using (var client = _sshTunnelService.GetSshClient("Bellacom"))
			{
				client.Connect();
				var forwardedPort = _sshTunnelService.GetForwardedPort("Bellacom", client, 3308);
				forwardedPort.Start();

				familias.SetConnection(MySqlConnections.BellacomTest);

				var familiasMsql = familias.Get<Tbl_aca_familia>();
				forwardedPort.Stop();
				client.Disconnect();
				try
				{
					BeginGlobalTransaction();
					familiasMsql.ForEach(tn =>
					{
						var existingFamilia = new Familias()
						{
							Id = tn.Idfamilia
						}.Find<Familias>();

						if (existingFamilia != null && (existingFamilia.Fecha_ultima_notificacion != tn.Fechaultimanotificacion))
						{
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
					CommitGlobalTransaction();
				}
				catch (System.Exception ex)
				{
					LoggerServices.AddMessageError("ERROR: MigrateParientes.MigrateFamilia.", ex);
					//RollBackGlobalTransaction(); // Descomentar para revertir la transacción en caso de error
					//throw;
				}

			}

			return true;
		}

		public async Task<bool> MigrateFamiliaOld()
		{

			Console.Write("-->MigrateFamilia");
			var rol = validateRolPariente();
			if (rol == null)
			{
				return false;
			}

			var familias = new Tbl_aca_familia();
			familias.SetConnection(MySqlConnections.Bellacom);
			var familiasMsql = familias.Get<Tbl_aca_familia>();

			try
			{
				BeginGlobalTransaction();
				familiasMsql.ForEach(tn =>
				{
					var existingFamilia = new Familias()
					{
						Id = tn.Idfamilia
					}.Find<Familias>();

					if (existingFamilia != null && (existingFamilia.Fecha_ultima_notificacion != existingFamilia.Fecha_ultima_notificacion))
					{

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
				CommitGlobalTransaction();
			}
			catch (System.Exception ex)
			{
				LoggerServices.AddMessageError("ERROR: MigrateParientes.MigrateFamilia.", ex);
				//RollBackGlobalTransaction();
				//throw;
			}

			return true;
		}

		public async Task<bool> MigrateParientesAndUsers()
		{
			Console.Write("-->MigrateParientesAndUsers");

			// Si no existe el rol de pariente, se debe crear para asignárselo al usuario de cada responsable.
			// Ya que se crea un usuario por cada miembro de familia que tenga el check de responsable.
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
					var dataMsql = data.Get<Tbl_aca_tutor>();

					BeginGlobalTransaction();

					dataMsql.ForEach(static tn =>
					{
						var existing = new Parientes()
						{
							Id = tn.Idtutor
						}.Find<Parientes>();

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
									Mail = StringUtil.GenerateNickName(tn.Nombres, tn.Apellidos),
									Token = null,
									Security_Users_Roles = new List<Security_Users_Roles>
									{
								new Security_Users_Roles { Security_Role = rolPadreResponsable, Estado = "ACTIVO" }
									}
								}.Save_User(null);

								newPariente.User_id = user.Id_User;
							}

							newPariente.Save();
						}
					});

					CommitGlobalTransaction();
				}
				catch (Exception ex)
				{
					LoggerServices.AddMessageError("ERROR: MigrateParientesAndUsers.", ex);
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

		public async Task<bool> MigrateParientesAndUsersOld()
		{
			Console.Write("-->MigrateParientesAndUsers");
			//si no eixiste el rol de pariente se debe crear para asignarselo al usuario de cada responsable 
			// ya que se crea un usuario por cada miembro de falia que tenga el check de responsable
			var rolResponsable = validateRolPariente();
			if (rolResponsable == null)
			{
				return false;
			}

			var data = new Tbl_aca_tutor();
			data.SetConnection(MySqlConnections.Bellacom);
			var dataMsql = data.Get<Tbl_aca_tutor>();// data.Where<Tbl_aca_tutor>(FilterData.Equal("responsablepago", "1"));

			try
			{
				BeginGlobalTransaction();
				dataMsql.ForEach(static tn =>
				{
					var existing = new Parientes()
					{
						Id = tn.Idtutor
					}.Find<Parientes>();

					tn.Fechagrabacion = DateUtil.ValidSqlDateTime(tn.Fechagrabacion.GetValueOrDefault());
					tn.Fechamodificacion = DateUtil.ValidSqlDateTime(tn.Fechamodificacion.GetValueOrDefault());
					tn.Fechaactualizacion = DateUtil.ValidSqlDateTime(tn.Fechaactualizacion.GetValueOrDefault());
					tn.Fechanacimiento = DateUtil.ValidSqlDateTime(tn.Fechanacimiento.GetValueOrDefault());

					if (existing != null /*&& existing.Fecha_Modificacion != tn.Fechamodificacion*/)
					{
						buildPariente(tn, existing);
						existing.Update();

					}
					else if (existing == null)
					{
						Parientes newPariente = new Parientes();
						buildPariente(tn, newPariente);
						if (newPariente.Responsable_Pago == true && StringUtil.IsValidEmail(tn.Email))
						{//se crea usuario ya que es responsable de pago y por ende de familia
							var rolPadreResponsable = new Security_Roles().Find<Security_Roles>(FilterData.Equal("descripcion", "PADRE_RESPONSABLE"));
							var user = (Security_Users?)new Security_Users
							{
								Nombres = tn.Nombres + " " + tn.Apellidos,
								Estado = "ACTIVO",
								Descripcion = tn.Nombres + " " + tn.Apellidos,
								Password = StringUtil.GeneratePassword(tn.Email, tn.Nombres, tn.Apellidos),
								Mail = StringUtil.GenerateNickName(tn.Nombres, tn.Apellidos),
								Token = null,
								Security_Users_Roles = new List<Security_Users_Roles>
									{
										new Security_Users_Roles { Security_Role = rolPadreResponsable, Estado = "ACTIVO" }
									}
							}.Save_User(null);
							newPariente.User_id = user.Id_User;
						}
						newPariente.Save();
					}

				});
				CommitGlobalTransaction();
			}
			catch (System.Exception ex)
			{
				LoggerServices.AddMessageError("ERROR: migrateTipoNotas.Migrate.", ex);
				//RollBackGlobalTransaction();
				//throw;
			}
			return true;
		}

		public async Task<bool> migrateEstudiantesReponsablesFamilia()
		{
			Console.Write("-->migrateEstudiantesReponsablesFamilia");
			var familia = new Familias();
			var familiasMsql = familia.Get<Familias>(
			);
			try
			{
				BeginGlobalTransaction();
				familiasMsql.ForEach(f =>
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
			catch (System.Exception ex)
			{
				LoggerServices.AddMessageError("ERROR: migrateEstudiantesReponsablesFamilia.", ex);
				//RollBackGlobalTransaction();
				//throw;
			}
			return true;
		}

		private static void buildEstudianteSiacOld(Estudiantes est, Estudiantes? existingEstudiante, SshTunnelService sshService)
		{
			ViewEstudiantesMigracion estudiantesView = null;  // Declarar la variable fuera del bloque `using`

			// Obtener el cliente SSH y el túnel para BellacomTest
			using (var bellacomSshClient = sshService.GetSshClient("Bellacom"))
			{
				bellacomSshClient.Connect();
				var bellacomTunnel = sshService.GetForwardedPort("Bellacom", bellacomSshClient, 3308);
				bellacomTunnel.Start();

				var bellacomConnection = MySqlConnections.BellacomTest;

				// Cargar la vista de estudiantes
				var estudianteView = new ViewEstudiantesMigracion();
				estudianteView.SetConnection(bellacomConnection);
				estudianteView.CreateView();
				estudiantesView = estudianteView.Where<ViewEstudiantesMigracion>(FilterData.Equal("codigo", est.Codigo)).FirstOrDefault();

				if (estudiantesView == null)
				{
					return;
				}

				// Obtener datos de la familia usando BellacomTest
				var familiaJoin = new Tbl_aca_familia();
				familiaJoin.SetConnection(bellacomConnection);
				var familiaDatos = familiaJoin.Where<Tbl_aca_familia>(FilterData.Equal("idfamilia", estudiantesView.Idfamilia)).FirstOrDefault();

				if (familiaDatos == null)
				{
					return;
				}

				existingEstudiante.Id_familia = familiaDatos.Idfamilia;

				// Cerrar el túnel y la conexión de BellacomTest
				bellacomTunnel.Stop();
				bellacomSshClient.Disconnect();
			}

			// Asignación de datos básicos del estudiante
			existingEstudiante.Id = est.Id;
			existingEstudiante.Primer_nombre = est.Primer_nombre;
			existingEstudiante.Segundo_nombre = est.Segundo_nombre;
			existingEstudiante.Primer_apellido = est.Primer_apellido;
			existingEstudiante.Segundo_apellido = est.Segundo_apellido;
			existingEstudiante.Fecha_nacimiento = est.Fecha_nacimiento;
			existingEstudiante.Sexo = est.Sexo;
			existingEstudiante.Foto = estudiantesView?.Foto ?? est.Foto;  // Usa el operador null conditional
			existingEstudiante.Direccion = est.Direccion;
			existingEstudiante.Codigo = est.Codigo;
			existingEstudiante.Created_at = est.Created_at;
			existingEstudiante.Updated_at = est.Updated_at;
			existingEstudiante.Fecha_ingreso = est.Fecha_ingreso;
			existingEstudiante.Id_cliente = est.Id_cliente;
			existingEstudiante.Codigomed = est.Codigomed;
			existingEstudiante.Saldoeamd = est.Saldoeamd;

			// Si no se encontró una foto, buscar en el sistema SIAC usando la otra conexión
			using (var siacSshClient = sshService.GetSshClient("Siac"))
			{
				siacSshClient.Connect();
				var siacTunnel = sshService.GetForwardedPort("Siac", siacSshClient, 3307);
				siacTunnel.Start();

				var siacConnection = MySqlConnections.SiacTest;

				var estudianteSiac = new Estudiantes();
				estudianteSiac.SetConnection(siacConnection);
				var existeRelacion = estudianteSiac.Where<Estudiantes>(FilterData.Equal("codigo", existingEstudiante.Codigo)).FirstOrDefault();

				if (existeRelacion != null)
				{
					existingEstudiante.Foto = existeRelacion.Foto;
				}

				// Cerrar el túnel y la conexión de SiacTest
				siacTunnel.Stop();
				siacSshClient.Disconnect();
			}
		}

		private static void buildEstudianteSiacOld(Estudiantes est, Estudiantes? existingEstudiante)
		{
			// Reutiliza la misma conexión para todas las operaciones relacionadas con Bellacom
			var connection = MySqlConnections.BellacomTest;

			// Carga la vista de estudiante
			var estudianteView = new ViewEstudiantesMigracion();
			estudianteView.SetConnection(connection);
			estudianteView.CreateView();
			var estudiantesView = estudianteView.Where<ViewEstudiantesMigracion>(FilterData.Equal("codigo", est.Codigo)).FirstOrDefault();

			// Si no se encuentra el estudiante en la vista, retornar
			if (estudiantesView == null)
			{
				return;
			}

			// Carga los datos de la familia si están disponibles
			var familiaJoin = new Tbl_aca_familia();
			familiaJoin.SetConnection(connection);
			var familiaDatos = familiaJoin.Where<Tbl_aca_familia>(FilterData.Equal("idfamilia", estudiantesView.Idfamilia)).FirstOrDefault();

			// Si no tiene familia, omitir al estudiante
			if (familiaDatos == null)
			{
				return;
			}

			existingEstudiante.Id_familia = familiaDatos.Idfamilia;

			// Asignación de datos básicos del estudiante
			existingEstudiante.Id = est.Id;
			existingEstudiante.Primer_nombre = est.Primer_nombre;
			existingEstudiante.Segundo_nombre = est.Segundo_nombre;
			existingEstudiante.Primer_apellido = est.Primer_apellido;
			existingEstudiante.Segundo_apellido = est.Segundo_apellido;
			existingEstudiante.Fecha_nacimiento = est.Fecha_nacimiento;
			existingEstudiante.Sexo = est.Sexo;
			existingEstudiante.Foto = estudiantesView.Foto ?? est.Foto;
			existingEstudiante.Direccion = est.Direccion;
			existingEstudiante.Codigo = est.Codigo;
			existingEstudiante.Created_at = est.Created_at;
			existingEstudiante.Updated_at = est.Updated_at;
			existingEstudiante.Fecha_ingreso = est.Fecha_ingreso;
			existingEstudiante.Id_cliente = est.Id_cliente;
			existingEstudiante.Codigomed = est.Codigomed;
			existingEstudiante.Saldoeamd = est.Saldoeamd;

			// Si no se encontró una foto, se busca en el sistema SIAC
			if (string.IsNullOrEmpty(existingEstudiante.Foto))
			{
				var estudianteSiac = new Estudiantes();
				estudianteSiac.SetConnection(MySqlConnections.SiacTest);
				var existeRelacion = estudianteSiac.Where<Estudiantes>(FilterData.Equal("codigo", existingEstudiante.Codigo)).FirstOrDefault();

				if (existeRelacion != null)
				{
					existingEstudiante.Foto = existeRelacion.Foto;
				}
			}
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
			catch (System.Exception ex)
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
			existing.Religion_id = tn.Idreligion;
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
		}

	}
}
