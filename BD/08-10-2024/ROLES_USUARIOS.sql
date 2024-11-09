---ROLES

INSERT INTO SIAC_CCA_BEFORE_DEMO.[security].Security_Roles
(Id_Role, Descripcion, Estado)
VALUES(1, N'ADMIN', N'ACTIVO');
INSERT INTO SIAC_CCA_BEFORE_DEMO.[security].Security_Roles
(Id_Role, Descripcion, Estado)
VALUES(2, N'PADRE_RESPONSABLE', N'ACTIVO');


-- PERMISOS
 

INSERT INTO [security].Security_Permissions
(Id_Permission, Descripcion, Estado, Detalles)
VALUES(1, N'ADMIN_ACCESS', N'Activo', N'ACCESO TOTAL DEL SISTEMA');
INSERT INTO [security].Security_Permissions
(Id_Permission, Descripcion, Estado, Detalles)
VALUES(2, N'PERFIL_ACCESS', N'Activo', N'PERMITE ACCEDER AL PERFIL DEL USUARIO');
INSERT INTO [security].Security_Permissions
(Id_Permission, Descripcion, Estado, Detalles)
VALUES(3, N'PERFIL_MANAGER', N'Activo', N'PERMITE MANIPULAR PERFILES DE LOS USUARIO');
INSERT INTO [security].Security_Permissions
(Id_Permission, Descripcion, Estado, Detalles)
VALUES(4, N'GESTION_ESTUDIANTES_PROPIOS', N'ACTIVO', NULL);
INSERT INTO [security].Security_Permissions
(Id_Permission, Descripcion, Estado, Detalles)
VALUES(5, N'NOTIFICACIONES', N'ACTIVO', NULL);
INSERT INTO [security].Security_Permissions
(Id_Permission, Descripcion, Estado, Detalles)
VALUES(6, N'GESTION_ESTUDIANTES', N'ACTIVO', NULL);
INSERT INTO [security].Security_Permissions
(Id_Permission, Descripcion, Estado, Detalles)
VALUES(7, N'CAN_CHANGE_PASSWORD', N'ACTIVO', NULL);
INSERT INTO [security].Security_Permissions
(Id_Permission, Descripcion, Estado, Detalles)
VALUES(8, N'CAN_CHANGE_OW_PASSWORD', N'ACTIVO', NULL);
INSERT INTO [security].Security_Permissions
(Id_Permission, Descripcion, Estado, Detalles)
VALUES(9, N'SEND_MESSAGE', N'ACTIVO', NULL);
INSERT INTO [security].Security_Permissions
(Id_Permission, Descripcion, Estado, Detalles)
VALUES(10, N'GESTION_CLASES', N'ACTIVO', NULL);
INSERT INTO [security].Security_Permissions
(Id_Permission, Descripcion, Estado, Detalles)
VALUES(11, N'GESTION_CLASES_ASIGNADAS', N'ACTIVO', NULL);
INSERT INTO [security].Security_Permissions
(Id_Permission, Descripcion, Estado, Detalles)
VALUES(12, N'NOTIFICACIONES_READER', N'ACTIVO', NULL);

--- PERMISOS ROLES
INSERT INTO [security].Security_Permissions_Roles
(Id_Role, Id_Permission, Estado)
VALUES(1, 1, N'ACTIVO');
INSERT INTO [security].Security_Permissions_Roles
(Id_Role, Id_Permission, Estado)
VALUES(2, 2, N'Activo');
INSERT INTO [security].Security_Permissions_Roles
(Id_Role, Id_Permission, Estado)
VALUES(2, 3, N'ACTIVO');
INSERT INTO [security].Security_Permissions_Roles
(Id_Role, Id_Permission, Estado)
VALUES(2, 4, N'ACTIVO');
INSERT INTO [security].Security_Permissions_Roles
(Id_Role, Id_Permission, Estado)
VALUES(2, 5, N'ACTIVO');
INSERT INTO [security].Security_Permissions_Roles
(Id_Role, Id_Permission, Estado)
VALUES(2, 8, N'ACTIVO');
INSERT INTO [security].Security_Permissions_Roles
(Id_Role, Id_Permission, Estado)
VALUES(2, 12, N'ACTIVO');