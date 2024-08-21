using CAPA_DATOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace DataBaseModel {
   public class Estudiante_Clases_View : EntityClass {
       public DateTime? Transferido { get; set; }
       public int? Estudiante_id { get; set; }
       public int? Id { get; set; }
       public DateTime? Retirado { get; set; }
       public Double? Promedio { get; set; }
       public bool? Repitente { get; set; }
       public int? Reprobadas { get; set; }
       public string? Nombre_periodo { get; set; }
       public string? Nombre_corto_periodo { get; set; }
       public DateTime? Inicio_periodo { get; set; }
       public DateTime? Fin_periodo { get; set; }
       public bool? Abierto { get; set; }
       public bool? Oculto { get; set; }
       public string? Nombre_nota { get; set; }
       public string? Nombre_corto_nota { get; set; }
       public int? Numero_consolidados { get; set; }
       public int? Consolidado_id { get; set; }
       public int? Orden { get; set; }
       public Double? Resultado { get; set; }
       public string? Tipo { get; set; }
       public ? Hora { get; set; }
       public DateTime? Fecha { get; set; }
       public Double? Porcentaje { get; set; }
       public string? Nombre_asignatura { get; set; }
       public string? Nombre_corto_asignatura { get; set; }
       public string? Nombre_grado { get; set; }
       public string? Nombre_corto_nivel { get; set; }
       public string? Nombre_nivel { get; set; }
       public int? Numero_grados { get; set; }
       public int? Inicio_grado { get; set; }
       public int? Grado { get; set; }
       public int? Clase_id { get; set; }
   }
}
