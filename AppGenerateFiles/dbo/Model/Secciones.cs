using CAPA_DATOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace DataBaseModel {
   public class Secciones : EntityClass {
       [PrimaryKey(Identity = false)]
       public int? Id { get; set; }
       public string? Nombre { get; set; }
       public int? Clase_id { get; set; }
       public int? Docente_id { get; set; }
       public string? Observaciones { get; set; }
       public DateTime? Created_at { get; set; }
       public DateTime? Updated_at { get; set; }
       [OneToMany(TableName = "Docente_materias", KeyColumn = "Id", ForeignKeyColumn = "Seccion_id")]
       public List<Docente_materias>? Docente_materias { get; set; }
       [OneToMany(TableName = "Estudiante_clases", KeyColumn = "Id", ForeignKeyColumn = "Seccion_id")]
       public List<Estudiante_clases>? Estudiante_clases { get; set; }
       [OneToMany(TableName = "Evaluaciones", KeyColumn = "Id", ForeignKeyColumn = "Seccion_id")]
       public List<Evaluaciones>? Evaluaciones { get; set; }
   }
}
