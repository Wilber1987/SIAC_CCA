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
	public class ApiUpdateController : ControllerBase
	{
		[HttpGet]
		[AuthController(Permissions.UPDATE_FAMILY_DATA)]
		public UpdateData GetUpdateData()
		{
			return UpdateOperation.GetUpdateData(HttpContext.Session.GetString("seassonKey"));
		}
		[HttpPost]
		[AuthController(Permissions.UPDATE_FAMILY_DATA)]
		public ResponseService UpdateEstudiante(Estudiantes_Data_Update Inst)
		{
			return UpdateOperation.UpdateEstudiante(HttpContext.Session.GetString("seassonKey"), Inst);
		}
		[HttpPost]
		[AuthController(Permissions.UPDATE_FAMILY_DATA)]
		public ResponseService UpdatePariente(Parientes_Data_Update Inst)
		{
			return UpdateOperation.UpdateParientes(HttpContext.Session.GetString("seassonKey"), Inst);
		}
		[HttpPost]
		[AuthController(Permissions.ADMIN_ACCESS)]
		public ResponseService StartUpdateProcess(UpdateData updateData)
		{
			return new UpdateOperation().StartUpdateProcess(updateData);
		}
		
	}
}
