ALTER TABLE [security].Security_Users ADD Password_Expiration_Date datetime NULL;

CREATE SCHEMA update_data;
-- dbo.estudiantes definition

-- Drop table
DROP TABLE update_data.estudiantes_data_update;

CREATE TABLE update_data.estudiantes_data_update (
	id int NOT NULL,
	primer_nombre nvarchar(255) COLLATE Modern_Spanish_CI_AS NOT NULL,
	segundo_nombre nvarchar(255) COLLATE Modern_Spanish_CI_AS NULL,
	primer_apellido nvarchar(255) COLLATE Modern_Spanish_CI_AS NOT NULL,
	segundo_apellido nvarchar(255) COLLATE Modern_Spanish_CI_AS NULL,
	fecha_nacimiento date NULL,
	lugar_nacimiento nvarchar(255) COLLATE Modern_Spanish_CI_AS NULL,
	sexo nvarchar(255) COLLATE Modern_Spanish_CI_AS NULL,
	direccion nvarchar(255) COLLATE Modern_Spanish_CI_AS NULL,
	codigo nvarchar(255) COLLATE Modern_Spanish_CI_AS NULL,
	religion_id int NULL,
	madre_id int NULL,
	padre_id int NULL,
	created_at datetime NULL,
	updated_at datetime NULL,
	foto nvarchar(255) COLLATE Modern_Spanish_CI_AS NULL,
	peso float NULL,
	altura float NULL,
	tipo_sangre nvarchar(255) COLLATE Modern_Spanish_CI_AS NULL,
	padecimientos nvarchar(MAX) COLLATE Modern_Spanish_CI_AS NULL,
	alergias nvarchar(MAX) COLLATE Modern_Spanish_CI_AS NULL,
	recorrido_id int NULL,
	activo bit NULL,
	id_familia int NULL,
	Idtestudiante int NULL,
	fecha_ingreso datetime NULL,
	id_cliente int NULL,
	codigomed varchar(255) COLLATE Modern_Spanish_CI_AS NULL,
	saldo_eamd decimal(18,2) NULL,

    puntos_transportes nvarchar(max) COLLATE Modern_Spanish_CI_AS NOT NULL,
    usa_transporte bit NULL,   
    
	CONSTRAINT PK__estudian__3213E83FD683993A PRIMARY KEY (id)
);
 CREATE NONCLUSTERED INDEX update_index_estudiantes_on_madre_id ON dbo.estudiantes (  madre_id ASC  )  
	 WITH (  PAD_INDEX = OFF ,FILLFACTOR = 100  ,SORT_IN_TEMPDB = OFF , IGNORE_DUP_KEY = OFF , STATISTICS_NORECOMPUTE = OFF , ONLINE = OFF , ALLOW_ROW_LOCKS = ON , ALLOW_PAGE_LOCKS = ON  )
	 ON [PRIMARY ] ;
 CREATE NONCLUSTERED INDEX update_index_estudiantes_on_padre_id ON dbo.estudiantes (  padre_id ASC  )  
	 WITH (  PAD_INDEX = OFF ,FILLFACTOR = 100  ,SORT_IN_TEMPDB = OFF , IGNORE_DUP_KEY = OFF , STATISTICS_NORECOMPUTE = OFF , ONLINE = OFF , ALLOW_ROW_LOCKS = ON , ALLOW_PAGE_LOCKS = ON  )
	 ON [PRIMARY ] ;
 CREATE NONCLUSTERED INDEX update_index_estudiantes_on_recorrido_id ON dbo.estudiantes (  recorrido_id ASC  )  
	 WITH (  PAD_INDEX = OFF ,FILLFACTOR = 100  ,SORT_IN_TEMPDB = OFF , IGNORE_DUP_KEY = OFF , STATISTICS_NORECOMPUTE = OFF , ONLINE = OFF , ALLOW_ROW_LOCKS = ON , ALLOW_PAGE_LOCKS = ON  )
	 ON [PRIMARY ] ;
 CREATE NONCLUSTERED INDEX update_index_estudiantes_on_religion_id ON dbo.estudiantes (  religion_id ASC  )  
	 WITH (  PAD_INDEX = OFF ,FILLFACTOR = 100  ,SORT_IN_TEMPDB = OFF , IGNORE_DUP_KEY = OFF , STATISTICS_NORECOMPUTE = OFF , ONLINE = OFF , ALLOW_ROW_LOCKS = ON , ALLOW_PAGE_LOCKS = ON  )
	 ON [PRIMARY ] ;


