using CAPA_DATOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace DataBaseModel {
   public class Security_Users : EntityClass {
       [PrimaryKey(Identity = true)]
       public int? Id_User { get; set; }
       public string? Nombres { get; set; }
       public string? Estado { get; set; }
       public string? Descripcion { get; set; }
       public string? Password { get; set; }
       public string? Mail { get; set; }
       public string? Token { get; set; }
       public DateTime? Token_Date { get; set; }
       public DateTime? Token_Expiration_Date { get; set; }
       [OneToMany(TableName = "Security_Users_Roles", KeyColumn = "Id_User", ForeignKeyColumn = "Id_User")]
       public List<Security_Users_Roles>? Security_Users_Roles { get; set; }
   }
}
