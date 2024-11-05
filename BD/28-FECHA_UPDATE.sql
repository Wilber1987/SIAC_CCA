CREATE TABLE fecha_actualizacion (
    id_region INT PRIMARY KEY,
    fecha datetime NOT NULL    
);

INSERT [administrative_access].[Transactional_Configuraciones] (
        [Nombre],
        [Descripcion],
        [Valor],
        [Tipo_Configuracion]
    )
VALUES (
        N'FECHA_VENCIMIENTO_BOLETAS',
        N'Esta fecha se muestra en las boletas de matriculas visualizadas por los padres, se debe actualizar cada año',
        N'',
        N'IMPRESION'
    )


