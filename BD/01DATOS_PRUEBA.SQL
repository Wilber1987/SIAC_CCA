
INSERT INTO SIAC_CCA.dbo.parientes
( primer_nombre, segundo_nombre, primer_apellido, segundo_apellido, sexo, profesion, 
direccion, lugar_trabajo, telefono, celular, telefono_trabajo, email, estado_civil_id, 
religion_id, created_at, updated_at)
VALUES(1, 'Papa', 'Prueba', 'Prueba', 'Prueba', 'Masculino', '-', 'jinotepe', 'jinotepe',
'8888-8888', '8888-8888', '8888-8888', 'admin@admin.net', null, null, '2000-01-01', '2000-01-01');


INSERT INTO SIAC_CCA.dbo.estudiantes
(id, primer_nombre, segundo_nombre, primer_apellido, segundo_apellido, fecha_nacimiento, lugar_nacimiento, sexo, direccion, codigo, religion_id, madre_id, padre_id, 
created_at, updated_at, foto, peso, altura, tipo_sangre, padecimientos, alergias, recorrido_id, activo)
VALUES(10000, 'wilber', 'jose', 'matus', 'gonzaez', '2000-01-01', 'jinotepe', 'masculino', 'jinotepe', '0001', null, 1, 1, '2000-01-01', '2000-01-01', 
'\Media\profiles\aa113127-7a45-4339-999e-9bc7558c8ce7.png', 1, 200, 'A', 'N/A', 'N/A', null, 1);

INSERT INTO SIAC_CCA.dbo.estudiantes
(id, primer_nombre, segundo_nombre, primer_apellido, segundo_apellido, fecha_nacimiento, lugar_nacimiento, sexo, direccion, codigo, religion_id, madre_id, padre_id, 
created_at, updated_at, foto, peso, altura, tipo_sangre, padecimientos, alergias, recorrido_id, activo)
VALUES(10001, 'Mario', 'jose', 'matus', 'gonzaez', '2000-01-01', 'jinotepe', 'masculino', 'jinotepe', '0002', null, 1, 1, '2000-01-01', '2000-01-01', 
'\Media\profiles\aa113127-7a45-4339-999e-9bc7558c8ce7.png', 1, 200, 'A', 'N/A', 'N/A', null, 1);


INSERT INTO SIAC_CCA.dbo.responsables
(id, estudiante_id, pariente_id, created_at, updated_at, parentesco)
VALUES(0, 10000, 1, '', '', 'padre');
INSERT INTO SIAC_CCA.dbo.responsables
(id, estudiante_id, pariente_id, created_at, updated_at, parentesco)
VALUES(1, 10001, 1, '', '', 'padre');
