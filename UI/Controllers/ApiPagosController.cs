using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Controllers;
using CAPA_DATOS;
using CAPA_DATOS.Security;
using CAPA_NEGOCIO.Gestion_Pagos.Model;
using CAPA_NEGOCIO.Gestion_Pagos.Model.PowerTranzTpv;
using CAPA_NEGOCIO.Gestion_Pagos.Operations;
using DataBaseModel;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

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
		public async Task<IActionResult> EjecutarPago([FromForm] TPV datosDePago)
		{
			// Obtener el objeto PagosRequest desde la base de datos o la sesión según el Id

			var response = await PagosOperation.EjecutarPago(datosDePago, HttpContext.Session.GetString("seassonKey"));
			//return RedirectToAction("PagoExitoso");
			if (response.status == 200) return RedirectToAction("AutorizatePayment");
			else return BadRequest(response.message);
		}
		[HttpPost]
		[Consumes("application/x-www-form-urlencoded")]
		public async Task<IActionResult> MerchantResponseURL([FromForm] IFormCollection formCollection)
		{
			//string stringR = Response.ToString();
			string? response = formCollection["response"];
			string? transactionIdentifier = formCollection["TransactionIdentifier"];
			string? spitoken = formCollection["spitoken"];
			// O usando TryGetValue para manejar posibles claves inexistentes
			if (formCollection.TryGetValue("response", out var responseValue))
			{
				response = responseValue;
			}
			var pagosResponse = await PagosOperation.AutorizarPago(HttpContext.Session.GetString("seassonKey"), JsonConvert.DeserializeObject<PT3DSResponse>(response));
			//return RedirectToAction("PagoExitoso");
			if (pagosResponse.status == 200) return Content((string)pagosResponse.body, "text/html");
			else return BadRequest(pagosResponse.message);
		}

		public class ResponseSPI
		{
			public string? Response { get; set; }
			public string? TransactionIdentifier { get; set; }
			public string? SpiToken { get; set; }
		}

		/*[HttpPost]
		public List<CuentasPorCobrarDocumentos> getCuentasPorCobrar()
		{
			// Obtener el los documentos que el padre tiene pendiente de pagar
			return CuentasPorCobrarOperation.GetCuentasPorCorar(HttpContext.Session.GetString("seassonKey"));
			
		}*/
	}
}