-- dbo.parientes definition

-- Drop table
DROP TABLE update_data.parientes_data_update

CREATE TABLE update_data.parientes_data_update (
	id int NOT NULL,
	primer_nombre nvarchar(255) COLLATE Modern_Spanish_CI_AS NOT NULL,
	segundo_nombre nvarchar(255) COLLATE Modern_Spanish_CI_AS NULL,
	primer_apellido nvarchar(255) COLLATE Modern_Spanish_CI_AS NOT NULL,
	segundo_apellido nvarchar(255) COLLATE Modern_Spanish_CI_AS NULL,
	sexo nvarchar(255) COLLATE Modern_Spanish_CI_AS NULL,
	profesion nvarchar(255) COLLATE Modern_Spanish_CI_AS NULL,
	direccion nvarchar(255) COLLATE Modern_Spanish_CI_AS NULL,
	lugar_trabajo nvarchar(255) COLLATE Modern_Spanish_CI_AS NULL,
	telefono nvarchar(255) COLLATE Modern_Spanish_CI_AS NULL,
	celular nvarchar(255) COLLATE Modern_Spanish_CI_AS NULL,
	telefono_trabajo nvarchar(255) COLLATE Modern_Spanish_CI_AS NULL,
	email nvarchar(255) COLLATE Modern_Spanish_CI_AS NULL,
	estado_civil_id int NULL,
	religion_id int NULL,
	created_at datetime NULL,
	updated_at datetime NULL,
	pais_id int NULL,
	resoponsable_pago bit NULL,
	noidentificacion varchar(100) COLLATE Modern_Spanish_CI_AS NULL,
	id_titulo int NULL,
	id_region int NULL,
	id_estado_civil int NULL,
	id_responsable_pago int NULL,
	ex_alumno nvarchar(MAX) COLLATE Modern_Spanish_CI_AS NULL,
	fecha_nacimiento datetime NULL,
	fecha_modificacion datetime NULL,
	usuario_grabacion nvarchar(50) COLLATE Modern_Spanish_CI_AS NULL,
	usuario_edicion nvarchar(50) COLLATE Modern_Spanish_CI_AS NULL,
	ejercicio float NULL,
	actualizado bit NULL,
	no_responsable int NULL,
	user_id int NULL,
	id_relacion_familiar int NULL,
	id_familia int NULL,


    correo_enviado bit NULL,
    actualizo bit NULL,
    fecha_actualizacion datetime NULL,
    acepto_terminos bit NULL,
    entro_al_sistema bit NULL,
    fecha_ingreso_al_sistema datetime NULL,
    ip_ingreso nvarchar(255) COLLATE Modern_Spanish_CI_AS NULL,

	CONSTRAINT PK__pariente__3213E83FC3C303EF PRIMARY KEY (id)
);
 CREATE NONCLUSTERED INDEX update_index_parientes_on_estado_civil_id ON dbo.parientes (  estado_civil_id ASC  )  
	 WITH (  PAD_INDEX = OFF ,FILLFACTOR = 100  ,SORT_IN_TEMPDB = OFF , IGNORE_DUP_KEY = OFF , STATISTICS_NORECOMPUTE = OFF , ONLINE = OFF , ALLOW_ROW_LOCKS = ON , ALLOW_PAGE_LOCKS = ON  )
	 ON [PRIMARY ] ;
 CREATE NONCLUSTERED INDEX update_index_parientes_on_religion_id ON dbo.parientes (  religion_id ASC  )  
	 WITH (  PAD_INDEX = OFF ,FILLFACTOR = 100  ,SORT_IN_TEMPDB = OFF , IGNORE_DUP_KEY = OFF , STATISTICS_NORECOMPUTE = OFF , ONLINE = OFF , ALLOW_ROW_LOCKS = ON , ALLOW_PAGE_LOCKS = ON  )
	 ON [PRIMARY ] ;


-- dbo.parientes foreign keys

ALTER TABLE dbo.parientes ADD CONSTRAINT FK_parientes_Security_Users FOREIGN KEY (user_id) REFERENCES [security].Security_Users(Id_User);