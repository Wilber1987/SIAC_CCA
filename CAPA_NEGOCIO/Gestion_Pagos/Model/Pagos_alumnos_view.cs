using CAPA_DATOS;

namespace CAPA_NEGOCIO.Gestion_Pagos.Model
{
    public class Pagos_alumnos_view : EntityClass
    {
        public int? Codigo_estudiante { get; set; }
        public int? Id_estudiante { get; set; }
        public string? Nombres { get; set; }
        public string? Apellidos { get; set; }
        public int? Id_clase_deudor { get; set; }
        public int? Id_plazo { get; set; }
        public string? Plazo_texto { get; set; }
        public int? dias_plazo { get; set; }
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
        public double? Importe_md { get; set; }
        public int? Id_cuenta { get; set; }
        public int? Id_ceco { get; set; }
        public int? Id_cebe { get; set; }
        public string? Clave { get; set; }
        public int? Id_indicador_impuesto { get; set; }
        public bool? Calcula_impuesto { get; set; }
        public double? Importe_impuesto_md { get; set; }
        public double? Importe_saldo_md { get; set; }
        public int? Id_documento_detalle { get; set; }
        public DateTime? Fecha_grabacion { get; set; }
        public string? Usuario_grabacion { get; set; }
        public int? No_documento_a { get; set; }
        public DateTime? Fecha_anulacion { get; set; }
        public string? Usuario_anulacion { get; set; }
        public int? Id_servicio_referencia { get; set; }
        public double? Importe_dif_cambiario { get; set; }
        public string? Id_moneda_t { get; set; }
        public string? Texto_corto { get; set; }
        public string? Texto_largo { get; set; }
        public string? Simbolo { get; set; }
        public string? Moneda { get; set; }




    }

    public class ViewPagosAlumnosExtraordinarios : Pagos_alumnos_view
    {

        public object? CreateViewPagosAlumnosExtraordinarios(string codigoEstudiante)
        {
            string query = $@"
                DROP VIEW IF EXISTS ViewPagosAlumnosExtraordinarios;
                CREATE VIEW ViewPagosAlumnosExtraordinarios AS
                SELECT 
                    cargo.iddocumento AS idestudiantecargo,
                    cargo.clave AS tipo,
                    cargo.idclasedocumento AS idservicio,
                    cargo.ejercicio AS ejercicio,
                    cargo.importemd AS importemd,
                    0 AS importedescuentomd,
                    cargo.importemd AS importenetomd,
                    'PENDIENTE DESARROLLO' AS estatus,
                    MONTH(doc.fechadocumento) AS mes,
                    YEAR(doc.fechadocumento) AS anio,
                    doc.iddocumentocc AS id_documento_cc,
                    doc.iddocumento AS id_documento,
                    doc.idsociedad AS id_sociedad,
                    doc.idclasedocumento AS id_clase_documento,
                    doc.fechadocumento AS fecha_documento,
                    doc.fechacontabilizacion AS fecha_contabilizacion,
                    doc.periodo AS periodo,
                    doc.nodocumento AS no_documento,
                    doc.posicion AS posicion,
                    doc.iddeudor AS id_deudor,
                    doc.asignacion AS asignacion,
                    doc.textoposicion AS texto_posicion,
                    doc.idmd AS id_moneda,
                    doc.importemd AS importe_md,
                    doc.idcuenta AS id_cuenta,
                    doc.idceco AS id_ceco,
                    doc.idcebe AS id_cebe,
                    doc.clave AS clave,
                    doc.idindicadorimpuesto AS id_indicador_impuesto,
                    doc.calculaimpuesto AS calcula_impuesto,
                    doc.importeimpuestomd AS importe_impuesto_md,
                    IFNULL(doc.importesaldomd, 0) AS importe_saldo_md,
                    doc.iddocumentodetalle AS id_documento_detalle,
                    doc.fechagrabacion AS fecha_grabacion,
                    doc.usuariograbacion AS usuario_grabacion,
                    doc.nodocumentoa AS no_documento_a,
                    doc.fechaanulacion AS fecha_anulacion,
                    doc.usuarioanulacion AS usuario_anulacion,
                    doc.idservicioreferencia AS id_servicio_referencia,
                    doc.importedffcambiario AS importe_dif_cambiario,
                    tcc.textocorto AS textocorto,
                    est.idtestudiante AS codigo_estudiante,
                    est.idestudiante AS id_estudiante,
                    est.nombres AS nombres,
                    est.apellidos AS apellidos,
                    fam.texto AS familia,
                    tcd.idclasedeudor AS id_clase_deudor,
                    tcd.idplazo AS id_plazo,
                    tcd.nombre AS nombre,
                    '1 AÑO' AS plazo_text,
                    360 AS dias_plazo,
                    tgm.textocorto AS moneda
                FROM tbl_cnt_documentodetalle cargo
                INNER JOIN tbl_cnt_documento tcd2 ON tcd2.iddocumento = cargo.iddocumento 
                INNER JOIN tbl_cnt_clasedocumento tcc ON tcc.idclasedocumento = cargo.idclasedocumento 
                LEFT JOIN tbl_cxc_documento doc ON tcd2.iddocumento = doc.iddocumento 
                    AND doc.fechaanulacion IS NULL 
                    AND doc.iddocumentodetalle = cargo.iddocumentodetalle 
                INNER JOIN tbl_aca_estudiante est ON est.idcliente = cargo.iddeudor 
                LEFT JOIN tbl_aca_familia fam ON fam.idfamilia = est.idfamilia
                INNER JOIN tbl_cxc_deudor tcd ON tcd.iddeudor = cargo.iddeudor 
                INNER JOIN tbl_gen_moneda tgm ON tgm.idmoneda = tcd2.idmonedadocumento 
                WHERE cargo.iddeudor IS NOT NULL 
                    AND doc.idservicioreferencia IS NOT NULL 
                    AND doc.importesaldomd > 0
                    AND est.idtestudiante = '{codigoEstudiante}';
            ";

            return ExecuteSqlQuery(query);
        }

        public object? DestroyView(string view)
        {
            string query = $"DROP VIEW IF EXISTS {view};";
            return ExecuteSqlQuery(query);
        }
    }
}