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
    user_id int null,
    no_responsable INT NULL;

ALTER TABLE Parientes
drop COLUMN Id_Responsable_Pago;

EXEC sp_rename 'Parientes.responsablepago', 'resoponsable_pago', 'COLUMN';


ALTER TABLE Estudiantes
ADD  id_familia INT;

ALTER TABLE Estudiantes ADD Idtestudiante INT NULL;
ALTER TABLE Estudiantes ADD periodo INT NULL;
ALTER TABLE Estudiantes ADD fecha_ingreso DATETIME NULL;
ALTER TABLE Estudiantes ADD id_pais INT NULL;
ALTER TABLE Estudiantes ADD id_sociedad INT NULL;
ALTER TABLE Estudiantes ADD id_region INT NULL;
ALTER TABLE Estudiantes ADD solvencia VARCHAR(255) NULL;
ALTER TABLE Estudiantes ADD saldomd DECIMAL(18, 2) NULL;
ALTER TABLE Estudiantes ADD estatus VARCHAR(255) NULL;
ALTER TABLE Estudiantes ADD retenido BIT NULL;
ALTER TABLE Estudiantes ADD referencia_estatus VARCHAR(255) NULL;
ALTER TABLE Estudiantes ADD usuario_grabacion VARCHAR(255) NULL;
ALTER TABLE Estudiantes ADD usuario_modificacion VARCHAR(255) NULL;
ALTER TABLE Estudiantes ADD id_old VARCHAR(255) NULL;
ALTER TABLE Estudiantes ADD id_cliente INT NULL;
ALTER TABLE Estudiantes ADD codigomed VARCHAR(255) NULL;
ALTER TABLE Estudiantes ADD ump INT NULL;
ALTER TABLE Estudiantes ADD uep INT NULL;
ALTER TABLE Estudiantes ADD colegio VARCHAR(255) NULL;
ALTER TABLE Estudiantes ADD vivecon VARCHAR(255) NULL;
ALTER TABLE Estudiantes ADD sacramento VARCHAR(255) NULL;
ALTER TABLE Estudiantes ADD aniosacra INT NULL;
ALTER TABLE Estudiantes ADD fecha_aceptacion DATETIME NULL;
ALTER TABLE Estudiantes ADD usuario_aceptacion VARCHAR(255) NULL;
ALTER TABLE Estudiantes ADD aceptacion BIT NULL;
ALTER TABLE Estudiantes ADD periodo_aceptacion INT NULL;
ALTER TABLE Estudiantes ADD fechaun DATETIME NULL;
ALTER TABLE Estudiantes ADD motivo VARCHAR(255) NULL;
ALTER TABLE Estudiantes ADD comentario VARCHAR(255) NULL;
ALTER TABLE Estudiantes ADD fecha_retencion DATETIME NULL;
ALTER TABLE Estudiantes ADD saldo_eamd DECIMAL(18, 2) NULL;



ALTER TABLE Parientes ADD  id_familia INT;

CREATE TABLE Parentesco (
    Id INT NOT NULL,
    Sigla NVARCHAR(100) NOT NULL,
    Parentesco_id int null,
    Descripcion NVARCHAR(100) NOT NULL
);

ALTER TABLE Parientes ADD  id_relacion_familiar INT;


ALTER TABLE estudiantes_responsables_familia
DROP CONSTRAINT PK__responsa__3213E83F886C0F8F;

ALTER TABLE estudiantes_responsables_familia
DROP COLUMN Id;

ALTER TABLE estudiantes_responsables_familia
ADD Id INT IDENTITY(1,1) PRIMARY KEY;


EXEC sys.sp_rename N'dbo.estudiantes_responsables_familia.responsable_id' , N'pariente_id', 'COLUMN';
EXEC sys.sp_rename N'dbo.estudiantes_responsables_familia.parentesco' , N'Parentesco_id', 'COLUMN';

EXEC sp_rename 'Parientes.user_id', 'id_user', 'COLUMN';
