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


INSERT [administrative_access].[Transactional_Configuraciones] (
        [Nombre],
        [Descripcion],
        [Valor],
        [Tipo_Configuracion]
    )
VALUES (
        N'URL_BASE',
        N'Url base de la aplicacion, este valor no se debe editar',
        N'https://portal.ni-alpha.cloud/Security/Login',
        N'THEME'
    )