using CAPA_DATOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace DataBaseModel {
   public class Docente_materias : EntityClass {
       [PrimaryKey(Identity = false)]
       public int? Id { get; set; }
       public int? Materia_id { get; set; }
       public int? Seccion_id { get; set; }
       public int? Docente_id { get; set; }
       public DateTime? Created_at { get; set; }
       public DateTime? Updated_at { get; set; }
       [ManyToOne(TableName = "Materias", KeyColumn = "Id", ForeignKeyColumn = "Materia_id")]
       public Materias? Materias { get; set; }
       [ManyToOne(TableName = "Secciones", KeyColumn = "Id", ForeignKeyColumn = "Seccion_id")]
       public Secciones? Secciones { get; set; }
       [ManyToOne(TableName = "Docentes", KeyColumn = "Id", ForeignKeyColumn = "Docente_id")]
       public Docentes? Docentes { get; set; }
   }
}
