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
				var periodoLectivo = Periodo_lectivos.PeriodoActivo();
				List<Notificaciones> notificaciones = new Notificaciones().Where<Notificaciones>(
					FilterData.Equal("Enviado", false)
				);
				var primerasNotificaciones = notificaciones.Take(40).ToList();

				if (primerasNotificaciones.Count > 0)
				{
					primerasNotificaciones.ForEach(notif =>
					{
						var parientesFamilia = new Parientes().Where<Parientes>(
							FilterData.Equal("user_id", notif.Id_User)
						);
						var tienePeriodoLectivo24 = parientesFamilia.Any(p =>
							p.Estudiantes_responsables_familia.Any(erf =>
								erf.Estudiantes.Any(est =>
									est.Estudiante_clases.Any(ec =>
										ec.Periodo_lectivo_id == periodoLectivo?.Id
									)
								)
							)
						);

						if (tienePeriodoLectivo24)
						{
							MailServices.SendMail([notif.Email], "", notif.Titulo, notif.Mensaje, notif.Media);
							notif.Enviado = true;
							notif.Update();
						}
						else
						{
							notif.Enviado = true;
							notif.Update();
						}
					});
				}
			}
			catch (Exception ex)
			{
				LoggerServices.AddMessageError("error enviando notificacion", ex);
			}
		}

		public static void SendCredentialsToParents()
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