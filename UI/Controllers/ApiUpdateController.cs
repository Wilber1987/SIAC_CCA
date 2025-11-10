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
		[AuthController(Permissions.UPDATE_FAMILY_DATA, Permissions.GESTION_ESTUDIANTES_PROPIOS)]
		public UpdateData GetUpdateData()
		{
			return UpdateOperation.GetOwUpdateData(HttpContext.Session.GetString("sessionKey"));
		}
		[HttpPost]
		[AuthController(Permissions.UPDATE_FAMILY_DATA, Permissions.GESTION_ESTUDIANTES_PROPIOS)]
		public ResponseService UpdateEstudiante(Estudiantes_Data_Update Inst)
		{
			return UpdateOperation.UpdateEstudiante(HttpContext.Session.GetString("sessionKey"), Inst);
		}
		[HttpPost]
		[AuthController(Permissions.UPDATE_FAMILY_DATA, Permissions.GESTION_ESTUDIANTES_PROPIOS)]
		public ResponseService UpdatePariente(Parientes_Data_Update Inst)
		{
			return UpdateOperation.UpdateParientes(HttpContext.Session.GetString("sessionKey"), Inst);
		}
		[HttpPost]
		[AuthController(Permissions.SEND_MESSAGE)]
		public ResponseService SaveUpdateData(UpdateData updateData)
		{
			return new UpdateOperation().StartUpdateProcess(updateData);
		}
		[HttpPost]
		[AuthController(Permissions.UPDATE_FAMILY_DATA, Permissions.GESTION_ESTUDIANTES_PROPIOS)]
		public async Task<ResponseService> SaveUpdateDataRequest(UpdateDataRequest Inst)
		{
			return await new UpdateOperation().Save(HttpContext.Session.GetString("sessionKey"), Inst);
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
		public object? GetParientesActulizacionData(Parientes_Data_Update inst)
		{
			return UpdateOperation.GetParientesActulizacionData(inst);
		}
		[HttpPost]
		[AuthController(Permissions.SEND_MESSAGE)]
		public List<Parientes_Data_Update>? GetParientesActualizados(Parientes_Data_Update inst)
		{
			return UpdateOperation.GetParientesActualizados(inst);
		}
		[HttpPost]
		[AuthController(Permissions.SEND_MESSAGE)]
		public List<Parientes_Data_Update>? GetParientesQueNoActulizaron(Parientes_Data_Update inst)
		{
			return UpdateOperation.GetParientesQueNoActulizaron(inst);
		}
		[HttpPost]
		[AuthController(Permissions.SEND_MESSAGE)]
		public async Task<ResponseService> ReenviarBoleta(Parientes_Data_Update inst)
		{
			return await UpdateOperation.ReenviarBoleta(inst);
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
		public UpdateData? GetUpdatedData(Parientes_Data_Update inst)
		{
			return UpdateOperation.GetUpdateDataById(inst);
		}

		[HttpPost]
		[AuthController(Permissions.UPDATE_FAMILY_DATA, Permissions.GESTION_ESTUDIANTES_PROPIOS)]
		public List<Estudiantes_Data_Update> GetEstudiantes_Data_Update(Estudiantes_Data_Update inst)
		{
			return UpdateOperation.GetEstudiantesActualizados(HttpContext.Session.GetString("sessionKey"), inst);
		}
		[HttpPost]
		[AuthController(Permissions.UPDATE_FAMILY_DATA, Permissions.GESTION_ESTUDIANTES_PROPIOS)]
		public ResponseService UpdateEstudiantes_Data_Update(Estudiantes_Data_Update Inst)
		{
			return UpdateOperation.UpdateEstudianteActualizados(HttpContext.Session.GetString("sessionKey"), Inst);
		}
	}
}
