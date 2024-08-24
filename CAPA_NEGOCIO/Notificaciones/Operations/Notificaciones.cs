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
		public List<Contacto> GetContactos(string? identity, Contacto contacto)
		{
			UserModel user = AuthNetCore.User(identity);

			return new Tbl_Profile()
				.Where<Tbl_Profile>(
					FilterData.Distinc("IdUser", user.UserId),
					FilterData.Limit(50),
					FilterData.Like("Nombre_Completo", contacto.Nombre_Completo)
				)
				.Select(u =>
				{
					int count = new Mensajes
					{
						Id_User = u.IdUser,
						Leido = false

					}.Count(
						FilterData.Like("Destinatarios", $"Id_User : {user.UserId}")//TODO REPARAR ESTE ERROR,FilterData.Equal("Leido", "0") 
					);
					return new Contacto
					{
						Id_User = u.IdUser,
						Nombre_Completo = u.GetNombreCompleto() ?? u.Nombres,
						Foto = u.Foto,
						Mensajes = count
					};
				}).ToList();
		}
	}
	public enum NotificacionType { MENSAJE, ALERTA, NOTICIA }
}