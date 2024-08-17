using DataBaseModel;
using Security;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
namespace API.Controllers {
   [Route("api/[controller]/[action]")]
   [ApiController]
   public class  ApiEntityDboController : ControllerBase {
       //Familias
       [HttpPost]
       [AuthController]
       public List<Familias> getFamilias(Familias Inst) {
           return Inst.Get<Familias>();
       }
       [HttpPost]
       [AuthController]
       public Familias findFamilias(Familias Inst) {
           return Inst.Find<Familias>();
       }
       [HttpPost]
       [AuthController]
       public object saveFamilias(Familias inst) {
           return inst.Save();
       }
       [HttpPost]
       [AuthController]
       public object updateFamilias(Familias inst) {
           return inst.Update();
       }
       //Asignaturas
       [HttpPost]
       [AuthController]
       public List<Asignaturas> getAsignaturas(Asignaturas Inst) {
           return Inst.Get<Asignaturas>();
       }
       [HttpPost]
       [AuthController]
       public Asignaturas findAsignaturas(Asignaturas Inst) {
           return Inst.Find<Asignaturas>();
       }
       [HttpPost]
       [AuthController]
       public object saveAsignaturas(Asignaturas inst) {
           return inst.Save();
       }
       [HttpPost]
       [AuthController]
       public object updateAsignaturas(Asignaturas inst) {
           return inst.Update();
       }
       //Calificaciones
       [HttpPost]
       [AuthController]
       public List<Calificaciones> getCalificaciones(Calificaciones Inst) {
           return Inst.Get<Calificaciones>();
       }
       [HttpPost]
       [AuthController]
       public Calificaciones findCalificaciones(Calificaciones Inst) {
           return Inst.Find<Calificaciones>();
       }
       [HttpPost]
       [AuthController]
       public object saveCalificaciones(Calificaciones inst) {
           return inst.Save();
       }
       [HttpPost]
       [AuthController]
       public object updateCalificaciones(Calificaciones inst) {
           return inst.Update();
       }
       //Clases
       [HttpPost]
       [AuthController]
       public List<Clases> getClases(Clases Inst) {
           return Inst.Get<Clases>();
       }
       [HttpPost]
       [AuthController]
       public Clases findClases(Clases Inst) {
           return Inst.Find<Clases>();
       }
       [HttpPost]
       [AuthController]
       public object saveClases(Clases inst) {
           return inst.Save();
       }
       [HttpPost]
       [AuthController]
       public object updateClases(Clases inst) {
           return inst.Update();
       }
       //Docente_asignaturas
       [HttpPost]
       [AuthController]
       public List<Docente_asignaturas> getDocente_asignaturas(Docente_asignaturas Inst) {
           return Inst.Get<Docente_asignaturas>();
       }
       [HttpPost]
       [AuthController]
       public Docente_asignaturas findDocente_asignaturas(Docente_asignaturas Inst) {
           return Inst.Find<Docente_asignaturas>();
       }
       [HttpPost]
       [AuthController]
       public object saveDocente_asignaturas(Docente_asignaturas inst) {
           return inst.Save();
       }
       [HttpPost]
       [AuthController]
       public object updateDocente_asignaturas(Docente_asignaturas inst) {
           return inst.Update();
       }
       //Docente_materias
       [HttpPost]
       [AuthController]
       public List<Docente_materias> getDocente_materias(Docente_materias Inst) {
           return Inst.Get<Docente_materias>();
       }
       [HttpPost]
       [AuthController]
       public Docente_materias findDocente_materias(Docente_materias Inst) {
           return Inst.Find<Docente_materias>();
       }
       [HttpPost]
       [AuthController]
       public object saveDocente_materias(Docente_materias inst) {
           return inst.Save();
       }
       [HttpPost]
       [AuthController]
       public object updateDocente_materias(Docente_materias inst) {
           return inst.Update();
       }
       //Docentes
       [HttpPost]
       [AuthController]
       public List<Docentes> getDocentes(Docentes Inst) {
           return Inst.Get<Docentes>();
       }
       [HttpPost]
       [AuthController]
       public Docentes findDocentes(Docentes Inst) {
           return Inst.Find<Docentes>();
       }
       [HttpPost]
       [AuthController]
       public object saveDocentes(Docentes inst) {
           return inst.Save();
       }
       [HttpPost]
       [AuthController]
       public object updateDocentes(Docentes inst) {
           return inst.Update();
       }
       //Escolaridades
       [HttpPost]
       [AuthController]
       public List<Escolaridades> getEscolaridades(Escolaridades Inst) {
           return Inst.Get<Escolaridades>();
       }
       [HttpPost]
       [AuthController]
       public Escolaridades findEscolaridades(Escolaridades Inst) {
           return Inst.Find<Escolaridades>();
       }
       [HttpPost]
       [AuthController]
       public object saveEscolaridades(Escolaridades inst) {
           return inst.Save();
       }
       [HttpPost]
       [AuthController]
       public object updateEscolaridades(Escolaridades inst) {
           return inst.Update();
       }
       //Estudiante_clases
       [HttpPost]
       [AuthController]
       public List<Estudiante_clases> getEstudiante_clases(Estudiante_clases Inst) {
           return Inst.Get<Estudiante_clases>();
       }
       [HttpPost]
       [AuthController]
       public Estudiante_clases findEstudiante_clases(Estudiante_clases Inst) {
           return Inst.Find<Estudiante_clases>();
       }
       [HttpPost]
       [AuthController]
       public object saveEstudiante_clases(Estudiante_clases inst) {
           return inst.Save();
       }
       [HttpPost]
       [AuthController]
       public object updateEstudiante_clases(Estudiante_clases inst) {
           return inst.Update();
       }
       //Estudiantes
       [HttpPost]
       [AuthController]
       public List<Estudiantes> getEstudiantes(Estudiantes Inst) {
           return Inst.Get<Estudiantes>();
       }
       [HttpPost]
       [AuthController]
       public Estudiantes findEstudiantes(Estudiantes Inst) {
           return Inst.Find<Estudiantes>();
       }
       [HttpPost]
       [AuthController]
       public object saveEstudiantes(Estudiantes inst) {
           return inst.Save();
       }
       [HttpPost]
       [AuthController]
       public object updateEstudiantes(Estudiantes inst) {
           return inst.Update();
       }
       //Evaluaciones
       [HttpPost]
       [AuthController]
       public List<Evaluaciones> getEvaluaciones(Evaluaciones Inst) {
           return Inst.Get<Evaluaciones>();
       }
       [HttpPost]
       [AuthController]
       public Evaluaciones findEvaluaciones(Evaluaciones Inst) {
           return Inst.Find<Evaluaciones>();
       }
       [HttpPost]
       [AuthController]
       public object saveEvaluaciones(Evaluaciones inst) {
           return inst.Save();
       }
       [HttpPost]
       [AuthController]
       public object updateEvaluaciones(Evaluaciones inst) {
           return inst.Update();
       }
       //Log
       [HttpPost]
       [AuthController]
       public List<Log> getLog(Log Inst) {
           return Inst.Get<Log>();
       }
       [HttpPost]
       [AuthController]
       public Log findLog(Log Inst) {
           return Inst.Find<Log>();
       }
       [HttpPost]
       [AuthController]
       public object saveLog(Log inst) {
           return inst.Save();
       }
       [HttpPost]
       [AuthController]
       public object updateLog(Log inst) {
           return inst.Update();
       }
       //Materias
       [HttpPost]
       [AuthController]
       public List<Materias> getMaterias(Materias Inst) {
           return Inst.Get<Materias>();
       }
       [HttpPost]
       [AuthController]
       public Materias findMaterias(Materias Inst) {
           return Inst.Find<Materias>();
       }
       [HttpPost]
       [AuthController]
       public object saveMaterias(Materias inst) {
           return inst.Save();
       }
       [HttpPost]
       [AuthController]
       public object updateMaterias(Materias inst) {
           return inst.Update();
       }
       //Niveles
       [HttpPost]
       [AuthController]
       public List<Niveles> getNiveles(Niveles Inst) {
           return Inst.Get<Niveles>();
       }
       [HttpPost]
       [AuthController]
       public Niveles findNiveles(Niveles Inst) {
           return Inst.Find<Niveles>();
       }
       [HttpPost]
       [AuthController]
       public object saveNiveles(Niveles inst) {
           return inst.Save();
       }
       [HttpPost]
       [AuthController]
       public object updateNiveles(Niveles inst) {
           return inst.Update();
       }
       //Parientes
       [HttpPost]
       [AuthController]
       public List<Parientes> getParientes(Parientes Inst) {
           return Inst.Get<Parientes>();
       }
       [HttpPost]
       [AuthController]
       public Parientes findParientes(Parientes Inst) {
           return Inst.Find<Parientes>();
       }
       [HttpPost]
       [AuthController]
       public object saveParientes(Parientes inst) {
           return inst.Save();
       }
       [HttpPost]
       [AuthController]
       public object updateParientes(Parientes inst) {
           return inst.Update();
       }
       //Periodo_lectivos
       [HttpPost]
       [AuthController]
       public List<Periodo_lectivos> getPeriodo_lectivos(Periodo_lectivos Inst) {
           return Inst.Get<Periodo_lectivos>();
       }
       [HttpPost]
       [AuthController]
       public Periodo_lectivos findPeriodo_lectivos(Periodo_lectivos Inst) {
           return Inst.Find<Periodo_lectivos>();
       }
       [HttpPost]
       [AuthController]
       public object savePeriodo_lectivos(Periodo_lectivos inst) {
           return inst.Save();
       }
       [HttpPost]
       [AuthController]
       public object updatePeriodo_lectivos(Periodo_lectivos inst) {
           return inst.Update();
       }
       //Estudiantes_responsables_familias
       [HttpPost]
       [AuthController]
       public List<Estudiantes_responsables_familias> getEstudiantes_responsables_familias(Estudiantes_responsables_familias Inst) {
           return Inst.Get<Estudiantes_responsables_familias>();
       }
       [HttpPost]
       [AuthController]
       public Estudiantes_responsables_familias findEstudiantes_responsables_familias(Estudiantes_responsables_familias Inst) {
           return Inst.Find<Estudiantes_responsables_familias>();
       }
       [HttpPost]
       [AuthController]
       public object saveEstudiantes_responsables_familias(Estudiantes_responsables_familias inst) {
           return inst.Save();
       }
       [HttpPost]
       [AuthController]
       public object updateEstudiantes_responsables_familias(Estudiantes_responsables_familias inst) {
           return inst.Update();
       }
       //Secciones
       [HttpPost]
       [AuthController]
       public List<Secciones> getSecciones(Secciones Inst) {
           return Inst.Get<Secciones>();
       }
       [HttpPost]
       [AuthController]
       public Secciones findSecciones(Secciones Inst) {
           return Inst.Find<Secciones>();
       }
       [HttpPost]
       [AuthController]
       public object saveSecciones(Secciones inst) {
           return inst.Save();
       }
       [HttpPost]
       [AuthController]
       public object updateSecciones(Secciones inst) {
           return inst.Update();
       }
       //Tipo_notas
       [HttpPost]
       [AuthController]
       public List<Tipo_notas> getTipo_notas(Tipo_notas Inst) {
           return Inst.Get<Tipo_notas>();
       }
       [HttpPost]
       [AuthController]
       public Tipo_notas findTipo_notas(Tipo_notas Inst) {
           return Inst.Find<Tipo_notas>();
       }
       [HttpPost]
       [AuthController]
       public object saveTipo_notas(Tipo_notas inst) {
           return inst.Save();
       }
       [HttpPost]
       [AuthController]
       public object updateTipo_notas(Tipo_notas inst) {
           return inst.Update();
       }
   }
}
