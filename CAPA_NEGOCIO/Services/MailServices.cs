using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using CAPA_DATOS;
using CAPA_DATOS.Services;
using CAPA_NEGOCIO.UpdateModule.Model;

namespace CAPA_NEGOCIO.Services
{
	public class MailServices
	{
		static readonly MailConfig Config = new()
		{
			HOST = "smtp.gmail.com",
			PASSWORD = "czavspiafvhcttdg",
			USERNAME = "notificacionesportal@cca.edu.ni"
		};

		public static async void SendMailInvitation<T>(List<string> toMails, string from, string subject, string templatePage, T model)
		{

			await SMTPMailServices.SendMail(
				 "",//todo tomar el from
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
				new UpdatedData
				{
					DataContract = new DataContract
					{
						Id_Tutor_responsable = tutor.Id,
						Estudiantes = updateData.Estudiantes.Select(e => e.Id.GetValueOrDefault()).ToList(),
						Tutores = updateData.Parientes.Select(p => p.Id.GetValueOrDefault()).ToList()
					},
					Documents_Contracts = [contrato],
					Documents_Boletas = [boleta]

				}.Save();
			}
			catch (Exception ex)
			{
				LoggerServices.AddMessageError($"error guardando los archivos", ex);
			}

			await SMTPMailServices.SendMail(
				"",//todo tomar el from
				[tutor.Email],
				"Contrato aceptado y datos actualizados",
				templatePage,
				Attach_Files,
				null,
				Config
			 );
		}

	}
}
