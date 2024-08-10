using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Controllers;
using CAPA_DATOS.Security;
using DataBaseModel;
using Microsoft.AspNetCore.Mvc;

namespace UI.Controllers
{
	[ApiController]
	[Route("api/[controller]/[action]")]
	public class GestionEstudiantesController : ControllerBase
	{
		[HttpPost]
		[AuthController(Permissions.ADMIN_ACCESS)]
		public List<Estudiantes> GetEstudiantes(Estudiantes Inst)
		{
			return Inst.Get<Estudiantes>();
		}
		
		//GESTION_ESTUDIANTES_PROPIOS

		[HttpPost]
		[AuthController(Permissions.GESTION_ESTUDIANTES_PROPIOS)]
		public List<Estudiantes> GetOwEstudiantes(Estudiantes Inst)
		{
			return Parientes.GetOwEstudiantes(HttpContext.Session.GetString("seassonKey"), Inst);
		}
		
		[HttpPost]
		[AuthController(Permissions.GESTION_ESTUDIANTES_PROPIOS)]
		public List<Estudiantes> UpdateOwEstudiantes(Estudiantes Inst)
		{
			return Inst.UpdateOwEstudiantes(HttpContext.Session.GetString("seassonKey"));
		}
	}
}