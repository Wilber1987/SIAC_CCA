CREATE TABLE estados_civiles (
    id INT PRIMARY KEY,
    idtestadocivil CHAR(1) NOT NULL,
    texto NVARCHAR(50) NOT NULL
);

CREATE TABLE religiones (
    id INT PRIMARY KEY,
    idtreligion CHAR(1) NOT NULL,
    texto NVARCHAR(50) NOT NULL
);

INSERT INTO estados_civiles (id,idtestadocivil,texto) VALUES
	 (1,'S','SOLTERO(A)'),
	 (2,'C','CASADO(A)'),
	 (3,'D','DIVORCIADO(A)'),
	 (4,'V','VIUDO(A)'),
	 (5,'U','UNION LIBRE');


INSERT INTO religiones (id,idtreligion,texto) VALUES
	 (1,'P','CRISTIANA'),
	 (2,'M','MUSULMAN'),
	 (3,'H','HINDUISTA'),
	 (4,'B','BUDISTA'),
	 (5,'C','CATOLICA'),
	 (6,'O','OTRA');

ALTER TABLE estudiantes
ADD CONSTRAINT FK_estudiantes_religion
FOREIGN KEY (religion_id) REFERENCES religiones(id);

ALTER TABLE parientes
ADD CONSTRAINT FK_parientes_religion
FOREIGN KEY (religion_id) REFERENCES religiones(id);

ALTER TABLE parientes
ADD CONSTRAINT FK_parientes_estadocivil
FOREIGN KEY (estado_civil_id) REFERENCES estados_civiles(id);

ALTER TABLE parientes
ADD identificacion nvarchar(150);