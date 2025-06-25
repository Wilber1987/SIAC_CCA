

using CAPA_DATOS.Services;
using CAPA_NEGOCIO.Gestion_Cursos.Model.QueryModel;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Twilio.TwiML.Voice;

namespace DataBaseModel
{
    internal class InformeClasesBuilder
    {
        //AGRUOADO POR DESCRIPCION (GRADO, NIVEL Y PERIODO)
        public static List<Clase_Group>? BuildClaseGroupMateriaList(List<Estudiante_Clases_View>? Clases)
        {
            return Clases?.OrderByDescending(C => C.Nombre_corto_periodo).ToList()
                    //.Where(C => C.Nombre_nota != null)
                    .GroupBy(C => C.Descripcion)
                    .Select(C => BuildClaseMateriaGroup(C)).ToList();
        }
        public static Clase_Group BuildClaseMateriaGroup(IGrouping<string?, Estudiante_Clases_View> C)
        {
            var clase = C.First();
            return new Clase_Group
            {
                Id_Clase = clase.Id,
                Clase = clase.Descripcion?.ToUpper(),
                Repite = clase.Repitente == true ? "SI" : "NO",
                Nivel = clase.Nombre_nivel,
                Seccion = clase.nombre_seccion,
                Estudiantes = C.GroupBy(E => E.Codigo)
                   .Select(E => BuildEstudianteGroup(E)).ToList()
            };
        }

        private static Estudiante_Group BuildEstudianteGroup(IGrouping<string, Estudiante_Clases_View> E)
        {
            var clase = E.First();
            return new Estudiante_Group
            {
                Descripcion = clase.Nombre_Estudiantes,
                Evaluaciones = E.GroupBy(e => e.Evaluacion).Where(g => g.Count() == 1).Select(g => g.First()).Select(g => g.Evaluacion).ToList(),
                Calificaciones = [.. E.Select(Calificacion =>
                {
                    return new Calificacion_Group
                    {
                        Id = Calificacion.Id,
                        Order = Calificacion.ThisConfig?.periodo_inicio ?? 1,
                        Resultado = Calificacion.Resultado,
                        Evaluacion = Calificacion.Evaluacion ?? "",
                        EvaluacionCompleta = Calificacion.EvaluacionCompleta ?? "",
                        Tipo = Calificacion.Tipo,
                        Fecha = Calificacion.Fecha,
                        Porcentaje = Calificacion.Porcentaje,
                        Observaciones = Calificacion.Observaciones ?? "Sin observaciones"
                     };
                }).OrderBy(c => c.Fecha)
                .ThenBy(c => c.Evaluacion.Contains("B") ? 1 :
                    c.Evaluacion.Contains("S") ? 2 :
                    c.Evaluacion.Contains("F") ? 3 : 4) // Ordenar por Evaluacion
                ]
            };
        }

        //AGRUOADO POR DESCRIPCION (GRADO, NIVEL Y PERIODO)
        public static List<Clase_Group>? BuildClaseGroupList(List<Estudiante_Clases_View>? clasesView, List<MateriasByClassQuery> clases)
        {
            return [BuildClaseGroup(clases, clasesView)];
            /*return Clases?.OrderByDescending(C => C.Nombre_corto_periodo).ToList()
                    //.Where(C => C.Nombre_nota != null)
                    .GroupBy(C => C.Descripcion)
                    .Select(C => BuildClaseGroup(C)).ToList();*/
        }

        public static Clase_Group BuildClaseGroup(List<MateriasByClassQuery> clases, List<Estudiante_Clases_View> clasesView)
        {
            Clases? claseE = new Clases { Id = clases.First().Clase_id }.Find<Clases>();
            //Estudiante_clases estudiante_Clases = new Estudiante_clases { Id = clases.est}
            MateriasByClassQuery clase = clases.First();
            Secciones? seccion = new Secciones { Id = clases.First().Seccion_id }.Find<Secciones>();

            return new Clase_Group
            {
                Id_Clase = clase?.Clase_id,
                Clase = claseE?.Descripcion?.ToUpper(),
                Repite = clase?.Repitente == true ? "SI" : "NO",
                Nivel = claseE?.Niveles?.Nombre,
                Seccion = seccion?.Nombre,
                Guia = seccion?.Guia?.Nombre_completo,
                Asignaturas = clases.Select(c => BuildAsignaturaGroup(c, clasesView)).ToList()
                /*Asignaturas = clasesView.Any(e => e.Nombre_nivel == "PREESCOLAR")
                                ? new List<Asignatura_Group>()
                                : clases.Select(c => BuildAsignaturaGroup(c, clasesView)).ToList()*/
                /*Asignaturas = C.GroupBy(A => A.Nombre_asignatura)
                    .Select(A => BuildAsignaturaGroup(A)).ToList()*/
            };
        }

