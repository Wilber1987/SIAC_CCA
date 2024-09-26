using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CAPA_DATOS;
using CAPA_NEGOCIO.Util;
using DataBaseModel;

namespace CAPA_NEGOCIO.Oparations
{
	public class MigrateGestionCursos : TransactionalClass
	{
		public bool Migrate()
		{			
			//return migrateEstudiantesClases();

			return	migrateNiveles() &&
				migrateSecciones() &&
				migratePeriodosLectivos() &&
				migrateAsignaturas() && migrateClases() && migrateMateria()
				&& migrateEstudiantesClases() 
				&& migrateDocentesAsignaturas()
				&& migrateDocentesMaterias();
		}

		public bool migrateNiveles()
		{
			Console.Write("-->migrateNiveles");
			var nivel = new Niveles();
			nivel.SetConnection(MySqlConnections.Siac);
			var nivelsMsql = nivel.Get<Niveles>();
			try
			{
				BeginGlobalTransaction();
				nivelsMsql.ForEach(niv =>
				{
					var existingNivel = new Niveles()
					{
						Id = niv.Id
					}.Find<Niveles>();
					niv.Created_at = DateUtil.ValidSqlDateTime(niv.Created_at.GetValueOrDefault());
					niv.Updated_at = DateUtil.ValidSqlDateTime(niv.Updated_at.GetValueOrDefault());
					if (existingNivel != null && existingNivel.Updated_at != niv.Updated_at)
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
					}
					else if (existingNivel == null)
					{
						niv.Save();
					}

				});
				CommitGlobalTransaction();
			}
			catch (System.Exception ex)
			{
				LoggerServices.AddMessageError("ERROR migrateNiveles.", ex);
				RollBackGlobalTransaction();
				throw;
			}

			return true;
		}

		public bool migrateSecciones()
		{
			Console.Write("-->migrateSecciones");
			var seccion = new Secciones();
			seccion.SetConnection(MySqlConnections.Siac);
			var seccionsMsql = seccion.Get<Secciones>();
			try
			{
				BeginGlobalTransaction();
				seccionsMsql.ForEach(secc =>
				{
					var existingSeccion = new Secciones()
					{
						Id = secc.Id
					}.Find<Secciones>();

					if (existingSeccion != null)
					{
						existingSeccion.Nombre = secc.Nombre;
						existingSeccion.Clase_id = secc.Clase_id;
						existingSeccion.Docente_id = secc.Docente_id;
						existingSeccion.Observaciones = secc.Observaciones;
						existingSeccion.Updated_at = secc.Updated_at;
						existingSeccion.Update();
					}
					else
					{
						secc.Save();
					}

				});
				CommitGlobalTransaction();
			}
			catch (System.Exception ex)
			{
				LoggerServices.AddMessageError("ERROR migrateSecciones.", ex);
				RollBackGlobalTransaction();
				throw;
			}

			return true;
		}

		public bool migratePeriodosLectivos()
		{
			Console.Write("-->migratePeriodosLectivos");
			var periodo = new Periodo_lectivos();
			periodo.SetConnection(MySqlConnections.Siac);
			var periodosMsql = periodo.Get<Periodo_lectivos>();
			try
			{
				BeginGlobalTransaction();
				periodosMsql.ForEach(periodo =>
				{
					var existingPeriodo = new Periodo_lectivos()
					{
						Id = periodo.Id
					}.Find<Periodo_lectivos>();

					if (existingPeriodo != null && existingPeriodo.Updated_at != periodo.Updated_at)
					{
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
						periodo.Save();
					}

				});
				CommitGlobalTransaction();
			}
			catch (System.Exception ex)
			{
				LoggerServices.AddMessageError("ERROR migratePeriodosLectivos.", ex);
				RollBackGlobalTransaction();
				throw;
			}

			return true;
		}

