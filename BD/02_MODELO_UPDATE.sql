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
	id_religion int NULL,
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
	id_region int NULL,
	Id_pais int NULL,
	Idbellacom int NULL,
	vivecon nvarchar(50) COLLATE Modern_Spanish_CI_AS NULL,
	sacramento nvarchar(50) COLLATE Modern_Spanish_CI_AS NULL,
	aniosacra int NULL,
	colegio_procede nvarchar(250) COLLATE Modern_Spanish_CI_AS NULL,


    puntos_transportes nvarchar(max) COLLATE Modern_Spanish_CI_AS  NULL,
    usa_transporte bit NULL,   
    
	CONSTRAINT PK__estudian__3213E83FD683993A PRIMARY KEY (id)
);
 CREATE NONCLUSTERED INDEX update_index_estudiantes_on_madre_id ON update_data.estudiantes_data_update (  madre_id ASC  )  
	 WITH (  PAD_INDEX = OFF ,FILLFACTOR = 100  ,SORT_IN_TEMPDB = OFF , IGNORE_DUP_KEY = OFF , STATISTICS_NORECOMPUTE = OFF , ONLINE = OFF , ALLOW_ROW_LOCKS = ON , ALLOW_PAGE_LOCKS = ON  )
	 ON [PRIMARY ] ;
 CREATE NONCLUSTERED INDEX update_index_estudiantes_on_padre_id ON update_data.estudiantes_data_update (  padre_id ASC  )  
	 WITH (  PAD_INDEX = OFF ,FILLFACTOR = 100  ,SORT_IN_TEMPDB = OFF , IGNORE_DUP_KEY = OFF , STATISTICS_NORECOMPUTE = OFF , ONLINE = OFF , ALLOW_ROW_LOCKS = ON , ALLOW_PAGE_LOCKS = ON  )
	 ON [PRIMARY ] ;
 CREATE NONCLUSTERED INDEX update_index_estudiantes_on_recorrido_id ON update_data.estudiantes_data_update (  recorrido_id ASC  )  
	 WITH (  PAD_INDEX = OFF ,FILLFACTOR = 100  ,SORT_IN_TEMPDB = OFF , IGNORE_DUP_KEY = OFF , STATISTICS_NORECOMPUTE = OFF , ONLINE = OFF , ALLOW_ROW_LOCKS = ON , ALLOW_PAGE_LOCKS = ON  )
	 ON [PRIMARY ] ;
 CREATE NONCLUSTERED INDEX update_index_estudiantes_on_Id_religion ON update_data.estudiantes_data_update (  Id_religion ASC  )  
	 WITH (  PAD_INDEX = OFF ,FILLFACTOR = 100  ,SORT_IN_TEMPDB = OFF , IGNORE_DUP_KEY = OFF , STATISTICS_NORECOMPUTE = OFF , ONLINE = OFF , ALLOW_ROW_LOCKS = ON , ALLOW_PAGE_LOCKS = ON  )
	 ON [PRIMARY ] ;
 CREATE NONCLUSTERED INDEX update_index_estudiantes_on_madre_id ON update_data.estudiantes_data_update (  madre_id ASC  )  
	 WITH (  PAD_INDEX = OFF ,FILLFACTOR = 100  ,SORT_IN_TEMPDB = OFF , IGNORE_DUP_KEY = OFF , STATISTICS_NORECOMPUTE = OFF , ONLINE = OFF , ALLOW_ROW_LOCKS = ON , ALLOW_PAGE_LOCKS = ON  )
	 ON [PRIMARY ] ;
 CREATE NONCLUSTERED INDEX update_index_estudiantes_on_padre_id ON update_data.estudiantes_data_update (  padre_id ASC  )  
	 WITH (  PAD_INDEX = OFF ,FILLFACTOR = 100  ,SORT_IN_TEMPDB = OFF , IGNORE_DUP_KEY = OFF , STATISTICS_NORECOMPUTE = OFF , ONLINE = OFF , ALLOW_ROW_LOCKS = ON , ALLOW_PAGE_LOCKS = ON  )
	 ON [PRIMARY ] ;
 CREATE NONCLUSTERED INDEX update_index_estudiantes_on_recorrido_id ON update_data.estudiantes_data_update (  recorrido_id ASC  )  
	 WITH (  PAD_INDEX = OFF ,FILLFACTOR = 100  ,SORT_IN_TEMPDB = OFF , IGNORE_DUP_KEY = OFF , STATISTICS_NORECOMPUTE = OFF , ONLINE = OFF , ALLOW_ROW_LOCKS = ON , ALLOW_PAGE_LOCKS = ON  )
	 ON [PRIMARY ] ;
 CREATE NONCLUSTERED INDEX update_index_estudiantes_on_Id_religion ON update_data.estudiantes_data_update (  Id_religion ASC  )  
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
	id_profesion int NULL,
	direccion nvarchar(255) COLLATE Modern_Spanish_CI_AS NULL,
	lugar_trabajo nvarchar(255) COLLATE Modern_Spanish_CI_AS NULL,
	telefono nvarchar(255) COLLATE Modern_Spanish_CI_AS NULL,
	celular nvarchar(255) COLLATE Modern_Spanish_CI_AS NULL,
	telefono_trabajo nvarchar(255) COLLATE Modern_Spanish_CI_AS NULL,
	email nvarchar(255) COLLATE Modern_Spanish_CI_AS NULL,
	estado_civil_id int NULL,
	id_religion int NULL,
	created_at datetime NULL,
	updated_at datetime NULL,
	resoponsable_pago bit NULL,
	noidentificacion varchar(100) COLLATE Modern_Spanish_CI_AS NULL,
	id_titulo int NULL,
	Id_Region int NULL,
	id_estado_civil int NULL,
	id_responsable_pago int NULL,
	ex_alumno nvarchar(50) COLLATE Modern_Spanish_CI_AS NULL,
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
	identificacion nvarchar(150) COLLATE Modern_Spanish_CI_AS NULL,
	id_pais int NULL,
    egresoexalumno int null,

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
 CREATE NONCLUSTERED INDEX update_index_parientes_on_Id_religion ON dbo.parientes (  Id_religion ASC  )  
	 WITH (  PAD_INDEX = OFF ,FILLFACTOR = 100  ,SORT_IN_TEMPDB = OFF , IGNORE_DUP_KEY = OFF , STATISTICS_NORECOMPUTE = OFF , ONLINE = OFF , ALLOW_ROW_LOCKS = ON , ALLOW_PAGE_LOCKS = ON  )
	 ON [PRIMARY ] ;
 CREATE NONCLUSTERED INDEX update_index_parientes_on_estado_civil_id ON dbo.parientes (  estado_civil_id ASC  )  
	 WITH (  PAD_INDEX = OFF ,FILLFACTOR = 100  ,SORT_IN_TEMPDB = OFF , IGNORE_DUP_KEY = OFF , STATISTICS_NORECOMPUTE = OFF , ONLINE = OFF , ALLOW_ROW_LOCKS = ON , ALLOW_PAGE_LOCKS = ON  )
	 ON [PRIMARY ] ;
 CREATE NONCLUSTERED INDEX update_index_parientes_on_Id_religion ON dbo.parientes (  Id_religion ASC  )  
	 WITH (  PAD_INDEX = OFF ,FILLFACTOR = 100  ,SORT_IN_TEMPDB = OFF , IGNORE_DUP_KEY = OFF , STATISTICS_NORECOMPUTE = OFF , ONLINE = OFF , ALLOW_ROW_LOCKS = ON , ALLOW_PAGE_LOCKS = ON  )
	 ON [PRIMARY ] ;


