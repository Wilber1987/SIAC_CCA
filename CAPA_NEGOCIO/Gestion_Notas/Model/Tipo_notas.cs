using CAPA_DATOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace DataBaseModel {
   public class Tipo_notas : EntityClass {
       [PrimaryKey(Identity = true)]
       public int? Id { get; set; }
       public string? Nombre { get; set; }
       public string? Nombre_corto { get; set; }
       public int? Periodo_lectivo_id { get; set; }
       public int? Consolidado_id { get; set; }
       public int? Numero_consolidados { get; set; }
       public string? Observaciones { get; set; }
       public DateTime? Created_at { get; set; }
       public DateTime? Updated_at { get; set; }
       public int? Orden { get; set; }
     
   }
}
