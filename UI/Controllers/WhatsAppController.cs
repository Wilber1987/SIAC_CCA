using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TwilioWhatsAppDemo.Services;
using Microsoft.Extensions.Configuration;

namespace TwilioWhatsAppDemo.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WhatsAppController : ControllerBase
    {
        private readonly WhatsAppService _whatsAppService;

        public WhatsAppController(WhatsAppService whatsAppService)
        {
            _whatsAppService = whatsAppService;
        }

        [HttpPost("send")]
        public async Task<IActionResult> SendWhatsAppMessages([FromBody] WhatsAppRequest request)
        {
            if (request.PhoneNumbers == null || !request.PhoneNumbers.Any())
            {
                return BadRequest("Se requiere al menos un número de teléfono.");
            }

            await _whatsAppService.SendMessagesToMultipleRecipientsAsync(request.PhoneNumbers, request.Message, request.mediaUrl);

            return Ok("Mensajes enviados exitosamente.");
        }
    }

    public class WhatsAppRequest
    {
        public List<string> PhoneNumbers { get; set; }
        public string Message { get; set; }

        public string mediaUrl { get; set; }
    }
}
