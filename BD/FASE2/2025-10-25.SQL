-- Check if the table does not exist before creating it
IF OBJECT_ID('administrative_access.TemplateData', 'U') IS NULL BEGIN 
CREATE TABLE administrative_access.TemplateData (
    Id_Template int IDENTITY(0, 1) NOT NULL,
    Descripcion nvarchar(1000) NULL,
    Sections nvarchar(MAX) NULL,
    CONSTRAINT TemplateData_PK PRIMARY KEY (Id_Template)
);


INSERT INTO OLIMPO.administrative_access.TemplateData
(Descripcion, Sections)
VALUES(N'Contrato', N'[
  {
    "Id_Section": null,
    "Data": null,
    "Body": "<table class=\"header-table first\" style=\"width:100%;\">\n            <tbody><tr>\n                <td><img class=\"logo\" src=\"{{ logo }}\"></td>\n                <td style=\"text-align: center; font-size: 16px;  font-weight: bold\" colspan=\"2\">\n                    <div>Contrato de Servicio Educativo</div>\n                    <div>Código de Familia: {{ codigo_familia }}</div>\n                </td>\n            </tr>\n        </tbody></table>"
  },
  {
    "Id_Section": null,
    "Data": null,
    "Body": "\n        <div style=\"display: flex; justify-content: space-between; align-items: center; margin-top: 10px;\">\n            <p style=\"flex: 1; text-align: center; margin: 0;\">\n                <strong>{{ codigo_estudiante }}: {{ nombre_estudiante }}</strong>\n            </p>\n        </div>"
  },
  {
    "Id_Section": null,
    "Data": null,
    "Body": "<div style=\"display: flex; justify-content: space-between; align-items: center;\">\n            <p style=\"flex: 1; text-align: center; margin: 0;\"></p>\n            <p> Impresión: {{ impresion }} </p>\n        </div>\n"
  },
  {
    "Id_Section": null,
    "Data": null,
    "Body": " <div class=\"header-contrato\">\n            <p><strong>CONTRATO DE SERVICIO EDUCATIVO</strong></p>\n            <p><strong>[Año Lectivo {{ current_year }}]</strong></p>\n        </div>"
  },
  {
    "Id_Section": null,
    "Data": null,
    "Body": "<div class=\"section-title\">PRIMERA: Definición del Contrato</div>\n        <p>El Colegio Centro América, debidamente representado por el Rector Padre Silvio Javier Avilez Cevasco, que en\n            delante se denominará “El Colegio” y los señores __<span class=\"underline \">{{ nombre_responsable1 }}</span>__ y\n            ___<span class=\"underline \">{{ nombre_responsable2 }}</span>____ mayores de edad, identificados con números\n            de\n            cédula de identidad\n            __<span class=\"underline \">{{ cedula1 }}</span>_ y __<span class=\"underline \">{{ cedula2 }}</span>__ de este\n            domicilio, en su propio nombre y en su condición\n            de padres, tutores o representantes legales, quienes en adelante y para los efectos de este contrato se\n            denominarán “Los Responsables”.\n        </p>\n        <p>El Colegio y Los Responsables acuerdan celebrar el presente contrato de prestación de servicios educativos a\n            su(s) hijo(s) regido por las siguientes cláusulas:\n        </p>"
  },
  {
    "Id_Section": null,
    "Data": null,
    "Body": " <div class=\"section-title\">SEGUNDA: Características del Proceso Educativo Ignaciano</div>\n        <p>El proceso educativo ignaciano se caracteriza por:</p>\n        <ul>\n            <li>A. Un modo de proceder educativo coherente con lo explicitado en los documentos “Características de la\n                Educación de la Compañía de Jesús” y “Pedagogía Ignaciana: “Un Planteamiento Práctico”, y otros\n                documentos del marco referencial de la Compañía.</li>\n            <li>B. La promoción de valores, el desarrollo integral, equilibrado y armónico del estudiante en las\n                dimensiones: afectiva, ética, corporal, espiritual, cognitiva, estética, socio-política y comunicativa.\n            </li>\n            <li>C. El desarrollo de competencias que permitan producir y seguir aprendiendo a lo largo de la vida.</li>\n            <li>D. Los currículos abiertos, flexibles, dinámicos, contextualizados e interdisciplinarios con propuestas\n                académicas de calidad.</li>\n            <li>E. El acompañamiento personal.</li>\n        </ul>"
  },
  {
    "Id_Section": null,
    "Data": null,
    "Body": "<div class=\"section-title\">TERCERA: Fecha, Lugar y Forma de Pago de Matrícula, Aranceles y Servicios Adicionales\n        </div>\n        <p>A. Las fechas para cancelar los aranceles, serán los siguientes:</p>\n        <ul>\n            <li>- El monto correspondiente a matrícula y colegiatura del mes de enero, deberán ser cancelados durante el\n                período comprendido desde la fecha de notificación de matrícula, hasta el día 06 de diciembre de 2024.\n            </li>\n            <li>- El monto correspondiente a colegiatura deberá ser cancelado mediante once cuotas iguales, mensuales y\n                consecutivas, entre los primeros diez días de cada mes, de febrero a diciembre. A partir de la fecha en\n                que entre en mora por esta obligación de pago, el Colegio aplicará un recargo consistente en adicionar\n                un 5% sobre el monto pendiente de pago.</li>\n            <li>- Los montos correspondientes a materiales educativos y otras actividades pedagógicas deberán ser\n                cancelados mediante cuotas mensuales durante los primeros diez días del período en que se causen.</li>\n        </ul>\n        <p>B. Todo lo anterior podrá pagarse indicando siempre el código de familia o los apellidos del estudiante en\n            Caja del Colegio o a través de las líneas de tele pagos de BANPRO o del CCA marcando al número 22785738,\n            ext. 1012 y 1055. También mediante transferencias bancarias, indicando el código de familia o los apellidos\n            del estudiante a los siguientes números de cuenta:</p>"
  },
  {
    "Id_Section": null,
    "Data": null,
    "Body": " <ul>\n            <li>- BANPRO córdobas: 10010706678600.</li>\n            <li>- BANPRO dólares: 10010710001467.</li>\n            <li>- BAC córdobas: 360067243.</li>\n            <li>- BAC dólares: 362965139.</li>\n        </ul>\n        <p>C. El monto correspondiente a matrícula no está sujeto a devolución, en caso de que el padre/madre decida\n            retirar a su hijo/hija del CCA.</p>\n        <p>D. En caso de incumplimiento en el pago de los servicios antes descritos, el CCA se reserva el derecho de dar\n            por finalizada la relación contractual.</p>\n"
  },
  {
    "Id_Section": null,
    "Data": null,
    "Body": " <div class=\"section-title\">CUARTA: Mantenimiento al Valor</div>\n        <p>Conforme el Art. 16 de la Ley Monetaria, publicada en La Gaceta, D. O. N°. 2 de 7 de enero de 1992, todas las\n            obligaciones dinerarias a cargo de Los Responsables, mantendrán el valor del adeudo en córdobas con relación\n            al dólar, moneda de curso legal de los Estados Unidos de América, al tipo de cambio oficial del córdoba con\n            respecto al dólar al día efectivo de pago; en consecuencia, si se produjese una modificación en el tipo de\n            cambio oficial del córdoba con relación al dólar a partir de esta fecha, dicho monto en córdobas deberá\n            ajustarse en la misma proporción que la modificación operada en el tipo de cambio oficial.</p>\n        <p>Cuando las circunstancias económicas y/o políticas monetarias establecidas por el Banco Central de Nicaragua,\n            dificultaren la libre adquisición de la moneda dólar, se utilizará el tipo de cambio establecido por la\n            libre oferta y demanda, en el mercado paralelo, necesario para la adquisición de la cantidad de dólares\n            establecida en el Programa de Pagos acordado.</p>"
  },
  {
    "Id_Section": null,
    "Data": null,
    "Body": " <div class=\"section-title\">QUINTA: Servicio de Transporte</div>\n        <p>El Colegio ofrece un servicio de transporte mediante un contrato independiente. Se debe solicitar y firmar en\n            la oficina de transporte.</p>\n"
  },
  {
    "Id_Section": null,
    "Data": null,
    "Body": "   <div class=\"section-title\">SÉPTIMA: Obligación Solidaria de Pago</div>\n        <p>El Primer(a) y Segundo(a) Responsable, se obligan solidaria y subsidiariamente al pago total de todos y cada\n            una de las obligaciones dinerarias, conjuntas y relacionadas en las cláusulas anteriores, para lo cual\n            renuncian desde ya, al beneficio de excusión y de la división de las referidas obligaciones, constituyéndose\n            en codeudores individuales e indistintos; es decir, tanto el Primer responsable como el Segundo, quedan\n            obligados a cumplir íntegramente las obligaciones.</p>"
  },
  {
    "Id_Section": null,
    "Data": null,
    "Body": "<div class=\"section-title\">OCTAVA: Derechos y Obligaciones del Colegio Centro América</div>\n        <p><strong>Derechos:</strong></p>\n        <ul>\n            <li>1. Exigir a Los Responsables el cumplimiento de la normativa interna vigente o aquella que, durante la\n                duración del presente contrato entre en vigor [Normativa de Convivencia, Protocolo de Protección de\n                Niñas, Niños y Adolescentes, entre otros]; documentos que Los Responsables declaran conocer y que desde\n                ya aceptan.</li>\n            <li>2. Actuar rápida y oportunamente para corregir comportamientos inadecuados en el educando; para lo cual\n                las personas delegadas por el Rector, se encuentran autorizadas para conversar directamente sin la\n                presencia de la madre/padre del estudiante, sobre aspectos académicos, conductuales y espirituales sin\n                perjuicio de citar a la madre/padre de familia para tratar la misma situación en reunión posterior.</li>\n            <li>3. A reservarse el derecho de permanencia del educando y de renovación de matrícula, con base a las\n                actitudes personales y familiares del estudiante y al cumplimiento de las obligaciones establecidas en\n                las normas internas, para las madres/padres de familia, conforme lo decida el Consejo de Dirección del\n                CCA.</li>\n            <li>4. Exigir a los estudiantes el cumplimiento de los deberes académicos que derivan del servicio educativo\n                que brinda el Colegio Centro América y lo reglamentado en sus documentos institucionales.</li>\n            <li>5. Exigir el pago de los aranceles de matrícula, colegiatura, transporte y demás obligaciones dinerarias\n                a cargo de Los Responsables, en los plazos y fechas establecidas, mediante el cobro administrativo,\n                extrajudicial o judicial, según sea el caso. En los casos que así se requiere, los gastos, honorarios y\n                costas de ejecución, correrán por cuenta del Primer y Segundo Responsable sujetos a los referidos\n                trámites.</li>\n            <li>6. A reservarse el derecho de permanencia del estudiante o la entrega de sus calificaciones por\n                cualquier medio escrito o electrónico, como efecto del impago de las obligaciones dinerarias de Los\n                Responsables con el Colegio.</li>\n        </ul>"
  },
  {
    "Id_Section": null,
    "Data": null,
    "Body": "<p><strong>Obligaciones:</strong></p>\n        <ul>\n            <li>1. Formar integralmente al estudiante, conforme las características del Proceso Educativo Ignaciano,\n                mientras sea estudiante activo/a.</li>\n            <li>2. Garantizar ambientes sanos y seguros para la protección integral de los estudiantes, que involucre un\n                esfuerzo conjunto de toda la comunidad educativa.</li>\n            <li>3. Notificar a través de su página WEB o el SIAC, la información correspondiente a: circulares,\n                horarios, cronogramas, boletines informativos, resultados académicos y otros.</li>\n            <li>4. Entregar documentos o informes únicamente a Los Responsables que han sido legalmente registrados a\n                través del presente Contrato, siempre que se encuentren solventes en sus obligaciones de pago.</li>\n        </ul>"
  },
  {
    "Id_Section": null,
    "Data": null,
    "Body": "<div class=\"section-title\">NOVENA: Derechos y Obligaciones de Los Responsables</div>\n        <p><strong>Derechos:</strong></p>\n        <ul>\n            <li>1. Recibir la enseñanza contratada para sus hijas/hijos conforme las características del Proceso\n                Educativo Ignaciano.</li>\n            <li>2. Recibir orientación sobre la educación de su hija/hijo.</li>\n            <li>3. Recibir información oportuna sobre la situación académica y conductual de su hija/hijo.</li>\n            <li>4. Participar en las actividades familiares que celebra el CCA (Día familiar, Día de mamá y papá, Ya\n                sabemos leer, Caminata de undécimo grado, entre otras).</li>\n        </ul>\n        "
  },
  {
    "Id_Section": null,
    "Data": null,
    "Body": "<p><strong>Obligaciones:</strong></p>\n        <ul>\n            <li>1. Cumplir y hacer cumplir a sus hijos/estudiantes con las normas internas que rigen a la Comunidad\n                Educativa del CCA, las cuales desde ya declaran conocer y aceptar.</li>\n            <li>2. Respetar la formación ético-religiosa, según el Programa de Formación Cristiana del CCA.</li>\n            <li>3. Pagar en el tiempo, plazo, forma y lugar previamente establecido, los aranceles por matrícula,\n                colegiatura, transporte y demás obligaciones derivadas del servicio educativo que recibe el estudiante\n                en el CCA. Pagar los gastos, honorarios y costas de ejecución, derivadas del cobro administrativo,\n                extrajudicial o judicial, según sea el caso, por incumplimiento de esta obligación.</li>\n            <li>4. Garantizar los requerimientos materiales necesarios para la participación efectiva de su hija/hijo,\n                en las actividades complementarias que ofrece el CCA; tales como: apostolado social, retiros\n                espirituales, cursos de reforzamiento, paseos, convivencias, talleres de danza, de música, de teatro,\n                disciplinas deportivas, club de lectores y laboratorios, entre otros.</li>\n            <li>5. Cumplir con el horario establecido institucionalmente, responsabilizándose de la seguridad de sus\n                hijas/hijos, durante el tiempo intermedio entre las actividades matutinas y vespertinas.</li>\n            <li>6. Retirar puntualmente a su hija/hijo, terminada la jornada escolar, ya que el CCA no dispone de\n                personal para la atención de los estudiantes pasado el horario establecido.</li>\n            <li>7. Colaborar con el Colegio de forma activa y propositiva en todas las actividades académicas,\n                deportivas, culturales y espirituales que se realizan durante el curso escolar, las cuales forman parte\n                del Programa Educativo del CCA, el cual declaran conocer y desde ya aceptan.</li>\n            <li>8. Solicitar permiso a la coordinación respectiva cuando amerite retirar al estudiante antes de\n                finalizar la jornada escolar.</li>\n            <li>9. Actualizar ante la asistente de rectoría (edificio administrativo) los cambios de residencia, números\n                telefónicos, dirección electrónica y otros datos relacionados con el entorno educativo.</li>\n            <li>10. Entregar por escrito a la coordinación respectiva el permiso para que su hija/hijo se retire del\n                colegio con otra persona ajena a los tutores registrados en la coordinación, presentando su debida\n                identificación.</li>\n            <li>11. Respetar dentro y fuera de las instalaciones del Colegio Centro América, las disposiciones\n                establecidas por las leyes, reglamentos y normativas internas asociados a los derechos de la niñez y la\n                adolescencia. Por tanto, es totalmente prohibido abordar directamente con otros estudiantes, situaciones\n                vinculadas a problemas en las relaciones interpersonales con su hija/hijo. Queda terminantemente\n                prohibido tomar acciones correctivas directas en tales casos.</li>\n            <li>12. Respetar los lineamientos sobre la evaluación de los aprendizajes. El estudiante que al terminar el\n                curso escolar haya reprobado una o dos asignaturas, debe presentarse a examen de reparación\n                (extraordinario); quedando establecido únicamente el examen de reparación oficial, no habrá segunda\n                convocatoria.</li>\n        </ul>"
  },
  {
    "Id_Section": null,
    "Data": null,
    "Body": " <div class=\"section-title\">DÉCIMA: Documentos integrantes del Contrato</div>\n        <p>Forman parte integrante del presente Contrato, como si a la letra se insertaran:</p>\n        <ul>\n            <li>1. Normativa de Convivencia</li>\n            <li>2. Protocolo de Protección de Niñas, Niños y Adolescentes</li>\n            <li>3. Contrato de Transporte (si aplica)</li>\n        </ul>"
  },
  {
    "Id_Section": null,
    "Data": null,
    "Body": "\n        <div class=\"section-title\">DÉCIMA PRIMERA: Vigencia del Contrato</div>\n        <p>La vigencia del presente Contrato inicia desde la fecha de su suscripción y finaliza con la firma de un nuevo\n            contrato o el último día lectivo del año 2025.</p>"
  },
  {
    "Id_Section": null,
    "Data": null,
    "Body": " <div class=\"section-title\">DÉCIMA SEGUNDA: Causas de Terminación del Contrato</div>\n        <p>El presente Contrato terminará por las siguientes causas:</p>\n        <ul>\n            <li>1. Por expiración del plazo.</li>\n            <li>2. Por retraso en el pago de la matrícula o aranceles.</li>\n            <li>3. Por resolución del Consejo Directivo del Colegio.</li>\n            <li>4. Por voluntad de Los Responsables.</li>\n            <li>5. Por fallecimiento del estudiante.</li>\n        </ul>"
  },
  {
    "Id_Section": null,
    "Data": null,
    "Body": " <div class=\"section-title\">DÉCIMA TERCERA: Disposición Especial</div>\n        <p>El incumplimiento de pago de las obligaciones dinerarias en la forma, fecha y demás condiciones establecidas\n            en este Contrato por parte de Los Responsables, dará lugar a la aplicación de las siguientes medidas por\n            parte del Colegio:</p>\n        <ul>\n            <li>- A los 30 días de mora se suspenderá el acceso al SIAC.</li>\n            <li>- A los 60 días de mora el acceso a las plataformas educativas y correo electrónico.</li>\n            <li>- A los 90 días de mora se suspenderá el ingreso a clases.</li>\n\n        </ul>"
  },
  {
    "Id_Section": null,
    "Data": null,
    "Body": " <div class=\"section-title\">DÉCIMA CUARTA: Aceptación</div>\n        <p>Aceptamos el presente Contrato en toda su extensión, firmándolo en la ciudad de Managua, a los {{ dia }} días del\n            mes de {{ mes }} del {{ anio }}.</p>\n\n            <table class=\"signature-table\" style=\"width: 100%; table-layout: fixed; border-collapse: collapse;\">\n                <tbody><tr>\n                    <td style=\"width: 50%; text-align: center; padding: 0px;\">                        \n                        <p style=\"text-align:center\">____________________________________</p>   \n                        <p style=\"text-align:center\"><strong>{{ nombre_responsable1 }}</strong><br>Primer Responsable</p>\n                    </td>\n                    <td style=\"width: 50%; text-align: center; padding: 0px;\">\n                        <p style=\"text-align:center\">____________________________________</p>   \n                        <p style=\"text-align:center\"><strong>{{ nombre_responsable2 }}</strong><br>Segundo\n                            Responsable</p>\n                    </td>\n                </tr>\n                <tr>\n                    <td colspan=\"2\" style=\"text-align: center; padding: 0px 140px 0px 140px !important;\">\n                        <div style=\"border-top: 1px solid #000000; width: 90%; margin: 0 auto;\"></div>\n                        <p style=\"text-align:center\"><strong>SILVIO JAVIER AVILEZ CEVASCO</strong><br>Representante Legal\n                        </p>\n                    </td>\n                </tr>\n            </tbody></table>"
  },
  {
    "Id_Section": null,
    "Data": null,
    "Body": "<style>\n        body {\n            font-family: Arial, sans-serif;\n            color: #333;\n            line-height: 1.6;\n            font-size: 12px;\n        }\n\n        .logo {\n            width: 150px;\n            position: absolute;\n            margin-top: -30px;\n        }\n\n        .underline {\n            text-decoration: underline;\n        }\n\n        .container {\n            width: 100%;\n            max-width: 700px;\n            margin: 0 auto;\n            padding: 20px;\n            page-break-after: always;\n        }\n\n        h1 {\n            color: black;\n            text-align: center;\n        }\n\n        .section-title {\n            font-weight: bold;\n            margin-top: 20px;\n            font-size: 1em;\n            color: black;\n        }\n\n        p,\n        ul {\n            margin: 10px 0;\n            text-align: justify;\n        }\n\n        ul {\n            padding-left: 20px;\n        }\n\n        .signature-table {\n            width: 100%;\n            margin-top: 20px;\n            border-collapse: collapse;\n        }\n\n        .signature-table td {\n            padding: 20px;\n            text-align: center;\n            vertical-align: top;\n        }\n\n        .header p {\n            margin: 0px;\n            text-align: center;\n        }\n\n        .header-impresion p {\n            margin-top: 15px;\n            margin-bottom: 0px;\n            text-align: center;\n        }\n\n        .header-contrato {\n            margin-top: 0px;\n        }\n\n        .header-contrato p {\n            text-align: center;\n            margin: 0px;\n        }\n\n        ul {\n            list-style-type: none;\n        }\n    </style>"
  }
]');
END

--todo borrar manualmente la llave primeria actual de parientes_data_update
ALTER TABLE OLIMPO.update_data.parientes_data_update ADD Periodo_Lectivo_Update nvarchar(100) DEFAULT 2024 NOT NULL;
ALTER TABLE OLIMPO.update_data.parientes_data_update ADD CONSTRAINT parientes_data_update_PK PRIMARY KEY (id,Periodo_Lectivo_Update);


--nuevo campo SecurityOption
ALTER TABLE OLIMPO.update_data.estudiantes_data_update ADD SecurityOption nvarchar(MAX) NULL;
--todo borrar manualmente si es necesario la llave primeria actual de estudiantes_data_update y colocar las llaves primarias de periodo lectivo y id
ALTER TABLE OLIMPO.update_data.estudiantes_data_update ADD Periodo_Lectivo_Update nvarchar(100) DEFAULT 2024 NOT NULL;


ALTER TABLE OLIMPO.update_data.estudiantes_data_update DROP CONSTRAINT PK__estudian__3213E83FD683993A;
ALTER TABLE OLIMPO.update_data.estudiantes_data_update ADD CONSTRAINT estudiantes_data_update_PK PRIMARY KEY (Periodo_Lectivo_Update,id);
