using CAPA_DATOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace DataBaseModel {
   public class Escolaridades : EntityClass {
       [PrimaryKey(Identity = true)]
       public int? Id { get; set; }
       public string? Nombre { get; set; }
       [OneToMany(TableName = "Docentes", KeyColumn = "Id", ForeignKeyColumn = "Escolaridad_id")]
       public List<Docentes>? Docentes { get; set; }
   }
}
