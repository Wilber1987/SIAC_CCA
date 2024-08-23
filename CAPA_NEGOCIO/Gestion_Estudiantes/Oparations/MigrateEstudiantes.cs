using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CAPA_DATOS;
using CAPA_DATOS.BDCore.Abstracts;
using CAPA_NEGOCIO.Util;
using DataBaseModel;

namespace CAPA_NEGOCIO.Oparations
{
	public class MigrateEstudiantes : TransactionalClass
	{
		
		public bool Migrate()
		{			
			return migrateEstudiantes() && migrateParientes() && migrateResponsables();			
		}
		public bool migrateEstudiantes()
		{
			Console.Write("-->migrateEstudiantes");
			var estudiante = new Estudiantes();
			estudiante.SetConnection(MySqlConnections.Bellacom);
			var EstudiantesMsql = estudiante.Get<Estudiantes>(
			//TODO ARREGLAR LO DEL PAGINADO
			//new FilterData { FilterType = "limit", Values = ["10000"] },
			// new FilterData { FilterType = "paginated", Values = ["1000"] }
			);			
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
					if (existingEstudiante != null && existingEstudiante.Updated_at != est.Updated_at)
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
					else if (existingEstudiante == null )
					{
						est.Save();
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
					else if (existingPariente == null )
					{
						est.Save();
					}

				});
				CommitGlobalTransaction();
			}
			catch (System.Exception ex)
			{
				LoggerServices.AddMessageError("ERROR: migrateParientes.", ex);
				RollBackGlobalTransaction();
				throw;
			}

			return true;
		}

		public bool migrateResponsables()
		{
			Console.Write("-->migrateResponsables");
			var responsable = new Responsables();
			responsable.SetConnection(MySQLConnection.SQLM);
			var responsablesMsql = responsable.Get<Responsables>();
			try
			{
				BeginGlobalTransaction();
				responsablesMsql.ForEach(est =>
				{
					var existingResponsable = new Responsables()
					{
						Id = est.Id
					}.Find<Responsables>();

					est.Updated_at = DateUtil.ValidSqlDateTime(est.Updated_at.GetValueOrDefault());
					est.Created_at = DateUtil.ValidSqlDateTime(est.Created_at.GetValueOrDefault());
					if (existingResponsable != null && existingResponsable.Updated_at != est.Updated_at)
					{
						existingResponsable.Estudiante_id = est.Estudiante_id;
						existingResponsable.Pariente_id = est.Pariente_id;
						existingResponsable.Updated_at = est.Updated_at;
						existingResponsable.Parentesco = est.Parentesco;
						//existingResponsable.Update();
					}
					else if (existingResponsable == null)
					{
						est.Save();
					}

				});
				CommitGlobalTransaction();
			}
			catch (System.Exception ex)
			{
				LoggerServices.AddMessageError("ERROR: migrateResponsables.", ex);
				RollBackGlobalTransaction();
				throw;
			}

			return true;
		}
	}
}
