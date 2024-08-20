-- dbo.Estudiante_Clases_View source
/* dbo.Estudiante_Clases_View source
 dbo.Estudiante_Clases_View source*/
CREATE VIEW dbo.Estudiante_Clases_View AS
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
    t.nombre AS nombre_nota,
    t.nombre_corto AS nombre_corto_nota,
    t.numero_consolidados,
    t.consolidado_id,
    t.orden,
    c2.resultado,
    e.tipo,
    e.hora,
    c2.updated_at AS fecha,
    e.porcentaje,
    a.nombre AS Nombre_asignatura,
    a.nombre_corto AS nombre_corto_asignatura,
    n.nombre_grado,
    n.nombre_corto AS nombre_corto_nivel,
    n.nombre AS nombre_nivel,
    n.numero_grados,
    n.inicio_grado,
    c.grado,
    ec.clase_id,
    m.config,
    m.MateriasConfig,
    m.id as materia_id,
    s.nombre as nombre_seccion,
    ec.seccion_id
from dbo.estudiante_clases ec 
inner join dbo.clases c   on ec.clase_id  = c.id
INNER JOIN dbo.secciones s ON ec.seccion_id = s.id
INNER JOIN dbo.periodo_lectivos pl ON pl.id = ec.periodo_lectivo_id 
INNER JOIN dbo.calificaciones c2 on c2.estudiante_clase_id = ec.id
INNER JOIN dbo.materias m on c2.materia_id  = m.id
INNER JOIN dbo.asignaturas a ON m.asignatura_id = a.id
LEFT OUTER JOIN dbo.niveles n ON a.nivel_id = n.id
LEFT OUTER JOIN dbo.evaluaciones e ON c2.evaluacion_id = e.id
LEFT OUTER JOIN dbo.tipo_notas t ON c2.tipo_nota_id = t.id