        private static Asignatura_Group BuildAsignaturaGroup(MateriasByClassQuery materiasByClass,
            List<Estudiante_Clases_View> clasesView)
        {
            Estudiante_Clases_View? clase = null;
            IGrouping<string?, Estudiante_Clases_View>? A = null;
            var datosClasesView = clasesView.Where(c => c.Nombre_asignatura == materiasByClass.Nombre)
                    .ToList();
            if (datosClasesView.Count > 0)
            {
                A = clasesView.Where(c => c.Nombre_asignatura == materiasByClass.Nombre)
                        .ToList().GroupBy(c => c.Nombre_asignatura).First();
            }
            if (A != null)
            {
                clase = A.First();
            }

            List<Calificacion_Group> calificaciones_Groups = [];
            List<IGrouping<int?, Estudiante_Clases_View>>? periodos = A?.GroupBy(c => c.Periodo).ToList();


            periodos?.ForEach(c =>
            {
                //string roman = NumberUtility.ToRoman((c.First()?.Periodo) ?? 1);
                List<Estudiante_Clases_View>? calificacionesValidas = c?.Where(Calificacion =>
                    Calificacion.Evaluacion != "B"
                ).ToList();
                List<Estudiante_Clases_View>? acumulados = c?.Where(Calificacion =>
                    Calificacion.Evaluacion != "S"
                    && Calificacion.Evaluacion != "B"
                    && Calificacion.Evaluacion != "F"
                ).ToList();
                
                string roman = NumberUtility.ToRoman((c.First()?.Periodo) ?? 1);
                calificaciones_Groups.AddRange([.. acumulados.Select(Calificacion => BuildCalificacionGroup(Calificacion, roman))]);

                Estudiante_Clases_View? notaBimestral = c?.Where(Calificacion =>
                   Calificacion.Evaluacion == "B"
                ).ToList().FirstOrDefault();
                if (notaBimestral != null)
                {
                    calificaciones_Groups.Add(BuildCalificacionGroup(notaBimestral, roman));
                }
                else
                {
                    calificaciones_Groups.Add(new Calificacion_Group
                    {
                        Order = c.First().ThisConfig?.periodo_inicio ?? 1,
                        Resultado = acumulados.Sum(c => c.Resultado),
                        Periodo = roman,
                        Evaluacion = $"{roman}B",
                        Tipo = $"{roman} BIMESTRE",
                        EvaluacionCompleta = $"{roman} BIMESTRE",
                        Porcentaje = 100,
                        Observaciones = "Sin observaciones",
                        Calificacion_updated_at = acumulados[acumulados.Count - 1].Calificacion_updated_at.GetValueOrDefault().AddSeconds(10),
                        Fecha = acumulados[acumulados.Count - 1].Fecha.GetValueOrDefault().AddSeconds(10)
                    });
                }
                Estudiante_Clases_View? notaSemestral = c?.Where(Calificacion =>
                    Calificacion.Evaluacion == "S"
                ).ToList().FirstOrDefault();
                Estudiante_Clases_View? notaFinal = c?.Where(Calificacion =>
                    Calificacion.Evaluacion == "F"
                ).ToList().FirstOrDefault();
                if (notaSemestral != null)
                {
                    calificaciones_Groups.Add(BuildCalificacionGroup(notaSemestral, NumberUtility.ToRoman(((c.First()?.Periodo)/2) ?? 1)));
                }
                if (notaFinal != null)
                {
                    calificaciones_Groups.Add(BuildCalificacionGroup(notaFinal, ""));
                }
            });

            /*var calificacion = A?.Select(Calificacion =>
                {
                    return new Calificacion_Group
                    {
                        Id = Calificacion.Id,
                        Order = Calificacion.ThisConfig?.periodo_inicio ?? 1,
                        Resultado = Calificacion.Resultado,
                        Evaluacion = Calificacion.Evaluacion ?? "",
                        EvaluacionCompleta = Calificacion.Observaciones_Puntaje ?? "",
                        Tipo = Calificacion.Tipo,
                        Fecha = Calificacion.Fecha,
                        Calificacion_updated_at = Calificacion.Calificacion_updated_at,
                        Fecha = Calificacion.Fecha_Evaluacion.HasValue
                                                ? Calificacion.Fecha_Evaluacion.Value.Date + (Calificacion.Hora ?? TimeSpan.Zero)
                                                : (DateTime?)null,
                        //Fecha = ObtenerFechaValida(Calificacion.Fecha_Evaluacion, Calificacion.Hora),

                        Porcentaje = Calificacion.Porcentaje,
                        Observaciones = Calificacion.Observaciones ?? "Sin observaciones",
                        //ObservacionesPuntaje = Calificacion.Observaciones_Puntaje
                    };
                }).OrderBy(c => c.Calificacion_updated_at)
                .ThenBy(c => c.Evaluacion!.Contains("B") ? 1 :
                    c.Evaluacion.Contains("S") ? 2 :
                    c.Evaluacion.Contains("F") ? 3 : 4).ToList() ?? []; // Ordenar por Evaluacion*/


            return new Asignatura_Group
            {
                Descripcion = materiasByClass.Nombre,
                Descripcion_Corta = materiasByClass.Nombre,
                Docente = materiasByClass.GetNombreCompletoDocente(),
                Evaluaciones = A?.GroupBy(e => e.Evaluacion).Where(g => g.Count() == 1).Select(g => g.First()).Select(g => g.Evaluacion).ToList() ?? [],
                Calificaciones = calificaciones_Groups.OrderBy(c => c.Calificacion_updated_at)
                .ThenBy(c => c.Evaluacion!.Contains('B') ? 1 :
                    c.Evaluacion.Contains("S") ? 2 :
                    c.Evaluacion.Contains("F") ? 3 : 4).ToList() ?? []
            };
        }

