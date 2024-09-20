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
                var idsList = new List<int>();
                if (request.NotificationType == NotificationTypeEnum.RESPONSABLE)
                {
                    foreach (var item in request.Responsables ?? [])
                    {
                        idsList.Add(item);
                    }
                    string result = string.Join(",", idsList);
                    var parientesFiltrados = new Parientes().Where<Parientes>(FilterData.In("Id", (result)));
                    foreach (var item in parientesFiltrados)
                    {
                        var newNotificaciones = new Notificaciones
                        {
                            Id_User = item.Id,
                            Mensaje = request.Mensaje,
                            Media = request.MediaUrl,
                            Enviado = false,
                            Leido = false,
                            Tipo = request.NotificationType.ToString(),
                            Telefono = item.Telefono,
                            Email = item.Email
                        };
                        newNotificaciones.Save();
                    }
                }

                //logica
                //BeginGlobalTransaction();
                //Logical de guardado
                //try { new notificacionEntity { Files = request.Files ?? [] ...}.Save(); } ....

                //sendWhatsapp();

                //CommitGlobalTransaction();
                LoggerServices.AddMessageInfo($"El usuario con id = {user.UserId} envio una notificación");
                return new ResponseService {
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