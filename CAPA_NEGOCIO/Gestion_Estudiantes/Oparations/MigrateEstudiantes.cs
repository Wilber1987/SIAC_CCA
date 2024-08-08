using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CAPA_DATOS;
using CAPA_NEGOCIO.Util;
using DataBaseModel;

namespace CAPA_NEGOCIO.Oparations
{
	public class MigrateEstudiantes : TransactionalClass
	{
		public bool Migrate()
		{
			var estudiante = new Estudiantes();
			estudiante.SetConnection(MySQLConnection.SQLM);
			var EstudiantesMsql = estudiante.Get<Estudiantes>();
			try
			{
				BeginGlobalTransaction();
				EstudiantesMsql.ForEach(est =>
				{
					var existingEstudiante = new Estudiantes()
					{
						Id = est.Id
					}.Find<Estudiantes>();

					est.Fecha_nacimiento = DateUtil.ValidSqlDateTime(est.Fecha_nacimiento.GetValueOrDefault());
					if (existingEstudiante != null)
					{
						existingEstudiante.Primer_nombre = est.Primer_nombre;
						existingEstudiante.Segundo_nombre = est.Segundo_nombre;
						existingEstudiante.Primer_apellido = est.Primer_apellido;
						existingEstudiante.Segundo_apellido = est.Segundo_apellido;
						existingEstudiante.Fecha_nacimiento = est.Fecha_nacimiento;
						existingEstudiante.Lugar_nacimiento = est.Lugar_nacimiento;
						existingEstudiante.Direccion = est.Direccion;
						existingEstudiante.Codigo = est.Codigo;
						existingEstudiante.Madre_id = est.Madre_id;
						existingEstudiante.Padre_id = est.Padre_id;
						existingEstudiante.Tipo_sangre = est.Tipo_sangre;
						existingEstudiante.Padecimientos = est.Padecimientos;
						existingEstudiante.Recorrido_id = est.Recorrido_id;
						existingEstudiante.Activo = est.Activo;
						existingEstudiante.Update();
					}
					else
					{
						est.Save();
					}

				});
				CommitGlobalTransaction();
			}
			catch (System.Exception ex)
			{
				LoggerServices.AddMessageError("ERROR:.", ex);
				RollBackGlobalTransaction();
				throw;
			}

			return true;
		}

	}
}
