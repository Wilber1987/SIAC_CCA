CREATE OR REPLACE VIEW pagos_alumnos_view AS
SELECT
    cargo.idestudiantecargo AS idestudiantecargo,
    cargo.tipo AS tipo,
    cargo.idservicio AS idservicio,
    cargo.ejercicio AS ejercicio,
    cargo.importemd AS importemd,
    cargo.importedescuentomd AS importedescuentomd,
    cargo.importenetomd AS importenetomd,
    cargo.estatus AS estatus,    
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

    serv.textocorto AS textocorto,

    est.idtestudiante AS codigo_estudiante,
    est.idestudiante AS id_estudiante,
    est.nombres AS nombres,
    est.apellidos AS apellidos,

    fam.texto AS familia,

    tcd.idclasedeudor AS id_clase_deudor,
    tcd.idplazo AS id_plazo,
    tcd.nombre AS nombre,

    tgp.texto AS plazo_text,
    tgp.dias AS dias_plazo,

    tgm.textocorto AS moneda

FROM tbl_aca_estudiantecargo cargo
LEFT JOIN tbl_cxc_documento doc ON cargo.iddocumentocargo = doc.iddocumento
JOIN tbl_aca_servicio serv ON serv.idservicio = cargo.idservicio
JOIN tbl_aca_estudiante est ON est.idestudiante = cargo.idestudiante
LEFT JOIN tbl_aca_familia fam ON fam.idfamilia = est.idfamilia
JOIN tbl_cxc_deudor tcd ON tcd.iddeudor = doc.iddeudor
LEFT JOIN tbl_gen_plazo tgp ON tgp.idplazo = tcd.idplazo
LEFT JOIN tbl_gen_moneda tgm ON tgm.idmoneda = doc.idmd

union all 

select 
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
    
from  tbl_cnt_documentodetalle cargo
inner join tbl_cnt_documento tcd2 on tcd2.iddocumento  = cargo.iddocumento 
inner join tbl_cnt_clasedocumento tcc on tcc.idclasedocumento  = cargo.idclasedocumento 
LEFT JOIN tbl_cxc_documento doc ON tcd2.iddocumento = doc.iddocumento and doc.fechaanulacion  is null and doc.iddocumentodetalle = cargo.iddocumentodetalle 
inner join tbl_aca_estudiante est on est.idcliente = cargo.iddeudor 
LEFT JOIN tbl_aca_familia fam ON fam.idfamilia = est.idfamilia
inner join tbl_cxc_deudor tcd on tcd.iddeudor  = cargo.iddeudor 
inner join tbl_gen_moneda tgm on tgm.idmoneda  = tcd2.idmonedadocumento 
where cargo.iddeudor  is not null and doc.idservicioreferencia is not null 
and tcd2.fechaanulacion  is null