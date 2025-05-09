using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CAPA_DATOS;
using CAPA_NEGOCIO.Services;
using CAPA_NEGOCIO.Util;
using DataBaseModel;
using Microsoft.Extensions.Configuration;
using Renci.SshNet;

namespace CAPA_NEGOCIO.Oparations
{
	public class MigrateGestionCursos : TransactionalClass
	{

		private readonly SshTunnelService _sshTunnelService;

		public MigrateGestionCursos()
		{
			_sshTunnelService = new SshTunnelService(LoadConfiguration());
		}

		private IConfigurationRoot LoadConfiguration()
		{
			return new ConfigurationBuilder()
				.SetBasePath(Directory.GetCurrentDirectory())
				.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
				.Build();
		}

		public async Task Migrate()
		{
			await migrateNiveles();
			await migrateSecciones();
			await migratePeriodosLectivos();
			await migrateAsignaturas();
			await migrateClases();
			await migrateMateria();
			await migrateEstudiantesClases();
			await migrateDocentesAsignaturas();
			await migrateDocentesMaterias();
		}

		public async Task<bool> migrateNiveles()
		{
			LoggerServices.AddMessageInfo("migrateNiveles--> Iniciando migrateNiveles");

			ForwardedPortLocal? siacTunnel = null;

			using (var siacSshClient = _sshTunnelService.GetSshClient("Siac"))
			{
				try
				{
					siacSshClient.Connect();
					LoggerServices.AddMessageInfo("migrateNiveles--> Cliente SSH conectado correctamente.");

					siacTunnel = _sshTunnelService.GetForwardedPort("Siac", siacSshClient, 3307);
					siacTunnel.Start();
					LoggerServices.AddMessageInfo("migrateNiveles--> Túnel SSH iniciado correctamente.");

					var nivel = new Niveles();
					nivel.SetConnection(MySqlConnections.SiacTest);
					var nivelsMsql = nivel.Get<Niveles>();

					LoggerServices.AddMessageInfo($"migrateNiveles--> Niveles encontrados en MySQL: {nivelsMsql.Count}");

					BeginGlobalTransaction();
					int actualizados = 0;
					int insertados = 0;

					nivelsMsql.ForEach(niv =>
					{
						var existingNivel = new Niveles()
						{
							Id = niv.Id
						}.SimpleFind<Niveles>();

						// Validar y ajustar las fechas
						niv.Created_at = DateUtil.ValidSqlDateTime(niv.Created_at.GetValueOrDefault());
						niv.Updated_at = DateUtil.ValidSqlDateTime(niv.Updated_at.GetValueOrDefault());

						if (existingNivel != null)
						{
							existingNivel.Nombre = niv.Nombre;
							existingNivel.Nombre_corto = niv.Nombre_corto;
							existingNivel.Nombre_grado = niv.Nombre_grado;
							existingNivel.Numero_grados = niv.Numero_grados;
							existingNivel.Observaciones = niv.Observaciones;
							existingNivel.Habilitado = niv.Habilitado;
							existingNivel.Orden = niv.Orden;
							existingNivel.Inicio_grado = niv.Inicio_grado;
							existingNivel.Updated_at = niv.Updated_at;
							existingNivel.Update();

							actualizados++;
							LoggerServices.AddMessageInfo($"migrateNiveles--> Nivel actualizado: ID = {existingNivel.Id}");
						}
						else
						{
							niv.Save();
							insertados++;
							LoggerServices.AddMessageInfo($"migrateNiveles--> Nuevo nivel insertado: ID = {niv.Id}");
						}
					});

					CommitGlobalTransaction();
					LoggerServices.AddMessageInfo($"migrateNiveles--> Transacción completada. Niveles actualizados: {actualizados}, insertados: {insertados}");
				}
				catch (Exception ex)
				{
					LoggerServices.AddMessageError("migrateNiveles--> ERROR en migrateNiveles", ex);
					RollBackGlobalTransaction();
				}
				finally
				{
					try
					{
						if (siacSshClient.IsConnected)
						{
							siacSshClient.Disconnect();
							LoggerServices.AddMessageInfo("migrateNiveles--> Cliente SSH desconectado.");
						}

						LoggerServices.AddMessageInfo("migrateNiveles--> Deteniendo el túnel SSH.");
						siacTunnel?.Stop();
					}
					catch (Exception ex)
					{
						LoggerServices.AddMessageError("migrateNiveles--> ERROR al cerrar conexión SSH o túnel.", ex);
					}
				}
			}

			return true;
		}


