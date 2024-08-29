using CAPA_DATOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace DataBaseModel {
   public class Tbl_gen_relacionfamilar : EntityClass {       
       public int? idrelacionfamiliar { get; set; }
       public string? idtrelacionfamiliar { get; set; }
       public string? texto { get; set; }
   }
}