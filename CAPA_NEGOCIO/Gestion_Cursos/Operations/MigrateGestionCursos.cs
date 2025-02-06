using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CAPA_DATOS;
using CAPA_NEGOCIO.Services;
using CAPA_NEGOCIO.Util;
using DataBaseModel;
using Microsoft.Extensions.Configuration;

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
			Console.Write("-->migrateNiveles");

			// Iniciar el túnel SSH para SiacTest
			using (var siacSshClient = _sshTunnelService.GetSshClient("Siac"))
			{
				// Conectar el cliente SSH
				siacSshClient.Connect();

				// Crear el puerto redirigido
				var siacTunnel = _sshTunnelService.GetForwardedPort("Siac", siacSshClient, 3307);
				siacTunnel.Start();

				try
				{
					// Establecer conexión con la base de datos SiacTest
					var nivel = new Niveles();
					nivel.SetConnection(MySqlConnections.SiacTest);
					var nivelsMsql = nivel.Get<Niveles>();

					BeginGlobalTransaction();

					nivelsMsql.ForEach(niv =>
					{
						var existingNivel = new Niveles()
						{
							Id = niv.Id
						}.Find<Niveles>();

						// Validar y ajustar las fechas
						niv.Created_at = DateUtil.ValidSqlDateTime(niv.Created_at.GetValueOrDefault());
						niv.Updated_at = DateUtil.ValidSqlDateTime(niv.Updated_at.GetValueOrDefault());

						if (existingNivel != null && existingNivel.Updated_at != niv.Updated_at)
						{
							// Actualizar el registro existente en Niveles
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
						}
						else if (existingNivel == null)
						{
							// Guardar el nuevo registro en Niveles
							niv.Save();
						}
					});

					CommitGlobalTransaction();
				}
				catch (System.Exception ex)
				{
					LoggerServices.AddMessageError("ERROR migrateNiveles.", ex);
					// RollBackGlobalTransaction(); // Descomentar si necesitas rollback
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

		public async Task<bool> migrateSecciones()
		{
			Console.Write("-->migrateSecciones");

			// Iniciar el túnel SSH para SiacTest
			using (var siacSshClient = _sshTunnelService.GetSshClient("Siac"))
			{
				// Conectar el cliente SSH
				siacSshClient.Connect();

				// Crear el puerto redirigido
				var siacTunnel = _sshTunnelService.GetForwardedPort("Siac", siacSshClient, 3307);
				siacTunnel.Start();
				try
				{
					// Establecer conexión con la base de datos SiacTest
					var seccion = new Secciones();
					seccion.SetConnection(MySqlConnections.SiacTest);
					var seccionsMsql = seccion.Get<Secciones>();

					BeginGlobalTransaction();

					seccionsMsql.ForEach(secc =>
					{
						var existingSeccion = new Secciones()
						{
							Id = secc.Id
						}.Find<Secciones>();

						if (existingSeccion != null)
						{
							// Actualizar el registro existente en Secciones
							existingSeccion.Nombre = secc.Nombre;
							existingSeccion.Clase_id = secc.Clase_id;
							existingSeccion.Docente_id = secc.Docente_id;
							existingSeccion.Observaciones = secc.Observaciones;
							existingSeccion.Updated_at = secc.Updated_at;
							existingSeccion.Update();
						}
						else
						{
							// Guardar un nuevo registro en Secciones
							secc.Save();
						}

					});

					CommitGlobalTransaction();
				}
				catch (System.Exception ex)
				{
					LoggerServices.AddMessageError("ERROR migrateSecciones.", ex);
					// RollBackGlobalTransaction(); // Descomentar si necesitas rollback
				}
				finally
				{
					// Detener el túnel SSH
					if (siacTunnel.IsStarted)
					{
						siacTunnel.Stop();
					}

					siacSshClient.Disconnect();
				}
			}

			return true;
		}

		public async Task<bool> migratePeriodosLectivos()
		{
			Console.Write("-->migratePeriodosLectivos");

			// Iniciar el túnel SSH para SiacTest
			using (var siacSshClient = _sshTunnelService.GetSshClient("Siac"))
			{
				// Conectar el cliente SSH
				siacSshClient.Connect();

				// Crear el puerto redirigido
				var siacTunnel = _sshTunnelService.GetForwardedPort("Siac", siacSshClient, 3307);
				siacTunnel.Start();
				try
				{
					// Establecer conexión con la base de datos SiacTest
					var periodo = new Periodo_lectivos();
					periodo.SetConnection(MySqlConnections.SiacTest);
					var periodosMsql = periodo.Get<Periodo_lectivos>();

					BeginGlobalTransaction();

					periodosMsql.ForEach(periodo =>
					{
						var existingPeriodo = new Periodo_lectivos()
						{
							Id = periodo.Id
						}.Find<Periodo_lectivos>();

						if (existingPeriodo != null && existingPeriodo.Updated_at != periodo.Updated_at)
						{
							// Actualizar el registro existente en Periodo_lectivos
							existingPeriodo.Nombre = periodo.Nombre;
							existingPeriodo.Nombre_corto = periodo.Nombre_corto;
							existingPeriodo.Observaciones = periodo.Observaciones;
							existingPeriodo.Config = periodo.Config;
							existingPeriodo.Abierto = periodo.Abierto;
							existingPeriodo.Oculto = periodo.Oculto;
							existingPeriodo.Update();
						}
						else if (existingPeriodo == null)
						{
							// Guardar un nuevo registro en Periodo_lectivos
							periodo.Save();
						}
					});

					CommitGlobalTransaction();
				}
				catch (System.Exception ex)
				{
					LoggerServices.AddMessageError("ERROR migratePeriodosLectivos.", ex);
					// RollBackGlobalTransaction(); // Descomentar si necesitas rollback
				}
				finally
				{
					// Detener el túnel SSH
					if (siacTunnel.IsStarted)
					{
						siacTunnel.Stop();
					}

					siacSshClient.Disconnect();
				}
			}

			return true;
		}

		public async Task<bool> migrateAsignaturas()
		{
			Console.Write("-->migrateAsignaturas");

			// Iniciar el túnel SSH para SiacTest
			using (var siacSshClient = _sshTunnelService.GetSshClient("Siac"))
			{
				// Conectar el cliente SSH
				siacSshClient.Connect();

				// Crear el puerto redirigido
				var siacTunnel = _sshTunnelService.GetForwardedPort("Siac", siacSshClient, 3307);
				siacTunnel.Start();
				try
				{

					// Establecer conexión con la base de datos SiacTest
					var asig = new Asignaturas();
					asig.SetConnection(MySqlConnections.SiacTest);
					var asigsMsql = asig.Get<Asignaturas>();

					BeginGlobalTransaction();

					asigsMsql.ForEach(asig =>
					{
						var existingAsignatura = new Asignaturas()
						{
							Id = asig.Id
						}.Find<Asignaturas>();

						asig.Created_at = DateUtil.ValidSqlDateTime(asig.Created_at.GetValueOrDefault());
						asig.Updated_at = DateUtil.ValidSqlDateTime(asig.Updated_at.GetValueOrDefault());

						if (existingAsignatura != null && existingAsignatura.Updated_at != asig.Updated_at)
						{
							// Actualizar el registro existente en Asignaturas
							existingAsignatura.Nombre = asig.Nombre;
							existingAsignatura.Nombre_corto = asig.Nombre_corto;
							existingAsignatura.Observaciones = asig.Observaciones;
							existingAsignatura.Nivel_id = asig.Nivel_id;
							existingAsignatura.Habilitado = asig.Habilitado;
							existingAsignatura.Updated_at = asig.Updated_at;
							existingAsignatura.Orden = asig.Orden;
							existingAsignatura.Update();
						}
						else if (existingAsignatura == null)
						{
							// Guardar un nuevo registro en Asignaturas
							asig.Save();
						}
					});

					CommitGlobalTransaction();
				}
				catch (System.Exception ex)
				{
					LoggerServices.AddMessageError("ERROR migrateAsignaturas.", ex);
					// RollBackGlobalTransaction(); // Descomentar si necesitas rollback
				}
				finally
				{
					// Detener el túnel SSH
					if (siacTunnel.IsStarted)
					{
						siacTunnel.Stop();
					}

					siacSshClient.Disconnect();
				}
			}

			return true;
		}

		public async Task<bool> migrateMateria()
		{
			Console.Write("-->migrateMateria");
			var fechaUltimaActualizacion = MigrateService.GetLastUpdate("MATERIA");
			// Iniciar el túnel SSH para SiacTest
			using (var siacSshClient = _sshTunnelService.GetSshClient("Siac"))
			{
				// Conectar el cliente SSH
				siacSshClient.Connect();

				// Crear el puerto redirigido
				var siacTunnel = _sshTunnelService.GetForwardedPort("Siac", siacSshClient, 3307);
				siacTunnel.Start();
				try
				{


					// Establecer conexión con la base de datos SiacTest
					var mat = new Materias();
					mat.SetConnection(MySqlConnections.SiacTest);
					var filter = new FilterData
					{
						PropName = "updated_at",
						FilterType = ">=",
						Values = new List<string?> { fechaUltimaActualizacion.ToString()}
					};
					var matsMsql = mat.Where<Materias>(filter);

					BeginGlobalTransaction();

					matsMsql.ForEach(mat =>
					{
						var existingMateria = new Materias()
						{
							Id = mat.Id
						}.Find<Materias>();

						mat.Created_at = DateUtil.ValidSqlDateTime(mat.Created_at.GetValueOrDefault());
						mat.Updated_at = DateUtil.ValidSqlDateTime(mat.Updated_at.GetValueOrDefault());

						if (existingMateria != null && existingMateria.Updated_at != mat.Updated_at)
						{
							// Actualizar el registro existente en Materias
							existingMateria.Clase_id = mat.Clase_id;
							existingMateria.Asignatura_id = mat.Asignatura_id;
							existingMateria.Observaciones = mat.Observaciones;
							existingMateria.Config = mat.Config;
							existingMateria.Lock_version = mat.Lock_version;
							existingMateria.Updated_at = mat.Updated_at;
							existingMateria.Update();
						}
						else if (existingMateria == null)
						{
							// Guardar un nuevo registro en Materias
							mat.Save();
						}
					});

					MigrateService.UpdateLastUpdate("MATERIA");
					CommitGlobalTransaction();
				}
				catch (System.Exception ex)
				{
					LoggerServices.AddMessageError("ERROR migrateMateria.", ex);
					// RollBackGlobalTransaction(); // Descomentar si necesitas rollback
				}
				finally
				{
					// Detener el túnel SSH
					if (siacTunnel.IsStarted)
					{
						siacTunnel.Stop();
					}

					siacSshClient.Disconnect();
				}
			}

			return true;
		}

		public async Task<bool> migrateClases()
		{
			Console.Write("-->migrateClases");

			// Iniciar el túnel SSH para SiacTest
			using (var siacSshClient = _sshTunnelService.GetSshClient("Siac"))
			{
				// Conectar el cliente SSH
				siacSshClient.Connect();

				// Crear el puerto redirigido
				var siacTunnel = _sshTunnelService.GetForwardedPort("Siac", siacSshClient, 3307);
				siacTunnel.Start();
				try
				{

					// Establecer conexión con la base de datos SiacTest
					var clase = new Clases();
					clase.SetConnection(MySqlConnections.SiacTest);
					var clasesMsql = clase.Get<Clases>();

					BeginGlobalTransaction();

					clasesMsql.ForEach(clase =>
					{
						var existingClase = new Clases()
						{
							Id = clase.Id
						}.Find<Clases>();

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
						}
						else if (existingClase == null)
						{
							// Guardar un nuevo registro en Clases
							clase.Save();
						}
					});

					CommitGlobalTransaction();
				}
				catch (System.Exception ex)
				{
					LoggerServices.AddMessageError("ERROR migrateClases.", ex);
					// RollBackGlobalTransaction(); // Descomentar si necesitas rollback
				}
				finally
				{
					// Detener el túnel SSH
					if (siacTunnel.IsStarted)
					{
						siacTunnel.Stop();
					}

					siacSshClient.Disconnect();
				}
			}

			return true;
		}

		public async Task<bool> migrateEstudiantesClases()
		{
			Console.Write("-->migrateEstudiantesClases");

			// Iniciar el túnel SSH para SiacTest
			using (var siacSshClient = _sshTunnelService.GetSshClient("Siac"))
			{
				siacSshClient.Connect();
				var siacTunnel = _sshTunnelService.GetForwardedPort("Siac", siacSshClient, 3307);
				siacTunnel.Start();
				try
				{

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
						return false;
					}

					// Establecer conexión con la base de datos SiacTest y obtener estudiantes clases
					var clase = new Estudiante_clases();
					clase.SetConnection(MySqlConnections.SiacTest);
					//var clasesMsql = clase.Where<Estudiante_clases>(FilterData.Equal("periodo_lectivo_id", periodo.Id));					
					
					var clasesMsql = clase.Get<Estudiante_clases>();
					if (siacTunnel.IsStarted)
					{
						siacTunnel.Stop();
					}

					siacSshClient.Disconnect();
					//BeginGlobalTransaction();

					clasesMsql.ForEach(clase =>
					{
						var estudiante = new Estudiantes()
						{
							Id = clase.Estudiante_id
						}.Find<Estudiantes>();

						// Si el estudiante no existe, omitir el registro
						if (estudiante == null)
						{
							Console.WriteLine($"Estudiante con ID {clase.Estudiante_id} no existe. Registro omitido.");
							return; // Omitir este registro
						}
						var existingClase = new Estudiante_clases()
						{
							Id = clase.Id
						}.Find<Estudiante_clases>();

						// Validar fechas y actualizar registro si ya existe
						clase.Created_at = DateUtil.ValidSqlDateTime(clase.Created_at.GetValueOrDefault());
						clase.Updated_at = DateUtil.ValidSqlDateTime(clase.Updated_at.GetValueOrDefault());

						if (existingClase != null && existingClase.Updated_at != clase.Updated_at)
						{
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
						}
						else if (existingClase == null)
						{
							clase.Save();
						}
					});

					//CommitGlobalTransaction();
				}
				catch (System.Exception ex)
				{
					LoggerServices.AddMessageError("ERROR migrateEstudiantesClases.", ex);
					// RollBackGlobalTransaction(); // Descomentar si necesitas rollback
				}
			}

			return true;
		}

		public async Task<bool> migrateDocentesAsignaturas()
		{
			Console.Write("-->migrateDocentesAsignaturas");

			// Iniciar el túnel SSH para SiacTest
			using (var siacSshClient = _sshTunnelService.GetSshClient("Siac"))
			{
				// Conectar el cliente SSH
				siacSshClient.Connect();

				// Crear el puerto redirigido
				var siacTunnel = _sshTunnelService.GetForwardedPort("Siac", siacSshClient, 3307);
				siacTunnel.Start();
				try
				{


					// Establecer conexión con la base de datos SiacTest y obtener las asignaturas de docentes
					var docAsig = new Docente_asignaturas();
					docAsig.SetConnection(MySqlConnections.SiacTest);
					var docAsigsMsql = docAsig.Get<Docente_asignaturas>();

					BeginGlobalTransaction();

					docAsigsMsql.ForEach(docAsig =>
					{
						var existingClase = new Docente_asignaturas()
						{
							Id = docAsig.Id
						}.Find<Docente_asignaturas>();

						// Validar fechas y actualizar registro si ya existe
						docAsig.Created_at = DateUtil.ValidSqlDateTime(docAsig.Created_at.GetValueOrDefault());
						docAsig.Updated_at = DateUtil.ValidSqlDateTime(docAsig.Updated_at.GetValueOrDefault());

						if (existingClase != null && existingClase.Updated_at != docAsig.Updated_at)
						{
							existingClase.Docente_id = docAsig.Docente_id;
							existingClase.Asignatura_id = docAsig.Asignatura_id;
							existingClase.Jefe = docAsig.Jefe;
							existingClase.Observaciones = docAsig.Observaciones;
							existingClase.Updated_at = docAsig.Updated_at;
							existingClase.Update();
						}
						else if (existingClase == null)
						{
							docAsig.Save();
						}
					});

					CommitGlobalTransaction();
				}
				catch (System.Exception ex)
				{
					LoggerServices.AddMessageError("ERROR migrateDocentesAsignaturas.", ex);
					// RollBackGlobalTransaction(); // Descomentar si necesitas rollback
				}
				finally
				{
					// Detener el túnel SSH
					if (siacTunnel.IsStarted)
					{
						siacTunnel.Stop();
					}

					siacSshClient.Disconnect();
				}
			}

			return true;
		}

		public async Task<bool> migrateDocentesMaterias()
		{
			Console.Write("-->migrateDocentesMaterias");

			// Iniciar el túnel SSH para SiacTest
			using (var siacSshClient = _sshTunnelService.GetSshClient("Siac"))
			{
				// Conectar el cliente SSH
				siacSshClient.Connect();

				// Crear el puerto redirigido
				var siacTunnel = _sshTunnelService.GetForwardedPort("Siac", siacSshClient, 3307);
				siacTunnel.Start();
				try
				{

					// Establecer conexión con la base de datos SiacTest y obtener las materias de docentes
					var docMat = new Docente_materias();
					docMat.SetConnection(MySqlConnections.SiacTest);
					var docMatsMsql = docMat.Get<Docente_materias>();

					BeginGlobalTransaction();

					docMatsMsql.ForEach(docMat =>
					{
						var existingClase = new Docente_materias()
						{
							Id = docMat.Id
						}.Find<Docente_materias>();

						// Validar fechas y actualizar registro si ya existe
						docMat.Created_at = DateUtil.ValidSqlDateTime(docMat.Created_at.GetValueOrDefault());
						docMat.Updated_at = DateUtil.ValidSqlDateTime(docMat.Updated_at.GetValueOrDefault());

						if (existingClase != null && existingClase.Updated_at != docMat.Updated_at)
						{
							existingClase.Materia_id = docMat.Materia_id;
							existingClase.Seccion_id = docMat.Seccion_id;
							existingClase.Docente_id = docMat.Docente_id;
							existingClase.Updated_at = docMat.Updated_at;
							existingClase.Update();
						}
						else if (existingClase == null)
						{
							docMat.Save();
						}
					});

					CommitGlobalTransaction();
				}
				catch (System.Exception ex)
				{
					LoggerServices.AddMessageError("ERROR migrateDocentesMaterias.", ex);
					// RollBackGlobalTransaction(); // Descomentar si necesitas rollback
				}
				finally
				{
					// Detener el túnel SSH
					if (siacTunnel.IsStarted)
					{
						siacTunnel.Stop();
					}

					siacSshClient.Disconnect();
				}
			}

			return true;
		}
				
	}
}