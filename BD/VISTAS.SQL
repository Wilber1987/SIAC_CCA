CREATE VIEW Estudiante_Clases_View 
AS
SELECT dbo.estudiante_clases.transferido, dbo.estudiante_clases.estudiante_id, dbo.estudiante_clases.id, dbo.estudiante_clases.retirado, dbo.estudiante_clases.promedio, dbo.estudiante_clases.repitente, dbo.estudiante_clases.reprobadas, 
                  dbo.periodo_lectivos.nombre AS nombre_periodo, dbo.periodo_lectivos.nombre_corto AS nombre_corto_periodo, dbo.periodo_lectivos.inicio AS inicio_periodo, dbo.periodo_lectivos.fin AS fin_periodo, dbo.periodo_lectivos.abierto, 
                  dbo.periodo_lectivos.oculto, dbo.tipo_notas.nombre AS nombre_nota, dbo.tipo_notas.nombre_corto AS nombre_corto_nota, dbo.tipo_notas.numero_consolidados, dbo.tipo_notas.consolidado_id, dbo.tipo_notas.orden, 
                  dbo.calificaciones.resultado, dbo.evaluaciones.tipo, dbo.evaluaciones.hora, dbo.evaluaciones.fecha, dbo.evaluaciones.porcentaje, dbo.asignaturas.nombre AS Nombre_asignatura, 
                  dbo.asignaturas.nombre_corto AS nombre_corto_asignatura, dbo.niveles.nombre_grado, dbo.niveles.nombre_corto AS nombre_corto_nivel, dbo.niveles.nombre AS nombre_nivel, dbo.niveles.numero_grados, dbo.niveles.inicio_grado, 
                  dbo.clases.grado, dbo.estudiante_clases.clase_id
FROM     dbo.estudiante_clases INNER JOIN
                  dbo.clases ON dbo.estudiante_clases.clase_id = dbo.clases.id INNER JOIN
                  dbo.calificaciones ON dbo.estudiante_clases.id = dbo.calificaciones.estudiante_clase_id INNER JOIN
                  dbo.materias ON dbo.calificaciones.materia_id = dbo.materias.id INNER JOIN
                  dbo.periodo_lectivos ON dbo.estudiante_clases.periodo_lectivo_id = dbo.periodo_lectivos.id LEFT OUTER JOIN
                  dbo.evaluaciones ON dbo.calificaciones.evaluacion_id = dbo.evaluaciones.id INNER JOIN
                  dbo.asignaturas ON dbo.materias.asignatura_id = dbo.asignaturas.id LEFT OUTER JOIN
                  dbo.tipo_notas ON dbo.calificaciones.tipo_nota_id = dbo.tipo_notas.id LEFT OUTER JOIN
                  dbo.niveles ON dbo.asignaturas.nivel_id = dbo.niveles.id