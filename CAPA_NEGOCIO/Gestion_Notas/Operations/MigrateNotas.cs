using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using APPCORE;
using CAPA_NEGOCIO.Services;
using CAPA_NEGOCIO.Util;
using DataBaseModel;
using Microsoft.Extensions.Configuration;

namespace CAPA_NEGOCIO.Oparations
{
	public class MigrateNotas : TransactionalClass
	{

		private readonly SshTunnelService _sshTunnelService;

		public MigrateNotas()
		{
			_sshTunnelService = new SshTunnelService(LoadConfiguration());
		}

		public async Task Migrate(String? codigo = null)
		{
			await migrateTipoNotas();
			await migrateEvaluaciones();
			await migrateCalificaciones();
		}

		private IConfigurationRoot LoadConfiguration()
		{
			return new ConfigurationBuilder()
				.SetBasePath(Directory.GetCurrentDirectory())
				.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
				.Build();
		}
		public async Task<bool> migrateTipoNotas()
		{
			Console.Write("-->migrateTipoNotas");

			// Iniciar el túnel SSH para SiacTest
			using (var siacSshClient = _sshTunnelService.GetSshClient("Siac"))
			{
				siacSshClient.Connect();
				var siacTunnel = _sshTunnelService.GetForwardedPort("Siac", siacSshClient, 3307);
				siacTunnel.Start();

				// Establecer conexión con la base de datos SiacTest
				var tipoNotas = new Tipo_notas();
				tipoNotas.SetConnection(MySqlConnections.SiacTest);
				var tipoNotasMsql = tipoNotas.Get<Tipo_notas>();

				try
				{
					tipoNotasMsql.ForEach(tn =>
					{
						var existingNota = new Tipo_notas()
						{
							Id = tn.Id
						}.SimpleFind<Tipo_notas>();

						tn.Created_at = DateUtil.ValidSqlDateTime(tn.Created_at.GetValueOrDefault());
						tn.Updated_at = DateUtil.ValidSqlDateTime(tn.Updated_at.GetValueOrDefault());

						if (existingNota != null)
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
						else
						{
							tn.Save();
						}
					});
				}
				catch (Exception ex)
				{
					LoggerServices.AddMessageError("ERROR: migrateTipoNotas.Migrate.", ex);
				}
				finally
				{
					// Detener el túnel SSH
					siacTunnel.Stop();
					siacSshClient.Disconnect();
				}
			}

			return true;
		}

		public async Task<bool> migrateCalificaciones()
		{
			LoggerServices.AddMessageInfo("migrateCalificaciones --> Iniciando migración de calificaciones");

			var fechaUltimaActualizacion = MigrateService.GetLastUpdate("CALIFICACIONES");

			using (var siacSshClient = _sshTunnelService.GetSshClient("Siac"))
			{
				int i = 0;
				siacSshClient.Connect();
				LoggerServices.AddMessageInfo("migrateCalificaciones --> Cliente SSH conectado");

				var siacTunnel = _sshTunnelService.GetForwardedPort("Siac", siacSshClient, 3307);
				siacTunnel.Start();
				LoggerServices.AddMessageInfo("migrateCalificaciones --> Túnel SSH iniciado");

				var calificacion = new ViewCalificacionesActivasSiac();
				calificacion.SetConnection(MySqlConnections.SiacTest);
				calificacion.CreateViewEstudiantesActivos();

				var filter = new FilterData
				{
					PropName = "updated_at",
					FilterType = ">=",
					Values = new List<string?> { fechaUltimaActualizacion.ToString() }
				};
				var calificacionMsql = calificacion.Where<ViewCalificacionesActivasSiac>(
					//filter
				);

				LoggerServices.AddMessageInfo($"migrateCalificaciones --> Registros encontrados en MySQL: {calificacionMsql.Count}");

				var clasesAgrupadas = calificacionMsql.GroupBy(cal => cal.Estudiante_clase_id);

				try
				{
					foreach (var grupo in clasesAgrupadas)
					{
						var estudianteClaseId = grupo.Key;
						var existingCalificacionInSqlServer = new Calificaciones().Where<Calificaciones>(
							FilterData.Equal("Estudiante_clase_id", estudianteClaseId)
						);

						foreach (var tn in grupo)
						{							
							try
							{
								var existingCalificacion = new Calificaciones()
								{
									Id = tn.Id
								}.Find<Calificaciones>();

								tn.Created_at = DateUtil.ValidSqlDateTime(tn.Created_at.GetValueOrDefault());
								tn.Updated_at = DateUtil.ValidSqlDateTime(tn.Updated_at.GetValueOrDefault());

								if (existingCalificacion != null)
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
								else
								{
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
									nuevaCalificacion.Save();
									LoggerServices.AddMessageInfo($"migrateCalificaciones --> Nueva calificación insertada: ID = {nuevaCalificacion.Id}");
								}
							}
							catch (Exception sqlEx)
							{
								LoggerServices.AddMessageError("SQL Error migrateCalificaciones:", sqlEx);
							}

							i++;
						}

						// Paso 2: Eliminar registros obsoletos
						try
						{
							var idsDesdeMysql = grupo.Select(x => x.Id).ToList();
							var registrosEnSqlServer = existingCalificacionInSqlServer;

							// Filtrar los que no están en MySQL
							var registrosAEliminar = registrosEnSqlServer
								.Where(x => !idsDesdeMysql.Contains(x.Id))
								.ToList();

							// Agrupar por clave compuesta si fuera necesario (ej. Estudiante_clase_id), o trabajar directamente
							foreach (var idGroup in registrosAEliminar.GroupBy(x => x.Estudiante_clase_id))
							{
								var duplicados = idGroup.ToList();

								if (duplicados.Count == 1)
								{
									// Solo uno para eliminar
									var registro = duplicados[0];
									registro.Delete();
									LoggerServices.AddMessageInfo($"migrateCalificaciones --> Calificación eliminada: ID = {registro.Id}");
								}
								else
								{
									// Eliminar primero los que tengan Resultado == null
									var conResultadoNull = duplicados.Where(x => x.Resultado == null).ToList();

									if (conResultadoNull.Any())
									{
										foreach (var registro in conResultadoNull)
										{
											registro.Delete();
											LoggerServices.AddMessageInfo($"migrateCalificaciones --> Calificación con Resultado NULL eliminada: ID = {registro.Id}");
										}
									}
									else
									{
										// Si no hay ninguno con Resultado null, eliminar todos (o dejar uno si lo deseas)
										foreach (var registro in duplicados)
										{
											registro.Delete();
											LoggerServices.AddMessageInfo($"migrateCalificaciones --> Calificación eliminada (sin Resultado NULL disponible): ID = {registro.Id}");
										}
									}
								}
							}
						}
						catch (Exception ex)
						{
							LoggerServices.AddMessageError("Error durante la eliminación de calificaciones obsoletas.", ex);
						}
					}
				}
				catch (Exception ex)
				{
					LoggerServices.AddMessageError("ERROR: migrateCalificaciones.Migrate.", ex);
				}
				finally
				{
					LoggerServices.AddMessageInfo("migrateCalificaciones --> Cerrando túnel SSH y eliminando vista.");
					siacTunnel.Stop();
					siacSshClient.Disconnect();
					calificacion.DestroyView("viewcalificacionesactivassiac");
				}
			}

			MigrateService.UpdateLastUpdate("CALIFICACIONES");
			LoggerServices.AddMessageInfo("migrateCalificaciones --> Migración finalizada correctamente.");
			return true;
		}


