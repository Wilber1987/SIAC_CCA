using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TwilioWhatsAppDemo.Services;
using Microsoft.Extensions.Configuration;
using DataBaseModel;
using System;
using Microsoft.AspNetCore.Http;
using CAPA_NEGOCIO.Gestion_Mensajes.Operations;

namespace TwilioWhatsAppDemo.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class NotificacionesMensajesController : ControllerBase
    {
        private readonly WhatsAppService _whatsAppService;

       /* public NotificacionesMensajesController(WhatsAppService whatsAppService)
        {
            _whatsAppService = whatsAppService;
        }*/

        [HttpPost]
        public object? saveNotificationRequest(NotificationRequest request)
        {
            if (request is null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            return new NotificationOperation().SaveNotificacion(HttpContext.Session.GetString("seassonKey"), request);
        }

        /*[HttpPost("send")]
        public async Task<IActionResult> SendWhatsAppMessages([FromBody] WhatsAppRequest request)
        {
            if (request.PhoneNumbers == null || !request.PhoneNumbers.Any())
            {
                return BadRequest("Se requiere al menos un número de teléfono.");
            }

            await _whatsAppService.SendMessagesToMultipleRecipientsAsync(request.PhoneNumbers, request.Message, request.mediaUrl);

            return Ok("Mensajes enviados exitosamente.");
        }*/
    }

}
