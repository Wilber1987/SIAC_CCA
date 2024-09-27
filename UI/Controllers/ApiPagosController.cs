using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Controllers;
using CAPA_DATOS;
using CAPA_DATOS.Security;
using CAPA_NEGOCIO.Gestion_Pagos.Model;
using CAPA_NEGOCIO.Gestion_Pagos.Operations;
using Microsoft.AspNetCore.Mvc;

namespace UI.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class ApiPagosController : ControllerBase
    {
        [HttpPost]
        [AuthController(Permissions.GESTION_ESTUDIANTES_PROPIOS)]
        public List<Pago> GetPagos(Pago Inst)
        {
            return PagosOperation.GetPagos(Inst, HttpContext.Session.GetString("seassonKey"));
        }
        [HttpPost]
        [AuthController(Permissions.GESTION_ESTUDIANTES_PROPIOS)]
        public ResponseService SavePagosRequest(PagosRequest Inst)
        {
            return PagosOperation.SetPagosRequest(Inst, HttpContext.Session.GetString("seassonKey"));
        }
        [HttpPost]
        public IActionResult EjecutarPago([FromForm] TPV datosDePago)
        {
            // Obtener el objeto PagosRequest desde la base de datos o la sesión según el Id
            var response = PagosOperation.EjecutarPago(datosDePago, HttpContext.Session.GetString("seassonKey"));
            if (response.status == 200) return RedirectToAction("PagoExitoso");
            else return BadRequest(response.message);
        }
    }
}