using CAPA_DATOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace DataBaseModel {
   public class Niveles : EntityClass {
       [PrimaryKey(Identity = false)]
       public int? Id { get; set; }
       public string? Nombre { get; set; }
       public string? Nombre_corto { get; set; }
       public string? Nombre_grado { get; set; }
       public int? Numero_grados { get; set; }
       public string? Observaciones { get; set; }
       public bool? Habilitado { get; set; }
       public DateTime? Created_at { get; set; }
       public DateTime? Updated_at { get; set; }
       public int? Orden { get; set; }
       public int? Inicio_grado { get; set; }
       [OneToMany(TableName = "Asignaturas", KeyColumn = "Id", ForeignKeyColumn = "Nivel_id")]
       public List<Asignaturas>? Asignaturas { get; set; }
   }
}
