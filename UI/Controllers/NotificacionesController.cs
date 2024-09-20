using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Controllers;
using CAPA_DATOS;
using CAPA_DATOS.Security;
using DataBaseModel;
using DatabaseModelNotificaciones;
using Microsoft.AspNetCore.Mvc;

namespace UI.Controllers
{
	[ApiController]
	[Route("api/[controller]/[action]")]
	public class ApiNotificacionesController : ControllerBase
	{
		[HttpPost]
		[AuthController(Permissions.SEND_MESSAGE)]
		public List<Notificaciones> getNotificaciones()
		{
			return new Notificaciones().Get(HttpContext.Session.GetString("seassonKey"));
		}
        

		
		#region  CHAT
		//Conversaciones
		[HttpPost]
        [AuthController(Permissions.SEND_MESSAGE)]
        public List<Contacto> getContactos(Contacto Inst)
        {
            return new Conversacion().GetContactos(HttpContext.Session.GetString("seassonKey"), Inst);
        }
		[HttpPost]
		[AuthController(Permissions.NOTIFICACIONES)]
		public List<Conversacion> getConversacion(Contacto Inst)
		{
			throw new NotImplementedException();
			return Conversacion.GetConversaciones(HttpContext.Session.GetString("seassonKey"), Inst);
		}
		[HttpPost]
		[AuthController(Permissions.NOTIFICACIONES)]
		public Conversacion? findConversacion(Conversacion Inst)
		{
			throw new NotImplementedException();
			return Inst.Find<Conversacion>();
		}
        [HttpPost]
        [AuthController(Permissions.NOTIFICACIONES)]
        public object? saveConversacion(Conversacion Inst)
        {
            throw new NotImplementedException();
			return Inst.SaveConversacion(HttpContext.Session.GetString("seassonKey"));
        }       
		#endregion
		//Notificaciones
        
    }
}