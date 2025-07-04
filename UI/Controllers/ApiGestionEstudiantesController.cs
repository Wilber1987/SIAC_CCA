using API.Controllers;
using APPCORE;
using APPCORE.Security;
using DataBaseModel;
using Microsoft.AspNetCore.Mvc;
using Twilio.Http;

namespace UI.Controllers
{
	[ApiController]
	[Route("api/[controller]/[action]")]
	public class ApiGestionEstudiantesController : ControllerBase
	{
		[HttpPost]
		[AuthController(Permissions.GESTION_CLASES_ASIGNADAS, Permissions.GESTION_CLASES)]
		public List<Estudiantes> GetEstudianBySectionClass(Estudiante_clases Inst)
		{
			return Inst.GetEstudianBySectionClass();
		}
		//GESTION_ESTUDIANTES_PROPIOS

		[HttpPost]
		[AuthController(Permissions.GESTION_ESTUDIANTES_PROPIOS)]
		public List<Estudiantes> GetOwEstudiantes(Estudiantes Inst)
		{
			return Parientes.GetOwEstudiantes(HttpContext.Session.GetString("sessionKey"), Inst);
		}

		[HttpPost]
		[AuthController(Permissions.GESTION_ESTUDIANTES_PROPIOS)]
		public ResponseService UpdateOwEstudiantes(Estudiantes Inst)
		{
			return Inst.UpdateOwEstudiantes(HttpContext.Session.GetString("sessionKey"));
		}
		//CRUD ESTUDIANTES
		//Estudiantes
		[HttpPost]
		[AuthController(Permissions.GESTION_ESTUDIANTES)]
		public List<Estudiantes> getEstudiantes(Estudiantes Inst)
		{
			return Inst.Get<Estudiantes>();
		}
		[HttpPost]
		[AuthController(Permissions.GESTION_ESTUDIANTES_PROPIOS, Permissions.GESTION_ESTUDIANTES)]
		public IActionResult findEstudiantes(Estudiantes Inst)
		{
			try
			{
				return Ok(Inst.FindEstudiante(HttpContext.Session.GetString("sessionKey")));
			}
			catch (Exception ex)
			{
				return StatusCode(403, ex.Message);				
			}

		}
		[HttpPost]
		[AuthController(Permissions.GESTION_ESTUDIANTES_PROPIOS, Permissions.GESTION_ESTUDIANTES)]
		public Clase_Group? GetClaseEstudianteConsolidado(Estudiante_Clases_View Inst)
		{
			return Inst.GetClaseEstudianteConsolidado();
		}
		[HttpPost]
		[AuthController(Permissions.GESTION_ESTUDIANTES_PROPIOS, Permissions.GESTION_ESTUDIANTES)]
		public Clase_Group? GetClaseEstudianteCompleta(Estudiante_Clases_View Inst)
		{
			return Inst.GetClaseEstudianteCompleta();
		}
		[HttpPost]
		[AuthController(Permissions.GESTION_ESTUDIANTES_PROPIOS, Permissions.GESTION_ESTUDIANTES)]
		public object? saveEstudiantes(Estudiantes inst)
		{
			return inst.Save();
		}
		[HttpPost]
		[AuthController(Permissions.GESTION_ESTUDIANTES_PROPIOS, Permissions.GESTION_ESTUDIANTES)]
		public object? updateEstudiantes(Estudiantes inst)
		{
			return inst.Update();
		}
		/* //Estudiante_clases
		[HttpPost]
		[AuthController(Permissions.GESTION_ESTUDIANTES_PROPIOS, Permissions.GESTION_ESTUDIANTES)]
		public List<Estudiante_clases> getEstudiante_clases(Estudiante_clases Inst)
		{
			return Inst.Get<Estudiante_clases>();
		}
		[HttpPost]
		[AuthController(Permissions.GESTION_ESTUDIANTES_PROPIOS, Permissions.GESTION_ESTUDIANTES)]
		public Estudiante_clases? findEstudiante_clases(Estudiante_clases Inst)
		{
			return Inst.FindEstudiante();
		}
		[HttpPost]
		[AuthController(Permissions.GESTION_ESTUDIANTES_PROPIOS, Permissions.GESTION_ESTUDIANTES)]
		public object? saveEstudiante_clases(Estudiante_clases inst)
		{
			return inst.Save();
		}
		[HttpPost]
		[AuthController(Permissions.GESTION_ESTUDIANTES_PROPIOS, Permissions.GESTION_ESTUDIANTES)]
		public object? updateEstudiante_clases(Estudiante_clases inst)
		{
			return inst.Update();
		} */
	}
}