		public async Task<bool> migrateSecciones()
		{
			LoggerServices.AddMessageInfo("migrateSecciones--> Iniciando migrateSecciones");

			ForwardedPortLocal? siacTunnel = null;

			using (var siacSshClient = _sshTunnelService.GetSshClient("Siac"))
			{
				try
				{
					siacSshClient.Connect();
					LoggerServices.AddMessageInfo("migrateSecciones--> Cliente SSH conectado correctamente.");

					siacTunnel = _sshTunnelService.GetForwardedPort("Siac", siacSshClient, 3307);
					siacTunnel.Start();
					LoggerServices.AddMessageInfo("migrateSecciones--> Túnel SSH iniciado correctamente.");

					var seccion = new Secciones();
					seccion.SetConnection(MySqlConnections.SiacTest);
					var seccionsMsql = seccion.Get<Secciones>();

					LoggerServices.AddMessageInfo($"migrateSecciones--> Secciones encontradas en MySQL: {seccionsMsql.Count}");

					BeginGlobalTransaction();
					int actualizados = 0;
					int insertados = 0;

					seccionsMsql.ForEach(secc =>
					{
						var existingSeccion = new Secciones()
						{
							Id = secc.Id
						}.SimpleFind<Secciones>();

						if (existingSeccion != null)
						{
							existingSeccion.Nombre = secc.Nombre;
							existingSeccion.Clase_id = secc.Clase_id;
							existingSeccion.Docente_id = secc.Docente_id;
							existingSeccion.Observaciones = secc.Observaciones;
							existingSeccion.Updated_at = secc.Updated_at;
							existingSeccion.Update();

							actualizados++;
							LoggerServices.AddMessageInfo($"migrateSecciones--> Sección actualizada: ID = {existingSeccion.Id}");
						}
						else
						{
							secc.Save();
							insertados++;
							LoggerServices.AddMessageInfo($"migrateSecciones--> Nueva sección insertada: ID = {secc.Id}");
						}
					});

					CommitGlobalTransaction();
					LoggerServices.AddMessageInfo($"migrateSecciones--> Transacción completada. Secciones actualizadas: {actualizados}, insertadas: {insertados}");
				}
				catch (Exception ex)
				{
					LoggerServices.AddMessageError("migrateSecciones--> ERROR en migrateSecciones", ex);
					RollBackGlobalTransaction();
				}
				finally
				{
					try
					{
						if (siacSshClient.IsConnected)
						{
							siacSshClient.Disconnect();
							LoggerServices.AddMessageInfo("migrateSecciones--> Cliente SSH desconectado.");
						}

						LoggerServices.AddMessageInfo("migrateSecciones--> Deteniendo el túnel SSH.");
						siacTunnel?.Stop();
					}
					catch (Exception ex)
					{
						LoggerServices.AddMessageError("migrateSecciones--> ERROR al cerrar conexión SSH o túnel.", ex);
					}
				}
			}

			return true;
		}


