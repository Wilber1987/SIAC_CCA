CREATE TABLE regiones (
    id_region INT PRIMARY KEY,
    idtregion nvarchar(10) NOT NULL,
    texto NVARCHAR(50) NOT NULL,
    id_pais int
);

CREATE TABLE paises (
    id_pais INT PRIMARY KEY,
    idtpais nvarchar(10) NOT NULL,
    texto NVARCHAR(50) NOT NULL
);




INSERT INTO paises
(id_pais, idtpais, texto)
VALUES(1, '1', 'Australia');
INSERT INTO paises
(id_pais, idtpais, texto)
VALUES(2, '2', 'Austria');
INSERT INTO paises
(id_pais, idtpais, texto)
VALUES(3, '3', 'Azerbaiyán');
INSERT INTO paises
(id_pais, idtpais, texto)
VALUES(4, '4', 'Anguilla');
INSERT INTO paises
(id_pais, idtpais, texto)
VALUES(5, '5', 'Argentina');
INSERT INTO paises
(id_pais, idtpais, texto)
VALUES(6, '6', 'Armenia');
INSERT INTO paises
(id_pais, idtpais, texto)
VALUES(7, '7', 'Bielorrusia');
INSERT INTO paises
(id_pais, idtpais, texto)
VALUES(8, '8', 'Belice');
INSERT INTO paises
(id_pais, idtpais, texto)
VALUES(9, '9', 'Bélgica');
INSERT INTO paises
(id_pais, idtpais, texto)
VALUES(10, '10', 'Bermudas');
INSERT INTO paises
(id_pais, idtpais, texto)
VALUES(11, '11', 'Bulgaria');
INSERT INTO paises
(id_pais, idtpais, texto)
VALUES(12, '12', 'Brasil');
INSERT INTO paises
(id_pais, idtpais, texto)
VALUES(13, '13', 'Reino Unido');
INSERT INTO paises
(id_pais, idtpais, texto)
VALUES(14, '14', 'Hungría');
INSERT INTO paises
(id_pais, idtpais, texto)
VALUES(15, '15', 'Vietnam');
INSERT INTO paises
(id_pais, idtpais, texto)
VALUES(16, '16', 'Haiti');
INSERT INTO paises
(id_pais, idtpais, texto)
VALUES(17, '17', 'Guadalupe');
INSERT INTO paises
(id_pais, idtpais, texto)
VALUES(18, '18', 'Alemania');
INSERT INTO paises
(id_pais, idtpais, texto)
VALUES(19, '19', 'Países Bajos, Holanda');
INSERT INTO paises
(id_pais, idtpais, texto)
VALUES(20, '20', 'Grecia');
INSERT INTO paises
(id_pais, idtpais, texto)
VALUES(21, '21', 'Georgia');
INSERT INTO paises
(id_pais, idtpais, texto)
VALUES(22, '22', 'Dinamarca');
INSERT INTO paises
(id_pais, idtpais, texto)
VALUES(23, '23', 'Egipto');
INSERT INTO paises
(id_pais, idtpais, texto)
VALUES(24, '24', 'Israel');
INSERT INTO paises
(id_pais, idtpais, texto)
VALUES(25, '25', 'India');
INSERT INTO paises
(id_pais, idtpais, texto)
VALUES(26, '26', 'Irán');
INSERT INTO paises
(id_pais, idtpais, texto)
VALUES(27, '27', 'Irlanda');
INSERT INTO paises
(id_pais, idtpais, texto)
VALUES(28, '28', 'España');
INSERT INTO paises
(id_pais, idtpais, texto)
VALUES(29, '29', 'Italia');
INSERT INTO paises
(id_pais, idtpais, texto)
VALUES(30, '30', 'Kazajstán');
INSERT INTO paises
(id_pais, idtpais, texto)
VALUES(31, '31', 'Camerún');
INSERT INTO paises
(id_pais, idtpais, texto)
VALUES(32, '32', 'Canadá');
INSERT INTO paises
(id_pais, idtpais, texto)
VALUES(33, '33', 'Chipre');
INSERT INTO paises
(id_pais, idtpais, texto)
VALUES(34, '34', 'Kirguistán');
INSERT INTO paises
(id_pais, idtpais, texto)
VALUES(35, '35', 'China');
INSERT INTO paises
(id_pais, idtpais, texto)
VALUES(36, '36', 'Costa Rica');
INSERT INTO paises
(id_pais, idtpais, texto)
VALUES(37, '37', 'Kuwait');
INSERT INTO paises
(id_pais, idtpais, texto)
VALUES(38, '38', 'Letonia');
INSERT INTO paises
(id_pais, idtpais, texto)
VALUES(39, '39', 'Libia');
INSERT INTO paises
(id_pais, idtpais, texto)
VALUES(40, '40', 'Lituania');
INSERT INTO paises
(id_pais, idtpais, texto)
VALUES(41, '41', 'Luxemburgo');
INSERT INTO paises
(id_pais, idtpais, texto)
VALUES(42, '42', 'México');
INSERT INTO paises
(id_pais, idtpais, texto)
VALUES(43, '43', 'Moldavia');
INSERT INTO paises
(id_pais, idtpais, texto)
VALUES(44, '44', 'Mónaco');
INSERT INTO paises
(id_pais, idtpais, texto)
VALUES(45, '45', 'Nueva Zelanda');
INSERT INTO paises
(id_pais, idtpais, texto)
VALUES(46, '46', 'Noruega');
INSERT INTO paises
(id_pais, idtpais, texto)
VALUES(47, '47', 'Polonia');
INSERT INTO paises
(id_pais, idtpais, texto)
VALUES(48, '48', 'Portugal');
INSERT INTO paises
(id_pais, idtpais, texto)
VALUES(49, '49', 'Reunión');
INSERT INTO paises
(id_pais, idtpais, texto)
VALUES(50, '50', 'Rusia');
INSERT INTO paises
(id_pais, idtpais, texto)
VALUES(51, '51', 'El Salvador');
INSERT INTO paises
(id_pais, idtpais, texto)
VALUES(52, '52', 'Eslovaquia');
INSERT INTO paises
(id_pais, idtpais, texto)
VALUES(53, '53', 'Eslovenia');
INSERT INTO paises
(id_pais, idtpais, texto)
VALUES(54, '54', 'Surinam');
INSERT INTO paises
(id_pais, idtpais, texto)
VALUES(55, '55', 'Estados Unidos');
INSERT INTO paises
(id_pais, idtpais, texto)
VALUES(56, '56', 'Tadjikistan');
INSERT INTO paises
(id_pais, idtpais, texto)
VALUES(57, '57', 'Turkmenistan');
INSERT INTO paises
(id_pais, idtpais, texto)
VALUES(58, '58', 'Islas Turcas y Caicos');
INSERT INTO paises
(id_pais, idtpais, texto)
VALUES(59, '59', 'Turquía');
INSERT INTO paises
(id_pais, idtpais, texto)
VALUES(60, '60', 'Uganda');
INSERT INTO paises
(id_pais, idtpais, texto)
VALUES(61, '61', 'Uzbekistán');
INSERT INTO paises
(id_pais, idtpais, texto)
VALUES(62, '62', 'Ucrania');
INSERT INTO paises
(id_pais, idtpais, texto)
VALUES(63, '63', 'Finlandia');
INSERT INTO paises
(id_pais, idtpais, texto)
VALUES(64, '64', 'Francia');
INSERT INTO paises
(id_pais, idtpais, texto)
VALUES(65, '65', 'República Checa');
INSERT INTO paises
(id_pais, idtpais, texto)
VALUES(66, '66', 'Suiza');
INSERT INTO paises
(id_pais, idtpais, texto)
VALUES(67, '67', 'Suecia');
INSERT INTO paises
(id_pais, idtpais, texto)
VALUES(68, '68', 'Estonia');
INSERT INTO paises
(id_pais, idtpais, texto)
VALUES(69, '69', 'Corea del Sur');
INSERT INTO paises
(id_pais, idtpais, texto)
VALUES(70, '70', 'Japón');
INSERT INTO paises
(id_pais, idtpais, texto)
VALUES(71, '71', 'Croacia');
INSERT INTO paises
(id_pais, idtpais, texto)
VALUES(72, '72', 'Rumanía');
INSERT INTO paises
(id_pais, idtpais, texto)
VALUES(73, '73', 'Hong Kong');
INSERT INTO paises
(id_pais, idtpais, texto)
VALUES(74, '74', 'Indonesia');
INSERT INTO paises
(id_pais, idtpais, texto)
VALUES(75, '75', 'Jordania');
INSERT INTO paises
(id_pais, idtpais, texto)
VALUES(76, '76', 'Malasia');
INSERT INTO paises
(id_pais, idtpais, texto)
VALUES(77, '77', 'Singapur');
INSERT INTO paises
(id_pais, idtpais, texto)
VALUES(78, '78', 'Taiwan');
INSERT INTO paises
(id_pais, idtpais, texto)
VALUES(79, '79', 'Bosnia y Herzegovina');
INSERT INTO paises
(id_pais, idtpais, texto)
VALUES(80, '80', 'Bahamas');
INSERT INTO paises
(id_pais, idtpais, texto)
VALUES(81, '81', 'Chile');
INSERT INTO paises
(id_pais, idtpais, texto)
VALUES(82, '82', 'Colombia');
INSERT INTO paises
(id_pais, idtpais, texto)
VALUES(83, '83', 'Islandia');
INSERT INTO paises
(id_pais, idtpais, texto)
VALUES(84, '84', 'Corea del Norte');
INSERT INTO paises
(id_pais, idtpais, texto)
VALUES(85, '85', 'Macedonia');
INSERT INTO paises
(id_pais, idtpais, texto)
VALUES(86, '86', 'Malta');
INSERT INTO paises
(id_pais, idtpais, texto)
VALUES(87, '87', 'Pakistán');
INSERT INTO paises
(id_pais, idtpais, texto)
VALUES(88, '88', 'Papúa-Nueva Guinea');
INSERT INTO paises
(id_pais, idtpais, texto)
VALUES(89, '89', 'Perú');
INSERT INTO paises
(id_pais, idtpais, texto)
VALUES(90, '90', 'Filipinas');
INSERT INTO paises
(id_pais, idtpais, texto)
VALUES(91, '91', 'Arabia Saudita');
INSERT INTO paises
(id_pais, idtpais, texto)
VALUES(92, '92', 'Tailandia');
INSERT INTO paises
(id_pais, idtpais, texto)
VALUES(93, '93', 'Emiratos árabes Unidos');
INSERT INTO paises
(id_pais, idtpais, texto)
VALUES(94, '94', 'Groenlandia');
INSERT INTO paises
(id_pais, idtpais, texto)
VALUES(95, '95', 'Venezuela');
INSERT INTO paises
(id_pais, idtpais, texto)
VALUES(96, '96', 'Zimbabwe');
INSERT INTO paises
(id_pais, idtpais, texto)
VALUES(97, '97', 'Kenia');
INSERT INTO paises
(id_pais, idtpais, texto)
VALUES(98, '98', 'Algeria');
INSERT INTO paises
(id_pais, idtpais, texto)
VALUES(99, '99', 'Líbano');
INSERT INTO paises
(id_pais, idtpais, texto)
VALUES(100, '100', 'Botsuana');
INSERT INTO paises
(id_pais, idtpais, texto)
VALUES(101, '101', 'Tanzania');
INSERT INTO paises
(id_pais, idtpais, texto)
VALUES(102, '102', 'Namibia');
INSERT INTO paises
(id_pais, idtpais, texto)
VALUES(103, '103', 'Ecuador');
INSERT INTO paises
(id_pais, idtpais, texto)
VALUES(104, '104', 'Marruecos');
INSERT INTO paises
(id_pais, idtpais, texto)
VALUES(105, '105', 'Ghana');
INSERT INTO paises
(id_pais, idtpais, texto)
VALUES(106, '106', 'Siria');
INSERT INTO paises
(id_pais, idtpais, texto)
VALUES(107, '107', 'Nepal');
INSERT INTO paises
(id_pais, idtpais, texto)
VALUES(108, '108', 'Mauritania');
INSERT INTO paises
(id_pais, idtpais, texto)
VALUES(109, '109', 'Seychelles');
INSERT INTO paises
(id_pais, idtpais, texto)
VALUES(110, '110', 'Paraguay');
INSERT INTO paises
(id_pais, idtpais, texto)
VALUES(111, '111', 'Uruguay');
INSERT INTO paises
(id_pais, idtpais, texto)
VALUES(112, '112', 'Congo (Brazzaville)');
INSERT INTO paises
(id_pais, idtpais, texto)
VALUES(113, '113', 'Cuba');
INSERT INTO paises
(id_pais, idtpais, texto)
VALUES(114, '114', 'Albania');
INSERT INTO paises
(id_pais, idtpais, texto)
VALUES(115, '115', 'Nigeria');
INSERT INTO paises
(id_pais, idtpais, texto)
VALUES(116, '116', 'Zambia');
INSERT INTO paises
(id_pais, idtpais, texto)
VALUES(117, '117', 'Mozambique');
INSERT INTO paises
(id_pais, idtpais, texto)
VALUES(119, '119', 'Angola');
INSERT INTO paises
(id_pais, idtpais, texto)
VALUES(120, '120', 'Sri Lanka');
INSERT INTO paises
(id_pais, idtpais, texto)
VALUES(121, '121', 'Etiopía');
INSERT INTO paises
(id_pais, idtpais, texto)
VALUES(122, '122', 'Túnez');
INSERT INTO paises
(id_pais, idtpais, texto)
VALUES(123, '123', 'Bolivia');
INSERT INTO paises
(id_pais, idtpais, texto)
VALUES(124, '124', 'Panamá');
INSERT INTO paises
(id_pais, idtpais, texto)
VALUES(125, '125', 'Malawi');
INSERT INTO paises
(id_pais, idtpais, texto)
VALUES(126, '126', 'Liechtenstein');
INSERT INTO paises
(id_pais, idtpais, texto)
VALUES(127, '127', 'Bahrein');
INSERT INTO paises
(id_pais, idtpais, texto)
VALUES(128, '128', 'Barbados');
INSERT INTO paises
(id_pais, idtpais, texto)
VALUES(130, '130', 'Chad');
INSERT INTO paises
(id_pais, idtpais, texto)
VALUES(131, '131', 'Man, Isla de');
INSERT INTO paises
(id_pais, idtpais, texto)
VALUES(132, '132', 'Jamaica');
INSERT INTO paises
(id_pais, idtpais, texto)
VALUES(133, '133', 'Malí');
INSERT INTO paises
(id_pais, idtpais, texto)
VALUES(134, '134', 'Madagascar');
INSERT INTO paises
(id_pais, idtpais, texto)
VALUES(135, '135', 'Senegal');
INSERT INTO paises
(id_pais, idtpais, texto)
VALUES(136, '136', 'Togo');
INSERT INTO paises
(id_pais, idtpais, texto)
VALUES(137, '137', 'Honduras');
INSERT INTO paises
(id_pais, idtpais, texto)
VALUES(138, '138', 'República Dominicana');
INSERT INTO paises
(id_pais, idtpais, texto)
VALUES(139, '139', 'Mongolia');
INSERT INTO paises
(id_pais, idtpais, texto)
VALUES(140, '140', 'Irak');
INSERT INTO paises
(id_pais, idtpais, texto)
VALUES(141, '141', 'Sudáfrica');
INSERT INTO paises
(id_pais, idtpais, texto)
VALUES(142, '142', 'Aruba');
INSERT INTO paises
(id_pais, idtpais, texto)
VALUES(143, '143', 'Gibraltar');
INSERT INTO paises
(id_pais, idtpais, texto)
VALUES(144, '144', 'Afganistán');
INSERT INTO paises
(id_pais, idtpais, texto)
VALUES(145, '145', 'Andorra');
INSERT INTO paises
(id_pais, idtpais, texto)
VALUES(147, '147', 'Antigua y Barbuda');
INSERT INTO paises
(id_pais, idtpais, texto)
VALUES(149, '149', 'Bangladesh');
INSERT INTO paises
(id_pais, idtpais, texto)
VALUES(151, '151', 'Benín');
INSERT INTO paises
(id_pais, idtpais, texto)
VALUES(152, '152', 'Bután');
INSERT INTO paises
(id_pais, idtpais, texto)
VALUES(154, '154', 'Islas Virgenes Británicas');
INSERT INTO paises
(id_pais, idtpais, texto)
VALUES(155, '155', 'Brunéi');
INSERT INTO paises
(id_pais, idtpais, texto)
VALUES(156, '156', 'Burkina Faso');
INSERT INTO paises
(id_pais, idtpais, texto)
VALUES(157, '157', 'Burundi');
INSERT INTO paises
(id_pais, idtpais, texto)
VALUES(158, '158', 'Camboya');
INSERT INTO paises
(id_pais, idtpais, texto)
VALUES(159, '159', 'Cabo Verde');
INSERT INTO paises
(id_pais, idtpais, texto)
VALUES(164, '164', 'Comores');
INSERT INTO paises
(id_pais, idtpais, texto)
VALUES(165, '165', 'Congo (Kinshasa)');
INSERT INTO paises
(id_pais, idtpais, texto)
VALUES(166, '166', 'Cook, Islas');
INSERT INTO paises
(id_pais, idtpais, texto)
VALUES(168, '168', 'Costa de Marfil');
INSERT INTO paises
(id_pais, idtpais, texto)
VALUES(169, '169', 'Djibouti, Yibuti');
INSERT INTO paises
(id_pais, idtpais, texto)
VALUES(171, '171', 'Timor Oriental');
INSERT INTO paises
(id_pais, idtpais, texto)
VALUES(172, '172', 'Guinea Ecuatorial');
INSERT INTO paises
(id_pais, idtpais, texto)
VALUES(173, '173', 'Eritrea');
INSERT INTO paises
(id_pais, idtpais, texto)
VALUES(175, '175', 'Feroe, Islas');
INSERT INTO paises
(id_pais, idtpais, texto)
VALUES(176, '176', 'Fiyi');
INSERT INTO paises
(id_pais, idtpais, texto)
VALUES(178, '178', 'Polinesia Francesa');
INSERT INTO paises
(id_pais, idtpais, texto)
VALUES(180, '180', 'Gabón');
INSERT INTO paises
(id_pais, idtpais, texto)
VALUES(181, '181', 'Gambia');
INSERT INTO paises
(id_pais, idtpais, texto)
VALUES(184, '184', 'Granada');
INSERT INTO paises
(id_pais, idtpais, texto)
VALUES(185, '185', 'Guatemala');
INSERT INTO paises
(id_pais, idtpais, texto)
VALUES(186, '186', 'Guernsey');
INSERT INTO paises
(id_pais, idtpais, texto)
VALUES(187, '187', 'Guinea');
INSERT INTO paises
(id_pais, idtpais, texto)
VALUES(188, '188', 'Guinea-Bissau');
INSERT INTO paises
(id_pais, idtpais, texto)
VALUES(189, '189', 'Guyana');
INSERT INTO paises
(id_pais, idtpais, texto)
VALUES(193, '193', 'Jersey');
INSERT INTO paises
(id_pais, idtpais, texto)
VALUES(195, '195', 'Kiribati');
INSERT INTO paises
(id_pais, idtpais, texto)
VALUES(196, '196', 'Laos');
INSERT INTO paises
(id_pais, idtpais, texto)
VALUES(197, '197', 'Lesotho');
INSERT INTO paises
(id_pais, idtpais, texto)
VALUES(198, '198', 'Liberia');
INSERT INTO paises
(id_pais, idtpais, texto)
VALUES(200, '200', 'Maldivas');
INSERT INTO paises
(id_pais, idtpais, texto)
VALUES(201, '201', 'Martinica');
INSERT INTO paises
(id_pais, idtpais, texto)
VALUES(202, '202', 'Mauricio');
INSERT INTO paises
(id_pais, idtpais, texto)
VALUES(205, '205', 'Myanmar');
INSERT INTO paises
(id_pais, idtpais, texto)
VALUES(206, '206', 'Nauru');
INSERT INTO paises
(id_pais, idtpais, texto)
VALUES(207, '207', 'Antillas Holandesas');
INSERT INTO paises
(id_pais, idtpais, texto)
VALUES(208, '208', 'Nueva Caledonia');
INSERT INTO paises
(id_pais, idtpais, texto)
VALUES(209, '209', 'Nicaragua');
INSERT INTO paises
(id_pais, idtpais, texto)
VALUES(210, '210', 'Níger');
INSERT INTO paises
(id_pais, idtpais, texto)
VALUES(212, '212', 'Norfolk Island');
INSERT INTO paises
(id_pais, idtpais, texto)
VALUES(213, '213', 'Omán');
INSERT INTO paises
(id_pais, idtpais, texto)
VALUES(215, '215', 'Isla Pitcairn');
INSERT INTO paises
(id_pais, idtpais, texto)
VALUES(216, '216', 'Qatar');
INSERT INTO paises
(id_pais, idtpais, texto)
VALUES(217, '217', 'Ruanda');
INSERT INTO paises
(id_pais, idtpais, texto)
VALUES(218, '218', 'Santa Elena');
INSERT INTO paises
(id_pais, idtpais, texto)
VALUES(219, '219', 'San Cristobal y Nevis');
INSERT INTO paises
(id_pais, idtpais, texto)
VALUES(220, '220', 'Santa Lucía');
INSERT INTO paises
(id_pais, idtpais, texto)
VALUES(221, '221', 'San Pedro y Miquelón');
INSERT INTO paises
(id_pais, idtpais, texto)
VALUES(222, '222', 'San Vincente y Granadinas');
INSERT INTO paises
(id_pais, idtpais, texto)
VALUES(223, '223', 'Samoa');
INSERT INTO paises
(id_pais, idtpais, texto)
VALUES(224, '224', 'San Marino');
INSERT INTO paises
(id_pais, idtpais, texto)
VALUES(225, '225', 'San Tomé y Príncipe');
INSERT INTO paises
(id_pais, idtpais, texto)
VALUES(226, '226', 'Serbia y Montenegro');



