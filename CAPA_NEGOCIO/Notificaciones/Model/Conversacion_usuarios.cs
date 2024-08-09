using CAPA_DATOS;
using CAPA_DATOS.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace DataBaseModel {
   public class Conversacion_usuarios : EntityClass {
       [PrimaryKey(Identity = false)]
       public int? Id_conversacion { get; set; }
       [PrimaryKey(Identity = false)]
       public int? Id_User { get; set; }
       [ManyToOne(TableName = "Conversacion", KeyColumn = "Id_conversacion", ForeignKeyColumn = "Id_conversacion")]
       public Conversacion? Conversacion { get; set; }
       [ManyToOne(TableName = "Security_Users", KeyColumn = "Id_User", ForeignKeyColumn = "Id_User")]
       public Security_Users? Security_Users { get; set; }
   }
}
