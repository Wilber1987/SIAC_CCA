using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CAPA_DATOS;
using CAPA_NEGOCIO.Util;
using DataBaseModel;

namespace CAPA_NEGOCIO.Oparations
{
	public class MigrateNotas : TransactionalClass
	{
		public bool Migrate()
		{

			return migrateTipoNotas() && migrateEvaluaciones() && migrateCalificaciones();

		}

		public bool migrateTipoNotas()
		{
			var tipoNotas = new Tipo_notas();
			tipoNotas.SetConnection(MySQLConnection.SQLM);
			var tipoNotasMsql = tipoNotas.Get<Tipo_notas>();
			try
			{
				BeginGlobalTransaction();
				tipoNotasMsql.ForEach(tn =>
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

		public bool migrateCalificaciones()
		{
			var calificacion = new Calificaciones();
			calificacion.SetConnection(MySQLConnection.SQLM);
			var calificacionMsql = calificacion.Get<Calificaciones>();
			try
			{
				BeginGlobalTransaction();
				calificacionMsql.ForEach(tn =>
				{
					try
					{
						var existingCalificacion = new Calificaciones()
						{
							Id = tn.Id
						}.Find<Calificaciones>();

						tn.Created_at = DateUtil.ValidSqlDateTime(tn.Created_at.GetValueOrDefault());
						tn.Updated_at = DateUtil.ValidSqlDateTime(tn.Updated_at.GetValueOrDefault());

						if (existingCalificacion != null && existingCalificacion.Updated_at != tn.Updated_at)
						{
							existingCalificacion.Resultado = tn.Resultado;
							existingCalificacion.Tipo_nota_id = tn.Tipo_nota_id;
							existingCalificacion.Evaluacion_id = tn.Evaluacion_id;
							existingCalificacion.Observaciones = tn.Observaciones;
							existingCalificacion.Updated_at = tn.Updated_at;
							existingCalificacion.Consolidado_id = tn.Consolidado_id;
							existingCalificacion.Estudiante_clase_id = tn.Estudiante_clase_id;
							existingCalificacion.Materia_id = tn.Materia_id;
							existingCalificacion.Periodo = tn.Periodo;
							existingCalificacion.Update();
						}
						else if (existingCalificacion == null)
						{
							tn.Save();
						}
					}
					catch (System.Data.SqlClient.SqlException sqlEx)
					{
						if (sqlEx.Number == 547) // 547 es el código de error para violación de restricción de clave externa
						{
							LoggerServices.AddMessageError("ADVERTENCIA: migrateCalificaciones - Error de clave externa ignorado.", sqlEx);
						}
						else
						{
						LoggerServices.AddMessageError("ADVERTENCIA: migrateCalificaciones - Error desconocido ignorado, Codigo: "+sqlEx.Number.ToString(), sqlEx);

						}
					}
				});
				CommitGlobalTransaction();
			}
			catch (System.Exception ex)
			{
				LoggerServices.AddMessageError("ERROR: migrateCalificaciones.Migrate.", ex);
				RollBackGlobalTransaction();
				throw;
			}

			return true;
		}


		public bool migrateEvaluaciones()
		{
			var Evaluacion = new Evaluaciones();
			Evaluacion.SetConnection(MySQLConnection.SQLM);
			var EvaluacionMsql = Evaluacion.Get<Evaluaciones>();
			try
			{
				BeginGlobalTransaction();
				EvaluacionMsql.ForEach(evaluacion =>
				{
					var existingEvaluacion = new Evaluaciones()
					{
						Id = evaluacion.Id
					}.Find<Evaluaciones>();

					evaluacion.Created_at = DateUtil.ValidSqlDateTime(evaluacion.Created_at.GetValueOrDefault());
					evaluacion.Updated_at = DateUtil.ValidSqlDateTime(evaluacion.Updated_at.GetValueOrDefault());
					if (existingEvaluacion != null && existingEvaluacion.Updated_at != evaluacion.Updated_at)
					{
						// existingEvaluacion.Fecha = evaluacion.Fecha;
						//existingEvaluacion.Hora = evaluacion.Hora; //TODO
						existingEvaluacion.Tipo = evaluacion.Tipo;
						existingEvaluacion.Porcentaje = evaluacion.Porcentaje;
						existingEvaluacion.Materia_id = evaluacion.Materia_id;
						existingEvaluacion.Seccion_id = evaluacion.Seccion_id;
						existingEvaluacion.Observaciones = evaluacion.Observaciones;
						existingEvaluacion.Updated_at = evaluacion.Updated_at;
						existingEvaluacion.Periodo = evaluacion.Periodo;
						existingEvaluacion.Nota_maxima = evaluacion.Nota_maxima;
						existingEvaluacion.Update();

					}
					else if (existingEvaluacion == null)
					{
						evaluacion.Save();
					}

				});
				CommitGlobalTransaction();
			}
			catch (System.Exception ex)
			{
				LoggerServices.AddMessageError("ERROR: migrateEvaluaciones.Migrate.", ex);
				RollBackGlobalTransaction();
				throw;
			}

			return true;
		}
	}
}