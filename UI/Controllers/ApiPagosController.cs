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
		public Object GetTbl_Pagos(Tbl_Pago Inst)
		{
			return new PagosOperation().GetPagosAllPagos(Inst, HttpContext.Session.GetString("seassonKey"));
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
		
		[HttpGet("{Id_Pago_Request}")]
		public IActionResult GetFactura(int Id_Pago_Request)
		{
			try
			{
				// Convertir HTML a PDF utilizando wkhtmltopdf
				byte[] pdfBytes = ApiDocumentsDataController.ConvertHtmlToPdf(PagosTemplate.GenerarFacturaHtml(new PagosRequest{Id_Pago_Request = Id_Pago_Request}.Find<PagosRequest>(), true), "A4");

				// Devolver el archivo PDF como respuesta
				return File(pdfBytes, "application/pdf", "generated.pdf");
			}
			catch (Exception ex)
			{
				return StatusCode(500, $"Error generating PDF: {ex.Message}");
			}
			// Aquí puedes usar el parámetro facturaId para realizar alguna operación
			
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