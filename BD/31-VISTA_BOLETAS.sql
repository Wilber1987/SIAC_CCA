
DROP VIEW IF EXISTS viewestudiantesboletas;
CREATE VIEW viewestudiantesboletas
AS
    SELECT per.idtperiodoacademico, cargo.ejercicio, cargo.tipo, cargo.idperiodoacademico, cargo.idestudiante, est.idtestudiante, cargo.periodo, cargo.idmoneda, tgm.idtmoneda, sum(cargo.importemd) as importemd, sum(cargo.descuento) as descuento, sum(cargo.importedescuentomd) as importedescuentomd
, sum(cargo.importenetomd) as importenetomd, cargo.estatus, cargo.contabilizado, cargo.fechagrabacion, est.idestudiante as codigo, est.nombres, est.apellidos, YEAR(current_date)as ciclo
, areaactual.texto as grado_actual, nactual.texto as curso_actual, areasiguiente.texto as grado_siguiente, ifnull(nsiguiente.texto,'N/D') as curso_siguiente
    from tbl_aca_estudiantecargo cargo
        inner join tbl_aca_estudiante est on est.idestudiante = cargo.idestudiante
        inner join tbl_aca_matricula matri on matri.idestudiante = est.idestudiante
        inner join tbl_aca_periodoacademico per on per.idperiodoacademico = matri.idperiodoacademico
        inner join tbl_aca_academianivel nactual on nactual.idacademianivel = matri.idacademianivel
        left join tbl_aca_academianivel nsiguiente on nsiguiente.idacademianivel = nactual.idnivelproximo
        INNER JOIN tbl_aca_academiaarea areaactual on areaactual.idacademiaarea = nactual.idacademiaarea
        INNER JOIN tbl_aca_academiaarea areasiguiente on areasiguiente.idacademiaarea = nsiguiente.idacademiaarea
        inner join tbl_gen_moneda tgm on tgm.idmoneda  = cargo.idmoneda
    where 	/*cargo.idestudiante  = 3225
		and cargo.ejercicio = 2025
		and per.idtperiodoacademico = 2024
		and*/ idservicio in (7,2,39)
        and cargo.contabilizado = 0
    group by per.idtperiodoacademico, cargo.ejercicio,cargo.tipo, cargo.idperiodoacademico, cargo.idestudiante, est.idtestudiante, cargo.periodo, cargo.idmoneda,tgm.idtmoneda
, cargo.estatus, cargo.contabilizado, cargo.fechagrabacion, est.idestudiante, est.nombres, est.apellidos, YEAR(current_date)
,areaactual.texto, nactual.texto, areasiguiente.texto, nsiguiente.texto
    order by fechagrabacion desc
;


INSERT [administrative_access].[Transactional_Configuraciones]
    (
    [Nombre],
    [Descripcion],
    [Valor],
    [Tipo_Configuracion]
    )
VALUES
    (
        N'FECHA_VENCIMIENTO_BOLETAS_ESTUDIANTES',
        N'Fecha de vencimiento de las boletas de matricula que se envian al actualizar datos de padres y alumnos',
        N'20/12/2024',
        N'THEME'
    )