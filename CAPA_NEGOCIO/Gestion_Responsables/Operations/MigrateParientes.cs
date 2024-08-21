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
			var dataMsql = data.Get<Tbl_aca_tutor>();
			try
			{
				BeginGlobalTransaction();
				dataMsql.ForEach(tn =>
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
						existing.Primer_nombre = tn.Nombres;
						//existing.Nombre_corto = tn.Nombre_corto;//todo
						/*existing.Primer_apellido = tn.Primer_apellido;
						existing.Segundo_apellido = tn.Segundo_apellido;*/
						existing.Sexo = tn.Sexo;
						//existing.Profesion = tn.Profesion;
						existing.Direccion = tn.Direccion;
						existing.Lugar_trabajo = tn.Lugartrabajo;
						existing.Telefono = tn.Telefono;
						existing.Celular = tn.Celular;
						existing.Telefono_trabajo = tn.Telefonotrabajo;
						existing.Email = tn.Email;
						//existing.Estado_civil_id = tn.Estado_civil_id;//todo
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

						existing.Update();

					}
					else if (existing == null)
					{						
						tn.Save();
						
					}

				});
				CommitGlobalTransaction();
			}
			catch (System.Exception ex)
			{
				LoggerServices.AddMessageError("ERROR: migrateTipoNotas.Migrate.", ex);
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

			var familias = new Familias();
			familias.SetConnection(MySqlConnections.Bellacom);
			var familiasMsql = familias.Get<Familias>();
			try
			{
				BeginGlobalTransaction();
				familiasMsql.ForEach(tn =>
				{
					var existingFamilia = new Familias()
					{
						Id = tn.Id
					}.Find<Familias>();

					if (existingFamilia != null && (existingFamilia.Fecha_ultima_notificacion != existingFamilia.Fecha_ultima_notificacion))
					{
						existingFamilia.Idtfamilia = tn.Idtfamilia;
						existingFamilia.Descripcion = tn.Descripcion;
						existingFamilia.Estado = tn.Estado;
						existingFamilia.Saldo = tn.Saldo;
						existingFamilia.Actualizado = tn.Actualizado;
						existingFamilia.Aceptacion = tn.Aceptacion;
						existingFamilia.Periodo_aceptacion = tn.Periodo_aceptacion;
						existingFamilia.Fecha_actualizacion = tn.Fecha_actualizacion;
						existingFamilia.Fecha_ultima_notificacion = tn.Fecha_ultima_notificacion;
						existingFamilia.Update();

					}
					else if (existingFamilia == null)
					{
						tn.Save();
					}

				});
				CommitGlobalTransaction();
			}
			catch (System.Exception ex)
			{
				LoggerServices.AddMessageError("ERROR: MigrateParientes.MigrateFamilia.", ex);
				RollBackGlobalTransaction();
				throw;
			}

			return true;
		}

		public Security_Roles validateRolPariente()
		{
			try
			{
				Security_Roles? responsableRol = new Security_Roles().Find<Security_Roles>(FilterData.Equal("descripcion", "PADRE_RESPONSABLE"));
				if (responsableRol.Exists())
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
	}
}
