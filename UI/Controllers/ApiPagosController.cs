using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Controllers;
using CAPA_DATOS;
using CAPA_DATOS.Security;
using CAPA_NEGOCIO.Gestion_Pagos.Model;
using CAPA_NEGOCIO.Gestion_Pagos.Operations;
using DataBaseModel;
using Microsoft.AspNetCore.Mvc;

namespace UI.Controllers
{
	[ApiController]
	[Route("api/[controller]/[action]")]
	public class ApiPagosController : ControllerBase
	{
		[HttpPost]
		[AuthController(Permissions.GESTION_ESTUDIANTES_PROPIOS)]
		public List<Tbl_Pago> GetTbl_Pago(Tbl_Pago Inst)
		{
			return new PagosOperation().GetPagos(Inst, HttpContext.Session.GetString("seassonKey"));
		}
		[HttpPost]
		[AuthController(Permissions.GESTION_ESTUDIANTES_PROPIOS)]
		public ResponseService SavePagosRequest(PagosRequest Inst)
		{
			return PagosOperation.SetPagosRequest(Inst, HttpContext.Session.GetString("seassonKey"));
		}
		[HttpPost]
        [AuthController(Permissions.GESTION_ESTUDIANTES_PROPIOS)]
        public List<PagosRequest> GetPagosRequest(PagosRequest Inst)
        {
            return PagosRequest.GetPagosRealizados(Inst, HttpContext.Session.GetString("seassonKey"));
        }
		[HttpPost]
		public IActionResult EjecutarPago([FromForm] TPV datosDePago)
		{
			// Obtener el objeto PagosRequest desde la base de datos o la sesión según el Id
			var response = PagosOperation.EjecutarPago(datosDePago, HttpContext.Session.GetString("seassonKey"));
			if (response.status == 200) return RedirectToAction("PagoExitoso");
			else return BadRequest(response.message);
		}

		/*[HttpPost]
		public List<CuentasPorCobrarDocumentos> getCuentasPorCobrar()
		{
			// Obtener el los documentos que el padre tiene pendiente de pagar
			return CuentasPorCobrarOperation.GetCuentasPorCorar(HttpContext.Session.GetString("seassonKey"));
			
		}*/
	}
}