		public async Task<bool> migratePeriodosLectivos()
		{
			LoggerServices.AddMessageInfo("migratePeriodosLectivos--> Iniciando migratePeriodosLectivos");

			ForwardedPortLocal? siacTunnel = null;

			using (var siacSshClient = _sshTunnelService.GetSshClient("Siac"))
			{
				try
				{
					siacSshClient.Connect();
					LoggerServices.AddMessageInfo("migratePeriodosLectivos--> Cliente SSH conectado correctamente.");

					siacTunnel = _sshTunnelService.GetForwardedPort("Siac", siacSshClient, 3307);
					siacTunnel.Start();
					LoggerServices.AddMessageInfo("migratePeriodosLectivos--> Túnel SSH iniciado correctamente.");

					var periodo = new Periodo_lectivos();
					periodo.SetConnection(MySqlConnections.SiacTest);
					var periodosMsql = periodo.Get<Periodo_lectivos>();

					LoggerServices.AddMessageInfo($"migratePeriodosLectivos--> Periodos encontrados en MySQL: {periodosMsql.Count}");

					BeginGlobalTransaction();
					int actualizados = 0;
					int insertados = 0;

					periodosMsql.ForEach(p =>
					{
						var existingPeriodo = new Periodo_lectivos()
						{
							Id = p.Id
						}.SimpleFind<Periodo_lectivos>();

						if (existingPeriodo != null && existingPeriodo.Updated_at != p.Updated_at)
						{
							existingPeriodo.Nombre = p.Nombre;
							existingPeriodo.Nombre_corto = p.Nombre_corto;
							existingPeriodo.Observaciones = p.Observaciones;
							existingPeriodo.Config = p.Config;
							existingPeriodo.Abierto = p.Abierto;
							existingPeriodo.Oculto = p.Oculto;
							existingPeriodo.Update();

							actualizados++;
							LoggerServices.AddMessageInfo($"migratePeriodosLectivos--> Periodo actualizado: ID = {existingPeriodo.Id}");
						}
						else if (existingPeriodo == null)
						{
							p.Save();
							insertados++;
							LoggerServices.AddMessageInfo($"migratePeriodosLectivos--> Nuevo periodo insertado: ID = {p.Id}");
						}
					});

					CommitGlobalTransaction();
					LoggerServices.AddMessageInfo($"migratePeriodosLectivos--> Transacción completada. Periodos actualizados: {actualizados}, insertados: {insertados}");
				}
				catch (Exception ex)
				{
					LoggerServices.AddMessageError("migratePeriodosLectivos--> ERROR durante la migración", ex);
					RollBackGlobalTransaction();
				}
				finally
				{
					try
					{
						if (siacSshClient.IsConnected)
						{
							siacSshClient.Disconnect();
							LoggerServices.AddMessageInfo("migratePeriodosLectivos--> Cliente SSH desconectado.");
						}

						LoggerServices.AddMessageInfo("migratePeriodosLectivos--> Deteniendo el túnel SSH.");
						siacTunnel?.Stop();
					}
					catch (Exception ex)
					{
						LoggerServices.AddMessageError("migratePeriodosLectivos--> ERROR al cerrar conexión SSH o túnel.", ex);
					}
				}
			}

			return true;
		}

		public async Task<bool> migrateAsignaturas()
		{
			LoggerServices.AddMessageInfo("migrateAsignaturas--> Iniciando migración de asignaturas.");

			ForwardedPortLocal? siacTunnel = null;

			using (var siacSshClient = _sshTunnelService.GetSshClient("Siac"))
			{
				try
				{
					siacSshClient.Connect();
					LoggerServices.AddMessageInfo("migrateAsignaturas--> Cliente SSH conectado.");

					siacTunnel = _sshTunnelService.GetForwardedPort("Siac", siacSshClient, 3307);
					siacTunnel.Start();
					LoggerServices.AddMessageInfo("migrateAsignaturas--> Túnel SSH iniciado.");

					var asignatura = new Asignaturas();
					asignatura.SetConnection(MySqlConnections.SiacTest);
					var asignaturasMsql = asignatura.Get<Asignaturas>();
					LoggerServices.AddMessageInfo($"migrateAsignaturas--> Asignaturas encontradas: {asignaturasMsql.Count}");

					BeginGlobalTransaction();
					int insertados = 0;
					int actualizados = 0;

					asignaturasMsql.ForEach(a =>
					{
						var existente = new Asignaturas { Id = a.Id }.SimpleFind<Asignaturas>();

						a.Created_at = DateUtil.ValidSqlDateTime(a.Created_at.GetValueOrDefault());
						a.Updated_at = DateUtil.ValidSqlDateTime(a.Updated_at.GetValueOrDefault());

						if (existente != null)
						{
							existente.Nombre = a.Nombre;
							existente.Nombre_corto = a.Nombre_corto;
							existente.Observaciones = a.Observaciones;
							existente.Nivel_id = a.Nivel_id;
							existente.Habilitado = a.Habilitado;
							existente.Updated_at = a.Updated_at;
							existente.Orden = a.Orden;
							existente.Update();

							actualizados++;
							LoggerServices.AddMessageInfo($"migrateAsignaturas--> Actualizado ID: {a.Id}");
						}
						else
						{
							a.Save();
							insertados++;
							LoggerServices.AddMessageInfo($"migrateAsignaturas--> Insertado ID: {a.Id}");
						}
					});

					CommitGlobalTransaction();
					LoggerServices.AddMessageInfo($"migrateAsignaturas--> Migración completada. Insertados: {insertados}, Actualizados: {actualizados}");
				}
				catch (Exception ex)
				{
					LoggerServices.AddMessageError("migrateAsignaturas--> ERROR durante la migración.", ex);
					RollBackGlobalTransaction();
				}
				finally
				{
					try
					{
						if (siacTunnel?.IsStarted == true)
						{
							siacTunnel.Stop();
							LoggerServices.AddMessageInfo("migrateAsignaturas--> Túnel SSH detenido.");
						}

						if (siacSshClient.IsConnected)
						{
							siacSshClient.Disconnect();
							LoggerServices.AddMessageInfo("migrateAsignaturas--> Cliente SSH desconectado.");
						}
					}
					catch (Exception ex)
					{
						LoggerServices.AddMessageError("migrateAsignaturas--> ERROR al cerrar conexiones.", ex);
					}
				}
			}

			return true;
		}


