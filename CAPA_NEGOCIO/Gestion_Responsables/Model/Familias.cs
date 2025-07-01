using APPCORE;
using APPCORE.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace DataBaseModel {
   public class Familias : EntityClass {
       [PrimaryKey(Identity = true)]
       public int? Id { get; set; }
       public string? Idtfamilia { get; set; }
       public string? Descripcion { get; set; }
       public bool? Estado { get; set; }
       public Double? Saldo { get; set; }
       public bool? Actualizado { get; set; }
       public bool? Aceptacion { get; set; }
       public int? Periodo_aceptacion { get; set; }
       public DateTime? Fecha_actualizacion { get; set; }
       public DateTime? Fecha_ultima_notificacion { get; set; }

       [OneToMany(TableName = "Estudiantes_responsables_familia", KeyColumn = "Id", ForeignKeyColumn = "Familia_id")]
       public List<Estudiantes_responsables_familia>? Estudiantes_responsables_familia { get; set; }
   }
}
