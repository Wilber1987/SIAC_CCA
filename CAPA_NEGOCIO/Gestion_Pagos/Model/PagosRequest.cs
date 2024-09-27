using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CAPA_DATOS;

namespace CAPA_NEGOCIO.Gestion_Pagos.Model
{
    public class PagosRequest: EntityClass
    {
       
        [PrimaryKey(Identity = true)]
        public int? Id { get; set; }
        public string? Referencia { get; set; } 
        public int? Responsable_Id { get; set; } 
        public DateTime? Fecha { get; set; } 
        public string? Creador { get; set; } 
        public List<Pago>? Pagos { get; set; }
         public  PagosState? Estado { get; internal set; }
    }

    public class TPV
    {
        public string? CardNumber { get; set; }
        public string? Cvv { get; set; }
        public int? ExpMonth { get; set; }
        public int? ExpYear { get; set; }
    }
}