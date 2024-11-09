ALTER TABLE SIAC_CCA.[security].Security_Users ADD Nombres_Completo nvarchar(500) NULL;

ALTER TABLE SIAC_CCA.dbo.docentes ADD Id_User int NULL;
ALTER TABLE SIAC_CCA.dbo.parientes ADD Id_User int NULL;

ALTER TABLE SIAC_CCA.dbo.conversacion_usuarios ADD CONSTRAINT conversacion_usuarios_PK PRIMARY KEY (id_conversacion,id_usuario);


ALTER TABLE SIAC_CCA.dbo.conversacion_usuarios ADD CONSTRAINT conversacion_usuarios_FK FOREIGN KEY (id_conversacion) REFERENCES SIAC_CCA.dbo.conversacion(id_conversacion);

ALTER TABLE SIAC_CCA.dbo.conversacion_usuarios ADD avatar nvarchar(250) NULL;
ALTER TABLE SIAC_CCA.dbo.conversacion_usuarios ADD name nvarchar(250) NULL;



