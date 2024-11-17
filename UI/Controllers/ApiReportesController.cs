using API.Controllers;
using CAPA_DATOS;
using CAPA_DATOS.Security;
using CAPA_NEGOCIO.UpdateModule.Model;
using CAPA_NEGOCIO.UpdateModule.Operations;
using DataBaseModel;
using Microsoft.AspNetCore.Mvc;

namespace UI.Controllers
{
	[ApiController]
	[Route("api/[controller]/[action]")]
	public class ApiReportesController : ControllerBase
	{
		[HttpPost]
		[AuthController(Permissions.UPDATE_FAMILY_DATA)]
		public List<Estudiantes_Data_Update> GetEstudiantesConRecorridos(Estudiantes_Data_Update inst)
		{
			return inst.GetEstudiantesConRecorridos();
		}
		
	}
}
