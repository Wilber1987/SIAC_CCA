using APPCORE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace DataBaseModel {
   public class Periodo_lectivos : EntityClass {
       [PrimaryKey(Identity = true)]
       public int? Id { get; set; }
       public string? Nombre { get; set; }
       public string? Nombre_corto { get; set; }
       public DateTime? Inicio { get; set; }
       public DateTime? Fin { get; set; }
       public string? Observaciones { get; set; }
       public string? Config { get; set; }
       public DateTime? Created_at { get; set; }
       public DateTime? Updated_at { get; set; }
       public bool? Abierto { get; set; }
       public bool? Oculto { get; set; } 
       public static Periodo_lectivos? PeriodoActivo(){
            return new Periodo_lectivos{ 
                Nombre_corto = DateTime.Now.Year.ToString() //  "2024"// DateTime.Now.Year.ToString() // todo
            }.Find<Periodo_lectivos>() ?? new Periodo_lectivos{ 
                Nombre_corto = (DateTime.Now.Year - 1).ToString() //"2024"//(DateTime.Now.Year - 1).ToString() //todo
            }.Find<Periodo_lectivos>();
       }   
   }
}
