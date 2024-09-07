using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CAPA_DATOS;
using CAPA_DATOS.BDCore.Abstracts;
using CAPA_NEGOCIO.Util;
using DataBaseModel;
using CAPA_DATOS.Security;
using Microsoft.Identity.Client;
using Twilio.Exceptions;


namespace CAPA_NEGOCIO.Oparations
{
	public class MigrateEstudiantes : TransactionalClass
	{

		public bool Migrate()
		{
			return MigrateParentesco() 
			&& MigrateFamilia() 
			&& migrateEstudiantes(MySqlConnections.Bellacom) /*&& migrateEstudiantes(MySqlConnections.Siac)*/
			&& MigrateParientesAndUsers() 
			&& migrateEstudiantesReponsablesFamilia();
		}

		public bool MigrateParentesco()
		{
			Console.Write("-->MigrateParentesco");
			var parentescos = new Tbl_gen_relacionfamilar();
			parentescos.SetConnection(MySqlConnections.Bellacom);
			var parentescosMsql = parentescos.Get<Tbl_gen_relacionfamilar>();
			try
			{
				BeginGlobalTransaction();
				parentescosMsql.ForEach(tn =>
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
					else if (existingParentesco == null)
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
				CommitGlobalTransaction();
			}
			catch (System.Exception ex)
			{
				LoggerServices.AddMessageError("ERROR: MigrateEstudiantes.MigrateParentesco.", ex);
				RollBackGlobalTransaction();
				throw;
			}

			return true;
		}

		public bool migrateEstudiantes(WDataMapper? connection)
		{
			Console.Write("-->migrateEstudiantes");
			var estudiante = new Tbl_aca_estudiante();
			estudiante.SetConnection(connection);
			var EstudiantesMsql = estudiante.Get<Tbl_aca_estudiante>();
			try
			{
				BeginGlobalTransaction();
				EstudiantesMsql.ForEach(est =>
				{
					var existingEstudiante = new Estudiantes()
					{
						Id = est.Idestudiante
					}.Find<Estudiantes>();

					est.Fechanacimiento = DateUtil.ValidSqlDateTime(est.Fechanacimiento.GetValueOrDefault());
					est.Fechamodificacion = DateUtil.ValidSqlDateTime(est.Fechamodificacion.GetValueOrDefault());
					est.Fechagrabacion = DateUtil.ValidSqlDateTime(est.Fechagrabacion.GetValueOrDefault());
					if (existingEstudiante != null /*&& existingEstudiante.Updated_at != est.Fechamodificacion*/)
					{
						buildEstudiante(est, existingEstudiante);

						existingEstudiante.Update();
					}
					else if (existingEstudiante == null)
					{
						Estudiantes newEstudiante = new Estudiantes();
						buildEstudiante(est, newEstudiante);
						newEstudiante.Save();
					}

				});
				CommitGlobalTransaction();
			}
			catch (System.Exception ex)
			{
				LoggerServices.AddMessageError("ERROR: migrateEstudiantes.", ex);
				RollBackGlobalTransaction();
				throw;
			}

			return true;
		}

		public bool MigrateFamilia()
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
				//LoggerServices.AddMessageError("ERROR: MigrateParientes.MigrateFamilia.", ex);
				//RollBackGlobalTransaction();
				throw;
			}

			return true;
		}

