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
					est.Fecha_nacimiento = DateUtil.ValidSqlDateTime(est.Fecha_nacimiento.GetValueOrDefault());
					est.Save();
				});
				CommitGlobalTransaction();
			}
			catch (System.Exception)
			{
				RollBackGlobalTransaction();
				throw;
			}

			return true;
		}
		
	}
}
