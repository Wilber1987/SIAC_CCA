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
		string from, string subject,
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
		
		public static async void SendMailAceptedContract(Parientes_Data_Update tutor, UpdateData updateData)
		{

			string templatePage = "<div><h1> Contrato aceptado y datos actualizados</h1><p>Hemos adjuntado los contratos y boletas, favor descarguelos</p></div>";
			List<ModelFiles> Attach_Files = [];
			ModelFiles boleta = new ModelFiles();
			ModelFiles contrato = new ModelFiles();
			if (updateData.Contrato != null && updateData.Contrato != "")
			{
				contrato = FileService.HtmlToPdfBase64(updateData.Contrato, "contrato_");
				Attach_Files.Add(contrato);
			}
			if (updateData.Boleta != null && updateData.Boleta != "")
			{
				boleta = FileService.HtmlToPdfBase64(updateData.Boleta, "boleta_");
				Attach_Files.Add(boleta);
			}
			foreach (var file in Attach_Files ?? new List<ModelFiles>())
			{
				ModelFiles? Response = (ModelFiles?)FileService.upload("Attach\\", file).body;
				file.Value = Response?.Value;
				file.Type = Response?.Type;
			}

			try
			{
				// guardo los archivos con su ruta
				new UpdatedData//todo meter en el try catch solo si se envia el correo
				{
					DataContract = new DataContract
					{
						Id_Tutor_responsable = tutor.Id,
						Tutor_responsable = tutor.Nombre_completo,
						Estudiantes = updateData.Estudiantes.Select(e => e.Id.GetValueOrDefault()).ToList(),
						Tutores = updateData.Parientes.Select(p => p.Id.GetValueOrDefault()).ToList(),
						Fecha = DateTime.Now
					},
					Documents_Contracts = [contrato],
					Documents_Boletas = [boleta]

				}.Save();
			}
			catch (Exception ex)
			{
				LoggerServices.AddMessageError($"error guardando los archivos", ex);
			}
			try
			{
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
