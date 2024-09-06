using CAPA_DATOS;
using CAPA_DATOS.Security;
using DataBaseModel;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers {
   [Route("api/[controller]/[action]")]
   [ApiController]
   public class  ApiEntityDboController : ControllerBase {
       //Asignaturas
       [HttpPost]
       [AuthController]
       public List<Asignaturas> getAsignaturas(Asignaturas Inst) {
           return Inst.Where<Asignaturas>(FilterData.Limit(30));
       }
       [HttpPost]
       [AuthController]
       public Asignaturas? findAsignaturas(Asignaturas Inst) {
           return Inst.Find<Asignaturas>();
       }
       [HttpPost]
       [AuthController(Permissions.ADMIN_ACCESS)]
       public object? saveAsignaturas(Asignaturas inst) {
           return inst.Save();
       }
       [HttpPost]
       [AuthController(Permissions.ADMIN_ACCESS)]
       public object? updateAsignaturas(Asignaturas inst) {
           return inst.Update();
       }
       //Calificaciones
       [HttpPost]
       [AuthController]
       public List<Calificaciones> getCalificaciones(Calificaciones Inst) {
           return Inst.Where<Calificaciones>(FilterData.Limit(30));
       }
       [HttpPost]
       [AuthController]
       public Calificaciones? findCalificaciones(Calificaciones Inst) {
           return Inst.Find<Calificaciones>();
       }
       [HttpPost]
       [AuthController(Permissions.ADMIN_ACCESS)]
       public object? saveCalificaciones(Calificaciones inst) 
       {
           return inst.Save();
       }
       [HttpPost]
       [AuthController(Permissions.ADMIN_ACCESS)]
       public object? updateCalificaciones(Calificaciones inst) {
           return inst.Update();
       }
       //Clases
       [HttpPost]
       [AuthController]
       public List<Clases> getClases(Clases Inst) {
           return Inst.Where<Clases>(FilterData.Limit(30));
       }
       [HttpPost]
       [AuthController]
       public Clases? findClases(Clases Inst) {
           return Inst.Find<Clases>();
       }
       [HttpPost]
       [AuthController(Permissions.ADMIN_ACCESS)]
       public object? saveClases(Clases inst) {
           return inst.Save();
       }
       [HttpPost]
       [AuthController(Permissions.ADMIN_ACCESS)]
       public object? updateClases(Clases inst) {
           return inst.Update();
       }
       //Docente_asignaturas
       [HttpPost]
       [AuthController]
       public List<Docente_asignaturas> getDocente_asignaturas(Docente_asignaturas Inst) {
           return Inst.Where<Docente_asignaturas>(FilterData.Limit(30));
       }
       [HttpPost]
       [AuthController]
       public Docente_asignaturas? findDocente_asignaturas(Docente_asignaturas Inst) {
           return Inst.Find<Docente_asignaturas>();
       }
       [HttpPost]
       [AuthController(Permissions.ADMIN_ACCESS)]
       public object? saveDocente_asignaturas(Docente_asignaturas inst) {
           return inst.Save();
       }
       [HttpPost]
       [AuthController(Permissions.ADMIN_ACCESS)]
       public object? updateDocente_asignaturas(Docente_asignaturas inst) {
           return inst.Update();
       }
       /* 
       [HttpPost]
        [AuthController(Permissions.ADMIN_ACCESS)]
        public object? saveMaterias(Materias inst)
        {
            return inst.Save();
        }
        [HttpPost]
        [AuthController(Permissions.ADMIN_ACCESS)]
        public object? updateMaterias(Materias inst)
        {
            return inst.Update();
        }
       
       
       //Docente_materias
       [HttpPost]
       [AuthController]
       public List<Docente_materias> getDocente_materias(Docente_materias Inst) {
           return Inst.Where<Docente_materias>(FilterData.Limit(30));
       }
       [HttpPost]
       [AuthController]
       public Docente_materias? findDocente_materias(Docente_materias Inst) {
           return Inst.Find<Docente_materias>();
       }
       [HttpPost]
       [AuthController(Permissions.ADMIN_ACCESS)]
       public object? saveDocente_materias(Docente_materias inst) {
           return inst.Save();
       }
       [HttpPost]
       [AuthController(Permissions.ADMIN_ACCESS)]
       public object? updateDocente_materias(Docente_materias inst) {
           return inst.Update();
       } */
       //Docentes
       [HttpPost]
       [AuthController]
       public List<Docentes> getDocentes(Docentes Inst) {
           return Inst.Where<Docentes>(FilterData.Limit(30));
       }
       [HttpPost]
       [AuthController]
       public Docentes? findDocentes(Docentes Inst) {
           return Inst.Find<Docentes>();
       }
       [HttpPost]
       [AuthController(Permissions.ADMIN_ACCESS)]
       public object? saveDocentes(Docentes inst) {
           return inst.Save();
       }
       [HttpPost]
       [AuthController(Permissions.ADMIN_ACCESS)]
       public object? updateDocentes(Docentes inst) {
           return inst.Update();
       }
       //Escolaridades
       [HttpPost]
       [AuthController]
       public List<Escolaridades> getEscolaridades(Escolaridades Inst) {
           return Inst.Where<Escolaridades>(FilterData.Limit(30));
       }
       [HttpPost]
       [AuthController]
       public Escolaridades? findEscolaridades(Escolaridades Inst) {
           return Inst.Find<Escolaridades>();
       }
       [HttpPost]
       [AuthController(Permissions.ADMIN_ACCESS)]
       public object? saveEscolaridades(Escolaridades inst) {
           return inst.Save();
       }
       [HttpPost]
       [AuthController(Permissions.ADMIN_ACCESS)]
       public object? updateEscolaridades(Escolaridades inst) {
           return inst.Update();
       }
       
       
       //Evaluaciones
       [HttpPost]
       [AuthController]
       public List<Evaluaciones> getEvaluaciones(Evaluaciones Inst) {
           return Inst.Where<Evaluaciones>(FilterData.Limit(30));
       }
       [HttpPost]
       [AuthController]
       public Evaluaciones? findEvaluaciones(Evaluaciones Inst) {
           return Inst.Find<Evaluaciones>();
       }
       [HttpPost]
       [AuthController(Permissions.ADMIN_ACCESS)]
       public object? saveEvaluaciones(Evaluaciones inst) {
           return inst.Save();
       }
       [HttpPost]
       [AuthController(Permissions.ADMIN_ACCESS)]
       public object? updateEvaluaciones(Evaluaciones inst) {
           return inst.Update();
       }
       
       //Niveles
       [HttpPost]
       [AuthController]
       public List<Niveles> getNiveles(Niveles Inst) {
           return Inst.Where<Niveles>(FilterData.Limit(30));
       }
       [HttpPost]
       [AuthController]
       public Niveles? findNiveles(Niveles Inst) {
           return Inst.Find<Niveles>();
       }
       [HttpPost]
       [AuthController(Permissions.ADMIN_ACCESS)]
       public object? saveNiveles(Niveles inst) {
           return inst.Save();
       }
       [HttpPost]
       [AuthController(Permissions.ADMIN_ACCESS)]
       public object? updateNiveles(Niveles inst) {
           return inst.Update();
       }
       //Parientes
       [HttpPost]
       [AuthController]
       public List<Parientes> getParientes(Parientes Inst) {
           return Inst.Where<Parientes>(FilterData.Limit(30));
       }
       [HttpPost]
       [AuthController]
       public Parientes? findParientes(Parientes Inst) {
           return Inst.Find<Parientes>();
       }
       [HttpPost]
       [AuthController(Permissions.ADMIN_ACCESS)]
       public object? saveParientes(Parientes inst) {
           return inst.Save();
       }
       [HttpPost]
       [AuthController(Permissions.ADMIN_ACCESS)]
       public object? updateParientes(Parientes inst) {
           return inst.Update();
       }
       //Periodo_lectivos
       [HttpPost]
       [AuthController]
       public List<Periodo_lectivos> getPeriodo_lectivos(Periodo_lectivos Inst) {
           return Inst.Where<Periodo_lectivos>(FilterData.Limit(30));
       }
       [HttpPost]
       [AuthController]
       public Periodo_lectivos? findPeriodo_lectivos(Periodo_lectivos Inst) {
           return Inst.Find<Periodo_lectivos>();
       }
       [HttpPost]
       [AuthController(Permissions.ADMIN_ACCESS)]
       public object? savePeriodo_lectivos(Periodo_lectivos inst) {
           return inst.Save();
       }
       [HttpPost]
       [AuthController(Permissions.ADMIN_ACCESS)]
       public object? updatePeriodo_lectivos(Periodo_lectivos inst) {
           return inst.Update();
       }       
       //Secciones
       [HttpPost]
       [AuthController]
       public List<Secciones> getSecciones(Secciones Inst) {
           return Inst.Where<Secciones>(FilterData.Limit(30));
       }
       [HttpPost]
       [AuthController]
       public Secciones? findSecciones(Secciones Inst) {
           return Inst.Find<Secciones>();
       }
       [HttpPost]
       [AuthController(Permissions.ADMIN_ACCESS)]
       public object? saveSecciones(Secciones inst) {
           return inst.Save();
       }
       [HttpPost]
       [AuthController(Permissions.ADMIN_ACCESS)]
       public object? updateSecciones(Secciones inst) {
           return inst.Update();
       }
       //Tipo_notas
       [HttpPost]
       [AuthController]
       public List<Tipo_notas> getTipo_notas(Tipo_notas Inst) {
           return Inst.Where<Tipo_notas>(FilterData.Limit(30));
       }
       [HttpPost]
       [AuthController]
       public Tipo_notas? findTipo_notas(Tipo_notas Inst) {
           return Inst.Find<Tipo_notas>();
       }
       [HttpPost]
       [AuthController(Permissions.ADMIN_ACCESS)]
       public object? saveTipo_notas(Tipo_notas inst) {
           return inst.Save();
       }
       [HttpPost]
       [AuthController(Permissions.ADMIN_ACCESS)]
       public object? updateTipo_notas(Tipo_notas inst) {
           return inst.Update();
       }
       //Log
       [HttpPost]
       [AuthController]
       public List<CAPA_DATOS.Log> getLog(CAPA_DATOS.Log Inst) {
           return Inst.Where<CAPA_DATOS.Log>(FilterData.Limit(30));
       }
       [HttpPost]
       [AuthController]
       public CAPA_DATOS.Log? findLog(CAPA_DATOS.Log Inst) {
           return Inst.Find<CAPA_DATOS.Log>();
       }
       [HttpPost]
       [AuthController(Permissions.ADMIN_ACCESS)]
       public object? saveLog(CAPA_DATOS.Log inst) {
           return inst.Save();
       }
       [HttpPost]
       [AuthController(Permissions.ADMIN_ACCESS)]
       public object? updateLog(CAPA_DATOS.Log inst) {
           return inst.Update();
       }
       //Estudiantes_responsables_familia
       [HttpPost]
       [AuthController]
       public List<Estudiantes_responsables_familia> getEstudiantes_responsables_familia(Estudiantes_responsables_familia Inst) {
           return Inst.Where<Estudiantes_responsables_familia>(FilterData.Limit(30));
       }
       [HttpPost]
       [AuthController]
       public Estudiantes_responsables_familia? findEstudiantes_responsables_familia(Estudiantes_responsables_familia Inst) {
           return Inst.Find<Estudiantes_responsables_familia>();
       }
       [HttpPost]
       [AuthController(Permissions.ADMIN_ACCESS)]
       public object? saveEstudiantes_responsables_familia(Estudiantes_responsables_familia inst) {
           return inst.Save();
       }
       [HttpPost]
       [AuthController(Permissions.ADMIN_ACCESS)]
       public object? updateEstudiantes_responsables_familia(Estudiantes_responsables_familia inst) {
           return inst.Update();
       }
       [HttpPost]
       [AuthController]
       public List<Familias> getFamilias(Familias Inst) {
           return Inst.Where<Familias>(FilterData.Limit(30));
       }
       [HttpPost]
       [AuthController]
       public Familias? findFamilias(Familias Inst) {
           return Inst.Find<Familias>();
       }
       [HttpPost]
       [AuthController(Permissions.ADMIN_ACCESS)]
       public object? saveFamilias(Familias inst) {
           return inst.Save();
       }
       [HttpPost]
       [AuthController(Permissions.ADMIN_ACCESS)]
       public object? updateFamilias(Familias inst) {
           return inst.Update();
       }
   }
}