		public bool MigrateParientesAndUsers()
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
				//LoggerServices.AddMessageError("ERROR: migrateTipoNotas.Migrate.", ex);
				//RollBackGlobalTransaction();
				throw;
			}
			return true;
		}

		public bool migrateEstudiantesReponsablesFamilia()
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
				//LoggerServices.AddMessageError("ERROR: migrateEstudiantesReponsablesFamilia.", ex);
				//RollBackGlobalTransaction();
				throw;
			}
			return true;
		}

		private static void buildEstudiante(Tbl_aca_estudiante est, Estudiantes? existingEstudiante)
		{
			var estudianteJoin = new Estudiantes();
			estudianteJoin.SetConnection(MySqlConnections.Bellacom);
			var otrosDatos = estudianteJoin.Where<Estudiantes>(FilterData.Equal("codigo", est.Idtestudiante)).FirstOrDefault();
			if (otrosDatos != null)
			{
				existingEstudiante.Lugar_nacimiento = otrosDatos.Lugar_nacimiento;
				existingEstudiante.Madre_id = otrosDatos.Madre_id;
				existingEstudiante.Padre_id = otrosDatos.Padre_id;
				existingEstudiante.Foto = otrosDatos.Foto;
				existingEstudiante.Peso = otrosDatos.Peso;
				existingEstudiante.Altura = otrosDatos.Altura;
				existingEstudiante.Tipo_sangre = otrosDatos.Tipo_sangre;
				existingEstudiante.Padecimientos = otrosDatos.Padecimientos;
				existingEstudiante.Alergias = otrosDatos.Alergias;
				existingEstudiante.Recorrido_id = otrosDatos.Recorrido_id;
				existingEstudiante.Idtestudiante = otrosDatos.Idtestudiante;
			}

			existingEstudiante.Id = est.Idestudiante;
			existingEstudiante.Primer_nombre = StringUtil.GetNombres(est.Nombres)[0];
			existingEstudiante.Segundo_nombre = StringUtil.GetNombres(est.Nombres)[1];
			existingEstudiante.Primer_apellido = StringUtil.GetNombres(est.Apellidos)[0];
			existingEstudiante.Segundo_apellido = StringUtil.GetNombres(est.Apellidos)[1];
			existingEstudiante.Fecha_nacimiento = est.Fechanacimiento;
			existingEstudiante.Sexo = est.Sexo;
			existingEstudiante.Direccion = est.Direccion;
			existingEstudiante.Codigo = est.Idtestudiante;
			existingEstudiante.Created_at = est.Fechagrabacion;
			existingEstudiante.Updated_at = est.Fechamodificacion;
			existingEstudiante.Id_familia = est.Idfamilia;
			existingEstudiante.Periodo = est.Periodo;
			existingEstudiante.Fecha_ingreso = est.Fechaingreso;
			existingEstudiante.Id_pais = est.Idpais;
			existingEstudiante.Id_sociedad = est.Idsociedad;
			existingEstudiante.Id_region = est.Idregion;
			existingEstudiante.Solvencia = est.Solvencia;
			existingEstudiante.Saldomd = est.Saldomd;
			existingEstudiante.Estatus = est.Estatus;
			existingEstudiante.Retenido = est.Retenido;
			existingEstudiante.Referencia_estatus = est.Referenciaestatus;
			existingEstudiante.Usuario_grabacion = est.Usuariograbacion;
			existingEstudiante.Usuario_modificacion = est.Usuariomodificacion;
			existingEstudiante.Id_old = est.Id_old;
			existingEstudiante.Id_cliente = est.Idcliente;
			existingEstudiante.Codigomed = est.Codigomed;
			existingEstudiante.Ump = est.Ump;
			existingEstudiante.Uep = est.Uep;
			existingEstudiante.Colegio = est.Colegio;
			existingEstudiante.Vivecon = est.Vivecon;
			existingEstudiante.Sacramento = est.Sacramento;
			existingEstudiante.Aniosacra = est.Aniosacra;
			existingEstudiante.Fecha_aceptacion = est.Fechaaceptacion;
			existingEstudiante.Usuario_aceptacion = est.Usuarioaceptacion;
			existingEstudiante.Aceptacion = est.Aceptacion;
			existingEstudiante.Periodo_aceptacion = est.Periodoaceptacion;
			existingEstudiante.Fechaun = est.Fechaun;
			existingEstudiante.Motivo = est.Motivo;
			existingEstudiante.Comentario = est.Comentario;
			existingEstudiante.Fecharetencion = est.Fecharetencion;
			existingEstudiante.Saldoeamd = est.Saldoeamd;
			existingEstudiante.Id_familia = est.Idfamilia;

			if (existingEstudiante.Foto == "" || existingEstudiante.Foto == null)
			{
				var estudianteSiac = new Estudiantes();
				estudianteSiac.SetConnection(MySqlConnections.Siac);
				var existeRelacion = new Estudiantes().Where<Estudiantes>(FilterData.Equal("codigo", existingEstudiante.Codigo)).FirstOrDefault();

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
			//existing.Profesion = tn.Profesion;
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
