using CAPA_DATOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace DataBaseModel {
   public class Materias : EntityClass {
       [PrimaryKey(Identity = false)]
       public int? Id { get; set; }
       public int? Clase_id { get; set; }
       public int? Asignatura_id { get; set; }
       public string? Observaciones { get; set; }
       public string? Config { get; set; }
       public DateTime? Created_at { get; set; }
       public DateTime? Updated_at { get; set; }
       public int? Lock_version { get; set; }
       [ManyToOne(TableName = "Asignaturas", KeyColumn = "Id", ForeignKeyColumn = "Asignatura_id")]
       public Asignaturas? Asignaturas { get; set; }
       [ManyToOne(TableName = "Clases", KeyColumn = "Id", ForeignKeyColumn = "Clase_id")]
       public Clases? Clases { get; set; }
       [OneToMany(TableName = "Docente_materias", KeyColumn = "Id", ForeignKeyColumn = "Materia_id")]
       public List<Docente_materias>? Docente_materias { get; set; }
       [OneToMany(TableName = "Evaluaciones", KeyColumn = "Id", ForeignKeyColumn = "Materia_id")]
       public List<Evaluaciones>? Evaluaciones { get; set; }
   }
}