		public async Task<bool> migrateMateria()
		{
			LoggerServices.AddMessageInfo("migrateMateria --> Iniciando migración de materias.");

			var fechaUltimaActualizacion = MigrateService.GetLastUpdate("MATERIAS");
			LoggerServices.AddMessageInfo($"migrateMateria --> Última actualización registrada: {fechaUltimaActualizacion}");

			ForwardedPortLocal? siacTunnel = null;

			using (var siacSshClient = _sshTunnelService.GetSshClient("Siac"))
			{
				try
				{
					siacSshClient.Connect();
					siacTunnel = _sshTunnelService.GetForwardedPort("Siac", siacSshClient, 3307);
					siacTunnel.Start();
					LoggerServices.AddMessageInfo("migrateMateria --> Túnel SSH establecido.");

					var materia = new Materias();
					materia.SetConnection(MySqlConnections.SiacTest);

					var filter = new FilterData
					{
						PropName = "updated_at",
						FilterType = ">=",
						Values = new List<string?> { fechaUltimaActualizacion.ToString("yyyy-MM-dd HH:mm:ss") }
					};

					var materiasMsql = materia.Where<Materias>(filter);
					LoggerServices.AddMessageInfo($"migrateMateria --> Materias encontradas: {materiasMsql.Count}");

					BeginGlobalTransaction();

					int insertados = 0;
					int actualizados = 0;

					materiasMsql.ForEach(m =>
					{
						var existente = new Materias { Id = m.Id }.SimpleFind<Materias>();

						m.Created_at = DateUtil.ValidSqlDateTime(m.Created_at.GetValueOrDefault());
						m.Updated_at = DateUtil.ValidSqlDateTime(m.Updated_at.GetValueOrDefault());

						if (existente != null)
						{
							existente.Clase_id = m.Clase_id;
							existente.Asignatura_id = m.Asignatura_id;
							existente.Observaciones = m.Observaciones;
							existente.Config = m.Config;
							existente.Lock_version = m.Lock_version;
							existente.Updated_at = m.Updated_at;
							existente.Update();

							actualizados++;
						}
						else if (existente == null)
						{
							m.Save();
							insertados++;
						}
					});

					MigrateService.UpdateLastUpdate("MATERIAS");
					CommitGlobalTransaction();

					LoggerServices.AddMessageInfo($"migrateMateria --> Migración finalizada. Insertados: {insertados}, Actualizados: {actualizados}");
				}
				catch (Exception ex)
				{
					LoggerServices.AddMessageError("migrateMateria --> ERROR durante la migración.", ex);
					RollBackGlobalTransaction();
				}
				finally
				{
					try
					{
						if (siacTunnel?.IsStarted == true)
						{
							siacTunnel.Stop();
							LoggerServices.AddMessageInfo("migrateMateria --> Túnel SSH detenido.");
						}

						if (siacSshClient.IsConnected)
						{
							siacSshClient.Disconnect();
							LoggerServices.AddMessageInfo("migrateMateria --> Cliente SSH desconectado.");
						}
					}
					catch (Exception ex)
					{
						LoggerServices.AddMessageError("migrateMateria --> ERROR al cerrar conexiones.", ex);
					}
				}
			}

			return true;
		}


