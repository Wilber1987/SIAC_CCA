using APPCORE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace DataBaseModel {
   public class Docente_asignaturas : EntityClass {
       [PrimaryKey(Identity = true)]
       public int? Id { get; set; }
       public int? Docente_id { get; set; }
       public int? Asignatura_id { get; set; }
       public bool? Jefe { get; set; }
       public string? Observaciones { get; set; }
       public DateTime? Created_at { get; set; }
       public DateTime? Updated_at { get; set; }
       [ManyToOne(TableName = "Asignaturas", KeyColumn = "Id", ForeignKeyColumn = "Asignatura_id")]
       public Asignaturas? Asignaturas { get; set; }
       [ManyToOne(TableName = "Docentes", KeyColumn = "Id", ForeignKeyColumn = "Docente_id")]
       public Docentes? Docentes { get; set; }
   }
}
