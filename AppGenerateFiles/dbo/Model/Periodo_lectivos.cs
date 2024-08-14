using CAPA_DATOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace DataBaseModel {
   public class Periodo_lectivos : EntityClass {
       [PrimaryKey(Identity = false)]
       public int? Id { get; set; }
       public string? Nombre { get; set; }
       public string? Nombre_corto { get; set; }
       public DateTime? Inicio { get; set; }
       public DateTime? Fin { get; set; }
       public string? Observaciones { get; set; }
       public string? Config { get; set; }
       public DateTime? Created_at { get; set; }
       public DateTime? Updated_at { get; set; }
       public bool? Abierto { get; set; }
       public bool? Oculto { get; set; }
       [OneToMany(TableName = "Estudiante_clases", KeyColumn = "Id", ForeignKeyColumn = "Periodo_lectivo_id")]
       public List<Estudiante_clases>? Estudiante_clases { get; set; }
   }
}
