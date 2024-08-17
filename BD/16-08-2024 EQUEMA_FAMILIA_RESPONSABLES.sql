EXEC SIAC_CCA.sys.sp_rename N'SIAC_CCA.dbo.responsables', N'estudiantes_responsables_familia', 'OBJECT';

ALTER TABLE SIAC_CCA.dbo.estudiantes_responsables_familias ADD familia_id int NULL;
ALTER TABLE SIAC_CCA.dbo.estudiantes_responsables_familias ALTER COLUMN responsable_id int NOT NULL;
ALTER TABLE SIAC_CCA.dbo.estudiantes_responsables_familias ALTER COLUMN estudiante_id int NOT NULL;


---EXEC SIAC_CCA.sys.sp_rename N'SIAC_CCA.dbo.parientes', N'tutores', 'OBJECT';
ALTER TABLE SIAC_CCA.dbo.parientes ADD pais_id int NULL;
ALTER TABLE SIAC_CCA.dbo.parientes ADD responsablepago bit NULL;
ALTER TABLE SIAC_CCA.dbo.parientes ADD noidentificacion varchar(100) NULL;


CREATE TABLE SIAC_CCA.dbo.familias (
	id int IDENTITY(1,1) PRIMARY KEY, 
	descripcion varchar(200) NOT NULL,
	estado bit NULL,
	saldo float DEFAULT 0 NULL,
	actualizado bit NULL,
	aceptacion bit NULL,
	periodo_aceptacion int NULL,
	fecha_actualizacion date NULL,
	fecha_ultima_notificacion varchar(100) NULL,
	id_usuario int NULL, 
	CONSTRAINT FK_familias_usuario FOREIGN KEY (id_usuario) REFERENCES security.Security_Users(Id_User)
);

ALTER TABLE SIAC_CCA.dbo.estudiantes_responsables_familias 
ADD CONSTRAINT FK_estudiantes_responsables_familia_familias FOREIGN KEY (familia_id) REFERENCES SIAC_CCA.dbo.familias(id);