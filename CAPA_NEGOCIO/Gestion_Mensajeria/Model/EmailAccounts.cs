using APPCORE;
using APPCORE.BDCore.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace DataBaseModel {
   public class EmailAccounts : EntityClass {
       [PrimaryKey(Identity = true)]
       public int? Id { get; set; }
       public string? Email { get; set; }
       public string? Password { get; set; }
       public string? Host { get; set; }
       public int? SentCount { get; set; }
       public DateTime? LastUsedDate { get; set; }

       public EmailAccounts withConection(WDataMapper conection){
            this.SetConnection(conection);
            return this;
       }
   }
}
