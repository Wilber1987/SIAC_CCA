using Microsoft.Extensions.Configuration;
using DataBaseModel;
using APPCORE.Services;
namespace CAPA_NEGOCIO.SystemConfig
{
	public class SystemConfigImpl : APPCORE.SystemConfig.SystemConfig
	{
		public SystemConfigImpl()
		{
			configuraciones = new Transactional_Configuraciones().Get<Transactional_Configuraciones>();			
		}
		public static new MailConfig? GetSMTPDefaultConfig()
		{
			var emailService = new EmailAccountService();
			var account = emailService.GetAvailableEmailAccount();
			return new MailConfig
			{
				PASSWORD = account.Password,
				HOST = account.Host,
				USERNAME = account.Email
			};
		}
		
		
		public new List<Transactional_Configuraciones> configuraciones = [];

		public static bool IsAutomaticCaseActive()
		{
			//TODO IMPLEMENTAR ESTE METODO
			return true;
		}
		public static bool IsNotificationsActive()
		{
			//TODO IMPLEMENTAR ESTE METODO
			return true;
		}
		public static bool IsMessagesActive()
		{
			//TODO IMPLEMENTAR ESTE METODO
			return false;
		}
		public static bool IsWhatsAppActive()
		{
			//TODO IMPLEMENTAR ESTE METODO
			return true;
		}
		public static bool IsQuestionnairesActive()
		{
			//TODO IMPLEMENTAR ESTE METODO
			return false;
		}
	}

}
