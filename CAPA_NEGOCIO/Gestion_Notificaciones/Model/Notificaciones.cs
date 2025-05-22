using API.Controllers;
using CAPA_DATOS;
using CAPA_DATOS.Security;
using CAPA_DATOS.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseModelNotificaciones
{

    public class Notificaciones : EntityClass
    {
        [PrimaryKey(Identity = true)]
        public int? Id { get; set; }
        public int? Id_User { get; set; }
        public string? Titulo { get; set; }
        public string? Mensaje { get; set; }
        public DateTime? Fecha { get; set; }
        [JsonProp]
        public List<ModelFiles>? Media { get; set; }
        public bool? Enviado { get; set; }
        public bool? Leido { get; set; }
        public string? Tipo { get; set; }
        public string? Telefono { get; set; }
        public string? Estado { get; set; }
        public string? Email { get; set; }

        public ResponseService MarcarComoLeido()
        {
            try
            {
                new Notificaciones { Id = Id, Leido = true }.Update();
                return new ResponseService
                {
                    status = 200,
                    message = "leido"
                };
            }
            catch (System.Exception ex)
            {
                LoggerServices.AddMessageError($"ERROR AL MARCAR NOTIFICACION COMO LEIDA #{Id}", ex);
                return new ResponseService
                {
                    status = 400,
                    message = "error: " + ex.Message
                };
            }

        }
    }
    public enum NotificacionesStates
    {
        ACTIVA, INACTIVA, VENCIDA
    }
}