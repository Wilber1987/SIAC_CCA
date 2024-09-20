using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CAPA_NEGOCIO.Security
{
    public class ProfileRequest
    {
        public string? Id { get; set; }
        public string? User_id { get; set; }
        public string? Correo { get; set; }
        public string? Telefono { get; set; }
        public string? Foto { get; set; }
        public string? Celular { get; set; }
        public string? Direccion { get; set; }
        public string? Estado { get; set; }
        public string? Correo_Anterior { get; set; }
        public string? Telefono_Anterior { get; set; }
        public string? Celular_Anterior { get; set; }
        public DateTime? Fecha_solicitud { get; set; }
        public DateTime? Fecha_respuesta { get; set; }
        public string? Observacion { get; set; }


    }

    public enum ProfileRequestsStatus
    {
        PENDIENTE,
        APROBADO,
        RECHAZADO
    }
}