using CAPA_DATOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace DataBaseModel {
   public class Estudiante_clases : EntityClass {
       [PrimaryKey(Identity = false)]
       public int? Id { get; set; }
       public int? Estudiante_id { get; set; }
       public int? Periodo_lectivo_id { get; set; }
       public int? Clase_id { get; set; }
       public int? Seccion_id { get; set; }
       public DateTime? Transferido { get; set; }
       public DateTime? Retirado { get; set; }
       public string? Observaciones { get; set; }
       public DateTime? Created_at { get; set; }
       public DateTime? Updated_at { get; set; }
       public Double? Promedio { get; set; }
       public bool? Repitente { get; set; }
       public int? Reprobadas { get; set; }
       [ManyToOne(TableName = "Clases", KeyColumn = "Id", ForeignKeyColumn = "Clase_id")]
       public Clases? Clases { get; set; }
       [ManyToOne(TableName = "Estudiantes", KeyColumn = "Id", ForeignKeyColumn = "Estudiante_id")]
       public Estudiantes? Estudiantes { get; set; }
       [ManyToOne(TableName = "Periodo_lectivos", KeyColumn = "Id", ForeignKeyColumn = "Periodo_lectivo_id")]
       public Periodo_lectivos? Periodo_lectivos { get; set; }
       [ManyToOne(TableName = "Secciones", KeyColumn = "Id", ForeignKeyColumn = "Seccion_id")]
       public Secciones? Secciones { get; set; }
       [OneToMany(TableName = "Calificaciones", KeyColumn = "Id", ForeignKeyColumn = "Estudiante_clase_id")]
       public List<Calificaciones>? Calificaciones { get; set; }
   }
}