-- dbo.parientes foreign keys

ALTER TABLE update_data.parientes_data_update ADD CONSTRAINT FK_parientes_update_Security_Users FOREIGN KEY (user_id) REFERENCES [security].Security_Users(Id_User);
ALTER TABLE SIAC_CCA_BEFORE_DEMO.update_data.parientes_data_update ADD CONSTRAINT FK_parientes_update_estadocivil FOREIGN KEY (estado_civil_id) REFERENCES SIAC_CCA_BEFORE_DEMO.dbo.estados_civiles(id);
ALTER TABLE SIAC_CCA_BEFORE_DEMO.update_data.parientes_data_update ADD CONSTRAINT FK_parientes_update_religion FOREIGN KEY (id_religion) REFERENCES SIAC_CCA_BEFORE_DEMO.dbo.religiones(id);

alter table [update_data].[parientes_data_update] add profesion nvarchar(250) null;

CREATE TABLE OLIMPO.update_data.UpdatedData (
	Id int IDENTITY(0,1) NOT NULL,
	DataContract nvarchar(MAX) NULL,
	Documents_Boletas nvarchar(MAX) NULL,
	Documents_Contracts nvarchar(MAX) NULL,
	CONSTRAINT ContractsData_PK PRIMARY KEY (Id)
);
