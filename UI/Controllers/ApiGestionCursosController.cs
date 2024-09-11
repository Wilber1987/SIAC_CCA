using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Controllers;
using CAPA_DATOS;
using CAPA_DATOS.Security;
using DataBaseModel;
using Microsoft.AspNetCore.Mvc;

namespace UI.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class ApiGestionCursosController : ControllerBase
    {
        [HttpPost]
        [AuthController(Permissions.GESTION_CLASES_ASIGNADAS)]
        public List<Materias> GetOwMaterias(Materias Inst)
        {
            return Materias.GetOwMaterias(HttpContext.Session.GetString("seassonKey"), Inst);
        }
        //Materias
        [HttpPost]
        [AuthController(Permissions.GESTION_CLASES)]
        public List<Materias> getMaterias(Materias Inst)
        {
            return Inst.Where<Materias>(FilterData.Limit(30));
        }
        [HttpPost]
        [AuthController(Permissions.GESTION_CLASES_ASIGNADAS, Permissions.GESTION_CLASES)]
        public Materias? findMaterias(Materias Inst)
        {
            return Inst.Find<Materias>();
        }

        [HttpPost]
        [AuthController(Permissions.GESTION_CLASES_ASIGNADAS, Permissions.GESTION_CLASES)]
        public Docente_materias? findDocente_materias(Docente_materias Inst)
        {
            return Inst.Find<Docente_materias>();
        }
        [HttpPost]
		[AuthController(Permissions.GESTION_ESTUDIANTES_PROPIOS, Permissions.GESTION_ESTUDIANTES)]
		public Clase_Group? GetClaseMateriaConsolidado(Estudiante_Clases_View Inst)
		{
			return Inst.GetClaseMateriaConsolidado();
		}
		[HttpPost]
		[AuthController(Permissions.GESTION_ESTUDIANTES_PROPIOS, Permissions.GESTION_ESTUDIANTES)]
		public Clase_Group? GetClaseMateriaCompleta(Estudiante_Clases_View Inst)
		{
			return Inst.GetClaseMateriaCompleta();
		}

        [HttpPost]
		[AuthController(Permissions.GESTION_CLASES)]
		public List<Clase_Group>? GetClasesCompleta(Estudiante_Clases_View Inst)
		{
			return Inst.GetClaseCompleta();
		}
    }
}