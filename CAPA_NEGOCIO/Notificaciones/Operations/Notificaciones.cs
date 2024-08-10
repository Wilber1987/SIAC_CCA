using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Controllers;
using CAPA_DATOS;
using CAPA_DATOS.Security;
using DataBaseModel;

namespace CAPA_NEGOCIO.Notificaciones
{
	public class Notificaciones
	{
		public DateTime? Date { get; set; }
		public string? Content { get; set; }
		public NotificacionType? Type { get; set; }
		public List<Notificaciones> Get(string? identity)
		{
			UserModel user = AuthNetCore.User(identity);
			var conversaciones = Conversacion.GetConversaciones(identity);
			var mensajesNoLeidos = conversaciones.SelectMany(x => x?.Mensajes ?? [])
			.ToList().Where(m => m.IsMensajeNoLeido(user)).ToList();
			List<Notificaciones> notificaciones = [];
			mensajesNoLeidos.ForEach(m => notificaciones.Add(new Notificaciones
			{
				Type = NotificacionType.MENSAJE,
				Date = m.Created_at,
				Content = $"{m.Remitente}: {m.Asunto}"
			}));
			return notificaciones;
		}
        public List<Contacto> GetContactos(string? identity)
        {
            UserModel user = AuthNetCore.User(identity);
            
            return new Security_Users()
				.Where<Security_Users>(FilterData.Distinc("Id_User", user.UserId))
				.Select(u => new Contacto
				{
					Id_User = u.Id_User,
					Nombre_Completo = u.Get_Profile()?.GetNombreCompleto() ?? u.Nombres,
					Foto = u.Get_Profile()?.Foto
				}).ToList();
        }
    }
	public enum NotificacionType { MENSAJE, ALERTA, NOTICIA }
}