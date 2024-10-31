CREATE TABLE profesiones (
    id_profesion INT PRIMARY KEY,
    texto CHAR(50) NOT NULL
);

INSERT INTO profesiones
(id_profesion, texto)
VALUES(1, 'ABOGADO');
INSERT INTO profesiones
(id_profesion, texto)
VALUES(2, 'ADMINISTRACION EMPRESAS');
INSERT INTO profesiones
(id_profesion, texto)
VALUES(3, 'AMA DE CASA');
INSERT INTO profesiones
(id_profesion, texto)
VALUES(4, 'BACHILLER');
INSERT INTO profesiones
(id_profesion, texto)
VALUES(5, 'BANCA Y FINANZAS');
INSERT INTO profesiones
(id_profesion, texto)
VALUES(6, 'BIOLOGIA');
INSERT INTO profesiones
(id_profesion, texto)
VALUES(7, 'CARPINTERO');
INSERT INTO profesiones
(id_profesion, texto)
VALUES(8, 'COMUNICACION SOCIAL');
INSERT INTO profesiones
(id_profesion, texto)
VALUES(9, 'CONDUCTOR');
INSERT INTO profesiones
(id_profesion, texto)
VALUES(10, 'CONSULTOR');
INSERT INTO profesiones
(id_profesion, texto)
VALUES(11, 'CONTADOR PUBLICO Y AUDITOR');
INSERT INTO profesiones
(id_profesion, texto)
VALUES(12, 'ECONOMIA');
INSERT INTO profesiones
(id_profesion, texto)
VALUES(13, 'ENFERMERIA');
INSERT INTO profesiones
(id_profesion, texto)
VALUES(14, 'HISTORIA');
INSERT INTO profesiones
(id_profesion, texto)
VALUES(15, 'HUMANIDADES');
INSERT INTO profesiones
(id_profesion, texto)
VALUES(16, 'ING. ELECTRICA');
INSERT INTO profesiones
(id_profesion, texto)
VALUES(17, 'ING. EN COMPUTACION');
INSERT INTO profesiones
(id_profesion, texto)
VALUES(18, 'ING. EN SISTEMAS');
INSERT INTO profesiones
(id_profesion, texto)
VALUES(19, 'ING. INDUSTRIAL');
INSERT INTO profesiones
(id_profesion, texto)
VALUES(20, 'ING. QUIMICA');
INSERT INTO profesiones
(id_profesion, texto)
VALUES(21, 'INSTRUCTOR DEPORTIVO FUTBOL');
INSERT INTO profesiones
(id_profesion, texto)
VALUES(22, 'LABORATORISTA');
INSERT INTO profesiones
(id_profesion, texto)
VALUES(23, 'LIC. EN ESPAÑOL');
INSERT INTO profesiones
(id_profesion, texto)
VALUES(24, 'LIC. EN ESTADISTICAS');
INSERT INTO profesiones
(id_profesion, texto)
VALUES(25, 'LIC. EN FILOSOFIA');
INSERT INTO profesiones
(id_profesion, texto)
VALUES(26, 'LIC. EN FISICA');
INSERT INTO profesiones
(id_profesion, texto)
VALUES(27, 'LIC. EN HISTORIA');
INSERT INTO profesiones
(id_profesion, texto)
VALUES(28, 'LIC. EN INFORMATICA EDUCATIVA');
INSERT INTO profesiones
(id_profesion, texto)
VALUES(29, 'LIC. EN INGLES');
INSERT INTO profesiones
(id_profesion, texto)
VALUES(30, 'LIC. EN LENGUA Y LITERATURA');
INSERT INTO profesiones
(id_profesion, texto)
VALUES(31, 'LIC. EN MATEMATICA');
INSERT INTO profesiones
(id_profesion, texto)
VALUES(32, 'LIC. EN MERCADEO Y PUBLICIDAD');
INSERT INTO profesiones
(id_profesion, texto)
VALUES(33, 'LIC. EN PEDAGOGIA');
INSERT INTO profesiones
(id_profesion, texto)
VALUES(34, 'LIC. EN PSICOLOGIA');
INSERT INTO profesiones
(id_profesion, texto)
VALUES(35, 'LIC. EN QUIMICA');
INSERT INTO profesiones
(id_profesion, texto)
VALUES(36, 'LIC. EN SOCIOLOGIA');
INSERT INTO profesiones
(id_profesion, texto)
VALUES(37, 'LIC. EN TEOLOGIA');
INSERT INTO profesiones
(id_profesion, texto)
VALUES(38, 'LIC. PSCIOLOGIA EDUCATIVA');
INSERT INTO profesiones
(id_profesion, texto)
VALUES(39, 'MAGISTERIO');
INSERT INTO profesiones
(id_profesion, texto)
VALUES(40, 'MECANICO');
INSERT INTO profesiones
(id_profesion, texto)
VALUES(41, 'MUSICO');
INSERT INTO profesiones
(id_profesion, texto)
VALUES(42, 'OTROS HUMANIDADES');
INSERT INTO profesiones
(id_profesion, texto)
VALUES(43, 'TEC.SUP. ADMINISTRACION DE EMPRESAS');
INSERT INTO profesiones
(id_profesion, texto)
VALUES(44, 'TECNICO ELECTROMECANICO');
INSERT INTO profesiones
(id_profesion, texto)
VALUES(45, 'TECNICO EN COMPUTACION');
INSERT INTO profesiones
(id_profesion, texto)
VALUES(46, 'TECNICO EN ELECTRONICA');
INSERT INTO profesiones
(id_profesion, texto)
VALUES(47, 'TECNICO EN MATEMATICA MERCANTIL');
INSERT INTO profesiones
(id_profesion, texto)
VALUES(48, 'TECNICO EN REFRIGERACION');
INSERT INTO profesiones
(id_profesion, texto)
VALUES(49, 'TECNICO MEDIO EN CONTADURIA');
INSERT INTO profesiones
(id_profesion, texto)
VALUES(50, 'TECNICO MEDIO EN SECRETARIADO EJECUTIVO');
INSERT INTO profesiones
(id_profesion, texto)
VALUES(51, 'TECNICO SUP.ED.FISICA Y DEPORTES');
INSERT INTO profesiones
(id_profesion, texto)
VALUES(52, 'TECNICO SUPERIOR EN PEDAGOGIA');

--campos de estudiantes

ALTER TABLE dbo.estudiantes ADD id_pais int NULL;
ALTER TABLE dbo.estudiantes ADD id_region int NULL;