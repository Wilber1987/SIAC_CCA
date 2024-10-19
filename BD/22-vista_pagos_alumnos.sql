CREATE OR REPLACE
ALGORITHM = UNDEFINED VIEW `pagos_alumnos_view` AS
select
	`tae`.`idtestudiante` AS `codigo_estudiante`,
    `tae`.`idestudiante` AS `id_estudiante`,
    `tae`.`nombres` AS `nombres`,
    `tae`.`apellidos` AS `apellidos`,
    `tcd2`.`idclasedeudor` AS `id_clase_deudor`,
    `tcd2`.`idplazo` AS `id_plazo`,
    `tcd2`.`nombre` AS `nombre`,
    `tcd2`.`razonsocial` AS `razon_social`,
    month(`tcd`.`fechadocumento`) AS `mes`,
    year(`tcd`.`fechadocumento`) AS `anio`,
    `tcd`.`iddocumentocc` AS `id_documento_cc`,
    `tcd`.`iddocumento` AS `id_documento`,
    `tcd`.`idsociedad` AS `id_sociedad`,
    `tcd`.`idclasedocumento` AS `id_clase_documento`,
    `tcd`.`fechadocumento` AS `fecha_documento`,
    `tcd`.`fechacontabilizacion` AS `fecha_contabilizacion`,
    `tcd`.`ejercicio` AS `ejercicio`,
    `tcd`.`periodo` AS `periodo`,
    `tcd`.`nodocumento` AS `no_documento`,
    `tcd`.`posicion` AS `posicion`,
    `tcd`.`iddeudor` AS `id_deudor`,
    `tcd`.`asignacion` AS `asignacion`,
    `tcd`.`textoposicion` AS `texto_posicion`,
    `tcd`.`idmd` AS `id_moneda`,
    `tcd`.`importemd` AS `importe_md`,
    `tcd`.`idcuenta` AS `id_cuenta`,
    `tcd`.`idceco` AS `id_ceco`,
    `tcd`.`idcebe` AS `id_cebe`,
    `tcd`.`clave` AS `clave`,
    `tcd`.`idindicadorimpuesto` AS `id_indicador_impuesto`,
    `tcd`.`calculaimpuesto` AS `calcula_impuesto`,
    `tcd`.`importeimpuestomd` AS `importe_impuesto_md`,
    `tcd`.`importesaldomd` AS `importe_saldo_md`,
    `tcd`.`iddocumentodetalle` AS `id_documento_detalle`,
    `tcd`.`fechagrabacion` AS `fecha_grabacion`,
    `tcd`.`usuariograbacion` AS `usuario_grabacion`,
    `tcd`.`nodocumentoa` AS `no_documento_a`,
    `tcd`.`fechaanulacion` AS `fecha_anulacion`,
    `tcd`.`usuarioanulacion` AS `usuario_anulacion`,
    `tcd`.`idservicioreferencia` AS `id_servicio_referencia`,
    `tcd`.`importedffcambiario` AS `importe_dif_cambiario`,
    `tgm`.`idtmoneda` AS `id_moneda_t`,
    `tgm`.`textocorto` AS `texto_corto`,
    `tgm`.`textolargo` AS `texto_largo`,
    `tgm`.`simbolo` AS `simbolo`
from
    (((`tbl_cxc_documento` `tcd`
join `tbl_cxc_deudor` `tcd2` on
    ((`tcd2`.`iddeudor` = `tcd`.`iddeudor`)))
join `tbl_aca_estudiante` `tae` on
    ((`tae`.`idestudiante` = `tcd2`.`idestudiante`)))
join `tbl_gen_moneda` `tgm` on
    ((`tgm`.`idmoneda` = `tcd`.`idmd`)))
where
    (`tcd`.`iddeudor` = 979)
order by
    `tcd`.`fechadocumento`,
    `tcd`.`asignacion`;