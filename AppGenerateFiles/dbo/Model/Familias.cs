using CAPA_DATOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace DataBaseModel {
   public class Familias : EntityClass {
       [PrimaryKey(Identity = false)]
       public int? Id { get; set; }
       public string? Descripcion { get; set; }
       public bool? Estado { get; set; }
       public Double? Saldo { get; set; }
       public bool? Actualizado { get; set; }
       public bool? Aceptacion { get; set; }
       public int? Periodo_aceptacion { get; set; }
       public DateTime? Fecha_actualizacion { get; set; }
       public string? Fecha_ultima_notificacion { get; set; }
       public int? Id_usuario { get; set; }
       [ManyToOne(TableName = "Security_Users", KeyColumn = "Id_User", ForeignKeyColumn = "Id_usuario")]
       public Security_Users? Security_Users { get; set; }
       [OneToMany(TableName = "Estudiantes_responsables_familias", KeyColumn = "Id", ForeignKeyColumn = "Familia_id")]
       public List<Estudiantes_responsables_familias>? Estudiantes_responsables_familias { get; set; }
   }
}