		public async Task<bool> migrateClases()
		{
			LoggerServices.AddMessageInfo("migrateClases --> Iniciando migración de clases.");

			Console.Write("-->migrateClases");
			ForwardedPortLocal? siacTunnel = null;
			// Iniciar el túnel SSH para SiacTest
			using (var siacSshClient = _sshTunnelService.GetSshClient("Siac"))
			{
				try
				{
					// Conectar el cliente SSH
					siacSshClient.Connect();
					LoggerServices.AddMessageInfo("migrateClases --> Cliente SSH conectado.");

					// Crear el puerto redirigido
					siacTunnel = _sshTunnelService.GetForwardedPort("Siac", siacSshClient, 3307);
					siacTunnel.Start();
					LoggerServices.AddMessageInfo("migrateClases --> Túnel SSH establecido.");

					// Establecer conexión con la base de datos SiacTest
					var clase = new Clases();
					clase.SetConnection(MySqlConnections.SiacTest);

					var clasesMsql = clase.Get<Clases>();
					LoggerServices.AddMessageInfo($"migrateClases --> Clases encontradas: {clasesMsql.Count}");

					BeginGlobalTransaction();

					clasesMsql.ForEach(clase =>
					{
						var existingClase = new Clases()
						{
							Id = clase.Id
						}.SimpleFind<Clases>();

						clase.Created_at = DateUtil.ValidSqlDateTime(clase.Created_at.GetValueOrDefault());
						clase.Updated_at = DateUtil.ValidSqlDateTime(clase.Updated_at.GetValueOrDefault());

						if (existingClase != null && existingClase.Updated_at != clase.Updated_at)
						{
							// Actualizar el registro existente en Clases
							existingClase.Grado = clase.Grado;
							existingClase.Nivel_id = clase.Nivel_id;
							existingClase.Observaciones = clase.Observaciones;
							existingClase.Periodo_lectivo_id = clase.Periodo_lectivo_id;
							existingClase.Updated_at = clase.Updated_at;
							existingClase.Update();
							LoggerServices.AddMessageInfo($"migrateClases --> Clase actualizada: {clase.Id}");
						}
						else if (existingClase == null)
						{
							// Guardar un nuevo registro en Clases
							clase.Save();
							LoggerServices.AddMessageInfo($"migrateClases --> Clase insertada: {clase.Id}");
						}
					});

					CommitGlobalTransaction();
					LoggerServices.AddMessageInfo("migrateClases --> Migración de clases finalizada.");
				}
				catch (Exception ex)
				{
					LoggerServices.AddMessageError("ERROR migrateClases.", ex);
				}
				finally
				{
					// Detener el túnel SSH
					if (siacTunnel.IsStarted)
					{
						siacTunnel.Stop();
						LoggerServices.AddMessageInfo("migrateClases --> Túnel SSH detenido.");
					}

					siacSshClient.Disconnect();
					LoggerServices.AddMessageInfo("migrateClases --> Cliente SSH desconectado.");
				}
			}

			return true;
		}


