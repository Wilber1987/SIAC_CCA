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
			Console.Write("-->migrateTipoNotas");
			var tipoNotas = new Tipo_notas();
			tipoNotas.SetConnection(MySqlConnections.Siac);
			var tipoNotasMsql = tipoNotas.Get<Tipo_notas>();
			try
			{
				//BeginGlobalTransaction();
				tipoNotasMsql.ForEach(tn =>
				{
					var existingNota = new Tipo_notas()
					{
						Id = tn.Id
					}.Find<Tipo_notas>();

					tn.Created_at = DateUtil.ValidSqlDateTime(tn.Created_at.GetValueOrDefault());
					tn.Updated_at = DateUtil.ValidSqlDateTime(tn.Updated_at.GetValueOrDefault());
					if (existingNota != null/* && existingNota.Updated_at != tn.Updated_at*/)
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
				//CommitGlobalTransaction();
			}
			catch (System.Exception ex)
			{
				LoggerServices.AddMessageError("ERROR: migrateTipoNotas.Migrate.", ex);
				//RollBackGlobalTransaction();
				//throw;
			}

			return true;
		}

		public bool migrateCalificaciones()
		{
			Console.Write("-->migrateCalificaciones");

			// Instancia para manejar los datos de la vista
			var calificacion = new ViewCalificacionesActivasSiac();
			calificacion.SetConnection(MySqlConnections.Siac);
			calificacion.CreateViewEstudiantesActivos();

			// Obtener los registros desde la vista
			var calificacionMsql = calificacion.Get<ViewCalificacionesActivasSiac>();

			// Destruir la vista después de obtener los datos
			calificacion.DestroyView("ViewCalificacionesActivasSiac");

			Console.Write("No de registros encontrados: " + calificacionMsql.Count);
			int i = 0;

			try
			{
				// Iterar sobre los registros obtenidos de la vista
				calificacionMsql.ForEach(tn =>
				{
					Console.Write("Registro no: " + i.ToString());
					try
					{
						// Buscar si el registro ya existe en la tabla Calificaciones
						var existingCalificacion = new Calificaciones()
						{
							Id = tn.Id // Usar el ID de la vista para buscar en la tabla Calificaciones
						}.Find<Calificaciones>();

						// Validar y ajustar las fechas
						tn.Created_at = DateUtil.ValidSqlDateTime(tn.Created_at.GetValueOrDefault());
						tn.Updated_at = DateUtil.ValidSqlDateTime(tn.Updated_at.GetValueOrDefault());

						// Si la calificación ya existe, actualizarla
						if (existingCalificacion != null)
						{
							// Actualizar el registro existente en Calificaciones
							existingCalificacion.Resultado = tn.Resultado;
							existingCalificacion.Tipo_nota_id = tn.Tipo_nota_id;
							existingCalificacion.Evaluacion_id = tn.Evaluacion_id;
							existingCalificacion.Observaciones = tn.Observaciones;
							existingCalificacion.Updated_at = tn.Updated_at;
							existingCalificacion.Consolidado_id = tn.Consolidado_id;
							existingCalificacion.Estudiante_clase_id = tn.Estudiante_clase_id;
							existingCalificacion.Materia_id = tn.Materia_id;
							existingCalificacion.Periodo = tn.Periodo;

							// Guardar los cambios en la tabla Calificaciones
							existingCalificacion.Update();
						}
						else
						{
							// Si no existe, mapear la entidad Calificaciones y guardar
							var nuevaCalificacion = new Calificaciones()
							{
								Id = tn.Id,
								Resultado = tn.Resultado,
								Tipo_nota_id = tn.Tipo_nota_id,
								Evaluacion_id = tn.Evaluacion_id,
								Observaciones = tn.Observaciones,
								Created_at = tn.Created_at,
								Updated_at = tn.Updated_at,
								Consolidado_id = tn.Consolidado_id,
								Estudiante_clase_id = tn.Estudiante_clase_id,
								Materia_id = tn.Materia_id,
								Periodo = tn.Periodo
							};

							// Guardar el nuevo registro en la tabla Calificaciones
							nuevaCalificacion.Save();
						}
					}
					catch (System.Data.SqlClient.SqlException sqlEx)
					{
						LoggerServices.AddMessageError("SQL Error: ", sqlEx);
					}
					i++;
				});
			}
			catch (System.Exception ex)
			{
				LoggerServices.AddMessageError("ERROR: migrateCalificaciones.Migrate.", ex);
				//throw;
			}

			return true;
		}


		public bool migrateEvaluaciones()
		{
			Console.Write("-->migrateEvaluaciones");
			var Evaluacion = new Evaluaciones();
			Evaluacion.SetConnection(MySqlConnections.Siac);
			
			var currentYear = MigrationDates.GetCurrentYear(); // Obtiene el año actual (por ejemplo, 2024)
			var filter = new FilterData
			{
				PropName = "YEAR(fecha)", // Extrae el año de la columna fecha
				FilterType = "=",
				Values = new List<string?> { currentYear.ToString() }
			};
			var EvaluacionMsql = Evaluacion.Where<Evaluaciones>(filter);


			try
			{
				//BeginGlobalTransaction();
				EvaluacionMsql.ForEach(evaluacion =>
				{
					var existingEvaluacion = new Evaluaciones()
					{
						Id = evaluacion.Id
					}.Find<Evaluaciones>();

					evaluacion.Created_at = DateUtil.ValidSqlDateTime(evaluacion.Created_at.GetValueOrDefault());
					evaluacion.Updated_at = DateUtil.ValidSqlDateTime(evaluacion.Updated_at.GetValueOrDefault());
					if (existingEvaluacion != null /*&& existingEvaluacion.Updated_at != evaluacion.Updated_aT*/)
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
				//CommitGlobalTransaction();
			}
			catch (System.Exception ex)
			{
				LoggerServices.AddMessageError("ERROR: migrateEvaluaciones.Migrate.", ex);
				//RollBackGlobalTransaction();
				//throw;
			}

			return true;
		}
	}
}