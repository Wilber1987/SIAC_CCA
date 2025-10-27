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

delete from regiones;
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
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(202, '50', 'Checheno-Ingushetiya', 50);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(203, '50', 'Chitinskaya obl.', 50);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(204, '50', 'Chuvashiya', 50);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(205, '50', 'Yaroslavskaya obl.', 50);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(206, '51', 'Ahuachapán', 51);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(207, '51', 'Cuscatlán', 51);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(208, '51', 'La Libertad', 51);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(209, '51', 'La Paz', 51);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(210, '51', 'La Unión', 51);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(211, '51', 'San Miguel', 51);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(212, '51', 'San Salvador', 51);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(213, '51', 'Santa Ana', 51);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(214, '51', 'Sonsonate', 51);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(215, '54', 'Paramaribo', 54);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(216, '56', 'Gorno-Badakhshan Region', 56);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(217, '56', 'Kuljabsk Region', 56);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(218, '56', 'Kurgan-Tjube Region', 56);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(219, '56', 'Sughd Region', 56);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(220, '56', 'Tajikistan', 56);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(221, '57', 'Ashgabat Region', 57);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(222, '57', 'Krasnovodsk Region', 57);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(223, '57', 'Mary Region', 57);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(224, '57', 'Tashauz Region', 57);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(225, '57', 'Chardzhou Region', 57);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(226, '58', 'Grand Turk', 58);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(227, '59', 'Bartin', 59);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(228, '59', 'Bayburt', 59);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(229, '59', 'Karabuk', 59);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(230, '59', 'Adana', 59);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(231, '59', 'Aydin', 59);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(232, '59', 'Amasya', 59);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(233, '59', 'Ankara', 59);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(234, '59', 'Antalya', 59);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(235, '59', 'Artvin', 59);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(236, '59', 'Afion', 59);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(237, '59', 'Balikesir', 59);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(238, '59', 'Bilecik', 59);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(239, '59', 'Bursa', 59);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(240, '59', 'Gaziantep', 59);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(241, '59', 'Denizli', 59);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(242, '59', 'Izmir', 59);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(243, '59', 'Isparta', 59);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(244, '59', 'Icel', 59);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(245, '59', 'Kayseri', 59);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(246, '59', 'Kars', 59);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(247, '59', 'Kodjaeli', 59);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(248, '59', 'Konya', 59);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(249, '59', 'Kirklareli', 59);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(250, '59', 'Kutahya', 59);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(251, '59', 'Malatya', 59);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(252, '59', 'Manisa', 59);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(253, '59', 'Sakarya', 59);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(254, '59', 'Samsun', 59);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(255, '59', 'Sivas', 59);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(256, '59', 'Istanbul', 59);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(257, '59', 'Trabzon', 59);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(258, '59', 'Corum', 59);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(259, '59', 'Edirne', 59);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(260, '59', 'Elazig', 59);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(261, '59', 'Erzincan', 59);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(262, '59', 'Erzurum', 59);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(263, '59', 'Eskisehir', 59);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(264, '60', 'Jinja', 60);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(265, '60', 'Kampala', 60);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(266, '61', 'Andijon Region', 61);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(267, '61', 'Buxoro Region', 61);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(268, '61', 'Jizzac Region', 61);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(269, '61', 'Qaraqalpaqstan', 61);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(270, '61', 'Qashqadaryo Region', 61);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(271, '61', 'Navoiy Region', 61);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(272, '61', 'Namangan Region', 61);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(273, '61', 'Samarqand Region', 61);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(274, '61', 'Surxondaryo Region', 61);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(275, '61', 'Sirdaryo Region', 61);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(276, '61', 'Tashkent Region', 61);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(277, '61', 'Fergana Region', 61);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(278, '61', 'Xorazm Region', 61);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(279, '62', 'Vinnitskaya obl.', 62);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(280, '62', 'Volynskaya obl.', 62);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(281, '62', 'Dnepropetrovskaya obl.', 62);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(282, '62', 'Donetskaya obl.', 62);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(283, '62', 'Zhitomirskaya obl.', 62);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(284, '62', 'Zakarpatskaya obl.', 62);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(285, '62', 'Zaporozhskaya obl.', 62);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(286, '62', 'Ivano-Frankovskaya obl.', 62);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(287, '62', 'Kievskaya obl.', 62);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(288, '62', 'Kirovogradskaya obl.', 62);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(289, '62', 'Krymskaya obl.', 62);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(290, '62', 'Luganskaya obl.', 62);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(291, '62', 'Lvovskaya obl.', 62);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(292, '62', 'Nikolaevskaya obl.', 62);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(293, '62', 'Odesskaya obl.', 62);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(294, '62', 'Poltavskaya obl.', 62);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(295, '62', 'Rovenskaya obl.', 62);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(296, '62', 'Sumskaya obl.', 62);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(297, '62', 'Ternopolskaya obl.', 62);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(298, '62', 'Harkovskaya obl.', 62);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(299, '62', 'Hersonskaya obl.', 62);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(300, '62', 'Hmelnitskaya obl.', 62);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(301, '62', 'Cherkasskaya obl.', 62);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(302, '62', 'Chernigovskaya obl.', 62);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(303, '62', 'Chernovitskaya obl.', 62);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(304, '68', 'Estoniya', 68);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(305, '69', 'Cheju', 69);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(306, '69', 'Chollabuk', 69);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(307, '69', 'Chollanam', 69);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(308, '69', 'Chungcheongbuk', 69);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(309, '69', 'Chungcheongnam', 69);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(310, '69', 'Incheon', 69);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(311, '69', 'Kangweon', 69);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(312, '69', 'Kwangju', 69);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(313, '69', 'Kyeonggi', 69);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(314, '69', 'Kyeongsangbuk', 69);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(315, '69', 'Kyeongsangnam', 69);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(316, '69', 'Pusan', 69);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(317, '69', 'Seoul', 69);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(318, '69', 'Taegu', 69);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(319, '69', 'Taejeon', 69);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(320, '69', 'Ulsan', 69);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(321, '70', 'Aichi', 70);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(322, '70', 'Akita', 70);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(323, '70', 'Aomori', 70);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(324, '70', 'Wakayama', 70);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(325, '70', 'Gifu', 70);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(326, '70', 'Gunma', 70);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(327, '70', 'Ibaraki', 70);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(328, '70', 'Iwate', 70);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(329, '70', 'Ishikawa', 70);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(330, '70', 'Kagawa', 70);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(331, '70', 'Kagoshima', 70);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(332, '70', 'Kanagawa', 70);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(333, '70', 'Kyoto', 70);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(334, '70', 'Kochi', 70);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(335, '70', 'Kumamoto', 70);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(336, '70', 'Mie', 70);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(337, '70', 'Miyagi', 70);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(338, '70', 'Miyazaki', 70);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(339, '70', 'Nagano', 70);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(340, '70', 'Nagasaki', 70);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(341, '70', 'Nara', 70);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(342, '70', 'Niigata', 70);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(343, '70', 'Okayama', 70);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(344, '70', 'Okinawa', 70);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(345, '70', 'Osaka', 70);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(346, '70', 'Saga', 70);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(347, '70', 'Saitama', 70);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(348, '70', 'Shiga', 70);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(349, '70', 'Shizuoka', 70);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(350, '70', 'Shimane', 70);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(351, '70', 'Tiba', 70);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(352, '70', 'Tokyo', 70);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(353, '70', 'Tokushima', 70);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(354, '70', 'Tochigi', 70);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(355, '70', 'Tottori', 70);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(356, '70', 'Toyama', 70);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(357, '70', 'Fukui', 70);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(358, '70', 'Fukuoka', 70);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(359, '70', 'Fukushima', 70);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(360, '70', 'Hiroshima', 70);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(361, '70', 'Hokkaido', 70);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(362, '70', 'Hyogo', 70);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(363, '70', 'Yoshimi', 70);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(364, '70', 'Yamagata', 70);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(365, '70', 'Yamaguchi', 70);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(366, '70', 'Yamanashi', 70);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(368, '73', 'Hong Kong', 73);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(369, '74', 'Indonesia', 74);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(370, '75', 'Jordan', 75);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(371, '76', 'Malaysia', 76);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(372, '77', 'Singapore', 77);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(373, '78', 'Taiwan', 78);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(374, '30', 'Kazahstan', 30);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(375, '62', 'Ukraina', 62);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(376, '25', 'India', 25);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(377, '23', 'Egypt', 23);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(378, '106', 'Damascus', 106);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(379, '131', 'Isle of Man', 131);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(380, '30', 'Zapadno-Kazahstanskaya obl.', 30);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(381, '50', 'Adygeya', 50);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(382, '50', 'Hakasiya', 50);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(383, '93', 'Dubai', 93);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(384, '50', 'Chukotskii AO', 50);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(385, '99', 'Beirut', 99);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(386, '137', 'Tegucigalpa', 137);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(387, '138', 'Santo Domingo', 138);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(388, '139', 'Ulan Bator', 139);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(389, '23', 'Sinai', 23);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(390, '140', 'Baghdad', 140);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(391, '140', 'Basra', 140);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(392, '140', 'Mosul', 140);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(393, '141', 'Johannesburg', 141);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(394, '104', 'Morocco', 104);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(395, '104', 'Tangier', 104);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(396, '50', 'Yamalo-Nenetskii AO', 50);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(397, '122', 'Tunisia', 122);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(398, '92', 'Thailand', 92);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(399, '117', 'Mozambique', 117);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(400, '84', 'Korea', 84);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(401, '87', 'Pakistan', 87);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(402, '142', 'Aruba', 142);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(403, '80', 'Bahamas', 80);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(404, '69', 'South Korea', 69);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(405, '132', 'Jamaica', 132);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(406, '93', 'Sharjah', 93);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(407, '93', 'Abu Dhabi', 93);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(409, '24', 'Ramat Hagolan', 24);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(410, '115', 'Nigeria', 115);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(411, '64', 'Ain', 64);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(412, '64', 'Haute-Savoie', 64);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(413, '64', 'Aisne', 64);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(414, '64', 'Allier', 64);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(415, '64', 'Alpes-de-Haute-Provence', 64);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(416, '64', 'Hautes-Alpes', 64);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(417, '64', 'Alpes-Maritimes', 64);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(418, '64', 'Ard&egrave;che', 64);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(419, '64', 'Ardennes', 64);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(420, '64', 'Ari&egrave;ge', 64);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(421, '64', 'Aube', 64);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(422, '64', 'Aude', 64);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(423, '64', 'Aveyron', 64);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(424, '64', 'Bouches-du-Rh&ocirc;ne', 64);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(425, '64', 'Calvados', 64);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(426, '64', 'Cantal', 64);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(427, '64', 'Charente', 64);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(428, '64', 'Charente Maritime', 64);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(429, '64', 'Cher', 64);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(430, '64', 'Corr&egrave;ze', 64);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(431, '64', 'Dordogne', 64);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(432, '64', 'Corse', 64);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(433, '64', 'C&ocirc;te d&#039;Or', 64);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(434, '64', 'Sa&ocirc;ne et Loire', 64);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(435, '64', 'C&ocirc;tes d&#039;Armor', 64);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(436, '64', 'Creuse', 64);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(437, '64', 'Doubs', 64);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(438, '64', 'Dr&ocirc;me', 64);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(439, '64', 'Eure', 64);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(440, '64', 'Eure-et-Loire', 64);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(441, '64', 'Finist&egrave;re', 64);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(442, '64', 'Gard', 64);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(443, '64', 'Haute-Garonne', 64);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(444, '64', 'Gers', 64);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(445, '64', 'Gironde', 64);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(446, '64', 'Hérault', 64);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(447, '64', 'Ille et Vilaine', 64);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(448, '64', 'Indre', 64);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(449, '64', 'Indre-et-Loire', 64);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(450, '64', 'Isère', 64);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(451, '64', 'Jura', 64);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(452, '64', 'Landes', 64);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(453, '64', 'Loir-et-Cher', 64);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(454, '64', 'Loire', 64);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(455, '64', 'Rh&ocirc;ne', 64);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(456, '64', 'Haute-Loire', 64);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(457, '64', 'Loire Atlantique', 64);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(458, '64', 'Loiret', 64);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(459, '64', 'Lot', 64);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(460, '64', 'Lot-et-Garonne', 64);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(461, '64', 'Loz&egrave;re', 64);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(462, '64', 'Maine et Loire', 64);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(463, '64', 'Manche', 64);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(464, '64', 'Marne', 64);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(465, '64', 'Haute-Marne', 64);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(466, '64', 'Mayenne', 64);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(467, '64', 'Meurthe-et-Moselle', 64);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(468, '64', 'Meuse', 64);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(469, '64', 'Morbihan', 64);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(470, '64', 'Moselle', 64);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(471, '64', 'Ni&egrave;vre', 64);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(472, '64', 'Nord', 64);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(473, '64', 'Oise', 64);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(474, '64', 'Orne', 64);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(475, '64', 'Pas-de-Calais', 64);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(476, '64', 'Puy-de-D&ocirc;me', 64);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(477, '64', 'Pyrénées-Atlantiques', 64);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(478, '64', 'Hautes-Pyrénées', 64);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(479, '64', 'Pyrénées-Orientales', 64);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(480, '64', 'Bas Rhin', 64);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(481, '64', 'Haut Rhin', 64);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(482, '64', 'Haute-Sa&ocirc;ne', 64);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(483, '64', 'Sarthe', 64);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(484, '64', 'Savoie', 64);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(485, '64', 'Paris', 64);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(486, '64', 'Seine-Maritime', 64);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(487, '64', 'Seine-et-Marne', 64);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(488, '64', 'Yvelines', 64);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(489, '64', 'Deux-S&egrave;vres', 64);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(490, '64', 'Somme', 64);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(491, '64', 'Tarn', 64);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(492, '64', 'Tarn-et-Garonne', 64);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(493, '64', 'Var', 64);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(494, '64', 'Vaucluse', 64);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(495, '64', 'Vendée', 64);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(496, '64', 'Vienne', 64);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(497, '64', 'Haute-Vienne', 64);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(498, '64', 'Vosges', 64);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(499, '64', 'Yonne', 64);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(500, '64', 'Territoire de Belfort', 64);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(501, '64', 'Essonne', 64);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(502, '64', 'Hauts-de-Seine', 64);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(503, '64', 'Seine-Saint-Denis', 64);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(504, '64', 'Val-de-Marne', 64);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(505, '64', 'Val-d&#039;Oise', 64);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(506, '29', 'Piemonte - Torino', 29);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(507, '29', 'Piemonte - Alessandria', 29);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(508, '29', 'Piemonte - Asti', 29);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(509, '29', 'Piemonte - Biella', 29);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(510, '29', 'Piemonte - Cuneo', 29);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(511, '29', 'Piemonte - Novara', 29);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(512, '29', 'Piemonte - Verbania', 29);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(513, '29', 'Piemonte - Vercelli', 29);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(514, '29', 'Valle d&#039;Aosta - Aosta', 29);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(515, '29', 'Lombardia - Milano', 29);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(516, '29', 'Lombardia - Bergamo', 29);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(517, '29', 'Lombardia - Brescia', 29);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(518, '29', 'Lombardia - Como', 29);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(519, '29', 'Lombardia - Cremona', 29);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(520, '29', 'Lombardia - Lecco', 29);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(521, '29', 'Lombardia - Lodi', 29);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(522, '29', 'Lombardia - Mantova', 29);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(523, '29', 'Lombardia - Pavia', 29);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(524, '29', 'Lombardia - Sondrio', 29);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(525, '29', 'Lombardia - Varese', 29);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(526, '29', 'Trentino Alto Adige - Trento', 29);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(527, '29', 'Trentino Alto Adige - Bolzano', 29);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(528, '29', 'Veneto - Venezia', 29);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(529, '29', 'Veneto - Belluno', 29);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(530, '29', 'Veneto - Padova', 29);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(531, '29', 'Veneto - Rovigo', 29);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(532, '29', 'Veneto - Treviso', 29);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(533, '29', 'Veneto - Verona', 29);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(534, '29', 'Veneto - Vicenza', 29);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(535, '29', 'Friuli Venezia Giulia - Trieste', 29);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(536, '29', 'Friuli Venezia Giulia - Gorizia', 29);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(537, '29', 'Friuli Venezia Giulia - Pordenone', 29);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(538, '29', 'Friuli Venezia Giulia - Udine', 29);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(539, '29', 'Liguria - Genova', 29);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(540, '29', 'Liguria - Imperia', 29);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(541, '29', 'Liguria - La Spezia', 29);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(542, '29', 'Liguria - Savona', 29);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(543, '29', 'Emilia Romagna - Bologna', 29);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(544, '29', 'Emilia Romagna - Ferrara', 29);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(545, '29', 'Emilia Romagna - Forlì-Cesena', 29);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(546, '29', 'Emilia Romagna - Modena', 29);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(547, '29', 'Emilia Romagna - Parma', 29);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(548, '29', 'Emilia Romagna - Piacenza', 29);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(549, '29', 'Emilia Romagna - Ravenna', 29);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(550, '29', 'Emilia Romagna - Reggio Emilia', 29);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(551, '29', 'Emilia Romagna - Rimini', 29);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(552, '29', 'Toscana - Firenze', 29);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(553, '29', 'Toscana - Arezzo', 29);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(554, '29', 'Toscana - Grosseto', 29);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(555, '29', 'Toscana - Livorno', 29);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(556, '29', 'Toscana - Lucca', 29);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(557, '29', 'Toscana - Massa Carrara', 29);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(558, '29', 'Toscana - Pisa', 29);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(559, '29', 'Toscana - Pistoia', 29);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(560, '29', 'Toscana - Prato', 29);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(561, '29', 'Toscana - Siena', 29);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(562, '29', 'Umbria - Perugia', 29);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(563, '29', 'Umbria - Terni', 29);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(564, '29', 'Marche - Ancona', 29);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(565, '29', 'Marche - Ascoli Piceno', 29);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(566, '29', 'Marche - Macerata', 29);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(567, '29', 'Marche - Pesaro - Urbino', 29);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(568, '29', 'Lazio - Roma', 29);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(569, '29', 'Lazio - Frosinone', 29);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(570, '29', 'Lazio - Latina', 29);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(571, '29', 'Lazio - Rieti', 29);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(572, '29', 'Lazio - Viterbo', 29);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(573, '29', 'Abruzzo - L´Aquila', 29);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(574, '29', 'Abruzzo - Chieti', 29);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(575, '29', 'Abruzzo - Pescara', 29);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(576, '29', 'Abruzzo - Teramo', 29);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(577, '29', 'Molise - Campobasso', 29);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(578, '29', 'Molise - Isernia', 29);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(579, '29', 'Campania - Napoli', 29);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(580, '29', 'Campania - Avellino', 29);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(581, '29', 'Campania - Benevento', 29);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(582, '29', 'Campania - Caserta', 29);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(583, '29', 'Campania - Salerno', 29);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(584, '29', 'Puglia - Bari', 29);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(585, '29', 'Puglia - Brindisi', 29);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(586, '29', 'Puglia - Foggia', 29);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(587, '29', 'Puglia - Lecce', 29);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(588, '29', 'Puglia - Taranto', 29);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(589, '29', 'Basilicata - Potenza', 29);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(590, '29', 'Basilicata - Matera', 29);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(591, '29', 'Calabria - Catanzaro', 29);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(592, '29', 'Calabria - Cosenza', 29);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(593, '29', 'Calabria - Crotone', 29);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(594, '29', 'Calabria - Reggio Calabria', 29);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(595, '29', 'Calabria - Vibo Valentia', 29);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(596, '29', 'Sicilia - Palermo', 29);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(597, '29', 'Sicilia - Agrigento', 29);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(598, '29', 'Sicilia - Caltanissetta', 29);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(599, '29', 'Sicilia - Catania', 29);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(600, '29', 'Sicilia - Enna', 29);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(601, '29', 'Sicilia - Messina', 29);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(602, '29', 'Sicilia - Ragusa', 29);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(603, '29', 'Sicilia - Siracusa', 29);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(604, '29', 'Sicilia - Trapani', 29);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(605, '29', 'Sardegna - Cagliari', 29);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(606, '29', 'Sardegna - Nuoro', 29);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(607, '29', 'Sardegna - Oristano', 29);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(608, '29', 'Sardegna - Sassari', 29);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(609, '28', 'Las Palmas', 28);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(610, '28', 'Soria', 28);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(611, '28', 'Palencia', 28);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(612, '28', 'Zamora', 28);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(613, '28', 'Cádiz', 28);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(614, '28', 'Navarra', 28);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(615, '28', 'Ourense', 28);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(616, '28', 'Segovia', 28);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(617, '28', 'Guip&uacute;zcoa', 28);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(618, '28', 'Ciudad Real', 28);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(619, '28', 'Vizcaya', 28);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(620, '28', 'álava', 28);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(621, '28', 'A Coruña', 28);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(622, '28', 'Cantabria', 28);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(623, '28', 'Almería', 28);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(624, '28', 'Zaragoza', 28);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(625, '28', 'Santa Cruz de Tenerife', 28);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(626, '28', 'Cáceres', 28);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(627, '28', 'Guadalajara', 28);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(628, '28', 'ávila', 28);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(629, '28', 'Toledo', 28);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(630, '28', 'Castellón', 28);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(631, '28', 'Tarragona', 28);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(632, '28', 'Lugo', 28);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(633, '28', 'La Rioja', 28);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(634, '28', 'Ceuta', 28);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(635, '28', 'Murcia', 28);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(636, '28', 'Salamanca', 28);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(637, '28', 'Valladolid', 28);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(638, '28', 'Jaén', 28);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(639, '28', 'Girona', 28);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(640, '28', 'Granada', 28);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(641, '28', 'Alacant', 28);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(642, '28', 'Córdoba', 28);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(643, '28', 'Albacete', 28);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(644, '28', 'Cuenca', 28);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(645, '28', 'Pontevedra', 28);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(646, '28', 'Teruel', 28);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(647, '28', 'Melilla', 28);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(648, '28', 'Barcelona', 28);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(649, '28', 'Badajoz', 28);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(650, '28', 'Madrid', 28);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(651, '28', 'Sevilla', 28);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(652, '28', 'Val&egrave;ncia', 28);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(653, '28', 'Huelva', 28);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(654, '28', 'Lleida', 28);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(655, '28', 'León', 28);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(656, '28', 'Illes Balears', 28);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(657, '28', 'Burgos', 28);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(658, '28', 'Huesca', 28);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(659, '28', 'Asturias', 28);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(660, '28', 'Málaga', 28);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(661, '144', 'Afghanistan', 144);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(662, '210', 'Niger', 210);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(663, '133', 'Mali', 133);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(664, '156', 'Burkina Faso', 156);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(665, '136', 'Togo', 136);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(666, '151', 'Benin', 151);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(667, '119', 'Angola', 119);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(668, '102', 'Namibia', 102);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(669, '100', 'Botswana', 100);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(670, '134', 'Madagascar', 134);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(671, '202', 'Mauritius', 202);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(672, '196', 'Laos', 196);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(673, '158', 'Cambodia', 158);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(674, '90', 'Philippines', 90);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(675, '88', 'Papua New Guinea', 88);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(676, '228', 'Solomon Islands', 228);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(677, '240', 'Vanuatu', 240);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(678, '176', 'Fiji', 176);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(679, '223', 'Samoa', 223);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(680, '206', 'Nauru', 206);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(681, '168', 'Cote D&#039;Ivoire', 168);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(682, '198', 'Liberia', 198);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(683, '187', 'Guinea', 187);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(684, '189', 'Guyana', 189);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(685, '98', 'Algeria', 98);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(686, '147', 'Antigua and Barbuda', 147);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(687, '127', 'Bahrain', 127);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(688, '149', 'Bangladesh', 149);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(689, '128', 'Barbados', 128);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(690, '152', 'Bhutan', 152);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(691, '155', 'Brunei', 155);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(692, '157', 'Burundi', 157);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(693, '159', 'Cape Verde', 159);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(694, '130', 'Chad', 130);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(695, '164', 'Comoros', 164);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(696, '112', 'Congo (Brazzaville)', 112);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(697, '169', 'Djibouti', 169);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(698, '171', 'East Timor', 171);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(699, '173', 'Eritrea', 173);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(700, '121', 'Ethiopia', 121);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(701, '180', 'Gabon', 180);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(702, '181', 'Gambia', 181);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(703, '105', 'Ghana', 105);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(704, '197', 'Lesotho', 197);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(705, '125', 'Malawi', 125);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(706, '200', 'Maldives', 200);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(707, '205', 'Myanmar (Burma)', 205);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(708, '107', 'Nepal', 107);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(709, '213', 'Oman', 213);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(710, '217', 'Rwanda', 217);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(711, '91', 'Saudi Arabia', 91);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(712, '120', 'Sri Lanka', 120);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(713, '232', 'Sudan', 232);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(714, '234', 'Swaziland', 234);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(715, '101', 'Tanzania', 101);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(716, '236', 'Tonga', 236);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(717, '239', 'Tuvalu', 239);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(718, '242', 'Western Sahara', 242);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(719, '243', 'Yemen', 243);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(720, '116', 'Zambia', 116);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(721, '96', 'Zimbabwe', 96);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(722, '66', 'Aargau', 66);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(723, '66', 'Appenzell Innerrhoden', 66);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(724, '66', 'Appenzell Ausserrhoden', 66);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(725, '66', 'Bern', 66);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(726, '66', 'Basel-Landschaft', 66);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(727, '66', 'Basel-Stadt', 66);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(728, '66', 'Fribourg', 66);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(729, '66', 'Gen&egrave;ve', 66);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(730, '66', 'Glarus', 66);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(731, '66', 'Graubünden', 66);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(732, '66', 'Jura', 66);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(733, '66', 'Luzern', 66);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(734, '66', 'Neuch&acirc;tel', 66);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(735, '66', 'Nidwalden', 66);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(736, '66', 'Obwalden', 66);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(737, '66', 'Sankt Gallen', 66);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(738, '66', 'Schaffhausen', 66);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(739, '66', 'Solothurn', 66);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(740, '66', 'Schwyz', 66);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(741, '66', 'Thurgau', 66);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(742, '66', 'Ticino', 66);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(743, '66', 'Uri', 66);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(744, '66', 'Vaud', 66);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(745, '66', 'Valais', 66);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(746, '66', 'Zug', 66);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(747, '66', 'Zürich', 66);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(749, '48', 'Aveiro', 48);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(750, '48', 'Beja', 48);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(751, '48', 'Braga', 48);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(752, '48', 'Braganca', 48);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(753, '48', 'Castelo Branco', 48);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(754, '48', 'Coimbra', 48);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(755, '48', 'Evora', 48);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(756, '48', 'Faro', 48);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(757, '48', 'Madeira', 48);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(758, '48', 'Guarda', 48);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(759, '48', 'Leiria', 48);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(760, '48', 'Lisboa', 48);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(761, '48', 'Portalegre', 48);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(762, '48', 'Porto', 48);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(763, '48', 'Santarem', 48);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(764, '48', 'Setubal', 48);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(765, '48', 'Viana do Castelo', 48);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(766, '48', 'Vila Real', 48);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(767, '48', 'Viseu', 48);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(768, '48', 'Azores', 48);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(769, '55', 'Armed Forces Americas', 55);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(770, '55', 'Armed Forces Europe', 55);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(771, '55', 'Alaska', 55);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(772, '55', 'Alabama', 55);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(773, '55', 'Armed Forces Pacific', 55);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(774, '55', 'Arkansas', 55);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(775, '55', 'American Samoa', 55);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(776, '55', 'Arizona', 55);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(777, '55', 'California', 55);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(778, '55', 'Colorado', 55);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(779, '55', 'Connecticut', 55);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(780, '55', 'District of Columbia', 55);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(781, '55', 'Delaware', 55);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(782, '55', 'Florida', 55);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(783, '55', 'Federated States of Micronesia', 55);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(784, '55', 'Georgia', 55);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(786, '55', 'Hawaii', 55);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(787, '55', 'Iowa', 55);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(788, '55', 'Idaho', 55);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(789, '55', 'Illinois', 55);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(790, '55', 'Indiana', 55);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(791, '55', 'Kansas', 55);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(792, '55', 'Kentucky', 55);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(793, '55', 'Louisiana', 55);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(794, '55', 'Massachusetts', 55);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(795, '55', 'Maryland', 55);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(796, '55', 'Maine', 55);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(797, '55', 'Marshall Islands', 55);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(798, '55', 'Michigan', 55);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(799, '55', 'Minnesota', 55);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(800, '55', 'Missouri', 55);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(801, '55', 'Northern Mariana Islands', 55);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(802, '55', 'Mississippi', 55);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(803, '55', 'Montana', 55);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(804, '55', 'North Carolina', 55);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(805, '55', 'North Dakota', 55);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(806, '55', 'Nebraska', 55);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(807, '55', 'New Hampshire', 55);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(808, '55', 'New Jersey', 55);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(809, '55', 'New Mexico', 55);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(810, '55', 'Nevada', 55);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(811, '55', 'New York', 55);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(812, '55', 'Ohio', 55);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(813, '55', 'Oklahoma', 55);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(814, '55', 'Oregon', 55);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(815, '55', 'Pennsylvania', 55);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(816, '246', 'Puerto Rico', 246);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(817, '55', 'Palau', 55);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(818, '55', 'Rhode Island', 55);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(819, '55', 'South Carolina', 55);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(820, '55', 'South Dakota', 55);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(821, '55', 'Tennessee', 55);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(822, '55', 'Texas', 55);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(823, '55', 'Utah', 55);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(824, '55', 'Virginia', 55);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(825, '55', 'Virgin Islands', 55);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(826, '55', 'Vermont', 55);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(827, '55', 'Washington', 55);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(828, '55', 'West Virginia', 55);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(829, '55', 'Wisconsin', 55);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(830, '55', 'Wyoming', 55);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(831, '94', 'Greenland', 94);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(832, '18', 'Brandenburg', 18);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(833, '18', 'Baden-Württemberg', 18);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(834, '18', 'Bayern', 18);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(835, '18', 'Hessen', 18);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(836, '18', 'Hamburg', 18);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(837, '18', 'Mecklenburg-Vorpommern', 18);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(838, '18', 'Niedersachsen', 18);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(839, '18', 'Nordrhein-Westfalen', 18);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(840, '18', 'Rheinland-Pfalz', 18);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(841, '18', 'Schleswig-Holstein', 18);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(842, '18', 'Sachsen', 18);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(843, '18', 'Sachsen-Anhalt', 18);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(844, '18', 'Thüringen', 18);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(845, '18', 'Berlin', 18);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(846, '18', 'Bremen', 18);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(847, '18', 'Saarland', 18);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(848, '13', 'Scotland North', 13);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(849, '13', 'England - East', 13);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(850, '13', 'England - West Midlands', 13);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(851, '13', 'England - South West', 13);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(852, '13', 'England - North West', 13);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(853, '13', 'England - Yorks &amp; Humber', 13);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(854, '13', 'England - South East', 13);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(855, '13', 'England - London', 13);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(856, '13', 'Northern Ireland', 13);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(857, '13', 'England - North East', 13);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(858, '13', 'Wales South', 13);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(859, '13', 'Wales North', 13);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(860, '13', 'England - East Midlands', 13);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(861, '13', 'Scotland Central', 13);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(862, '13', 'Scotland South', 13);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(863, '13', 'Channel Islands', 13);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(864, '13', 'Isle of Man', 13);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(865, '2', 'Burgenland', 2);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(866, '2', 'Kärnten', 2);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(867, '2', 'Niederösterreich', 2);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(868, '2', 'Oberösterreich', 2);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(869, '2', 'Salzburg', 2);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(870, '2', 'Steiermark', 2);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(871, '2', 'Tirol', 2);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(872, '2', 'Vorarlberg', 2);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(873, '2', 'Wien', 2);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(874, '9', 'Bruxelles', 9);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(875, '9', 'West-Vlaanderen', 9);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(876, '9', 'Oost-Vlaanderen', 9);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(877, '9', 'Limburg', 9);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(878, '9', 'Vlaams Brabant', 9);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(879, '9', 'Antwerpen', 9);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(880, '9', 'LiÄge', 9);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(881, '9', 'Namur', 9);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(882, '9', 'Hainaut', 9);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(883, '9', 'Luxembourg', 9);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(884, '9', 'Brabant Wallon', 9);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(887, '67', 'Blekinge Lan', 67);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(888, '67', 'Gavleborgs Lan', 67);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(890, '67', 'Gotlands Lan', 67);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(891, '67', 'Hallands Lan', 67);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(892, '67', 'Jamtlands Lan', 67);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(893, '67', 'Jonkopings Lan', 67);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(894, '67', 'Kalmar Lan', 67);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(895, '67', 'Dalarnas Lan', 67);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(897, '67', 'Kronobergs Lan', 67);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(899, '67', 'Norrbottens Lan', 67);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(900, '67', 'Orebro Lan', 67);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(901, '67', 'Ostergotlands Lan', 67);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(903, '67', 'Sodermanlands Lan', 67);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(904, '67', 'Uppsala Lan', 67);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(905, '67', 'Varmlands Lan', 67);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(906, '67', 'Vasterbottens Lan', 67);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(907, '67', 'Vasternorrlands Lan', 67);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(908, '67', 'Vastmanlands Lan', 67);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(909, '67', 'Stockholms Lan', 67);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(910, '67', 'Skane Lan', 67);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(911, '67', 'Vastra Gotaland', 67);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(913, '46', 'Akershus', 46);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(914, '46', 'Aust-Agder', 46);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(915, '46', 'Buskerud', 46);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(916, '46', 'Finnmark', 46);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(917, '46', 'Hedmark', 46);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(918, '46', 'Hordaland', 46);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(919, '46', 'More og Romsdal', 46);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(920, '46', 'Nordland', 46);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(921, '46', 'Nord-Trondelag', 46);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(922, '46', 'Oppland', 46);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(923, '46', 'Oslo', 46);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(924, '46', 'Ostfold', 46);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(925, '46', 'Rogaland', 46);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(926, '46', 'Sogn og Fjordane', 46);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(927, '46', 'Sor-Trondelag', 46);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(928, '46', 'Telemark', 46);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(929, '46', 'Troms', 46);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(930, '46', 'Vest-Agder', 46);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(931, '46', 'Vestfold', 46);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(933, '63', '&ETH;&bull;land', 63);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(934, '63', 'Lapland', 63);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(935, '63', 'Oulu', 63);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(936, '63', 'Southern Finland', 63);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(937, '63', 'Eastern Finland', 63);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(938, '63', 'Western Finland', 63);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(940, '22', 'Arhus', 22);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(941, '22', 'Bornholm', 22);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(942, '22', 'Frederiksborg', 22);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(943, '22', 'Fyn', 22);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(944, '22', 'Kobenhavn', 22);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(945, '22', 'Staden Kobenhavn', 22);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(946, '22', 'Nordjylland', 22);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(947, '22', 'Ribe', 22);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(948, '22', 'Ringkobing', 22);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(949, '22', 'Roskilde', 22);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(950, '22', 'Sonderjylland', 22);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(951, '22', 'Storstrom', 22);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(952, '22', 'Vejle', 22);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(953, '22', 'Vestsjalland', 22);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(954, '22', 'Viborg', 22);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(956, '65', 'Hlavni Mesto Praha', 65);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(957, '65', 'Jihomoravsky Kraj', 65);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(958, '65', 'Jihocesky Kraj', 65);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(959, '65', 'Vysocina', 65);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(960, '65', 'Karlovarsky Kraj', 65);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(961, '65', 'Kralovehradecky Kraj', 65);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(962, '65', 'Liberecky Kraj', 65);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(963, '65', 'Olomoucky Kraj', 65);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(964, '65', 'Moravskoslezsky Kraj', 65);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(965, '65', 'Pardubicky Kraj', 65);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(966, '65', 'Plzensky Kraj', 65);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(967, '65', 'Stredocesky Kraj', 65);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(968, '65', 'Ustecky Kraj', 65);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(969, '65', 'Zlinsky Kraj', 65);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(971, '114', 'Berat', 114);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(972, '114', 'Diber', 114);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(973, '114', 'Durres', 114);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(974, '114', 'Elbasan', 114);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(975, '114', 'Fier', 114);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(976, '114', 'Gjirokaster', 114);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(977, '114', 'Korce', 114);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(978, '114', 'Kukes', 114);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(979, '114', 'Lezhe', 114);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(980, '114', 'Shkoder', 114);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(981, '114', 'Tirane', 114);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(982, '114', 'Vlore', 114);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(984, '145', 'Canillo', 145);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(985, '145', 'Encamp', 145);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(986, '145', 'La Massana', 145);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(987, '145', 'Ordino', 145);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(988, '145', 'Sant Julia de Loria', 145);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(989, '145', 'Andorra la Vella', 145);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(990, '145', 'Escaldes-Engordany', 145);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(992, '6', 'Aragatsotn', 6);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(993, '6', 'Ararat', 6);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(994, '6', 'Armavir', 6);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(995, '6', 'Geghark&#039;unik&#039;', 6);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(996, '6', 'Kotayk&#039;', 6);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(997, '6', 'Lorri', 6);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(998, '6', 'Shirak', 6);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(999, '6', 'Syunik&#039;', 6);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1000, '6', 'Tavush', 6);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1001, '6', 'Vayots&#039; Dzor', 6);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1002, '6', 'Yerevan', 6);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1004, '79', 'Federation of Bosnia and Herzegovina', 79);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1005, '79', 'Republika Srpska', 79);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1007, '11', 'Mikhaylovgrad', 11);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1008, '11', 'Blagoevgrad', 11);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1009, '11', 'Burgas', 11);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1010, '11', 'Dobrich', 11);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1011, '11', 'Gabrovo', 11);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1012, '11', 'Grad Sofiya', 11);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1013, '11', 'Khaskovo', 11);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1014, '11', 'Kurdzhali', 11);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1015, '11', 'Kyustendil', 11);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1016, '11', 'Lovech', 11);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1017, '11', 'Montana', 11);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1018, '11', 'Pazardzhik', 11);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1019, '11', 'Pernik', 11);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1020, '11', 'Pleven', 11);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1021, '11', 'Plovdiv', 11);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1022, '11', 'Razgrad', 11);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1023, '11', 'Ruse', 11);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1024, '11', 'Shumen', 11);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1025, '11', 'Silistra', 11);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1026, '11', 'Sliven', 11);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1027, '11', 'Smolyan', 11);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1028, '11', 'Sofiya', 11);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1029, '11', 'Stara Zagora', 11);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1030, '11', 'Turgovishte', 11);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1031, '11', 'Varna', 11);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1032, '11', 'Veliko Turnovo', 11);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1033, '11', 'Vidin', 11);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1034, '11', 'Vratsa', 11);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1035, '11', 'Yambol', 11);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1037, '71', 'Bjelovarsko-Bilogorska', 71);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1038, '71', 'Brodsko-Posavska', 71);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1039, '71', 'Dubrovacko-Neretvanska', 71);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1040, '71', 'Istarska', 71);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1041, '71', 'Karlovacka', 71);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1042, '71', 'Koprivnicko-Krizevacka', 71);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1043, '71', 'Krapinsko-Zagorska', 71);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1044, '71', 'Licko-Senjska', 71);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1045, '71', 'Medimurska', 71);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1046, '71', 'Osjecko-Baranjska', 71);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1047, '71', 'Pozesko-Slavonska', 71);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1048, '71', 'Primorsko-Goranska', 71);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1049, '71', 'Sibensko-Kninska', 71);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1050, '71', 'Sisacko-Moslavacka', 71);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1051, '71', 'Splitsko-Dalmatinska', 71);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1052, '71', 'Varazdinska', 71);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1053, '71', 'Viroviticko-Podravska', 71);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1054, '71', 'Vukovarsko-Srijemska', 71);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1055, '71', 'Zadarska', 71);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1056, '71', 'Zagrebacka', 71);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1057, '71', 'Grad Zagreb', 71);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1059, '143', 'Gibraltar', 143);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1060, '20', 'Evros', 20);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1061, '20', 'Rodhopi', 20);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1062, '20', 'Xanthi', 20);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1063, '20', 'Drama', 20);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1064, '20', 'Serrai', 20);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1065, '20', 'Kilkis', 20);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1066, '20', 'Pella', 20);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1067, '20', 'Florina', 20);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1068, '20', 'Kastoria', 20);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1069, '20', 'Grevena', 20);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1070, '20', 'Kozani', 20);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1071, '20', 'Imathia', 20);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1072, '20', 'Thessaloniki', 20);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1073, '20', 'Kavala', 20);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1074, '20', 'Khalkidhiki', 20);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1075, '20', 'Pieria', 20);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1076, '20', 'Ioannina', 20);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1077, '20', 'Thesprotia', 20);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1078, '20', 'Preveza', 20);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1079, '20', 'Arta', 20);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1080, '20', 'Larisa', 20);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1081, '20', 'Trikala', 20);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1082, '20', 'Kardhitsa', 20);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1083, '20', 'Magnisia', 20);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1084, '20', 'Kerkira', 20);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1085, '20', 'Levkas', 20);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1086, '20', 'Kefallinia', 20);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1087, '20', 'Zakinthos', 20);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1088, '20', 'Fthiotis', 20);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1089, '20', 'Evritania', 20);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1090, '20', 'Aitolia kai Akarnania', 20);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1091, '20', 'Fokis', 20);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1092, '20', 'Voiotia', 20);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1093, '20', 'Evvoia', 20);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1094, '20', 'Attiki', 20);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1095, '20', 'Argolis', 20);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1096, '20', 'Korinthia', 20);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1097, '20', 'Akhaia', 20);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1098, '20', 'Ilia', 20);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1099, '20', 'Messinia', 20);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1100, '20', 'Arkadhia', 20);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1101, '20', 'Lakonia', 20);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1102, '20', 'Khania', 20);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1103, '20', 'Rethimni', 20);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1104, '20', 'Iraklion', 20);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1105, '20', 'Lasithi', 20);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1106, '20', 'Dhodhekanisos', 20);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1107, '20', 'Samos', 20);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1108, '20', 'Kikladhes', 20);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1109, '20', 'Khios', 20);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1110, '20', 'Lesvos', 20);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1112, '14', 'Bacs-Kiskun', 14);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1113, '14', 'Baranya', 14);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1114, '14', 'Bekes', 14);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1115, '14', 'Borsod-Abauj-Zemplen', 14);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1116, '14', 'Budapest', 14);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1117, '14', 'Csongrad', 14);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1118, '14', 'Debrecen', 14);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1119, '14', 'Fejer', 14);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1120, '14', 'Gyor-Moson-Sopron', 14);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1121, '14', 'Hajdu-Bihar', 14);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1122, '14', 'Heves', 14);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1123, '14', 'Komarom-Esztergom', 14);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1124, '14', 'Miskolc', 14);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1125, '14', 'Nograd', 14);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1126, '14', 'Pecs', 14);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1127, '14', 'Pest', 14);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1128, '14', 'Somogy', 14);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1129, '14', 'Szabolcs-Szatmar-Bereg', 14);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1130, '14', 'Szeged', 14);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1131, '14', 'Jasz-Nagykun-Szolnok', 14);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1132, '14', 'Tolna', 14);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1133, '14', 'Vas', 14);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1134, '14', 'Veszprem', 14);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1135, '14', 'Zala', 14);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1136, '14', 'Gyor', 14);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1150, '14', 'Veszprem', 14);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1152, '126', 'Balzers', 126);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1153, '126', 'Eschen', 126);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1154, '126', 'Gamprin', 126);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1155, '126', 'Mauren', 126);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1156, '126', 'Planken', 126);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1157, '126', 'Ruggell', 126);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1158, '126', 'Schaan', 126);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1159, '126', 'Schellenberg', 126);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1160, '126', 'Triesen', 126);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1161, '126', 'Triesenberg', 126);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1162, '126', 'Vaduz', 126);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1163, '41', 'Diekirch', 41);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1164, '41', 'Grevenmacher', 41);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1165, '41', 'Luxembourg', 41);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1167, '85', 'Aracinovo', 85);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1168, '85', 'Bac', 85);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1169, '85', 'Belcista', 85);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1170, '85', 'Berovo', 85);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1171, '85', 'Bistrica', 85);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1172, '85', 'Bitola', 85);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1173, '85', 'Blatec', 85);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1174, '85', 'Bogdanci', 85);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1175, '85', 'Bogomila', 85);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1176, '85', 'Bogovinje', 85);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1177, '85', 'Bosilovo', 85);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1179, '85', 'Cair', 85);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1180, '85', 'Capari', 85);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1181, '85', 'Caska', 85);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1182, '85', 'Cegrane', 85);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1184, '85', 'Centar Zupa', 85);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1187, '85', 'Debar', 85);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1188, '85', 'Delcevo', 85);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1190, '85', 'Demir Hisar', 85);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1191, '85', 'Demir Kapija', 85);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1195, '85', 'Dorce Petrov', 85);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1198, '85', 'Gazi Baba', 85);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1199, '85', 'Gevgelija', 85);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1200, '85', 'Gostivar', 85);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1201, '85', 'Gradsko', 85);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1204, '85', 'Jegunovce', 85);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1205, '85', 'Kamenjane', 85);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1207, '85', 'Karpos', 85);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1208, '85', 'Kavadarci', 85);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1209, '85', 'Kicevo', 85);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1210, '85', 'Kisela Voda', 85);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1211, '85', 'Klecevce', 85);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1212, '85', 'Kocani', 85);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1214, '85', 'Kondovo', 85);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1217, '85', 'Kratovo', 85);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1219, '85', 'Krivogastani', 85);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1220, '85', 'Krusevo', 85);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1223, '85', 'Kumanovo', 85);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1224, '85', 'Labunista', 85);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1225, '85', 'Lipkovo', 85);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1228, '85', 'Makedonska Kamenica', 85);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1229, '85', 'Makedonski Brod', 85);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1234, '85', 'Murtino', 85);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1235, '85', 'Negotino', 85);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1238, '85', 'Novo Selo', 85);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1240, '85', 'Ohrid', 85);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1242, '85', 'Orizari', 85);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1245, '85', 'Petrovec', 85);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1248, '85', 'Prilep', 85);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1249, '85', 'Probistip', 85);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1250, '85', 'Radovis', 85);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1252, '85', 'Resen', 85);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1253, '85', 'Rosoman', 85);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1256, '85', 'Saraj', 85);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1260, '85', 'Srbinovo', 85);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1262, '85', 'Star Dojran', 85);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1264, '85', 'Stip', 85);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1265, '85', 'Struga', 85);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1266, '85', 'Strumica', 85);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1267, '85', 'Studenicani', 85);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1268, '85', 'Suto Orizari', 85);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1269, '85', 'Sveti Nikole', 85);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1270, '85', 'Tearce', 85);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1271, '85', 'Tetovo', 85);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1273, '85', 'Valandovo', 85);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1275, '85', 'Veles', 85);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1277, '85', 'Vevcani', 85);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1278, '85', 'Vinica', 85);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1281, '85', 'Vrapciste', 85);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1286, '85', 'Zelino', 85);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1289, '85', 'Zrnovci', 85);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1291, '86', 'Malta', 86);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1292, '44', 'La Condamine', 44);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1293, '44', 'Monaco', 44);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1294, '44', 'Monte-Carlo', 44);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1295, '47', 'Biala Podlaska', 47);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1296, '47', 'Bialystok', 47);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1297, '47', 'Bielsko', 47);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1298, '47', 'Bydgoszcz', 47);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1299, '47', 'Chelm', 47);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1300, '47', 'Ciechanow', 47);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1301, '47', 'Czestochowa', 47);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1302, '47', 'Elblag', 47);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1303, '47', 'Gdansk', 47);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1304, '47', 'Gorzow', 47);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1305, '47', 'Jelenia Gora', 47);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1306, '47', 'Kalisz', 47);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1307, '47', 'Katowice', 47);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1308, '47', 'Kielce', 47);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1309, '47', 'Konin', 47);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1310, '47', 'Koszalin', 47);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1311, '47', 'Krakow', 47);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1312, '47', 'Krosno', 47);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1313, '47', 'Legnica', 47);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1314, '47', 'Leszno', 47);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1315, '47', 'Lodz', 47);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1316, '47', 'Lomza', 47);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1317, '47', 'Lublin', 47);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1318, '47', 'Nowy Sacz', 47);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1319, '47', 'Olsztyn', 47);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1320, '47', 'Opole', 47);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1321, '47', 'Ostroleka', 47);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1322, '47', 'Pila', 47);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1323, '47', 'Piotrkow', 47);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1324, '47', 'Plock', 47);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1325, '47', 'Poznan', 47);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1326, '47', 'Przemysl', 47);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1327, '47', 'Radom', 47);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1328, '47', 'Rzeszow', 47);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1329, '47', 'Siedlce', 47);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1330, '47', 'Sieradz', 47);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1331, '47', 'Skierniewice', 47);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1332, '47', 'Slupsk', 47);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1333, '47', 'Suwalki', 47);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1335, '47', 'Tarnobrzeg', 47);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1336, '47', 'Tarnow', 47);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1337, '47', 'Torun', 47);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1338, '47', 'Walbrzych', 47);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1339, '47', 'Warszawa', 47);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1340, '47', 'Wloclawek', 47);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1341, '47', 'Wroclaw', 47);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1342, '47', 'Zamosc', 47);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1343, '47', 'Zielona Gora', 47);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1344, '47', 'Dolnoslaskie', 47);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1345, '47', 'Kujawsko-Pomorskie', 47);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1346, '47', 'Lodzkie', 47);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1347, '47', 'Lubelskie', 47);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1348, '47', 'Lubuskie', 47);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1349, '47', 'Malopolskie', 47);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1350, '47', 'Mazowieckie', 47);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1351, '47', 'Opolskie', 47);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1352, '47', 'Podkarpackie', 47);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1353, '47', 'Podlaskie', 47);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1354, '47', 'Pomorskie', 47);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1355, '47', 'Slaskie', 47);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1356, '47', 'Swietokrzyskie', 47);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1357, '47', 'Warminsko-Mazurskie', 47);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1358, '47', 'Wielkopolskie', 47);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1359, '47', 'Zachodniopomorskie', 47);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1361, '72', 'Alba', 72);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1362, '72', 'Arad', 72);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1363, '72', 'Arges', 72);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1364, '72', 'Bacau', 72);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1365, '72', 'Bihor', 72);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1366, '72', 'Bistrita-Nasaud', 72);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1367, '72', 'Botosani', 72);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1368, '72', 'Braila', 72);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1369, '72', 'Brasov', 72);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1370, '72', 'Bucuresti', 72);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1371, '72', 'Buzau', 72);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1372, '72', 'Caras-Severin', 72);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1373, '72', 'Cluj', 72);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1374, '72', 'Constanta', 72);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1375, '72', 'Covasna', 72);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1376, '72', 'Dambovita', 72);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1377, '72', 'Dolj', 72);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1378, '72', 'Galati', 72);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1379, '72', 'Gorj', 72);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1380, '72', 'Harghita', 72);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1381, '72', 'Hunedoara', 72);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1382, '72', 'Ialomita', 72);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1383, '72', 'Iasi', 72);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1384, '72', 'Maramures', 72);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1385, '72', 'Mehedinti', 72);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1386, '72', 'Mures', 72);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1387, '72', 'Neamt', 72);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1388, '72', 'Olt', 72);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1389, '72', 'Prahova', 72);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1390, '72', 'Salaj', 72);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1391, '72', 'Satu Mare', 72);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1392, '72', 'Sibiu', 72);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1393, '72', 'Suceava', 72);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1394, '72', 'Teleorman', 72);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1395, '72', 'Timis', 72);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1396, '72', 'Tulcea', 72);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1397, '72', 'Vaslui', 72);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1398, '72', 'Valcea', 72);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1399, '72', 'Vrancea', 72);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1400, '72', 'Calarasi', 72);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1401, '72', 'Giurgiu', 72);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1404, '224', 'Acquaviva', 224);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1405, '224', 'Chiesanuova', 224);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1406, '224', 'Domagnano', 224);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1407, '224', 'Faetano', 224);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1408, '224', 'Fiorentino', 224);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1409, '224', 'Borgo Maggiore', 224);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1410, '224', 'San Marino', 224);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1411, '224', 'Monte Giardino', 224);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1412, '224', 'Serravalle', 224);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1413, '52', 'Banska Bystrica', 52);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1414, '52', 'Bratislava', 52);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1415, '52', 'Kosice', 52);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1416, '52', 'Nitra', 52);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1417, '52', 'Presov', 52);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1418, '52', 'Trencin', 52);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1419, '52', 'Trnava', 52);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1420, '52', 'Zilina', 52);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1423, '53', 'Beltinci', 53);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1425, '53', 'Bohinj', 53);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1426, '53', 'Borovnica', 53);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1427, '53', 'Bovec', 53);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1428, '53', 'Brda', 53);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1429, '53', 'Brezice', 53);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1430, '53', 'Brezovica', 53);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1432, '53', 'Cerklje na Gorenjskem', 53);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1434, '53', 'Cerkno', 53);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1436, '53', 'Crna na Koroskem', 53);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1437, '53', 'Crnomelj', 53);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1438, '53', 'Divaca', 53);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1439, '53', 'Dobrepolje', 53);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1440, '53', 'Dol pri Ljubljani', 53);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1443, '53', 'Duplek', 53);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1447, '53', 'Gornji Grad', 53);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1450, '53', 'Hrastnik', 53);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1451, '53', 'Hrpelje-Kozina', 53);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1452, '53', 'Idrija', 53);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1453, '53', 'Ig', 53);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1454, '53', 'Ilirska Bistrica', 53);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1455, '53', 'Ivancna Gorica', 53);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1462, '53', 'Komen', 53);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1463, '53', 'Koper-Capodistria', 53);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1464, '53', 'Kozje', 53);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1465, '53', 'Kranj', 53);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1466, '53', 'Kranjska Gora', 53);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1467, '53', 'Krsko', 53);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1469, '53', 'Lasko', 53);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1470, '53', 'Ljubljana', 53);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1471, '53', 'Ljubno', 53);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1472, '53', 'Logatec', 53);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1475, '53', 'Medvode', 53);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1476, '53', 'Menges', 53);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1478, '53', 'Mezica', 53);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1480, '53', 'Moravce', 53);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1482, '53', 'Mozirje', 53);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1483, '53', 'Murska Sobota', 53);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1487, '53', 'Nova Gorica', 53);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1489, '53', 'Ormoz', 53);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1491, '53', 'Pesnica', 53);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1494, '53', 'Postojna', 53);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1497, '53', 'Radece', 53);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1498, '53', 'Radenci', 53);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1500, '53', 'Radovljica', 53);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1502, '53', 'Rogaska Slatina', 53);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1505, '53', 'Sencur', 53);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1506, '53', 'Sentilj', 53);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1508, '53', 'Sevnica', 53);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1509, '53', 'Sezana', 53);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1511, '53', 'Skofja Loka', 53);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1513, '53', 'Slovenj Gradec', 53);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1514, '53', 'Slovenske Konjice', 53);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1515, '53', 'Smarje pri Jelsah', 53);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1521, '53', 'Tolmin', 53);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1522, '53', 'Trbovlje', 53);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1524, '53', 'Trzic', 53);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1526, '53', 'Velenje', 53);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1528, '53', 'Vipava', 53);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1531, '53', 'Vrhnika', 53);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1532, '53', 'Vuzenica', 53);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1533, '53', 'Zagorje ob Savi', 53);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1535, '53', 'Zelezniki', 53);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1536, '53', 'Ziri', 53);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1537, '53', 'Zrece', 53);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1539, '53', 'Domzale', 53);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1540, '53', 'Jesenice', 53);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1541, '53', 'Kamnik', 53);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1542, '53', 'Kocevje', 53);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1544, '53', 'Lenart', 53);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1545, '53', 'Litija', 53);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1546, '53', 'Ljutomer', 53);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1550, '53', 'Maribor', 53);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1552, '53', 'Novo Mesto', 53);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1553, '53', 'Piran', 53);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1554, '53', 'Preddvor', 53);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1555, '53', 'Ptuj', 53);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1556, '53', 'Ribnica', 53);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1558, '53', 'Sentjur pri Celju', 53);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1559, '53', 'Slovenska Bistrica', 53);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1560, '53', 'Videm', 53);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1562, '53', 'Zalec', 53);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1564, '109', 'Seychelles', 109);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1565, '108', 'Mauritania', 108);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1566, '135', 'Senegal', 135);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1567, '154', 'Road Town', 154);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1568, '165', 'Congo', 165);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1569, '166', 'Avarua', 166);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1570, '172', 'Malabo', 172);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1571, '175', 'Torshavn', 175);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1572, '178', 'Papeete', 178);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1573, '184', 'St George&#039;s', 184);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1574, '186', 'St Peter Port', 186);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1575, '188', 'Bissau', 188);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1576, '193', 'Saint Helier', 193);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1577, '201', 'Fort-de-France', 201);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1578, '207', 'Willemstad', 207);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1579, '208', 'Noumea', 208);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1580, '212', 'Kingston', 212);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1581, '215', 'Adamstown', 215);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1582, '216', 'Doha', 216);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1583, '218', 'Jamestown', 218);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1584, '219', 'Basseterre', 219);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1585, '220', 'Castries', 220);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1586, '221', 'Saint Pierre', 221);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1587, '222', 'Kingstown', 222);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1588, '225', 'San Tome', 225);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1589, '226', 'Belgrade', 226);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1590, '227', 'Freetown', 227);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1591, '229', 'Mogadishu', 229);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1592, '235', 'Fakaofo', 235);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1593, '237', 'Port of Spain', 237);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1594, '241', 'Mata-Utu', 241);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1596, '89', 'Amazonas', 89);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1597, '89', 'Ancash', 89);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1598, '89', 'Apurímac', 89);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1599, '89', 'Arequipa', 89);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1600, '89', 'Ayacucho', 89);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1601, '89', 'Cajamarca', 89);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1602, '89', 'Callao', 89);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1603, '89', 'Cusco', 89);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1604, '89', 'Huancavelica', 89);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1605, '89', 'Huánuco', 89);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1606, '89', 'Ica', 89);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1607, '89', 'Junín', 89);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1608, '89', 'La Libertad', 89);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1609, '89', 'Lambayeque', 89);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1610, '89', 'Lima', 89);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1611, '89', 'Loreto', 89);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1612, '89', 'Madre de Dios', 89);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1613, '89', 'Moquegua', 89);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1614, '89', 'Pasco', 89);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1615, '89', 'Piura', 89);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1616, '89', 'Puno', 89);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1617, '89', 'San Martín', 89);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1618, '89', 'Tacna', 89);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1619, '89', 'Tumbes', 89);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1620, '89', 'Ucayali', 89);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1622, '110', 'Alto Paraná', 110);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1623, '110', 'Amambay', 110);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1624, '110', 'Boquerón', 110);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1625, '110', 'Caaguaz&uacute;', 110);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1626, '110', 'Caazapá', 110);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1627, '110', 'Central', 110);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1628, '110', 'Concepción', 110);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1629, '110', 'Cordillera', 110);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1630, '110', 'Guairá', 110);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1631, '110', 'Itap&uacute;a', 110);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1632, '110', 'Misiones', 110);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1633, '110', 'Neembuc&uacute;', 110);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1634, '110', 'Paraguarí', 110);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1635, '110', 'Presidente Hayes', 110);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1636, '110', 'San Pedro', 110);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1637, '110', 'Alto Paraguay', 110);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1638, '110', 'Canindey&uacute;', 110);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1639, '110', 'Chaco', 110);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1642, '111', 'Artigas', 111);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1643, '111', 'Canelones', 111);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1644, '111', 'Cerro Largo', 111);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1645, '111', 'Colonia', 111);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1646, '111', 'Durazno', 111);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1647, '111', 'Flores', 111);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1648, '111', 'Florida', 111);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1649, '111', 'Lavalleja', 111);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1650, '111', 'Maldonado', 111);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1651, '111', 'Montevideo', 111);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1652, '111', 'Paysand&uacute;', 111);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1653, '111', 'Río Negro', 111);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1654, '111', 'Rivera', 111);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1655, '111', 'Rocha', 111);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1656, '111', 'Salto', 111);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1657, '111', 'San José', 111);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1658, '111', 'Soriano', 111);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1659, '111', 'Tacuarembó', 111);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1660, '111', 'Treinta y Tres', 111);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1662, '81', 'Valparaíso', 81);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1663, '81', 'Aisén del General Carlos Ibánez del Campo', 81);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1664, '81', 'Antofagasta', 81);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1665, '81', 'Araucanía', 81);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1666, '81', 'Atacama', 81);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1667, '81', 'Bío-Bío', 81);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1668, '81', 'Coquimbo', 81);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1669, '81', 'Libertador General Bernardo O&#039;Higgins', 81);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1670, '81', 'Los Lagos', 81);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1671, '81', 'Magallanes y de la Antártica Chilena', 81);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1672, '81', 'Maule', 81);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1673, '81', 'Region Metropolitana', 81);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1674, '81', 'Tarapacá', 81);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1676, '185', 'Alta Verapaz', 185);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1677, '185', 'Baja Verapaz', 185);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1678, '185', 'Chimaltenango', 185);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1679, '185', 'Chiquimula', 185);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1680, '185', 'El Progreso', 185);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1681, '185', 'Escuintla', 185);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1682, '185', 'Guatemala', 185);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1683, '185', 'Huehuetenango', 185);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1684, '185', 'Izabal', 185);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1685, '185', 'Jalapa', 185);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1686, '185', 'Jutiapa', 185);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1687, '185', 'Petén', 185);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1688, '185', 'Quetzaltenango', 185);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1689, '185', 'Quiché', 185);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1690, '185', 'Retalhuleu', 185);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1691, '185', 'Sacatepéquez', 185);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1692, '185', 'San Marcos', 185);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1693, '185', 'Santa Rosa', 185);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1694, '185', 'Sololá', 185);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1695, '185', 'Suchitepequez', 185);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1696, '185', 'Totonicapán', 185);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1697, '185', 'Zacapa', 185);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1699, '82', 'Amazonas', 82);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1700, '82', 'Antioquia', 82);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1701, '82', 'Arauca', 82);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1702, '82', 'Atlántico', 82);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1703, '82', 'Caquetá', 82);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1704, '82', 'Cauca', 82);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1705, '82', 'César', 82);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1706, '82', 'Chocó', 82);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1707, '82', 'Córdoba', 82);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1708, '82', 'Guaviare', 82);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1709, '82', 'Guainía', 82);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1710, '82', 'Huila', 82);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1711, '82', 'La Guajira', 82);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1712, '82', 'Meta', 82);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1713, '82', 'Narino', 82);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1714, '82', 'Norte de Santander', 82);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1715, '82', 'Putumayo', 82);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1716, '82', 'Quindío', 82);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1717, '82', 'Risaralda', 82);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1718, '82', 'San Andrés y Providencia', 82);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1719, '82', 'Santander', 82);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1720, '82', 'Sucre', 82);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1721, '82', 'Tolima', 82);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1722, '82', 'Valle del Cauca', 82);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1723, '82', 'Vaupés', 82);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1724, '82', 'Vichada', 82);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1725, '82', 'Casanare', 82);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1726, '82', 'Cundinamarca', 82);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1727, '82', 'Distrito Especial', 82);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1730, '82', 'Caldas', 82);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1731, '82', 'Magdalena', 82);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1733, '42', 'Aguascalientes', 42);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1734, '42', 'Baja California', 42);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1735, '42', 'Baja California Sur', 42);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1736, '42', 'Campeche', 42);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1737, '42', 'Chiapas', 42);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1738, '42', 'Chihuahua', 42);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1739, '42', 'Coahuila de Zaragoza', 42);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1740, '42', 'Colima', 42);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1741, '42', 'Distrito Federal', 42);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1742, '42', 'Durango', 42);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1743, '42', 'Guanajuato', 42);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1744, '42', 'Guerrero', 42);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1745, '42', 'Hidalgo', 42);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1746, '42', 'Jalisco', 42);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1747, '42', 'México', 42);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1748, '42', 'Michoacán de Ocampo', 42);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1749, '42', 'Morelos', 42);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1750, '42', 'Nayarit', 42);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1751, '42', 'Nuevo León', 42);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1752, '42', 'Oaxaca', 42);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1753, '42', 'Puebla', 42);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1754, '42', 'Querétaro de Arteaga', 42);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1755, '42', 'Quintana Roo', 42);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1756, '42', 'San Luis Potosí', 42);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1757, '42', 'Sinaloa', 42);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1758, '42', 'Sonora', 42);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1759, '42', 'Tabasco', 42);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1760, '42', 'Tamaulipas', 42);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1761, '42', 'Tlaxcala', 42);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1762, '42', 'Veracruz-Llave', 42);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1763, '42', 'Yucatán', 42);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1764, '42', 'Zacatecas', 42);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1766, '124', 'Bocas del Toro', 124);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1767, '124', 'Chiriquí', 124);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1768, '124', 'Coclé', 124);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1769, '124', 'Colón', 124);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1770, '124', 'Darién', 124);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1771, '124', 'Herrera', 124);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1772, '124', 'Los Santos', 124);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1773, '124', 'Panamá', 124);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1774, '124', 'San Blas', 124);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1775, '124', 'Veraguas', 124);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1777, '123', 'Chuquisaca', 123);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1778, '123', 'Cochabamba', 123);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1779, '123', 'El Beni', 123);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1780, '123', 'La Paz', 123);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1781, '123', 'Oruro', 123);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1782, '123', 'Pando', 123);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1783, '123', 'Potosí', 123);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1784, '123', 'Santa Cruz', 123);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1785, '123', 'Tarija', 123);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1787, '36', 'Alajuela', 36);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1788, '36', 'Cartago', 36);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1789, '36', 'Guanacaste', 36);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1790, '36', 'Heredia', 36);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1791, '36', 'Limón', 36);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1792, '36', 'Puntarenas', 36);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1793, '36', 'San José', 36);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1795, '103', 'Galápagos', 103);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1796, '103', 'Azuay', 103);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1797, '103', 'Bolívar', 103);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1798, '103', 'Canar', 103);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1799, '103', 'Carchi', 103);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1800, '103', 'Chimborazo', 103);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1801, '103', 'Cotopaxi', 103);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1802, '103', 'El Oro', 103);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1803, '103', 'Esmeraldas', 103);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1804, '103', 'Guayas', 103);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1805, '103', 'Imbabura', 103);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1806, '103', 'Loja', 103);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1807, '103', 'Los Ríos', 103);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1808, '103', 'Manabí', 103);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1809, '103', 'Morona-Santiago', 103);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1810, '103', 'Pastaza', 103);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1811, '103', 'Pichincha', 103);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1812, '103', 'Tungurahua', 103);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1813, '103', 'Zamora-Chinchipe', 103);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1814, '103', 'Sucumbíos', 103);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1815, '103', 'Napo', 103);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1816, '103', 'Orellana', 103);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1818, '5', 'Buenos Aires', 5);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1819, '5', 'Catamarca', 5);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1820, '5', 'Chaco', 5);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1821, '5', 'Chubut', 5);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1822, '5', 'Córdoba', 5);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1823, '5', 'Corrientes', 5);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1824, '5', 'Distrito Federal', 5);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1825, '5', 'Entre Ríos', 5);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1826, '5', 'Formosa', 5);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1827, '5', 'Jujuy', 5);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1828, '5', 'La Pampa', 5);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1829, '5', 'La Rioja', 5);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1830, '5', 'Mendoza', 5);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1831, '5', 'Misiones', 5);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1832, '5', 'Neuquén', 5);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1833, '5', 'Río Negro', 5);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1834, '5', 'Salta', 5);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1835, '5', 'San Juan', 5);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1836, '5', 'San Luis', 5);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1837, '5', 'Santa Cruz', 5);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1838, '5', 'Santa Fe', 5);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1839, '5', 'Santiago del Estero', 5);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1840, '5', 'Tierra del Fuego', 5);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1841, '5', 'Tucumán', 5);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1843, '95', 'Amazonas', 95);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1844, '95', 'Anzoategui', 95);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1845, '95', 'Apure', 95);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1846, '95', 'Aragua', 95);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1847, '95', 'Barinas', 95);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1848, '95', 'Bolívar', 95);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1849, '95', 'Carabobo', 95);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1850, '95', 'Cojedes', 95);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1851, '95', 'Delta Amacuro', 95);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1852, '95', 'Falcón', 95);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1853, '95', 'Guárico', 95);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1854, '95', 'Lara', 95);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1855, '95', 'Mérida', 95);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1856, '95', 'Miranda', 95);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1857, '95', 'Monagas', 95);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1858, '95', 'Nueva Esparta', 95);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1859, '95', 'Portuguesa', 95);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1860, '95', 'Sucre', 95);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1861, '95', 'Táchira', 95);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1862, '95', 'Trujillo', 95);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1863, '95', 'Yaracuy', 95);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1864, '95', 'Zulia', 95);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1865, '95', 'Dependencias Federales', 95);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1866, '95', 'Distrito Capital', 95);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1867, '95', 'Vargas', 95);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1869, '209', 'Boaco', 209);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1870, '209', 'Carazo', 209);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1871, '209', 'Chinandega', 209);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1872, '209', 'Chontales', 209);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1873, '209', 'Estelí', 209);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1874, '209', 'Granada', 209);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1875, '209', 'Jinotega', 209);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1876, '209', 'León', 209);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1877, '209', 'Madriz', 209);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1878, '209', 'Managua', 209);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1879, '209', 'Masaya', 209);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1880, '209', 'Matagalpa', 209);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1881, '209', 'Nueva Segovia', 209);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1882, '209', 'Rio San Juan', 209);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1883, '209', 'Rivas', 209);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1884, '209', 'Zelaya', 209);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1886, '113', 'Pinar del Rio', 113);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1887, '113', 'Ciudad de la Habana', 113);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1888, '113', 'Matanzas', 113);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1889, '113', 'Isla de la Juventud', 113);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1890, '113', 'Camaguey', 113);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1891, '113', 'Ciego de Avila', 113);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1892, '113', 'Cienfuegos', 113);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1893, '113', 'Granma', 113);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1894, '113', 'Guantanamo', 113);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1895, '113', 'La Habana', 113);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1896, '113', 'Holguin', 113);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1897, '113', 'Las Tunas', 113);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1898, '113', 'Sancti Spiritus', 113);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1899, '113', 'Santiago de Cuba', 113);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1900, '113', 'Villa Clara', 113);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1901, '12', 'Acre', 12);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1902, '12', 'Alagoas', 12);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1903, '12', 'Amapa', 12);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1904, '12', 'Amazonas', 12);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1905, '12', 'Bahia', 12);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1906, '12', 'Ceara', 12);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1907, '12', 'Distrito Federal', 12);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1908, '12', 'Espirito Santo', 12);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1909, '12', 'Mato Grosso do Sul', 12);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1910, '12', 'Maranhao', 12);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1911, '12', 'Mato Grosso', 12);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1912, '12', 'Minas Gerais', 12);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1913, '12', 'Para', 12);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1914, '12', 'Paraiba', 12);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1915, '12', 'Parana', 12);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1916, '12', 'Piaui', 12);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1917, '12', 'Rio de Janeiro', 12);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1918, '12', 'Rio Grande do Norte', 12);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1919, '12', 'Rio Grande do Sul', 12);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1920, '12', 'Rondonia', 12);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1921, '12', 'Roraima', 12);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1922, '12', 'Santa Catarina', 12);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1923, '12', 'Sao Paulo', 12);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1924, '12', 'Sergipe', 12);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1925, '12', 'Goias', 12);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1926, '12', 'Pernambuco', 12);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1927, '12', 'Tocantins', 12);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1930, '83', 'Akureyri', 83);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1931, '83', 'Arnessysla', 83);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1932, '83', 'Austur-Bardastrandarsysla', 83);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1933, '83', 'Austur-Hunavatnssysla', 83);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1934, '83', 'Austur-Skaftafellssysla', 83);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1935, '83', 'Borgarfjardarsysla', 83);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1936, '83', 'Dalasysla', 83);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1937, '83', 'Eyjafjardarsysla', 83);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1938, '83', 'Gullbringusysla', 83);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1939, '83', 'Hafnarfjordur', 83);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1943, '83', 'Kjosarsysla', 83);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1944, '83', 'Kopavogur', 83);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1945, '83', 'Myrasysla', 83);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1946, '83', 'Neskaupstadur', 83);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1947, '83', 'Nordur-Isafjardarsysla', 83);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1948, '83', 'Nordur-Mulasysla', 83);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1949, '83', 'Nordur-Tingeyjarsysla', 83);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1950, '83', 'Olafsfjordur', 83);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1951, '83', 'Rangarvallasysla', 83);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1952, '83', 'Reykjavik', 83);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1953, '83', 'Saudarkrokur', 83);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1954, '83', 'Seydisfjordur', 83);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1956, '83', 'Skagafjardarsysla', 83);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1957, '83', 'Snafellsnes- og Hnappadalssysla', 83);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1958, '83', 'Strandasysla', 83);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1959, '83', 'Sudur-Mulasysla', 83);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1960, '83', 'Sudur-Tingeyjarsysla', 83);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1961, '83', 'Vestmannaeyjar', 83);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1962, '83', 'Vestur-Bardastrandarsysla', 83);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1964, '83', 'Vestur-Isafjardarsysla', 83);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1965, '83', 'Vestur-Skaftafellssysla', 83);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1966, '35', 'Anhui', 35);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1967, '35', 'Zhejiang', 35);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1968, '35', 'Jiangxi', 35);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1969, '35', 'Jiangsu', 35);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1970, '35', 'Jilin', 35);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1971, '35', 'Qinghai', 35);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1972, '35', 'Fujian', 35);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1973, '35', 'Heilongjiang', 35);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1974, '35', 'Henan', 35);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1975, '35', 'Hebei', 35);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1976, '35', 'Hunan', 35);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1977, '35', 'Hubei', 35);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1978, '35', 'Xinjiang', 35);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1979, '35', 'Xizang', 35);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1980, '35', 'Gansu', 35);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1981, '35', 'Guangxi', 35);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1982, '35', 'Guizhou', 35);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1983, '35', 'Liaoning', 35);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1984, '35', 'Nei Mongol', 35);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1985, '35', 'Ningxia', 35);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1986, '35', 'Beijing', 35);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1987, '35', 'Shanghai', 35);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1988, '35', 'Shanxi', 35);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1989, '35', 'Shandong', 35);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1990, '35', 'Shaanxi', 35);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1991, '35', 'Sichuan', 35);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1992, '35', 'Tianjin', 35);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1993, '35', 'Yunnan', 35);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1994, '35', 'Guangdong', 35);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1995, '35', 'Hainan', 35);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1996, '35', 'Chongqing', 35);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1997, '97', 'Central', 97);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1998, '97', 'Coast', 97);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(1999, '97', 'Eastern', 97);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(2000, '97', 'Nairobi Area', 97);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(2001, '97', 'North-Eastern', 97);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(2002, '97', 'Nyanza', 97);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(2003, '97', 'Rift Valley', 97);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(2004, '97', 'Western', 97);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(2006, '195', 'Gilbert Islands', 195);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(2007, '195', 'Line Islands', 195);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(2008, '195', 'Phoenix Islands', 195);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(2010, '1', 'Australian Capital Territory', 1);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(2011, '1', 'New South Wales', 1);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(2012, '1', 'Northern Territory', 1);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(2013, '1', 'Queensland', 1);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(2014, '1', 'South Australia', 1);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(2015, '1', 'Tasmania', 1);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(2016, '1', 'Victoria', 1);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(2017, '1', 'Western Australia', 1);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(2018, '27', 'Dublin', 27);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(2019, '27', 'Galway', 27);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(2020, '27', 'Kildare', 27);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(2021, '27', 'Leitrim', 27);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(2022, '27', 'Limerick', 27);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(2023, '27', 'Mayo', 27);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(2024, '27', 'Meath', 27);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(2025, '27', 'Carlow', 27);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(2026, '27', 'Kilkenny', 27);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(2027, '27', 'Laois', 27);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(2028, '27', 'Longford', 27);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(2029, '27', 'Louth', 27);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(2030, '27', 'Offaly', 27);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(2031, '27', 'Westmeath', 27);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(2032, '27', 'Wexford', 27);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(2033, '27', 'Wicklow', 27);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(2034, '27', 'Roscommon', 27);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(2035, '27', 'Sligo', 27);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(2036, '27', 'Clare', 27);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(2037, '27', 'Cork', 27);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(2038, '27', 'Kerry', 27);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(2039, '27', 'Tipperary', 27);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(2040, '27', 'Waterford', 27);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(2041, '27', 'Cavan', 27);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(2042, '27', 'Donegal', 27);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(2043, '27', 'Monaghan', 27);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(2044, '50', 'Karachaeva-Cherkesskaya Respublica', 50);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(2045, '50', 'Raimirskii (Dolgano-Nenetskii) AO', 50);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(2046, '50', 'Respublica Tiva', 50);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(2047, '32', 'Newfoundland', 32);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(2048, '32', 'Nova Scotia', 32);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(2049, '32', 'Prince Edward Island', 32);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(2050, '32', 'New Brunswick', 32);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(2051, '32', 'Quebec', 32);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(2052, '32', 'Ontario', 32);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(2053, '32', 'Manitoba', 32);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(2054, '32', 'Saskatchewan', 32);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(2055, '32', 'Alberta', 32);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(2056, '32', 'British Columbia', 32);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(2057, '32', 'Nunavut', 32);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(2058, '32', 'Northwest Territories', 32);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(2059, '32', 'Yukon Territory', 32);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(2060, '19', 'Drenthe', 19);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(2061, '19', 'Friesland', 19);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(2062, '19', 'Gelderland', 19);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(2063, '19', 'Groningen', 19);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(2064, '19', 'Limburg', 19);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(2065, '19', 'Noord-Brabant', 19);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(2066, '19', 'Noord-Holland', 19);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(2067, '19', 'Utrecht', 19);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(2068, '19', 'Zeeland', 19);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(2069, '19', 'Zuid-Holland', 19);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(2071, '19', 'Overijssel', 19);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(2072, '19', 'Flevoland', 19);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(2073, '138', 'Duarte', 138);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(2074, '138', 'Puerto Plata', 138);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(2075, '138', 'Valverde', 138);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(2076, '138', 'María Trinidad Sánchez', 138);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(2077, '138', 'Azua', 138);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(2078, '138', 'Santiago', 138);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(2079, '138', 'San Cristóbal', 138);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(2080, '138', 'Peravia', 138);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(2081, '138', 'Elías Piña', 138);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(2082, '138', 'Barahona', 138);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(2083, '138', 'Monte Plata', 138);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(2084, '138', 'Salcedo', 138);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(2085, '138', 'La Altagracia', 138);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(2086, '138', 'San Juan', 138);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(2087, '138', 'Monseñor Nouel', 138);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(2088, '138', 'Monte Cristi', 138);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(2089, '138', 'Espaillat', 138);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(2090, '138', 'Sánchez Ramírez', 138);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(2091, '138', 'La Vega', 138);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(2092, '138', 'San Pedro de Macorís', 138);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(2093, '138', 'Independencia', 138);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(2094, '138', 'Dajabón', 138);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(2095, '138', 'Baoruco', 138);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(2096, '138', 'El Seibo', 138);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(2097, '138', 'Hato Mayor', 138);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(2098, '138', 'La Romana', 138);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(2099, '138', 'Pedernales', 138);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(2100, '138', 'Samaná', 138);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(2101, '138', 'Santiago Rodríguez', 138);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(2102, '138', 'San José de Ocoa', 138);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(2103, '70', 'Chiba', 70);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(2104, '70', 'Ehime', 70);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(2105, '70', 'Oita', 70);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(2106, '85', 'Skopje', 85);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(2108, '35', 'Schanghai', 35);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(2109, '35', 'Hongkong', 35);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(2110, '35', 'Neimenggu', 35);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(2111, '35', 'Aomen', 35);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(2112, '92', 'Amnat Charoen', 92);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(2113, '92', 'Ang Thong', 92);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(2114, '92', 'Bangkok', 92);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(2115, '92', 'Buri Ram', 92);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(2116, '92', 'Chachoengsao', 92);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(2117, '92', 'Chai Nat', 92);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(2118, '92', 'Chaiyaphum', 92);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(2119, '92', 'Chanthaburi', 92);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(2120, '92', 'Chiang Mai', 92);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(2121, '92', 'Chiang Rai', 92);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(2122, '92', 'Chon Buri', 92);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(2124, '92', 'Kalasin', 92);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(2126, '92', 'Kanchanaburi', 92);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(2127, '92', 'Khon Kaen', 92);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(2128, '92', 'Krabi', 92);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(2129, '92', 'Lampang', 92);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(2131, '92', 'Loei', 92);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(2132, '92', 'Lop Buri', 92);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(2133, '92', 'Mae Hong Son', 92);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(2134, '92', 'Maha Sarakham', 92);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(2137, '92', 'Nakhon Pathom', 92);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(2139, '92', 'Nakhon Ratchasima', 92);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(2140, '92', 'Nakhon Sawan', 92);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(2141, '92', 'Nakhon Si Thammarat', 92);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(2143, '92', 'Narathiwat', 92);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(2144, '92', 'Nong Bua Lam Phu', 92);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(2145, '92', 'Nong Khai', 92);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(2146, '92', 'Nonthaburi', 92);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(2147, '92', 'Pathum Thani', 92);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(2148, '92', 'Pattani', 92);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(2149, '92', 'Phangnga', 92);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(2150, '92', 'Phatthalung', 92);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(2154, '92', 'Phichit', 92);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(2155, '92', 'Phitsanulok', 92);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(2156, '92', 'Phra Nakhon Si Ayutthaya', 92);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(2157, '92', 'Phrae', 92);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(2158, '92', 'Phuket', 92);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(2159, '92', 'Prachin Buri', 92);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(2160, '92', 'Prachuap Khiri Khan', 92);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(2162, '92', 'Ratchaburi', 92);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(2163, '92', 'Rayong', 92);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(2164, '92', 'Roi Et', 92);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(2165, '92', 'Sa Kaeo', 92);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(2166, '92', 'Sakon Nakhon', 92);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(2167, '92', 'Samut Prakan', 92);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(2168, '92', 'Samut Sakhon', 92);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(2169, '92', 'Samut Songkhran', 92);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(2170, '92', 'Saraburi', 92);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(2172, '92', 'Si Sa Ket', 92);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(2173, '92', 'Sing Buri', 92);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(2174, '92', 'Songkhla', 92);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(2175, '92', 'Sukhothai', 92);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(2176, '92', 'Suphan Buri', 92);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(2177, '92', 'Surat Thani', 92);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(2178, '92', 'Surin', 92);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(2180, '92', 'Trang', 92);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(2182, '92', 'Ubon Ratchathani', 92);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(2183, '92', 'Udon Thani', 92);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(2184, '92', 'Uthai Thani', 92);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(2185, '92', 'Uttaradit', 92);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(2186, '92', 'Yala', 92);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(2187, '92', 'Yasothon', 92);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(2188, '69', 'Busan', 69);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(2189, '69', 'Daegu', 69);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(2191, '69', 'Gangwon', 69);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(2192, '69', 'Gwangju', 69);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(2193, '69', 'Gyeonggi', 69);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(2194, '69', 'Gyeongsangbuk', 69);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(2195, '69', 'Gyeongsangnam', 69);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(2196, '69', 'Jeju', 69);
INSERT INTO regiones
(id_region, idtregion, texto, id_pais)
VALUES(2201, '25', 'Delhi', 25);