		public async Task<bool> migrateEstudiantesClases()
		{
			LoggerServices.AddMessageInfo("migrateEstudiantesClases --> Iniciando migración de estudiantes y clases.");

			Console.Write("-->migrateEstudiantesClases");

			// Declaración de la variable para el túnel SSH
			ForwardedPortLocal? siacTunnel = null;

			// Iniciar el túnel SSH para SiacTest
			using (var siacSshClient = _sshTunnelService.GetSshClient("Siac"))
			{
				try
				{
					// Conectar el cliente SSH
					siacSshClient.Connect();
					LoggerServices.AddMessageInfo("migrateEstudiantesClases --> Cliente SSH conectado.");

					// Crear el puerto redirigido
					siacTunnel = _sshTunnelService.GetForwardedPort("Siac", siacSshClient, 3307);
					siacTunnel.Start();
					LoggerServices.AddMessageInfo("migrateEstudiantesClases --> Túnel SSH establecido.");

					// Obtener el periodo lectivo actual
					var periodo_lectivo = new Periodo_lectivos();
					var periodo = periodo_lectivo.Where<Periodo_lectivos>(new FilterData
					{
						PropName = "nombre_corto",
						FilterType = "=",
						Values = new List<string?> { MigrationDates.GetCurrentYear().ToString() }
					}).FirstOrDefault();

					if (periodo == null)
					{
						LoggerServices.AddMessageInfo("migrateEstudiantesClases --> No se encontró el periodo lectivo actual.");
						return false;
					}

					// Establecer conexión con la base de datos SiacTest y obtener estudiantes clases
					var clase = new Estudiante_clases();
					clase.SetConnection(MySqlConnections.SiacTest);
					var clasesMsql = clase.Get<Estudiante_clases>();
					LoggerServices.AddMessageInfo($"migrateEstudiantesClases --> Estudiantes y clases encontradas: {clasesMsql.Count}");

					clasesMsql.ForEach(clase =>
					{
						var estudiante = new Estudiantes()
						{
							Id = clase.Estudiante_id
						}.Find<Estudiantes>();

						// Si el estudiante no existe, omitir el registro
						if (estudiante == null)
						{
							LoggerServices.AddMessageInfo($"migrateEstudiantesClases --> Estudiante con ID {clase.Estudiante_id} no existe. Registro omitido.");
							return; // Omitir este registro
						}

						var existingClase = new Estudiante_clases()
						{
							Id = clase.Id
						}.SimpleFind<Estudiante_clases>();

						// Validar fechas y actualizar registro si ya existe
						clase.Created_at = DateUtil.ValidSqlDateTime(clase.Created_at.GetValueOrDefault());
						clase.Updated_at = DateUtil.ValidSqlDateTime(clase.Updated_at.GetValueOrDefault());

						if (existingClase != null && existingClase.Updated_at != clase.Updated_at)
						{
							// Actualizar el registro existente
							existingClase.Estudiante_id = clase.Estudiante_id;
							existingClase.Periodo_lectivo_id = clase.Periodo_lectivo_id;
							existingClase.Clase_id = clase.Clase_id;
							existingClase.Seccion_id = clase.Seccion_id;
							existingClase.Retirado = clase.Retirado;
							existingClase.Observaciones = clase.Observaciones;
							existingClase.Updated_at = clase.Updated_at;
							existingClase.Promedio = clase.Promedio;
							existingClase.Repitente = clase.Repitente;
							existingClase.Reprobadas = clase.Reprobadas;
							existingClase.Update();
							//LoggerServices.AddMessageInfo($"migrateEstudiantesClases --> Clase actualizada: {clase.Id}");
						}
						else if (existingClase == null)
						{
							// Guardar un nuevo registro en Estudiante_clases
							clase.Save();
							//LoggerServices.AddMessageInfo($"migrateEstudiantesClases --> Clase insertada: {clase.Id}");
						}
					});

					// CommitGlobalTransaction();  
					LoggerServices.AddMessageInfo("migrateEstudiantesClases --> Migración de estudiantes y clases finalizada.");
				}
				catch (Exception ex)
				{
					LoggerServices.AddMessageError("ERROR migrateEstudiantesClases.", ex);
				}
				finally
				{
					// Detener el túnel SSH
					if (siacTunnel?.IsStarted == true)
					{
						siacTunnel.Stop();
						LoggerServices.AddMessageInfo("migrateEstudiantesClases --> Túnel SSH detenido.");
					}

					siacSshClient.Disconnect();
					LoggerServices.AddMessageInfo("migrateEstudiantesClases --> Cliente SSH desconectado.");
				}
			}

			return true;
		}


