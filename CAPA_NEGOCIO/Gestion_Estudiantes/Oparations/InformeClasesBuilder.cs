

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
        public static List<Clase_Group>? BuildClaseGroupList(List<Estudiante_Clases_View>? Clases)
        {
            return Clases?.OrderByDescending(C => C.Nombre_corto_periodo).ToList()
                    //.Where(C => C.Nombre_nota != null)
                    .GroupBy(C => C.Descripcion)
                    .Select(C => BuildClaseGroup(C)).ToList();
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
            Docente_materias? docente_materia = new Docente_materias { Materia_id = clase.Materia_id, Seccion_id = clase.Seccion_id }.Find<Docente_materias>();
            return new Asignatura_Group
            {
                Descripcion = A.First().Nombre_asignatura,
                Docente = docente_materia?.Docentes?.Nombre_completo,
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
