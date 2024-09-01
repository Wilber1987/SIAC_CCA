using CAPA_DATOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace DataBaseModel {
   public class Responsables : EntityClass {
       public required string Mensaje { get; set; }
       public string? MediaURL { get; set; }
       public required string ParamType { get; set; }
       public bool? EsResponsable { get; set; }
       public List<int>? Padres { get; set; }
       public List<int>? Niveles { get; set; }
       public List<int>? Clases { get; set; }
       public List<int>? Secciones { get; set; }
   }


    /*
        NIVEL => PRIMARIA, SECUNDARIA, PREESCOLAR
        CLASE => 1, 2, 3 (PRIMERO,SEGUNDO,TERCERO)
        SECCION => A,B,C
    */
    public enum ParamType
   {
        NIVEL, CLASE, SECCION, PADRES  
   }


}