		public async Task<bool> migrateDocentesAsignaturas()
		{
			Console.Write("-->migrateDocentesAsignaturas");

			// Iniciar túnel SSH para conexión remota con SiacTest
			using (var siacSshClient = _sshTunnelService.GetSshClient("Siac"))
			{
				try
				{
					siacSshClient.Connect();
					var siacTunnel = _sshTunnelService.GetForwardedPort("Siac", siacSshClient, 3307);
					siacTunnel.Start();

					try
					{
						// Conectar con la base de datos SiacTest y obtener datos
						var docAsig = new Docente_asignaturas();
						docAsig.SetConnection(MySqlConnections.SiacTest);
						var docAsigsMsql = docAsig.Get<Docente_asignaturas>();

						// Iniciar transacción para entorno local
						BeginGlobalTransaction();

						foreach (var item in docAsigsMsql)
						{
							// Validar y ajustar fechas inválidas para SQL Server
							item.Created_at = DateUtil.ValidSqlDateTime(item.Created_at.GetValueOrDefault());
							item.Updated_at = DateUtil.ValidSqlDateTime(item.Updated_at.GetValueOrDefault());

							var existing = new Docente_asignaturas { Id = item.Id }.SimpleFind<Docente_asignaturas>();

							if (existing != null)
							{
								// Actualizar solo si es necesario
								existing.Docente_id = item.Docente_id;
								existing.Asignatura_id = item.Asignatura_id;
								existing.Jefe = item.Jefe;
								existing.Observaciones = item.Observaciones;
								existing.Updated_at = item.Updated_at;
								existing.Update();
							}
							else
							{
								item.Save();
							}
						}

						// Confirmar transacción
						CommitGlobalTransaction();
					}
					catch (Exception ex)
					{
						LoggerServices.AddMessageError("ERROR migrateDocentesAsignaturas.", ex);
						// Puedes agregar RollbackGlobalTransaction() si fuera necesario aquí
					}
					finally
					{
						// Detener túnel y desconectar SSH
						if (siacTunnel.IsStarted)
							siacTunnel.Stop();

						siacSshClient.Disconnect();
					}
				}
				catch (Exception ex)
				{
					LoggerServices.AddMessageError("ERROR SSH connection migrateDocentesAsignaturas.", ex);
					return false;
				}
			}

			return true;
		}


		public async Task<bool> migrateDocentesMaterias()
		{
			Console.WriteLine("--> migrateDocentesMaterias");

			using (var siacSshClient = _sshTunnelService.GetSshClient("Siac"))
			{
				try
				{
					// Conexión SSH
					siacSshClient.Connect();
					var siacTunnel = _sshTunnelService.GetForwardedPort("Siac", siacSshClient, 3307);
					siacTunnel.Start();

					try
					{
						// Obtener datos desde SiacTest
						var docMat = new Docente_materias();
						docMat.SetConnection(MySqlConnections.SiacTest);
						var docMatsMsql = docMat.Where<Docente_materias>();						

						// Transacción local
						BeginGlobalTransaction();

						foreach (var item in docMatsMsql)
						{
							item.Created_at = DateUtil.ValidSqlDateTime(item.Created_at.GetValueOrDefault());
							item.Updated_at = DateUtil.ValidSqlDateTime(item.Updated_at.GetValueOrDefault());

							var existing = new Docente_materias { Id = item.Id }.SimpleFind<Docente_materias>();

							if (existing != null)
							{
								// Actualizar solo si hay cambios en Updated_at
								//if (existing.Updated_at != item.Updated_at)
								//{
									existing.Materia_id = item.Materia_id;
									existing.Seccion_id = item.Seccion_id;
									existing.Docente_id = item.Docente_id;
									existing.Updated_at = item.Updated_at;
									existing.Docentes = null;
									existing.Update();
								//}
							}
							else
							{
								item.Save();
							}
						}

						CommitGlobalTransaction();
					}
					catch (Exception ex)
					{
						LoggerServices.AddMessageError("ERROR en migrateDocentesMaterias durante la transacción.", ex);
						// Podrías hacer RollbackGlobalTransaction(); aquí si tu sistema lo soporta
					}
					finally
					{
						if (siacTunnel.IsStarted)
							siacTunnel.Stop();

						siacSshClient.Disconnect();
					}
				}
				catch (Exception ex)
				{
					LoggerServices.AddMessageError("ERROR en conexión SSH migrateDocentesMaterias.", ex);
					return false;
				}
			}

			return true;
		}

	}
}