
/*** importante ejecutar en base de datos de produccion en SIAC esto para optimizar respuesta de consultas**/

CREATE INDEX idx_calificaciones_estudiante_clase ON calificaciones(estudiante_clase_id);
CREATE INDEX idx_estudiante_clases_periodo ON estudiante_clases(periodo_lectivo_id);
CREATE INDEX idx_periodo_lectivos_nombre ON periodo_lectivos(nombre_corto);