        private static Calificacion_Group BuildCalificacionGroup(Estudiante_Clases_View Calificacion, string roman)
        {
            return new Calificacion_Group
            {
                Id = Calificacion.Id,
                Order = Calificacion.ThisConfig?.periodo_inicio ?? 1,
                Resultado = Calificacion.Resultado,
                Periodo = roman,
                Evaluacion = $"{roman}" + (Calificacion.Evaluacion  ?? ""),
                EvaluacionCompleta = Calificacion.Observaciones_Puntaje ?? "",
                Tipo = Calificacion.Tipo ?? ($"{roman}" + Calificacion.EvaluacionCompleta),
                Fecha = Calificacion.Fecha,
                Calificacion_updated_at = Calificacion.Calificacion_updated_at,
                Porcentaje = Calificacion.Porcentaje,
                Observaciones = Calificacion.Observaciones ?? "Sin observaciones",
            };
        }

        public static DateTime? ObtenerFechaValida(DateTime? fecha, TimeSpan? hora)
        {
            if (!fecha.HasValue || fecha.Value < new DateTime(1900, 1, 1))
                return null;

            var fechaCompleta = fecha.Value.Date + (hora ?? TimeSpan.Zero);
            string fechaTexto = fechaCompleta.ToString("yyyy-MM-dd HH:mm:ss");

            if (DateTime.TryParseExact(fechaTexto, "yyyy-MM-dd HH:mm:ss",
                System.Globalization.CultureInfo.InvariantCulture,
                System.Globalization.DateTimeStyles.None, out DateTime resultado))
            {
                return resultado;
            }

            return null;
        }


        public static Clase_Group BuildClaseGroup(IGrouping<string?, Estudiante_Clases_View> C)
        {
            var clase = C.First();
            Secciones? seccion = new Secciones { Id = clase.Seccion_id }.Find<Secciones>();
            C.OrderBy(c => c.Orden_Asignatura);
            return new Clase_Group
            {
                Id_Clase = clase.Id,
                Clase = clase.Descripcion?.ToUpper(),
                Repite = clase.Repitente == true ? "SI" : "NO",
                Nivel = clase.Nombre_nivel,
                Seccion = clase.nombre_seccion,
                Guia = seccion?.Guia?.Nombre_completo,
                Asignaturas = C.GroupBy(A => A.Nombre_asignatura)
                    .Select(A => BuildAsignaturaGroup(A)).ToList()
            };
        }

