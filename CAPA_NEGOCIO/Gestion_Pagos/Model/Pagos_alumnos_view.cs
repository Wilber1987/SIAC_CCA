using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Controllers;
using CAPA_DATOS;

namespace CAPA_NEGOCIO.Gestion_Pagos.Model
{
    public class Pagos_alumnos_view : EntityClass
    {
        public int? Id_estudiante { get; set; }
        public string? Nombres { get; set; }
        public string? Apellidos { get; set; }
        public int? Id_clase_deudor { get; set; }
        public int? Id_plazo { get; set; }
        public string? Nombre { get; set; }
        public string? Razon_social { get; set; }
        public int? Mes { get; set; }
        public int? Anio { get; set; }
        public int? Id_documento_cc { get; set; }
        public int? Id_documento { get; set; }
        public int? Id_sociedad { get; set; }
        public int? Id_clase_documento { get; set; }
        public DateTime? Fecha_documento { get; set; }
        public DateTime? Fecha_contabilizacion { get; set; }
        public int? Ejercicio { get; set; }
        public int? Periodo { get; set; }
        public int? No_documento { get; set; }
        public int? Posicion { get; set; }
        public int? Id_deudor { get; set; }
        public string? Asignacion { get; set; }
        public string? Texto_posicion { get; set; }
        public int? Id_moneda { get; set; }
        public decimal? Importe_md { get; set; }
        public int? Id_cuenta { get; set; }
        public int? Id_ceco { get; set; }
        public int? Id_cebe { get; set; }
        public string? Clave { get; set; }
        public int? Id_indicador_impuesto { get; set; }
        public bool? Calcula_impuesto { get; set; }
        public decimal? Importe_impuesto_md { get; set; }
        public decimal? Importe_saldo_md { get; set; }
        public int? Id_documento_detalle { get; set; }
        public DateTime? Fecha_grabacion { get; set; }
        public string? Usuario_grabacion { get; set; }
        public int? No_documento_a { get; set; }
        public DateTime? Fecha_anulacion { get; set; }
        public string? Usuario_anulacion { get; set; }
        public int? Id_servicio_referencia { get; set; }
        public decimal? Importe_dif_cambiario { get; set; }
        public string? Id_moneda_t { get; set; }
        public string? Texto_corto { get; set; }
        public string? Texto_largo { get; set; }
        public string? Simbolo { get; set; }
    }
}