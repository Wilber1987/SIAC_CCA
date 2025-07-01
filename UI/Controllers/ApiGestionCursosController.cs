using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Controllers;
using APPCORE;
using APPCORE.Security;
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
        public IActionResult GetOwMaterias(Materias Inst)
        {
            try
            {
                return Ok(Materias.GetOwMaterias(HttpContext.Session.GetString("seassonKey"), Inst));
            }
            catch (Exception ex)
            {
                return StatusCode(403, ex.Message);
            }
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
        public IActionResult GetClaseMateriaConsolidado(Estudiante_Clases_View Inst)
        {
            try
            {
                return Ok(Inst.GetClaseMateriaConsolidado());
            }
            catch (Exception ex)
            {
                return StatusCode(403, ex.Message);
            }
        }
        [HttpPost]
        [AuthController(Permissions.GESTION_ESTUDIANTES_PROPIOS, Permissions.GESTION_ESTUDIANTES)]
        public IActionResult GetClaseMateriaCompleta(Estudiante_Clases_View Inst)
        {
            try
            {
                return Ok(Inst.GetClaseMateriaCompleta());
            }
            catch (Exception ex)
            {
                return StatusCode(403, ex.Message);
            }
        }

        [HttpPost]
        [AuthController(Permissions.GESTION_CLASES)]
        public IActionResult GetClasesCompleta(Estudiante_Clases_View Inst)
        {
            try
            {
                return Ok(Inst.GetClaseCompleta());
            }
            catch (Exception ex)
            {
                return StatusCode(403, ex.Message);
            }
        }
    }
}