		public async Task<bool> migrateEvaluaciones()
		{
			Console.Write("--> migrateEvaluaciones");
			var fechaUltimaActualizacion = MigrateService.GetLastUpdate("EVALUACIONES");
			// Iniciar el túnel SSH para SiacTest
			using (var siacSshClient = _sshTunnelService.GetSshClient("Siac"))
			{
				siacSshClient.Connect();
				var siacTunnel = _sshTunnelService.GetForwardedPort("Siac", siacSshClient, 3307);
				siacTunnel.Start();

				// Establecer conexión con la base de datos SiacTest
				var Evaluacion = new Evaluaciones();
				Evaluacion.SetConnection(MySqlConnections.SiacTest);

				var filter = new FilterData
				{
					PropName = "updated_at",
					FilterType = ">=",
					Values = new List<string?> { fechaUltimaActualizacion.ToString() }
				};

				var EvaluacionMsql = Evaluacion.Where<Evaluaciones>(filter);

				try
				{
					EvaluacionMsql.ForEach(evaluacion =>
					{
						var existingEvaluacion = new Evaluaciones()
						{
							Id = evaluacion.Id
						}.Find<Evaluaciones>();

						// Validar y ajustar las fechas
						evaluacion.Created_at = DateUtil.ValidSqlDateTime(evaluacion.Created_at.GetValueOrDefault());
						evaluacion.Updated_at = DateUtil.ValidSqlDateTime(evaluacion.Updated_at.GetValueOrDefault());

						if (existingEvaluacion != null)
						{
							existingEvaluacion.Tipo = evaluacion.Tipo;
							existingEvaluacion.Porcentaje = evaluacion.Porcentaje;
							existingEvaluacion.Materia_id = evaluacion.Materia_id;
							existingEvaluacion.Seccion_id = evaluacion.Seccion_id;
							existingEvaluacion.Observaciones = evaluacion.Observaciones;
							existingEvaluacion.Updated_at = evaluacion.Updated_at;
							existingEvaluacion.Periodo = evaluacion.Periodo;
							existingEvaluacion.Nota_maxima = evaluacion.Nota_maxima;
							existingEvaluacion.Fecha = evaluacion.Fecha;
							existingEvaluacion.Hora = evaluacion.Hora;

							// Guardar los cambios en la evaluación existente
							existingEvaluacion.Update();
						}
						else
						{
							evaluacion.Save();
						}
					});
					MigrateService.UpdateLastUpdate("EVALUACIONES");
				}
				catch (Exception ex)
				{
					LoggerServices.AddMessageError("ERROR: migrateEvaluaciones.Migrate.", ex);
				}
				finally
				{
					siacTunnel.Stop();
					siacSshClient.Disconnect();
				}
			}

			return true;
		}

	}
}