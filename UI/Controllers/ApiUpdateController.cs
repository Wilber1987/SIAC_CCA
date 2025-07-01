using API.Controllers;
using APPCORE;
using APPCORE.Security;
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
		[HttpPost]
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
		[AuthController(Permissions.SEND_MESSAGE)]
		public ResponseService SaveUpdateData(UpdateData updateData)
		{
			return new UpdateOperation().StartUpdateProcess(updateData);
		}
		[HttpPost]
		[AuthController(Permissions.SEND_MESSAGE)]
		public ResponseService SaveUpdateDataRequest(UpdateDataRequest Inst)
		{
			return new UpdateOperation().Save(HttpContext.Session.GetString("seassonKey"), Inst );
		}
		[HttpPost]
		[AuthController(Permissions.SEND_MESSAGE)]
		public List<ViewParientesUpdate>? GetParientes(Parientes inst)
		{
			return UpdateOperation.GetParientesToInvite(inst);
		}
		[HttpPost]
		[AuthController(Permissions.SEND_MESSAGE)]
		public List<ViewParientesUpdate>? GetParientesQueLoguearon(Parientes_Data_Update inst)
		{
			return UpdateOperation.GetParientesQueLoguearon(inst);
		}
		[HttpPost]
		[AuthController(Permissions.SEND_MESSAGE)]
		public List<ViewParientesUpdate>? GetParientesQueActulizaron(Parientes_Data_Update inst)
		{
			return UpdateOperation.GetParientesQueActulizaron(inst);
		}
		
		[HttpPost]
		[AuthController(Permissions.SEND_MESSAGE)]
		public List<ViewParientesUpdate>? GetParientesQueNoLoguearon(Parientes_Data_Update inst)
		{
			return UpdateOperation.GetParientesQueNoLoguearon(inst);
		}
		[HttpPost]
		[AuthController(Permissions.SEND_MESSAGE)]
		public List<ViewParientesUpdate>? GetParientesInvitados(ViewParientesUpdate inst)
		{
			return UpdateOperation.GetParientesInvitados(inst);
		}		
		[HttpPost]
		[AuthController(Permissions.SEND_MESSAGE)]
		public UpdateData GetUpdatedData(Parientes_Data_Update inst)
		{
			return UpdateOperation.GetUpdateDataById(inst);
		}	
	}
}
