using API.Controllers;
using APPCORE;
using APPCORE.Security;
using APPCORE.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twilio.Rest.Video.V1.Room.Participant;
namespace DataBaseModel
{
    public class NotificationRequest
    {
        public string? MediaUrl { get; set; }//creo que no se va utilizar
        public string? Titulo { get; set; }
        public string? Mensaje { get; set; }
        public List<ModelFiles>? Files { get; set; }
        public NotificationTypeEnum? NotificationType { get; set; }
        public bool? EsResponsable { get; set; }
        public List<int?>? Responsables { get; set; }
        public List<int?>? Niveles { get; set; }
        public List<int?>? Clases { get; set; }
        public List<int?>? Secciones { get; set; }
        public List<int?>? Periodos { get; set; }
        public List<NotificationsServicesEnum>? NotificationsServices { get; set; }
    }
    public enum NotificationsServicesEnum
    {
        WHATSAPP, MAIL
    } 

    public enum NotificationTypeEnum
    {
        SECCION, CLASE, PERIODO, NIVEL, RESPONSABLE
    }
}