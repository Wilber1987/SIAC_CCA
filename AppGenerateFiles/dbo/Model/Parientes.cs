using CAPA_DATOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace DataBaseModel {
   public class Parientes : EntityClass {
       [PrimaryKey(Identity = false)]
       public int? Id { get; set; }
       public string? Primer_nombre { get; set; }
       public string? Segundo_nombre { get; set; }
       public string? Primer_apellido { get; set; }
       public string? Segundo_apellido { get; set; }
       public string? Sexo { get; set; }
       public string? Profesion { get; set; }
       public string? Direccion { get; set; }
       public string? Lugar_trabajo { get; set; }
       public string? Telefono { get; set; }
       public string? Celular { get; set; }
       public string? Telefono_trabajo { get; set; }
       public string? Email { get; set; }
       public int? Estado_civil_id { get; set; }
       public int? Religion_id { get; set; }
       public DateTime? Created_at { get; set; }
       public DateTime? Updated_at { get; set; }
       public int? Pais_id { get; set; }
       public bool? Resoponsable_pago { get; set; }
       public string? Noidentificacion { get; set; }
       public int? Id_titulo { get; set; }
       public int? Id_region { get; set; }
       public int? Id_estado_civil { get; set; }
       public int? Id_responsable_pago { get; set; }
       public string? Ex_alumno { get; set; }
       public DateTime? Fecha_nacimiento { get; set; }
       public DateTime? Fecha_modificacion { get; set; }
       public string? Usuario_grabacion { get; set; }
       public string? Usuario_edicion { get; set; }
       public Double? Ejercicio { get; set; }
       public bool? Actualizado { get; set; }
       public int? No_responsable { get; set; }
       [OneToMany(TableName = "Estudiantes_responsables_familias", KeyColumn = "Id", ForeignKeyColumn = "Pariente_id")]
       public List<Estudiantes_responsables_familias>? Estudiantes_responsables_familias { get; set; }
   }
}
