

ALTER TABLE dbo.materias ADD MateriasConfig nvarchar(MAX) NULL;


-- siac_cca_production.mensaje_adjuntos definition

CREATE TABLE mensaje_adjuntos (
  id INT NOT NULL PRIMARY KEY,
  mensaje_id INT NULL,
  archivo NVARCHAR(255) NULL,
  created_at DATETIME NOT NULL,
  updated_at DATETIME NOT NULL,
  INDEX index_mensaje_adjuntos_on_mensaje_id (mensaje_id)
);

CREATE TABLE conversacion (
    id_conversacion INT IDENTITY(1, 1) NOT NULL PRIMARY KEY,
    descripcion NVARCHAR(255) NULL
);
CREATE TABLE conversacion_usuarios (
    id_conversacion INT NOT NULL ,
    id_usuario INT NOT NULL 
);
-- siac_cca_production.mensajes definition
CREATE TABLE mensajes (
    id_mensaje INT IDENTITY(1, 1) NOT  NULL PRIMARY KEY,
    remitente NVARCHAR(255) NULL,
    usuario_id INT NULL,
    destinatarios NVARCHAR(max) NULL,
    asunto NVARCHAR(255) NULL,
    body NVARCHAR(MAX) NULL,
    id_conversacion INT NULL,
    created_at DATETIME NOT NULL,
    updated_at DATETIME NOT NULL,
    enviado BIT NULL,
    leido BIT NULL
);


ALTER TABLE [security].Security_Users ADD Nombres_Completo nvarchar(500) NULL;

ALTER TABLE dbo.docentes ADD Id_User int NULL;
ALTER TABLE dbo.parientes ADD Id_User int NULL;

ALTER TABLE dbo.conversacion_usuarios ADD CONSTRAINT conversacion_usuarios_PK PRIMARY KEY (id_conversacion,id_usuario);


ALTER TABLE dbo.conversacion_usuarios ADD CONSTRAINT conversacion_usuarios_FK FOREIGN KEY (id_conversacion) REFERENCES dbo.conversacion(id_conversacion);

ALTER TABLE dbo.conversacion_usuarios ADD avatar nvarchar(250) NULL;
ALTER TABLE dbo.conversacion_usuarios ADD name nvarchar(250) NULL;



