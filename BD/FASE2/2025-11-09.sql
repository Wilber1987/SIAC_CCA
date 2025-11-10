-- dbo.Estudiante_View source

alter VIEW [dbo].[Estudiante_View] AS
SELECT ec.transferido,
    ec.estudiante_id,
    ec.id,
    ec.retirado,
    ec.promedio,
    ec.repitente,
    ec.reprobadas,
    pl.nombre AS nombre_periodo,
    pl.nombre_corto AS nombre_corto_periodo,
    pl.inicio AS inicio_periodo,
    pl.fin AS fin_periodo,
    pl.abierto,
    pl.oculto,   
    c.grado,
    ec.clase_id,
    e2.id_familia,
    f2.Idtfamilia,
	c.nivel_id,
    s.nombre as nombre_seccion,
    ec.seccion_id,
    CONCAT(e2.primer_nombre , ' ',e2.segundo_nombre, ' ', e2.primer_apellido  , ' ',e2.segundo_apellido) AS Nombre_Estudiantes,
    e2.codigo, 
    e2.sexo,
	 n.nombre_grado, 
	 n.nombre_corto AS nombre_corto_nivel, 
	 n.nombre AS nombre_nivel,
	 n.numero_grados, 
	 n.inicio_grado,
	 ec.periodo_lectivo_id 
from dbo.estudiante_clases ec 
INNER JOIN dbo.estudiantes e2 on ec.estudiante_id = e2.id 
inner join dbo.clases c   on ec.clase_id  = c.id
INNER JOIN dbo.familias f2 on f2.id = e2.id_familia 
INNER JOIN dbo.secciones s ON ec.seccion_id = s.id
INNER JOIN dbo.periodo_lectivos pl ON pl.id = ec.periodo_lectivo_id 
inner join niveles n on n.id = c.nivel_id;