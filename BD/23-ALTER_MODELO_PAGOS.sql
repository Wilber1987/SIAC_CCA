-- Agregar columnas a la tabla facturacion.Tbl_Pago
ALTER TABLE facturacion.Tbl_Pago ADD
    Id_plazo INT NULL,
    Anio INT NULL,
    Id_documento_cc INT NULL,
    Id_documento INT NULL,
    Id_clase_documento INT NULL,
    Fecha_documento DATETIME NULL,
    Fecha_contabilizacion DATETIME NULL,
    Ejercicio INT NULL,
    Periodo INT NULL,
    No_documento INT NULL,
    Id_deudor INT NULL,
    Asignacion NVARCHAR(MAX) NULL,
    Texto_posicion NVARCHAR(MAX) NULL,
    Id_cuenta INT NULL,
    Id_indicador_impuesto INT NULL,
    Id_documento_detalle INT NULL,   
    Fecha_anulacion DATETIME NULL,
    Usuario_anulacion NVARCHAR(MAX) NULL,
    Texto_corto NVARCHAR(MAX) NULL,
    Simbolo NVARCHAR(MAX) NULL;


