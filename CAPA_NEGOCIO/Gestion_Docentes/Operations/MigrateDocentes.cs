using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataBaseModel;
using CAPA_DATOS;
using CAPA_NEGOCIO.Util;


namespace CAPA_NEGOCIO.Oparations
{
    public class MigrateDocentes: TransactionalClass
    {
		public bool Migrate()
		{
			return  migrateDocentes();
		}
	

		public bool migrateDocentes()
        {
			Console.Write("-->migrateDocentes");

			var docentes = new Docentes();
		 	docentes.SetConnection(MySqlConnections.Siac);
			var docentesMsql = docentes.Get<Docentes>();
			try
			{
				BeginGlobalTransaction();
			 	docentesMsql.ForEach(tn =>
				{
					var existingDocente = new Docentes()
					{
						Id = tn.Id
					}.Find<Docentes>();
					
					if (existingDocente != null && existingDocente.Updated_at != tn.Updated_at)
					{
						existingDocente.Primer_nombre = tn.Primer_nombre;
						existingDocente.Segundo_nombre = tn.Segundo_nombre;
						existingDocente.Primer_apellido = tn.Primer_apellido;
						existingDocente.Segundo_apellido= tn.Segundo_apellido;
						existingDocente.Sexo = tn.Sexo;
						existingDocente.Fecha_nacimiento = tn.Fecha_nacimiento;
						existingDocente.Lugar_nacimiento = tn.Lugar_nacimiento;
						existingDocente.Direccion = tn.Direccion;
						existingDocente.Telefono = tn.Telefono;
						existingDocente.Celular = tn.Celular;
						existingDocente.Email = tn.Email;
						existingDocente.Estado_civil_id = tn.Estado_civil_id;
						existingDocente.Religion_id = tn.Religion_id;
						existingDocente.Escolaridad_id = tn.Escolaridad_id;
						existingDocente.Foto = tn.Foto;
						existingDocente.Updated_at = tn.Updated_at;
						existingDocente.Habilitado = tn.Habilitado;
						existingDocente.Cargo = tn.Cargo;
						existingDocente.Update();
					}
					else if (existingDocente == null)
					{
						tn.Save();
					}

				});
				CommitGlobalTransaction();
			}
			catch (System.Exception ex)
			{
				LoggerServices.AddMessageError("ERROR: migrateDocentes.Migrate.", ex);
				/*RollBackGlobalTransaction();
				throw;*/
			}

			return true;
		}
	}
}
