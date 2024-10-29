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
		public List<Parientes>? GetParientes(Parientes inst)
		{
			return UpdateOperation.GetParientesToInvite(inst);
		}
		[HttpPost]
		[AuthController(Permissions.SEND_MESSAGE)]
		public List<Parientes_Data_Update>? GetParientesQueLoguearon(Parientes_Data_Update inst)
		{
			return UpdateOperation.GetParientesQueLoguearon(inst);
		}
		[HttpPost]
		[AuthController(Permissions.SEND_MESSAGE)]
		public List<Parientes_Data_Update>? GetParientesQueActulizaron(Parientes_Data_Update inst)
		{
			return UpdateOperation.GetParientesQueActulizaron(inst);
		}
		[HttpPost]
		[AuthController(Permissions.SEND_MESSAGE)]
		public List<Parientes_Data_Update>? GetParientesInvitados(Parientes_Data_Update inst)
		{
			return UpdateOperation.GetParientesInvitados(inst);
		}		
	}
}