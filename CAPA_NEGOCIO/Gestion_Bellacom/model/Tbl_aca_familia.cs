using CAPA_DATOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace DataBaseModel {
   public class Tbl_aca_familia : EntityClass {
       [PrimaryKey(Identity = false)]
       public int? Idfamilia { get; set; }
       public string? Idtfamilia { get; set; }
       public string? Texto { get; set; }
       public bool? Estatus { get; set; }
       public Double? Saldomd { get; set; }
       public DateTime? Fechaultimanotificacion { get; set; }
       public DateTime? Fechaactualizacion { get; set; }
       public bool? Actualizado { get; set; }
       public string? Usuario { get; set; }
       public bool? Aceptacion { get; set; }
       public int? Periodoaceptacion { get; set; }
   }
}
