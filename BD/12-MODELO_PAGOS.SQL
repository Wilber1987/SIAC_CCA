CREATE TABLE [dbo].[PagosRequest]
(
    [Id] [int] IDENTITY(1,1) NOT NULL,
    [Id_User] [int] NULL,
    [Responsable_Id] [int] NULL,
    [Referencia] [nvarchar](250) NULL,
    [Descripcion] [nvarchar](800) NULL,
    [Monto] [float] NULL,
    [Moneda] [nvarchar](250) NULL,
    [Fecha] [datetime] NULL,
    [Pagos] [nvarchar](max) NULL,    
    [Creador] [nvarchar](max) NULL,
    [Estado] [nvarchar](50) NULL, 
    CONSTRAINT [PK_PagosRequest] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
