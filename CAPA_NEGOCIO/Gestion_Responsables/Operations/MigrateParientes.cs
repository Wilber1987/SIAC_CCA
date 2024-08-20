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
			return MigrateParientesAndUsers();
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





			var familia = new Tbl_aca_familia();
			familia.SetConnection(MySqlConnections.Bellacom);
			var familiaMsql = familia.Get<Tbl_aca_familia>();
			try
			{
				BeginGlobalTransaction();
				familiaMsql.ForEach(tn =>
				{
					var existingNota = new Tipo_notas()
					{
						Id = tn.Id
					}.Find<Tipo_notas>();

					tn.Created_at = DateUtil.ValidSqlDateTime(tn.Created_at.GetValueOrDefault());
					tn.Updated_at = DateUtil.ValidSqlDateTime(tn.Updated_at.GetValueOrDefault());
					if (existingNota != null && existingNota.Updated_at != tn.Updated_at)
					{
						existingNota.Nombre = tn.Nombre;
						existingNota.Nombre_corto = tn.Nombre_corto;
						existingNota.Periodo_lectivo_id = tn.Periodo_lectivo_id;
						existingNota.Consolidado_id = tn.Consolidado_id;
						existingNota.Numero_consolidados = tn.Numero_consolidados;
						existingNota.Observaciones = tn.Observaciones;
						existingNota.Orden = tn.Orden;
						existingNota.Update();

					}
					else if (existingNota == null)
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
