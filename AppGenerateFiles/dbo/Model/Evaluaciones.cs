using CAPA_DATOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace DataBaseModel {
   public class Evaluaciones : EntityClass {
       [PrimaryKey(Identity = true)]
       public int? Id { get; set; }
       public DateTime? Fecha { get; set; }
       public ? Hora { get; set; }
       public string? Tipo { get; set; }
       public Double? Porcentaje { get; set; }
       public int? Materia_id { get; set; }
       public int? Seccion_id { get; set; }
       public string? Observaciones { get; set; }
       public DateTime? Created_at { get; set; }
       public DateTime? Updated_at { get; set; }
       public int? Periodo { get; set; }
       public Double? Nota_maxima { get; set; }
       [ManyToOne(TableName = "Materias", KeyColumn = "Id", ForeignKeyColumn = "Materia_id")]
       public Materias? Materias { get; set; }
       [ManyToOne(TableName = "Secciones", KeyColumn = "Id", ForeignKeyColumn = "Seccion_id")]
       public Secciones? Secciones { get; set; }
       [OneToMany(TableName = "Calificaciones", KeyColumn = "Id", ForeignKeyColumn = "Evaluacion_id")]
       public List<Calificaciones>? Calificaciones { get; set; }
   }
}
