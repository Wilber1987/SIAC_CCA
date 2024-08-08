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
        public bool Migrate(){
            migrateNiveles();
            migrateSecciones();
            migratePeriodosLectivos();
            migrateAsignaturas();
			return true;
        }

        public bool migrateNiveles(){
            var nivel = new Niveles();
			nivel.SetConnection(MySQLConnection.SQLM);
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
					if (existingNivel!= null )
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
					} else 
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

        public bool migrateSecciones(){
            var seccion = new Secciones();
			seccion.SetConnection(MySQLConnection.SQLM);
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

					if (existingSeccion!=null)
					{
						existingSeccion.Nombre = secc.Nombre;
                        existingSeccion.Clase_id = secc.Clase_id;
                        existingSeccion.Docente_id = secc.Docente_id;
                        existingSeccion.Observaciones = secc.Observaciones;                        
                        existingSeccion.Updated_at = secc.Updated_at;
                        existingSeccion.Update();
					} else 
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

        public bool migratePeriodosLectivos(){
            var periodo = new Periodo_lectivos();
			periodo.SetConnection(MySQLConnection.SQLM);
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

					if (existingPeriodo!=null)
					{
						existingPeriodo.Nombre = periodo.Nombre;
                        existingPeriodo.Nombre_corto = periodo.Nombre_corto;
                        existingPeriodo.Observaciones = periodo.Observaciones;
                        existingPeriodo.Config = periodo.Config;
                        existingPeriodo.Abierto = periodo.Abierto;
                        existingPeriodo.Oculto = periodo.Oculto;
                        existingPeriodo.Update();
					} else 
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

        public bool migrateAsignaturas(){
            var asig = new Asignaturas();
			asig.SetConnection(MySQLConnection.SQLM);
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
					if (existingAsignatura!=null)
					{
						/*asig.Nombre = asig.Nombre;
                        asig.Nombre_corto = asig.Nombre_corto;
                        asig.Observaciones = asig.Observaciones;
                        asig.config = asig.config;
                        asig.Abierto = asig.Abierto;
                        asig.Oculto = asig.Oculto;
                        asig.Nivel_id = asig.Nivel_id;
                        asig.Update();*/
					} else 
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

        public bool migrateMateria(){
            var mat = new Materias();
			mat.SetConnection(MySQLConnection.SQLM);
			var matsMsql = mat.Get<Materias>();
			try
			{
				BeginGlobalTransaction();
				matsMsql.ForEach(mat =>
				{
                    mat.Created_at = DateUtil.ValidSqlDateTime(mat.Created_at.GetValueOrDefault());
                    mat.Updated_at = DateUtil.ValidSqlDateTime(mat.Updated_at.GetValueOrDefault());
					if (mat.Exists<Materias>())
					{
						mat.Clase_id = mat.Clase_id;
                        mat.Asignatura_id = mat.Asignatura_id;
                        mat.Observaciones = mat.Observaciones;
                        mat.Config = mat.Config;                        
                        mat.Lock_version = mat.Lock_version;
                        mat.Update();
					} else 
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
    }
}