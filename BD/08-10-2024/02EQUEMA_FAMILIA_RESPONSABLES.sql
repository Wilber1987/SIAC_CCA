EXEC sys.sp_rename N'dbo.responsables', N'estudiantes_responsables_familia', 'OBJECT';

EXEC sp_rename 'dbo.estudiantes_responsables_familia.pariente_id', 'responsable_id', 'COLUMN';

delete from dbo.estudiantes_responsables_familia

ALTER TABLE dbo.estudiantes_responsables_familia ADD familia_id int NULL;-- ejecutar si no existe columna
ALTER TABLE dbo.estudiantes_responsables_familia ALTER COLUMN responsable_id int NOT NULL;
ALTER TABLE dbo.estudiantes_responsables_familia ALTER COLUMN estudiante_id int NOT NULL;


---EXEC sys.sp_rename N'dbo.parientes', N'tutores', 'OBJECT';
ALTER TABLE dbo.parientes ADD Id_pais int NULL;
ALTER TABLE dbo.parientes ADD responsablepago bit NULL;
ALTER TABLE dbo.parientes ADD noidentificacion varchar(100) NULL;


CREATE TABLE dbo.familias (
    id int PRIMARY KEY,
    idtfamilia varchar(20) NOT NULL,
    descripcion varchar(200) NOT NULL,
    estado bit NULL,
    saldo float DEFAULT 0 NULL,
    actualizado bit NULL,
    aceptacion bit NULL,
    periodo_aceptacion int NULL,
    fecha_actualizacion date NULL,
    fecha_ultima_notificacion date NULL  
);


ALTER TABLE dbo.estudiantes_responsables_familia 
ADD CONSTRAINT FK_estudiantes_responsables_familia_familia FOREIGN KEY (familia_id) REFERENCES dbo.familias(id);

ALTER TABLE dbo.parientes
ADD id_usuario int NULL;

ALTER TABLE dbo.parientes
ADD CONSTRAINT FK_parientes_usuario FOREIGN KEY (id_usuario) REFERENCES security.Security_Users(Id_User);