/*****regiones ****/

INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1, '3', 'Azerbaijan', 3);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(2, '3', 'Nargorni Karabakh', 3);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(3, '3', 'Nakhichevanskaya Region', 3);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(4, '4', 'Anguilla', 4);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(5, '7', 'Brestskaya obl.', 7);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(6, '7', 'Vitebskaya obl.', 7);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(7, '7', 'Gomelskaya obl.', 7);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(8, '7', 'Grodnenskaya obl.', 7);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(9, '7', 'Minskaya obl.', 7);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(10, '7', 'Mogilevskaya obl.', 7);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(11, '8', 'Belize', 8);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(12, '10', 'Hamilton', 10);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(13, '15', 'Dong Bang Song Cuu Long', 15);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(14, '15', 'Dong Bang Song Hong', 15);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(15, '15', 'Dong Nam Bo', 15);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(16, '15', 'Duyen Hai Mien Trung', 15);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(17, '15', 'Khu Bon Cu', 15);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(18, '15', 'Mien Nui Va Trung Du', 15);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(19, '15', 'Thai Nguyen', 15);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(20, '16', 'Artibonite', 16);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(21, '16', 'Grand&#039;Anse', 16);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(22, '16', 'North West', 16);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(23, '16', 'West', 16);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(24, '16', 'South', 16);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(25, '16', 'South East', 16);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(26, '17', 'Grande-Terre', 17);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(27, '17', 'Basse-Terre', 17);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(28, '21', 'Abkhazia', 21);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(29, '21', 'Ajaria', 21);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(30, '21', 'Georgia', 21);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(31, '21', 'South Ossetia', 21);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(32, '23', 'Al QÄhira', 23);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(33, '23', 'Aswan', 23);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(34, '23', 'Asyut', 23);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(35, '23', 'Beni Suef', 23);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(36, '23', 'Gharbia', 23);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(37, '23', 'Damietta', 23);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(38, '24', 'Southern District', 24);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(39, '24', 'Central District', 24);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(40, '24', 'Northern District', 24);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(41, '24', 'Haifa', 24);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(42, '24', 'Tel Aviv', 24);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(43, '24', 'Jerusalem', 24);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(44, '25', 'Bangala', 25);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(45, '25', 'Chhattisgarh', 25);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(46, '25', 'Karnataka', 25);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(47, '25', 'Uttaranchal', 25);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(48, '25', 'Andhara Pradesh', 25);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(49, '25', 'Assam', 25);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(50, '25', 'Bihar', 25);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(51, '25', 'Gujarat', 25);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(52, '25', 'Jammu and Kashmir', 25);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(53, '25', 'Kerala', 25);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(54, '25', 'Madhya Pradesh', 25);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(55, '25', 'Manipur', 25);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(56, '25', 'Maharashtra', 25);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(57, '25', 'Megahalaya', 25);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(58, '25', 'Orissa', 25);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(59, '25', 'Punjab', 25);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(60, '25', 'Pondisheri', 25);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(61, '25', 'Rajasthan', 25);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(62, '25', 'Tamil Nadu', 25);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(63, '25', 'Tripura', 25);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(64, '25', 'Uttar Pradesh', 25);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(65, '25', 'Haryana', 25);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(66, '25', 'Chandigarh', 25);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(67, '26', 'Azarbayjan-e Khavari', 26);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(68, '26', 'Esfahan', 26);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(69, '26', 'Hamadan', 26);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(70, '26', 'Kordestan', 26);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(71, '26', 'Markazi', 26);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(72, '26', 'Sistan-e Baluches', 26);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(73, '26', 'Yazd', 26);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(74, '26', 'Kerman', 26);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(75, '26', 'Kermanshakhan', 26);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(76, '26', 'Mazenderan', 26);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(77, '26', 'Tehran', 26);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(78, '26', 'Fars', 26);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(79, '26', 'Horasan', 26);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(80, '26', 'Husistan', 26);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(81, '30', 'Aktyubinskaya obl.', 30);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(82, '30', 'Alma-Atinskaya obl.', 30);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(83, '30', 'Vostochno-Kazahstanskaya obl.', 30);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(84, '30', 'Gurevskaya obl.', 30);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(85, '30', 'Zhambylskaya obl. (Dzhambulskaya obl.)', 30);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(86, '30', 'Dzhezkazganskaya obl.', 30);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(87, '30', 'Karagandinskaya obl.', 30);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(88, '30', 'Kzyl-Ordinskaya obl.', 30);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(89, '30', 'Kokchetavskaya obl.', 30);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(90, '30', 'Kustanaiskaya obl.', 30);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(91, '30', 'Mangystauskaya (Mangyshlakskaya obl.)', 30);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(92, '30', 'Pavlodarskaya obl.', 30);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(93, '30', 'Severo-Kazahstanskaya obl.', 30);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(94, '30', 'Taldy-Kurganskaya obl.', 30);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(95, '30', 'Turgaiskaya obl.', 30);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(96, '30', 'Akmolinskaya obl. (Tselinogradskaya obl.)', 30);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(97, '30', 'Chimkentskaya obl.', 30);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(98, '31', 'Littoral', 31);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(99, '31', 'Southwest Region', 31);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(100, '31', 'North', 31);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(101, '31', 'Central', 31);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(102, '33', 'Government controlled area', 33);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(103, '33', 'Turkish controlled area', 33);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(104, '34', 'Issik Kulskaya Region', 34);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(105, '34', 'Kyrgyzstan', 34);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(106, '34', 'Narinskaya Region', 34);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(107, '34', 'Oshskaya Region', 34);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(108, '34', 'Tallaskaya Region', 34);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(109, '37', 'al-Jahra', 37);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(110, '37', 'al-Kuwait', 37);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(111, '38', 'Latviya', 38);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(112, '39', 'Tarabulus', 39);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(113, '39', 'Bengasi', 39);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(114, '40', 'Litva', 40);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(115, '43', 'Moldova', 43);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(116, '45', 'Auckland', 45);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(117, '45', 'Bay of Plenty', 45);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(118, '45', 'Canterbury', 45);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(119, '45', 'Gisborne', 45);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(120, '45', 'Hawke&#039;s Bay', 45);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(121, '45', 'Manawatu-Wanganui', 45);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(122, '45', 'Marlborough', 45);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(123, '45', 'Nelson', 45);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(124, '45', 'Northland', 45);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(125, '45', 'Otago', 45);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(126, '45', 'Southland', 45);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(127, '45', 'Taranaki', 45);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(128, '45', 'Tasman', 45);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(129, '45', 'Waikato', 45);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(130, '45', 'Wellington', 45);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(131, '45', 'West Coast', 45);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(132, '49', 'Saint-Denis', 49);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(133, '50', 'Altaiskii krai', 50);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(134, '50', 'Amurskaya obl.', 50);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(135, '50', 'Arhangelskaya obl.', 50);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(136, '50', 'Astrahanskaya obl.', 50);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(137, '50', 'Bashkiriya obl.', 50);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(138, '50', 'Belgorodskaya obl.', 50);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(139, '50', 'Bryanskaya obl.', 50);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(140, '50', 'Buryatiya', 50);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(141, '50', 'Vladimirskaya obl.', 50);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(142, '50', 'Volgogradskaya obl.', 50);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(143, '50', 'Vologodskaya obl.', 50);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(144, '50', 'Voronezhskaya obl.', 50);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(145, '50', 'Nizhegorodskaya obl.', 50);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(146, '50', 'Dagestan', 50);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(147, '50', 'Evreiskaya obl.', 50);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(148, '50', 'Ivanovskaya obl.', 50);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(149, '50', 'Irkutskaya obl.', 50);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(150, '50', 'Kabardino-Balkariya', 50);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(151, '50', 'Kaliningradskaya obl.', 50);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(152, '50', 'Tverskaya obl.', 50);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(153, '50', 'Kalmykiya', 50);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(154, '50', 'Kaluzhskaya obl.', 50);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(155, '50', 'Kamchatskaya obl.', 50);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(156, '50', 'Kareliya', 50);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(157, '50', 'Kemerovskaya obl.', 50);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(158, '50', 'Kirovskaya obl.', 50);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(159, '50', 'Komi', 50);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(160, '50', 'Kostromskaya obl.', 50);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(161, '50', 'Krasnodarskii krai', 50);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(162, '50', 'Krasnoyarskii krai', 50);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(163, '50', 'Kurganskaya obl.', 50);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(164, '50', 'Kurskaya obl.', 50);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(165, '50', 'Lipetskaya obl.', 50);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(166, '50', 'Magadanskaya obl.', 50);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(167, '50', 'Marii El', 50);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(168, '50', 'Mordoviya', 50);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(169, '50', 'Moscow &amp; Moscow Region', 50);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(170, '50', 'Murmanskaya obl.', 50);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(171, '50', 'Novgorodskaya obl.', 50);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(172, '50', 'Novosibirskaya obl.', 50);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(173, '50', 'Omskaya obl.', 50);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(174, '50', 'Orenburgskaya obl.', 50);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(175, '50', 'Orlovskaya obl.', 50);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(176, '50', 'Penzenskaya obl.', 50);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(177, '50', 'Permskiy krai', 50);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(178, '50', 'Primorskii krai', 50);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(179, '50', 'Pskovskaya obl.', 50);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(180, '50', 'Rostovskaya obl.', 50);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(181, '50', 'Ryazanskaya obl.', 50);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(182, '50', 'Samarskaya obl.', 50);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(183, '50', 'Saint-Petersburg and Region', 50);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(184, '50', 'Saratovskaya obl.', 50);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(185, '50', 'Saha (Yakutiya)', 50);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(186, '50', 'Sahalin', 50);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(187, '50', 'Sverdlovskaya obl.', 50);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(188, '50', 'Severnaya Osetiya', 50);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(189, '50', 'Smolenskaya obl.', 50);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(190, '50', 'Stavropolskii krai', 50);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(191, '50', 'Tambovskaya obl.', 50);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(192, '50', 'Tatarstan', 50);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(193, '50', 'Tomskaya obl.', 50);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(195, '50', 'Tulskaya obl.', 50);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(196, '50', 'Tyumenskaya obl. i Hanty-Mansiiskii AO', 50);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(197, '50', 'Udmurtiya', 50);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(198, '50', 'Ulyanovskaya obl.', 50);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(199, '50', 'Uralskaya obl.', 50);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(200, '50', 'Habarovskii krai', 50);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(201, '50', 'Chelyabinskaya obl.', 50);



