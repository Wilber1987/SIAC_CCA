using CAPA_DATOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace DataBaseModel {
   public class Tbl_aca_tutor : EntityClass {
       [PrimaryKey(Identity = false)]
       public int? Idtutor { get; set; }
       public int? Idfamilia { get; set; }
       public short? Ididentificacion { get; set; }
       public string? Noidentificacion { get; set; }
       public string? Nombres { get; set; }
       public string? Apellidos { get; set; }
       public string? Sexo { get; set; }
       public DateTime? Fechanacimiento { get; set; }
       public int? Idtitulo { get; set; }
       public int? Idestadocivil { get; set; }
       public short? Idpais { get; set; }
       public short? Idregion { get; set; }
       public string? Direccion { get; set; }
       public string? Telefono { get; set; }
       public string? Celular { get; set; }
       public string? Lugartrabajo { get; set; }
       public string? Telefonotrabajo { get; set; }
       public string? Email { get; set; }
       public int? Idrelacionfamiliar { get; set; }
       public int? Idreligion { get; set; }
       public bool? Responsable { get; set; }
       public bool Responsablepago { get; set; }
       public string? Exalumno { get; set; }
       public int? Ejercicio { get; set; }
       public DateTime? Fechagrabacion { get; set; }
       public string? Usuariograbacion { get; set; }
       public DateTime? Fechamodificacion { get; set; }
       public string? Usuariomodificacion { get; set; }
       public DateTime? Fechaactualizacion { get; set; }
       public bool? Actualizado { get; set; }
       public int? Noresponsable { get; set; }
   }
}
