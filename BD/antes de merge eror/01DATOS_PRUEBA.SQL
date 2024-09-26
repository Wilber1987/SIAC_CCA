INSERT INTO SIAC_CCA.dbo.parientes
( id, primer_nombre, segundo_nombre, primer_apellido, segundo_apellido, sexo, profesion, 
direccion, lugar_trabajo, telefono, celular, telefono_trabajo, email, estado_civil_id, 
religion_id, created_at, updated_at)
VALUES(20000, 'Papa', 'Prueba', 'Prueba', 'Prueba', 'Masculino', '-', 'jinotepe', 'jinotepe',
'8888-8888', '8888-8888', '8888-8888', 'admin@admin.net', null, null, '2000-01-01', '2000-01-01');


INSERT INTO SIAC_CCA.dbo.estudiantes_responsables_familia
(id, estudiante_id, pariente_id, created_at, updated_at, Parentesco_id, familia_id)
VALUES(20000,701 , 298, '2013-07-26 00:44:14.000', '2013-07-26 00:44:14.000', 1, NULL);
INSERT INTO SIAC_CCA.dbo.estudiantes_responsables_familia
(id, estudiante_id, pariente_id, created_at, updated_at, Parentesco_id, familia_id)
VALUES(20001, 1693	, 298, '2013-07-26 00:44:14.000', '2013-07-26 00:44:14.000', 1, NULL);
INSERT INTO SIAC_CCA.dbo.estudiantes_responsables_familia
(id, estudiante_id, pariente_id, created_at, updated_at, Parentesco_id, familia_id)
VALUES(20002,2000 , 298, '2013-07-26 00:44:14.000', '2013-07-26 00:44:14.000', 1, NULL);
INSERT INTO SIAC_CCA.dbo.estudiantes_responsables_familia
(id, estudiante_id, pariente_id, created_at, updated_at, Parentesco_id, familia_id)
VALUES(20003,4000 , 298, '2013-07-26 00:44:14.000', '2013-07-26 00:44:14.000', 1, NULL);





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



/*
delete from SIAC_CCA.dbo.calificaciones;
delete from SIAC_CCA.dbo.evaluaciones;

INSERT INTO SIAC_CCA.dbo.evaluaciones
(id, fecha, hora, tipo, porcentaje, materia_id, seccion_id, observaciones, created_at, updated_at, periodo, nota_maxima)
SELECT id, fecha, hora, tipo, porcentaje, materia_id, seccion_id, observaciones, created_at, updated_at, periodo, nota_maxima
FROM RESPALDOSIAC.siac_cca_production.evaluaciones;


INSERT INTO SIAC_CCA.dbo.calificaciones
(id, resultado, tipo_nota_id, evaluacion_id, observaciones, created_at, updated_at, consolidado_id, estudiante_clase_id, materia_id, periodo)
SELECT id, resultado, tipo_nota_id, evaluacion_id, observaciones, created_at, updated_at, consolidado_id, estudiante_clase_id, materia_id, periodo
FROM RESPALDOSIAC.siac_cca_production.calificaciones 
where evaluacion_id is null or evaluacion_id  in (select id from SIAC_CCA.dbo.evaluaciones);*/