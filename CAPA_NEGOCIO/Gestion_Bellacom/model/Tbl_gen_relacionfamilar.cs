using CAPA_DATOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace DataBaseModel {
   public class Tbl_gen_relacionfamilar : EntityClass {
       [PrimaryKey(Identity = false)]
       public int? Id { get; set; }
       public string? Texto { get; set; }
   }
}