

INSERT [administrative_access].[Transactional_Configuraciones]
    (
    [Nombre],
    [Descripcion],
    [Valor],
    [Tipo_Configuracion]
    )
VALUES
    (
        N'TWILLIO_ACCOUNT',
        N'TWILLIO SSID',
        N'ACb85c756cec91afa909566f87fca70709',
        N'TWILLIO'
    ),
    (
        N'TWILLIO_TOKEN',
        N'TWILLIO TOKEN',
        N'f2de850384bc43b7124c08f2ca4e94bf"',
        N'TWILLIO'
    ),
    (
        N'TWILLIO_NUMBER',
        N'TWILLIO NUMBER',
        N'whatsapp:+14155238886',
        N'TWILLIO'
    )


USE [SIAC_CCA_BEFORE_DEMO]
GO

/****** Object:  Table [dbo].[notificaciones]    Script Date: 1/9/2024 17:01:02 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO
DROP TABLE [dbo].[notificaciones]
GO
CREATE TABLE [dbo].[notificaciones]
(
    [Id] [int] IDENTITY(1,1) NOT NULL,
    [Id_User] [int] NULL,
    [Mensaje] [nvarchar](max) NULL,
    [Fecha] [datetime] NULL,
    [Media] [nvarchar](max) NULL,
    [Tipo] [nvarchar](50) NULL,
    [Estado] [nvarchar](50) NULL,
    [Enviado] [bit] NULL,
    [Leido] [bit] NULL,
    [Telefono] [nvarchar](20) NULL,
    [Email] [nvarchar](50) NULL,
    CONSTRAINT [PK_notificaciones] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO


