ALTER TABLE Parientes ADD
    id_titulo INT NULL,
    id_region INT NULL,
    id_estado_civil INT NULL,
    ex_alumno NVARCHAR(5) NULL,
    fecha_nacimiento DATETIME NULL,
    fecha_modificacion DATETIME NULL,
    usuario_grabacion NVARCHAR(50) NULL,
    usuario_edicion NVARCHAR(50)  NULL,
    ejercicio FLOAT NULL,
    actualizado BIT NULL,
    no_responsable INT NULL;

ALTER TABLE Parientes
drop COLUMN Id_Responsable_Pago;

EXEC sp_rename 'Parientes.responsablepago', 'resoponsable_pago', 'COLUMN';