        public static Asignatura_Group BuildAsignaturaGroup(IGrouping<string?, Estudiante_Clases_View> A)
        {
            var clase = A.First();
            MateriasByClassQuery? materia = new MateriasByClassQuery
            {
                Estudiante_id = clase.Estudiante_id,
                Clase_id = clase.Clase_id,
                Materia_id = clase.Materia_id
            }.Find<MateriasByClassQuery>();

            return new Asignatura_Group
            {
                Descripcion = A.First().Nombre_asignatura,
                Docente = materia?.GetNombreCompletoDocente(),
                Evaluaciones = A.GroupBy(e => e.Evaluacion).Where(g => g.Count() == 1).Select(g => g.First()).Select(g => g.Evaluacion).ToList(),
                Calificaciones = [.. A.Select(Calificacion =>
                {
                    return new Calificacion_Group
                    {
                        Id = Calificacion.Id,
                        Order = Calificacion.ThisConfig?.periodo_inicio ?? 1,
                        Resultado = Calificacion.Resultado,
                        Evaluacion = Calificacion.Evaluacion ?? "",
                        EvaluacionCompleta = Calificacion.Observaciones_Puntaje ?? "",
                        Tipo = Calificacion.Tipo,
                        Fecha = Calificacion.Fecha,
                        Porcentaje = Calificacion.Porcentaje,
                        Observaciones =  Calificacion.Observaciones ?? "Sin observaciones",
                        //ObservacionesPuntaje = Calificacion.Observaciones_Puntaje
                     };
                }).OrderBy(c => c.Fecha)
                .ThenBy(c => c.Evaluacion!.Contains("B") ? 1 :
                    c.Evaluacion.Contains("S") ? 2 :
                    c.Evaluacion.Contains("F") ? 3 : 4) // Ordenar por Evaluacion
                ]
            };
        }

        internal static List<Clase_Group>? BuildClaseList(List<Estudiante_Clases_View> Clases)
        {
            return Clases?.OrderByDescending(C => C.Nombre_corto_periodo).ToList()
                    .GroupBy(C => C.nombre_seccion)
                    .Select(C => BuildClaseMateriaGroupList(C)).ToList();
        }
        public static Clase_Group BuildClaseMateriaGroupList(IGrouping<string?, Estudiante_Clases_View> C)
        {
            var clase = C.First();
            return new Clase_Group
            {
                Id_Clase = clase.Id,
                Clase = clase.Descripcion?.ToUpper(),
                Repite = clase.Repitente == true ? "SI" : "NO",
                Nivel = clase.Nombre_nivel,
                Seccion = clase.nombre_seccion,
                Estudiantes = C.GroupBy(E => E.Codigo)
                   .Select(E => BuildEstudianteAsignaturaGroup(E)).ToList()
            };
        }
        private static Estudiante_Group BuildEstudianteAsignaturaGroup(IGrouping<string, Estudiante_Clases_View> E)
        {
            var clase = E.First();
            return new Estudiante_Group
            {
                Descripcion = clase.Nombre_Estudiantes,
                Estado = clase.Estado,
                Sexo = clase.Sexo,
                Asignaturas = E.GroupBy(A => A.Nombre_asignatura)
                    .Select(A => BuildAsignaturaEstudiante(A)).ToList()
            };

        }
        private static Asignatura_Group BuildAsignaturaEstudiante(IGrouping<string, Estudiante_Clases_View> A)
        {
            var clase = A.First();
            return new Asignatura_Group
            {
                Descripcion = A.First().Nombre_asignatura,
                Descripcion_Corta = A.First().Nombre_corto_asignatura,
                Evaluaciones = A.GroupBy(e => e.Evaluacion).Where(g => g.Count() == 1).Select(g => g.First()).Select(g => g.Evaluacion).ToList(),
                Calificaciones = [.. A.Select(Calificacion =>
                {
                    return new Calificacion_Group
                    {
                        Id = Calificacion.Id,
                        Order = Calificacion.ThisConfig?.periodo_inicio ?? 1,
                        Resultado = Calificacion.Resultado,
                        Evaluacion = Calificacion.Evaluacion ?? "",
                        EvaluacionCompleta = Calificacion.EvaluacionCompleta ?? "",
                        Tipo = Calificacion.Tipo,
                        Fecha = Calificacion.Fecha,
                        Porcentaje = Calificacion?.Porcentaje,
                        Observaciones = Calificacion?.Observaciones ?? "Sin observaciones"
                    };
                }).OrderBy(c => c.Fecha)
                .ThenBy(c => c.Evaluacion!.Contains("B") ? 1 :
                    c.Evaluacion.Contains("S") ? 2 :
                    c.Evaluacion.Contains("F") ? 3 : 4) // Ordenar por Evaluacion
                ]
            };
        }
    }
}
