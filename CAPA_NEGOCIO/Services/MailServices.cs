using APPCORE;
using APPCORE.Services;
using CAPA_NEGOCIO.SystemConfig;
using CAPA_NEGOCIO.UpdateModule.Model;

namespace CAPA_NEGOCIO.Services
{
	public class MailServices
	{
		public static readonly MailConfig? Config = SystemConfigImpl.GetSMTPDefaultConfig();

		public static async void SendMail(List<string> toMails,
		string? from, string subject,
		string templatePage,
		 List<ModelFiles>? attachs = null)
		{
			try
			{
				var emailService = new EmailAccountService();
				var account = emailService.GetAvailableEmailAccount();
				await SMTPMailServices.SendMail(
					 "",//todo tomar el from
					 toMails,
					 subject,
					 templatePage,
					 attachs,
					 null,
					 new MailConfig
					 {
						 USERNAME = account.Email,
						 PASSWORD = account.Password,
						 HOST = account.Host
					 }
				 );
				emailService.IncrementEmailSentCount(account.Email);

			}
			catch (Exception ex)
			{
				LoggerServices.AddMessageError($"error enviando correos de invitacion", ex);
			}
		}

		public static async Task SendContractMail(Parientes_Data_Update tutor, string templatePage, List<ModelFiles> Attach_Files)
		{
			try
			{
				if (SystemConfigImpl.IsWMachine())
				{
					tutor.Email = "wilberj1987@gmail.com";
				}
				//tutor.Email = "alderhernandez@gmail.com";
				var emailService = new EmailAccountService();
				var account = emailService.GetAvailableEmailAccount();
				await SMTPMailServices.SendMail(
					"",
					[tutor.Email],
					"Contrato aceptado y datos familiares actualizados",
					templatePage,
					Attach_Files,
					null, SystemConfigImpl.GetSMTPDefaultConfig()
				 );
				emailService.IncrementEmailSentCount(account.Email);
			}
			catch (Exception ex)
			{
				LoggerServices.AddMessageError($"error enviando correos de contrato y boleta", ex);
			}
		}
	}
}
