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
        public bool MigrateNotas(){
            migrateNiveles()
            migrateSecciones();
            migratePeriodosLectivos();
            migrateAsignaturas();
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
					if (niv.Exists<Niveles>())
					{						
                        niv.Nombre = niv.Nombre;
                        niv.Nombre_corto = niv.Nombre_corto;
                        niv.Nombre_grado = niv.Nombre_grado;
                        niv.Numero_grados = niv.Numero_grados;
                        niv.Habilitado = niv.Habilitado;
                        niv.Orden = niv.Orden;
                        niv.Inicio_grado = niv.Inicio_grado;
                        niv.Update();
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
					if (secc.Exists<Secciones>())
					{
						secc.Nombre = secc.Nombre;
                        secc.Clase_id = secc.Clase_id;
                        secc.Docente_id = secc.Docente_id;
                        secc.Observaciones = secc.Observaciones;                        
                        secc.Updated_at = secc.Updated_at;
                        secc.Update();
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
                    periodo.inicio = DateUtil.ValidSqlDateTime(periodo.inicio.GetValueOrDefault());
                    periodo.fin = DateUtil.ValidSqlDateTime(periodo.fin.GetValueOrDefault());
					if (periodo.Exists<Periodo_lectivos>())
					{
						periodo.Nombre = periodo.Nombre;
                        periodo.Nombre_corto = periodo.Nombre_corto;
                        periodo.Observaciones = periodo.Observaciones;
                        periodo.config = periodo.config;
                        periodo.Abierto = periodo.Abierto;
                        periodo.Oculto = periodo.Oculto;
                        periodo.Update();
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
                    asig.Created_at = DateUtil.ValidSqlDateTime(asig.Created_at.GetValueOrDefault());
                    asig.Updated_at = DateUtil.ValidSqlDateTime(asig.Updated_at.GetValueOrDefault());
					if (asig.Exists<Asignaturas>())
					{
						asig.Nombre = asig.Nombre;
                        asig.Nombre_corto = asig.Nombre_corto;
                        asig.Observaciones = asig.Observaciones;
                        asig.config = asig.config;
                        asig.Abierto = asig.Abierto;
                        asig.Oculto = asig.Oculto;
                        asig.Nivel_id = asig.Nivel_id;
                        asig.Update();
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
            var asig = new Materias();
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
                        mat.config = mat.config;                        
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