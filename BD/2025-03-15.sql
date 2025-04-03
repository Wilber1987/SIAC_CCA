ALTER TABLE OLIMPO.dbo.parientes ADD Foto nvarchar(500) NULL;


INSERT INTO administrative_access.Transactional_Configuraciones (Nombre, Descripcion, Valor, Tipo_Configuracion)
VALUES(
        N'RUC',
        N'Ruc',
        N'J0810000169317',
        N'THEME'
    );


    ALTER TABLE Facturacion.Detalle_Pago ADD Fecha DateTime ;
