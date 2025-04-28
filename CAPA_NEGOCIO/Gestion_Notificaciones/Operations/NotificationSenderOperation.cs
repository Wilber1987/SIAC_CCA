using APPCORE;
using CAPA_DATOS;
using CAPA_NEGOCIO.Services;
using DataBaseModel;
using DatabaseModelNotificaciones;

namespace BusinessLogic.Notificaciones_Mensajeria.Gestion_Notificaciones.Operations
{
	public class NotificationSenderOperation
	{
		public static void SendNotifications()
		{
			try
			{				
				List<Notificaciones> notificaciones = new Notificaciones().Where<Notificaciones>(
					FilterData.Equal("Enviado", false)
				);
				if (notificaciones.Count > 0)
				{
					notificaciones.ForEach(notif => 
					{
					    MailServices.SendMail([notif.Email], "", notif.Titulo, notif.Mensaje, notif.Media);
						notif.Enviado = true;
						notif.Update();
					});					
				}
			}
			catch (Exception ex)
			{
				LoggerServices.AddMessageError("error enviando report", ex);
			}
		}

	}

}