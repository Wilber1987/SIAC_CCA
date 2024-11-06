using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using CAPA_DATOS.Services;

namespace CAPA_NEGOCIO.Services
{
    public class MailServices
    {
        static MailConfig config = new MailConfig()
        {
            HOST = "smtp.gmail.com",
            PASSWORD = "nbixjsqrnhkblxag",
            USERNAME = "alderhernandez@gmail.com"
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
                 config
             );
        }
    }
}
