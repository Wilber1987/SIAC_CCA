using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Controllers;
using CAPA_DATOS;
using CAPA_DATOS.Security;
using CAPA_NEGOCIO.Notificaciones;
using DataBaseModel;
using Microsoft.AspNetCore.Mvc;

namespace UI.Controllers
{
	[ApiController]
	[Route("api/[controller]/[action]")]
	public class ApiNotificacionesController : ControllerBase
	{
		//Conversaciones
		[HttpPost]
		[AuthController(Permissions.NOTIFICACIONES)]
		public List<Conversacion> getConversacion(Contacto Inst)
		{
			return Conversacion.GetConversaciones(HttpContext.Session.GetString("seassonKey"), Inst);
		}
		[HttpPost]
		[AuthController(Permissions.NOTIFICACIONES)]
		public Conversacion? findConversacion(Conversacion Inst)
		{
			return Inst.Find<Conversacion>();
		}
        [HttpPost]
        [AuthController(Permissions.NOTIFICACIONES)]
        public object? saveConversacion(Conversacion Inst)
        {
            return Inst.SaveConversacion(HttpContext.Session.GetString("seassonKey"));
        }       
		//Notificaciones
        [HttpPost]
		[AuthController(Permissions.NOTIFICACIONES)]
		public List<Notificaciones> getNotificaciones()
		{
			return new Notificaciones().Get(HttpContext.Session.GetString("seassonKey"));;
		}
        [HttpPost]
        [AuthController(Permissions.NOTIFICACIONES)]
        public List<Contacto> getContactos(Contacto Inst)
        {
            return new Conversacion().GetContactos(HttpContext.Session.GetString("seassonKey"), Inst);
        }
    }
}