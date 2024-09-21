using API.Controllers;
using CAPA_DATOS;
using CAPA_DATOS.Services;
using DataBaseModel;
using DatabaseModelNotificaciones;

namespace CAPA_NEGOCIO.Gestion_Mensajes.Operations
{
    public class NotificationOperation : TransactionalClass
    {

        public ResponseService SaveNotificacion(string identity, NotificationRequest request)
        {
            UserModel user = AuthNetCore.User(identity);

            //1- CREAR UNA TABLA EN BD PARA ALMACENAR NOTIFICACIONES (CREAR EL SCRIPT EN LA CARPETA DE LOS SQL)
            //2- IMPLEMENTAR LOGICA PARA GUARDAR NOTIFICACIONES, ARCHIVOS DE LAS NOTIFICACIONES.
            //3- CREAR CONTROLLADOR PARA INVOCAR ESTE METODO DE ESTA CLASE (NotificationOperation().SaveNotificacion())
            try
            {
                foreach (var file in request.Files ?? [])
                {
                    ModelFiles? Response = (ModelFiles?)FileService.upload("Attach\\", file).body;
                    file.Value = Response?.Value;
                    file.Type = Response?.Type;
                }
                //hacer consultas para obtener el telefono
                var idsList = new List<int?>();
                List<Parientes> parientesFiltrados = [];

                if (request.NotificationType == NotificationTypeEnum.RESPONSABLE && request.Responsables?.Count > 0)
                {
                    idsList.AddRange([.. request.Responsables]);
                    parientesFiltrados = new Parientes().Where<Parientes>(FilterData.In("User_Id", idsList.ToArray()));
                    SendNotificacion(request, parientesFiltrados);
                }
                else if (request.NotificationType == NotificationTypeEnum.SECCION && request.Secciones?.Count > 0)
                {
                    var estudiante_Clases = new Estudiante_clases()
                        .Where<Estudiante_clases>(FilterData.In("Seccion_id", 
                            request.Secciones?.ToArray()));
                    var responsables = new Estudiantes_responsables_familia()
                        .Where<Estudiantes_responsables_familia>(FilterData.In("Estudiante_id", 
                            estudiante_Clases.Select(ec => ec.Estudiante_id).ToArray()));
                    parientesFiltrados = responsables?
                        .Where(responsable => responsable.Parientes != null && responsable.Parientes.User_id != null)
                        .Select(responsable => responsable?.Parientes ?? new Parientes())                        
                        .ToList() ?? [];
                    
                    SendNotificacion(request, parientesFiltrados);
                } 
                else if (request.NotificationType == NotificationTypeEnum.CLASE && request.Clases?.Count > 0)
                {
                    var estudiante_Clases = new Estudiante_clases()
                        .Where<Estudiante_clases>(FilterData.In("Clase_id", 
                            request.Clases?.ToArray()));
                    var responsables = new Estudiantes_responsables_familia()
                        .Where<Estudiantes_responsables_familia>(FilterData.In("Estudiante_id", 
                            estudiante_Clases.Select(ec => ec.Estudiante_id).ToArray()));
                    parientesFiltrados = responsables?
                        .Where(responsable => responsable.Parientes != null && responsable.Parientes.User_id != null)
                        .Select(responsable => responsable?.Parientes ?? new Parientes())                        
                        .ToList() ?? [];
                    
                    SendNotificacion(request, parientesFiltrados);
                } else {
                    parientesFiltrados = new  Parientes().Where<Parientes>(FilterData.NotNull("User_Id"));
                     SendNotificacion(request, parientesFiltrados);
                }

                //logica
                //BeginGlobalTransaction();
                //Logical de guardado
                //try { new notificacionEntity { Files = request.Files ?? [] ...}.Save(); } ....

                //sendWhatsapp();

                //CommitGlobalTransaction();
                LoggerServices.AddMessageInfo($"El usuario con id = {user.UserId} envio una notificación");
                return new ResponseService
                {
                    status = 200,
                    message = "Notificacion enviada"
                };
            }
            catch (System.Exception)
            {
                //RollBackGlobalTransaction();
                throw;
            }
        }

        private static void SendNotificacion(NotificationRequest request, List<Parientes> parientesFiltrados)
        {
            foreach (var item in parientesFiltrados)
            {
                var newNotificaciones = new Notificaciones
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
                };
                newNotificaciones.Save();
            }
        }

        public static List<Notificaciones> GetNotificaciones(string identity)
        {
            UserModel user = AuthNetCore.User(identity);
            return new Notificaciones { Id_User = user.UserId }.Get<Notificaciones>();
        }

    }

    /* public bool enviarWhatsapp(){
         //enviar whatsapp consultado los registros de notificaciones donde el campo enviado sea false 

         var notificacionesSinLeer = new Notificaciones().Find<Notificaciones>(FilterData.Equal("enviado", false));
         foreach (var notificacion in notificacionesSinLeer)
         {
             //codigo para enviar whatsapp aqui
             notificacion.Enviado = true;
             notificacion.Update();
         }

         return true;
     }*/
}