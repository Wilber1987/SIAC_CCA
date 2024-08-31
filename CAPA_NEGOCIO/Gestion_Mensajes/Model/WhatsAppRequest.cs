using API.Controllers;
using CAPA_DATOS;
using CAPA_DATOS.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace DataBaseModel
{
    class whatsappRequest: EntityClass 
    {
        public List<string>? PhoneNumbers { get; set; } = new List<string>(); 
        public string? message { get; set; }
        public string? MediaUrl { get; set; }
        public List<Responsables>? Responsables { get; set; }
        public List<Responsables>? Responsables { get; set; }
        public List<Responsables>? Responsables { get; set; }

    }
}