using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Controllers;
using APPCORE;
using APPCORE.Security;
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
	public class ApiPagosManageController : ControllerBase
	{
		[HttpPost]
		[AuthController(Permissions.REPORTE_ACCESS)]
		public List<PagosRequest> GetPagosRealizados(PagosRequest Inst)
		{
			return new PagosOperation().GetManagePagos(Inst, HttpContext.Session.GetString("sessionKey"));
		}
		
		[HttpPost]
		[AuthController(Permissions.REPORTE_ACCESS)]
		public List<PagosRequest> GetPagosNoRealizados(PagosRequest Inst)
		{
			return new PagosOperation().GetManagePagosNoRealizados(Inst, HttpContext.Session.GetString("sessionKey"));
		}
	}
}