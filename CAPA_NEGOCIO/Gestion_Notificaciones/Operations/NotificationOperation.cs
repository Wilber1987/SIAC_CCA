using API.Controllers;
using APPCORE;
using APPCORE.Services;
using DataBaseModel;
using DatabaseModelNotificaciones;

namespace CAPA_NEGOCIO.Gestion_Mensajes.Operations
{
    public class NotificationOperation : TransactionalClass
    {
        public ResponseService SaveNotificacion(string identity, NotificationRequest request)
        {
            UserModel user = AuthNetCore.User(identity);
            int? periodoActivoId = Periodo_lectivos.PeriodoActivo()?.Id;

            try
            {
                // Manejo de archivos
                foreach (var file in request.Files ?? [])
                {
                    ModelFiles? Response = (ModelFiles?)FileService.upload("Attach\\", file).body;
                    file.Value = Response?.Value;
                    file.Type = Response?.Type;
                }

                List<Parientes> parientesFiltrados = [];

                // Lógica de segmentación según el tipo de notificación
                if (request.NotificationType == NotificationTypeEnum.RESPONSABLE && request.Responsables?.Count > 0)
                {
                    parientesFiltrados = new Parientes().Where<Parientes>(FilterData.In("User_Id", request.Responsables.ToArray()));
                }
                else if (request.NotificationType == NotificationTypeEnum.SECCION && request.Secciones?.Count > 0)
                {
                    var estudiante_Clases = new Estudiante_clases().Where<Estudiante_clases>(
                        FilterData.In("Seccion_id", request.Secciones.ToArray()),
                        FilterData.In("Clase_id", request.Clases?.ToArray() ?? [])
                    );
                    parientesFiltrados = GetParientesFromClases(estudiante_Clases);
                }
                else if (request.NotificationType == NotificationTypeEnum.CLASE && request.Clases?.Count > 0)
                {
                    var estudiante_Clases = new Estudiante_clases().Where<Estudiante_clases>(
                        FilterData.In("Clase_id", request.Clases.ToArray())
                    );
                    parientesFiltrados = GetParientesFromClases(estudiante_Clases);
                }
                else
                {
                    parientesFiltrados = new Parientes().GetResponsables();
                }

                // --- FILTRO CRÍTICO: Solo parientes con estudiantes en el periodo activo ---
                var parientesConAlumnosActivos = FiltrarPorPeriodoActivo(parientesFiltrados, periodoActivoId);

                // Guardar en la tabla de notificaciones
                SendNotificacion(request, parientesConAlumnosActivos);

                LoggerServices.AddMessageInfo($"El usuario con id = {user.UserId} guardó {parientesConAlumnosActivos.Count} notificaciones filtradas por periodo {periodoActivoId}");
                
                return new ResponseService { status = 200, message = "Notificaciones programadas correctamente" };
            }
            catch (System.Exception EX)
            {
                LoggerServices.AddMessageError($"Error al guardar notificación - User: {user.UserId}", EX);
                return new ResponseService { status = 500, message = EX.Message };
            }
        }

        // Método auxiliar para obtener parientes desde las clases
        private List<Parientes> GetParientesFromClases(List<Estudiante_clases> estudianteClases)
        {
            var idsEstudiantes = estudianteClases.Select(ec => ec.Estudiante_id).Distinct().ToArray();
            var responsables = new Estudiantes_responsables_familia().Where<Estudiantes_responsables_familia>(
                FilterData.In("Estudiante_id", idsEstudiantes)
            );

            return responsables?
                .Where(r => r.Parientes?.User_id != null)
                .Select(r => r.Parientes!)
                .GroupBy(p => p.User_id) // Evitar duplicados si un padre tiene varios hijos en la misma clase
                .Select(g => g.First())
                .ToList() ?? [];
        }

        // Método para filtrar la lista final según el Periodo Lectivo Activo
        private List<Parientes> FiltrarPorPeriodoActivo(List<Parientes> listaOriginal, int? periodoId)
        {
            if (periodoId == null) return [];

            return listaOriginal.Where(p => 
                p.Estudiantes_responsables_familia != null && 
                p.Estudiantes_responsables_familia.Any(erf => 
                    erf.Estudiantes?.Estudiante_clases?.Any(ec => ec.Periodo_lectivo_id == periodoId) ?? false
                )
            ).ToList();
        }

        private static void SendNotificacion(NotificationRequest request, List<Parientes> parientesFiltrados)
        {
            foreach (var item in parientesFiltrados)
            {
                new Notificaciones
                {
                    Id_User = item.User_id,
                    Mensaje = request.Mensaje,
                    Titulo = request.Titulo,
                    Media = request.Files,
                    Enviado = false,
                    Leido = false,
                    Tipo = request.NotificationType.ToString(),
                    Telefono = item.Telefono,
                    Email = item.Email,
                    Fecha = DateTime.Now
                }.Save();
            }
        }

        public static List<Notificaciones> GetNotificaciones(Notificaciones Inst, string identity)
        {
            UserModel user = AuthNetCore.User(identity);
            Inst.Id_User = user.UserId;
            // Retorna las notificaciones del usuario autenticado (para ver su historial en el portal)
            return Inst.Get<Notificaciones>();
        }
    }
}