		public bool migrateAsignaturas()
		{
			Console.Write("-->migrateAsignaturas");
			var asig = new Asignaturas();
			asig.SetConnection(MySqlConnections.Siac);
			var asigsMsql = asig.Get<Asignaturas>();
			try
			{
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
						asig.Save();
					}

				});
				CommitGlobalTransaction();
			}
			catch (System.Exception ex)
			{
				LoggerServices.AddMessageError("ERROR migrateAsignaturas.", ex);
				RollBackGlobalTransaction();
				throw;
			}

			return true;
		}

		public bool migrateMateria()
		{
			Console.Write("-->migrateMateria");
			var mat = new Materias();
			mat.SetConnection(MySqlConnections.Siac);
			var matsMsql = mat.Get<Materias>();
			try
			{
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
						mat.Save();
					}
				});
				CommitGlobalTransaction();
			}
			catch (System.Exception ex)
			{
				LoggerServices.AddMessageError("ERROR migrateMateria.", ex);
				RollBackGlobalTransaction();
				throw;
			}

			return true;
		}

		public bool migrateClases()
		{
			Console.Write("-->migrateClases");
			var clase = new Clases();
			clase.SetConnection(MySqlConnections.Siac);
			var clasesMsql = clase.Get<Clases>();
			try
			{
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
						existingClase.Grado = clase.Grado;
						existingClase.Nivel_id = clase.Nivel_id;
						existingClase.Observaciones = clase.Observaciones;
						existingClase.Periodo_lectivo_id = clase.Periodo_lectivo_id;
						existingClase.Updated_at = clase.Updated_at;
						existingClase.Update();
					}
					else if (existingClase == null)
					{
						clase.Save();
					}
				});
				CommitGlobalTransaction();
			}
			catch (System.Exception ex)
			{
				LoggerServices.AddMessageError("ERROR migrateClases.", ex);
				RollBackGlobalTransaction();
				throw;
			}

			return true;
		}

		public bool migrateEstudiantesClases()
		{
			Console.Write("-->migrateEstudiantesClases");
			var clase = new Estudiante_clases();
			clase.SetConnection(MySqlConnections.Siac);
			//var clasesMsql = clase.Get<Estudiante_clases>();

			var filter = new FilterData
			{
				PropName = "created_at",
				FilterType = ">=",
				Values = new List<string?> { "2022-01-01 00:00:00" }
			};
			var clasesMsql = clase.Where<Estudiante_clases>(filter);


			try
			{
				BeginGlobalTransaction();
				clasesMsql.ForEach(clase =>
				{
					var existingClase = new Estudiante_clases()
					{
						Id = clase.Id
					}.Find<Estudiante_clases>();

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
						/*var estExists = new Estudiantes();
						var exists = estExists.Where<Estudiantes>(FilterData.Equal("Id", clase.Estudiante_id));
						// Verificar si el estudiante existe antes de guardar la clase		
						if (exists.Count()>0)
						{*/
							clase.Save();
						//}
					}
				});
				CommitGlobalTransaction();
			}
			catch (System.Exception ex)
			{
				LoggerServices.AddMessageError("ERROR migrateEstudiantesClases.", ex);
				RollBackGlobalTransaction();
				throw;
			}

			return true;
		}


		public bool migrateDocentesAsignaturas()
		{
			Console.Write("-->migrateDocentesAsignaturas");
			var docAsig = new Docente_asignaturas();
			docAsig.SetConnection(MySqlConnections.Siac);
			var docAsigsMsql = docAsig.Get<Docente_asignaturas>();
			try
			{
				BeginGlobalTransaction();
				docAsigsMsql.ForEach(docAsig =>
				{

					var existingClase = new Docente_asignaturas()
					{
						Id = docAsig.Id
					}.Find<Docente_asignaturas>();

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
				RollBackGlobalTransaction();
				throw;
			}

			return true;
		}

		public bool migrateDocentesMaterias()
		{
			Console.Write("-->migrateDocentesMaterias");
			var docMat = new Docente_materias();
			docMat.SetConnection(MySqlConnections.Siac);
			var docMatsMsql = docMat.Get<Docente_materias>();
			try
			{
				BeginGlobalTransaction();
				docMatsMsql.ForEach(docMat =>
				{

					var existingClase = new Docente_materias()
					{
						Id = docMat.Id
					}.Find<Docente_materias>();

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
				RollBackGlobalTransaction();
				throw;
			}

			return true;
		}
	}
}