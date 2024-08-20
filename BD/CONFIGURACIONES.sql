delete from [administrative_access].[Transactional_Configuraciones]
INSERT [administrative_access].[Transactional_Configuraciones] (
        [Nombre],
        [Descripcion],
        [Valor],
        [Tipo_Configuracion]
    )
VALUES (
        N'TITULO',
        N'Encabezado de página',
        N'Colegio Centro América ',
        N'THEME'
    )
INSERT [administrative_access].[Transactional_Configuraciones] (
        [Nombre],
        [Descripcion],
        [Valor],
        [Tipo_Configuracion]
    )
VALUES (
        N'SUB_TITULO',
        N'Subtitulo que se muestra en el encabezado',
        N'',
        N'THEME'
    )
INSERT [administrative_access].[Transactional_Configuraciones] (
        [Nombre],
        [Descripcion],
        [Valor],
        [Tipo_Configuracion]
    )
VALUES (
        N'NOMBRE_EMPRESA',
        N'nombre de la empresa',
        N'Colegio Centro América',
        N'THEME'
    )
INSERT [administrative_access].[Transactional_Configuraciones] (
        [Nombre],
        [Descripcion],
        [Valor],
        [Tipo_Configuracion]
    )
VALUES (
        N'LOGO_PRINCIPAL',
        N'Logo que se muestra en los encabezados',
        N'LogotipoCCA_azul.png',
        N'THEME'
    )
INSERT [administrative_access].[Transactional_Configuraciones] (
        [Nombre],
        [Descripcion],
        [Valor],
        [Tipo_Configuracion]
    )
VALUES (
        N'MEDIA_IMG_PATH',
        N'Ruta de recursos',
        N'/media/img/',
        N'THEME'
    )
INSERT [administrative_access].[Transactional_Configuraciones] (
        [Nombre],
        [Descripcion],
        [Valor],
        [Tipo_Configuracion]
    )
VALUES (
        N'VERSION',
        N'Versión de la aplicación',
        N'2024.08',
        N'NUMBER'
    )
INSERT INTO administrative_access.Transactional_Configuraciones (Nombre, Descripcion, Valor, Tipo_Configuracion)
VALUES(
        N'WATHERMARK',
        N'Marca de agua',
        N'LogotipoCCA_azul.png',
        N'THEME'
    );