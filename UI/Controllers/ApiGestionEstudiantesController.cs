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
	public class ApiGestionEstudiantesController : ControllerBase
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
		public Estudiantes? findEstudiantes(Estudiantes Inst)
		{
			return Inst.FindEstudiante(HttpContext.Session.GetString("seassonKey"));
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