using CAPA_DATOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace DataBaseModel {
   public class Docentes : EntityClass {
       [PrimaryKey(Identity = true)]
       public int? Id { get; set; }
       public string? Primer_nombre { get; set; }
       public string? Segundo_nombre { get; set; }
       public string? Primer_apellido { get; set; }
       public string? Segundo_apellido { get; set; }
       public string? Sexo { get; set; }
       public DateTime? Fecha_nacimiento { get; set; }
       public string? Lugar_nacimiento { get; set; }
       public string? Direccion { get; set; }
       public string? Telefono { get; set; }
       public string? Celular { get; set; }
       public string? Email { get; set; }
       public int? Estado_civil_id { get; set; }
       public int? Religion_id { get; set; }
       public int? Escolaridad_id { get; set; }
       public string? Foto { get; set; }
       public DateTime? Created_at { get; set; }
       public DateTime? Updated_at { get; set; }
       public bool? Habilitado { get; set; }
       public string? Cargo { get; set; }
       [ManyToOne(TableName = "Escolaridades", KeyColumn = "Id", ForeignKeyColumn = "Escolaridad_id")]
       public Escolaridades? Escolaridades { get; set; }
       [OneToMany(TableName = "Docente_asignaturas", KeyColumn = "Id", ForeignKeyColumn = "Docente_id")]
       public List<Docente_asignaturas>? Docente_asignaturas { get; set; }
       [OneToMany(TableName = "Docente_materias", KeyColumn = "Id", ForeignKeyColumn = "Docente_id")]
       public List<Docente_materias>? Docente_materias { get; set; }
   }
}
