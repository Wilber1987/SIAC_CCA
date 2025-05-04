
ALTER TABLE parientes
ADD credenciales_enviadas BIT NOT NULL DEFAULT 0;


UPDATE parientes
SET credenciales_enviadas = 0
WHERE user_id IS NOT NULL
AND id_familia IN (
    SELECT e.id_familia
    FROM estudiantes e
    INNER JOIN estudiante_clases ec ON ec.estudiante_id = e.id
    INNER JOIN periodo_lectivos pl ON pl.id = ec.periodo_lectivo_id
    WHERE pl.nombre_corto = '2025'
);