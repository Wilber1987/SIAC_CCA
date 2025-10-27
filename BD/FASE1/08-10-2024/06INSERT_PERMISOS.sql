
-- PERMISOS  ADMIN
insert into security.Security_Permissions (descripcion,estado, detalles)
values
        ('GESTION_ESTUDIANTES_PROPIOS', 'ACTIVO', null),
        ('NOTIFICACIONES', 'ACTIVO', null),
        ('GESTION_ESTUDIANTES', 'ACTIVO', null),
        ('CAN_CHANGE_PASSWORD', 'ACTIVO', null),
        ('CAN_CHANGE_OW_PASSWORD', 'ACTIVO', null), 
		('SEND_MESSAGE', 'ACTIVO', null),
		('GESTION_CLASES', 'ACTIVO', null),
		('GESTION_CLASES_ASIGNADAS', 'ACTIVO', NULL)

        -- TODO ACTUALIZAR DESCRIPCIONES DE LOS PERMISOS ANTERIORES