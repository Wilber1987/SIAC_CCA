using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using CAPA_DATOS.Services;
using CAPA_NEGOCIO.UpdateModule.Model;

namespace CAPA_NEGOCIO.Services
{
	public class MailServices
	{
		static readonly MailConfig Config = new()
		{
			HOST = "smtp.gmail.com",
			PASSWORD = "nbixjsqrnhkblxag",
			USERNAME = "alderhernandez@gmail.com"
		};

		public static async void SendMailInvitation<T>(List<string> toMails, string from, string subject, string templatePage, T model)
		{
			
			await SMTPMailServices.SendMail(
				 "cca@noreply.com",//todo tomar el from
				 toMails,
				 subject,
				 templatePage,
				 null,
				 null,
				 Config
			 );
		}
		public static async void SendMailAceptedContract(Parientes_Data_Update tutor, UpdateData updateData)
		{
			
			string templatePage = "<div><h1> Contrato aceptado y datos actualizados</h1><p>Contrato aceptado y datos actualizados</p></div>";
			await SMTPMailServices.SendMail(
				 "cca@noreply.com",//todo tomar el from
				 [tutor.Email],
				 "Contrato aceptado y datos actualizados",
				 templatePage,
				 [FileService.HtmlToPdfBase64(updateData.Contrato,"contrato.pdf"),
				  FileService.HtmlToPdfBase64(updateData.Boleta,"boletas.pdf")],
				 null,
				 Config
			 );
		}
        
    }
}
