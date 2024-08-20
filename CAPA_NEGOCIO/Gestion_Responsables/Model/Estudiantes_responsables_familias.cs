using CAPA_DATOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace DataBaseModel {
   public class Estudiantes_responsables_familias : EntityClass {
       [PrimaryKey(Identity = false)]
       public int? Id { get; set; }
       public int? Estudiante_id { get; set; }
       public int? Pariente_id { get; set; }
       public DateTime? Created_at { get; set; }
       public DateTime? Updated_at { get; set; }
       public string? Parentesco { get; set; }
       public int? Familia_id { get; set; }
       [ManyToOne(TableName = "Familias", KeyColumn = "Id", ForeignKeyColumn = "Familia_id")]
       public Familias? Familias { get; set; }
       [ManyToOne(TableName = "Estudiantes", KeyColumn = "Id", ForeignKeyColumn = "Estudiante_id")]
       public Estudiantes? Estudiantes { get; set; }
       [ManyToOne(TableName = "Parientes", KeyColumn = "Id", ForeignKeyColumn = "Pariente_id")]
       public Parientes? Parientes { get; set; }
   }
}