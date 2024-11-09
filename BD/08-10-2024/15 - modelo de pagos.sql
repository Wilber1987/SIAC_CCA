DROP  table Detalle_Pago;
drop table PagosRequest;
DROP table Tbl_Pago;

DROP  table facturacion.Detalle_Pago;
drop table facturacion.PagosRequest;
DROP table facturacion.Tbl_Pago;

create schema facturacion;
go

CREATE TABLE facturacion.PagosRequest (
    Id_Pago_Request INT IDENTITY(1,1) PRIMARY KEY,
    Referencia NVARCHAR(MAX),
    Descripcion NVARCHAR(MAX),
    Responsable_Id INT,
    Id_User INT,
    Fecha DATETIME,
    Creador NVARCHAR(MAX),
    Estado NVARCHAR(MAX),
    Monto FLOAT,
    Moneda NVARCHAR(MAX)
);
go
CREATE TABLE facturacion.Tbl_Pago (
    Id_Pago INT IDENTITY(1,1) PRIMARY KEY,
    Estudiante_Id INT,
    Responsable_Id INT,
    Monto FLOAT,
    Monto_Pagado FLOAT,
    Monto_Pendiente FLOAT,
    Periodo_lectivo NVARCHAR(MAX),
    Documento NVARCHAR(MAX),
    Concepto NVARCHAR(MAX),
    Mes NVARCHAR(MAX),
    Money NVARCHAR(MAX),  -- Asumimos que `MoneyEnum` se mapea a NVARCHAR
    Fecha_Pago DATETIME,
    Fecha_Limite DATETIME,
    Fecha DATETIME,
    Estado NVARCHAR(MAX)
);
go
CREATE TABLE facturacion.Detalle_Pago (
    Id_Detalle INT IDENTITY(1,1) PRIMARY KEY,
    Id_Pago INT,
    Total FLOAT,
    Cantidad FLOAT,
    Monto FLOAT,
    Impuesto FLOAT,
    Concepto NVARCHAR(MAX),
    Id_Pago_Request INT,
    Estado_Anterior_Pago NVARCHAR(MAX),  -- Este es el campo JSONProp

    CONSTRAINT FK_Detalle_Pago_PagosRequest FOREIGN KEY (Id_Pago_Request) REFERENCES facturacion.PagosRequest(Id_Pago_Request),
    CONSTRAINT FK_Detalle_Pago_Tbl_Pago FOREIGN KEY (Id_Pago) REFERENCES facturacion.Tbl_Pago(Id_Pago)
);
