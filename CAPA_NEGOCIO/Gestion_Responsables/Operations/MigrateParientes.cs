using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CAPA_DATOS;
using CAPA_DATOS.Security;
using CAPA_NEGOCIO.Util;
using DataBaseModel;
using Microsoft.Identity.Client;

namespace CAPA_NEGOCIO.Oparations
{
	public class MigrateParientes : TransactionalClass
	{
		public bool Migrate()
		{
			/*var carajo = MySqlConnections.ConnectToMysql;
			return false;*/
			return MigrateFamilia() && MigrateParientesAndUsers();
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

					if (existing != null && existing.Fecha_Modificacion != tn.Fechamodificacion)
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
				LoggerServices.AddMessageError("ERROR: MigrateParientes.MigrateFamilia.", ex);
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
			catch (System.Exception ex)
			{
				LoggerServices.AddMessageError("ADVERTENCIA: validateRolPariente - Error al verificar perfil de responsable.", ex);
				return null;
			}
		}

		private static void buildPariente(Tbl_aca_tutor tn, Parientes? existing)
		{
			existing.Id = tn.Idtutor;
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
		}

		//metoos antiguos que no estan siendo utilizados (eran de migrateEstudiantes anteriormente),
		// los tengo aca por si acaso, pendiente borrar

		public bool migrateParientes()
		{
			Console.Write("-->migrateParientes");
			var Pariente = new Parientes();
			Pariente.SetConnection(MySQLConnection.SQLM);
			var ParientesMsql = Pariente.Get<Parientes>();
			try
			{
				BeginGlobalTransaction();
				ParientesMsql.ForEach(est =>
				{
					var existingPariente = new Parientes()
					{
						Id = est.Id
					}.Find<Parientes>();

					// est.Updated_at = DateUtil.ValidSqlDateTime(est.Updated_at.GetValueOrDefault());
					// est.Created_at = DateUtil.ValidSqlDateTime(est.Created_at.GetValueOrDefault());

					if (existingPariente != null /*&& existingPariente.Updated_at != est.Updated_at*/)
					{
						existingPariente.Primer_nombre = est.Primer_nombre;
						existingPariente.Segundo_nombre = est.Segundo_nombre;
						existingPariente.Primer_apellido = est.Primer_apellido;
						existingPariente.Segundo_apellido = est.Segundo_apellido;
						existingPariente.Sexo = est.Sexo;
						existingPariente.Profesion = est.Profesion;
						existingPariente.Direccion = est.Direccion;
						existingPariente.Lugar_trabajo = est.Lugar_trabajo;
						existingPariente.Telefono = est.Telefono;
						existingPariente.Celular = est.Celular;
						existingPariente.Email = est.Email;
						existingPariente.Estado_civil_id = est.Estado_civil_id;
						existingPariente.Religion_id = est.Religion_id;
						existingPariente.Update();
					}
					else if (existingPariente == null)
					{
						est.Save();
					}

				});
				CommitGlobalTransaction();
			}
			catch (System.Exception ex)
			{
				LoggerServices.AddMessageError("ERROR: migrateParientes.", ex);
				//RollBackGlobalTransaction();
				//throw;
			}

			return true;
		}		
	
	}
}
