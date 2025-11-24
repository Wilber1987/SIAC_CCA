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
			return Convert.ToBoolean(new Transactional_Configuraciones().GetParam(ConfiguracionesThemeEnum.ENVIO_NOTIFICACIONES_ACTIVO,
				 "false", ConfiguracionesTypeEnum.BOOLEAN)?.Valor ?? "false");

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

		public static bool IsWMachine()
		{
			return Environment.MachineName == "WILBER";
		}
		public static bool IsUpdateProcessActive()
		{
			return Convert.ToBoolean(new Transactional_Configuraciones().GetParam(ConfiguracionesThemeEnum.IS_UPDATE_PROCESS_ACTIVE,
				 "true", ConfiguracionesTypeEnum.BOOLEAN)?.Valor ?? "true");
		}

		public static bool IsBoletinInactive()
		{
			return Convert.ToBoolean(new Transactional_Configuraciones().GetParam(ConfiguracionesThemeEnum.BOLETIN_INACTIVE,
				 "false", ConfiguracionesTypeEnum.BOOLEAN)?.Valor ?? "false");
		}

		public static string? BoletinInactiveMessage()
		{
			return new Transactional_Configuraciones().GetParam(ConfiguracionesThemeEnum.BOLETIN_INACTIVE_MESSAGE, "<h2>El boletín esta inactivo, por el momento</h2>",
			 ConfiguracionesTypeEnum.TEMPLATE)?.Valor;
		}

	}

}
