

alter view [dbo].[Estudiante_Clases_View] as
SELECT  ec.transferido, ec.estudiante_id, ec.id, ec.retirado, ec.promedio, ec.repitente, ec.reprobadas, pl.nombre AS nombre_periodo, pl.nombre_corto AS nombre_corto_periodo, pl.inicio AS inicio_periodo, pl.fin AS fin_periodo, pl.abierto, 
                         pl.oculto, t.nombre AS nombre_nota, t.nombre_corto AS nombre_corto_nota, t.numero_consolidados, t.consolidado_id, t.orden, c2.resultado, e.tipo, e.hora, c2.updated_at AS fecha, e.porcentaje, a.nombre AS Nombre_asignatura, 
                         a.nombre_corto AS nombre_corto_asignatura, n.nombre_grado, n.nombre_corto AS nombre_corto_nivel, n.nombre AS nombre_nivel, n.numero_grados, n.inicio_grado, c.grado, ec.clase_id, m.config, m.id AS materia_id, 
                         m.MateriasConfig, s.nombre AS nombre_seccion, ec.seccion_id,  CONCAT(e2.primer_nombre, ' ', e2.segundo_nombre, ' ', e2.primer_apellido, ' ', e2.segundo_apellido)  AS Nombre_Estudiantes, e2.codigo, e2.sexo, 
                         t.orden AS orden_Asignatura,
						-- case when e.observaciones is not null then concat(e.tipo,' - ', e.observaciones ,' (',porcentaje,' pts.)') else null end as observaciones_Puntaje
						ifnull(e.observaciones,' ') as observaciones_Puntaje
FROM            dbo.estudiante_clases AS ec INNER JOIN
                         dbo.estudiantes AS e2 ON ec.estudiante_id = e2.id INNER JOIN
                         dbo.clases AS c ON ec.clase_id = c.id INNER JOIN
                         dbo.secciones AS s ON ec.seccion_id = s.id INNER JOIN
                         dbo.periodo_lectivos AS pl ON pl.id = ec.periodo_lectivo_id INNER JOIN
                         dbo.calificaciones AS c2 ON c2.estudiante_clase_id = ec.id INNER JOIN
                         dbo.materias AS m ON c2.materia_id = m.id INNER JOIN
                         dbo.asignaturas AS a ON m.asignatura_id = a.id LEFT OUTER JOIN
                         dbo.niveles AS n ON a.nivel_id = n.id LEFT OUTER JOIN
                         dbo.evaluaciones AS e ON c2.evaluacion_id = e.id LEFT OUTER JOIN
                         dbo.tipo_notas AS t ON c2.tipo_nota_id = t.id
