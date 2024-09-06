using CAPA_DATOS;
using System;
using Microsoft.Extensions.Configuration;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;
using DataBaseModel;

namespace TwilioWhatsAppDemo.Services
{
    public class WhatsAppService
    {
        private readonly string _accountSid;
        private readonly string _authToken;
        private readonly string _fromNumber;

        public WhatsAppService(IConfiguration configuration)
        {

            var twilio = new TwillioConfig();
            //todo tomar del metodo: var pinky = new TwillioConfig().getTwillioConfig(); piiny.TWILLIO_ACCOUNT 
            _accountSid = twilio.TWILLIO_ACCOUNT;// configuration["Twilio:Accou-ntSid"];//todo quitar esto del archivo de configuration
            _authToken = twilio.TWILLIO_TOKEN;// configuration["Twilio:AuthToken"];//todo quitar esto del archivo de configuration
            _fromNumber = twilio.TWILLIO_NUMBER;// configuration["Twilio:FromWhatsAppNumber"];//todo quitar esto del archivo de configuration

            // Inicializa el cliente de Twilio con las credenciales
            TwilioClient.Init(_accountSid, _authToken);
        }

        public async Task SendMessageAsync(string to, string message, string mediaUrl)
        {
            var messageResource = await MessageResource.CreateAsync(
                to: new PhoneNumber($"whatsapp:{to}"),
                from: new PhoneNumber(_fromNumber),
                body: message,
                mediaUrl: new List<Uri> { new Uri(mediaUrl) }
            );
            Console.WriteLine($"Mensaje enviado a {to} con SID: {messageResource.Sid} con mediaUrl: {mediaUrl}");
        }

        public async Task SendMessagesToMultipleRecipientsAsync(List<string> phoneNumbers, string message, string mediaUrl)
        {
            var tasks = phoneNumbers.Select(number => SendMessageAsync(number, message, mediaUrl));
            await Task.WhenAll(tasks);
        }
    }
}
