using CAPA_DATOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace DataBaseModel {
   public class Estudiantes : EntityClass {
       [PrimaryKey(Identity = true)]
       public int? Id { get; set; }
       public string? Primer_nombre { get; set; }
       public string? Segundo_nombre { get; set; }
       public string? Primer_apellido { get; set; }
       public string? Segundo_apellido { get; set; }
       public DateTime? Fecha_nacimiento { get; set; }
       public string? Lugar_nacimiento { get; set; }
       public string? Sexo { get; set; }
       public string? Direccion { get; set; }
       public string? Codigo { get; set; }
       public int? Religion_id { get; set; }
       public int? Madre_id { get; set; }
       public int? Padre_id { get; set; }
       public DateTime? Created_at { get; set; }
       public DateTime? Updated_at { get; set; }
       public string? Foto { get; set; }
       public Double? Peso { get; set; }
       public Double? Altura { get; set; }
       public string? Tipo_sangre { get; set; }
       public string? Padecimientos { get; set; }
       public string? Alergias { get; set; }
       public int? Recorrido_id { get; set; }
       public bool? Activo { get; set; }
      //[OneToMany(TableName = "Estudiante_clases", KeyColumn = "Id", ForeignKeyColumn = "Estudiante_id")]
       public List<Estudiante_clases>? Estudiante_clases { get; set; }
       //[OneToMany(TableName = "Responsables", KeyColumn = "Id", ForeignKeyColumn = "Estudiante_id")]
       public List<Responsables>? Responsables { get; set; }
   }
}
