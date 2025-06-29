using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using APPCORE;
using APPCORE.Security;
using APPCORE.Services;
using CAPA_NEGOCIO.Services;
using CAPA_NEGOCIO.Templates;
using CAPA_NEGOCIO.Util;
using AppCore.Services;
using DataBaseModel;
using Microsoft.Identity.Client;
using APPCORE.Util;

namespace CAPA_NEGOCIO.Oparations
{
    public class CredentialsOperation : TransactionalClass
    {
        public static void sendInvitations()
        {
            var tutor = new Parientes();
            var filter = FilterData.And(
                FilterData.Distinc("credenciales_enviadas", true),
                FilterData.NotNull("user_id")
            );
            //tutor.filterData?.Add(FilterData.Equal("Id", 1881));
            tutor.filterData?.Add(FilterData.Limit(200));
            var tutores = tutor.Where<Parientes>(filter);

            tutores.ForEach(t =>
            {
                try
                {
                    Security_Users? usuario = new Security_Users().Find<Security_Users>(FilterData.Equal("id_user", t.User_id));

                    var plantillaString = HtmlContentGetter.ReadHtmlFile("credencialesUsuario.html", "Resources");
                    var template = TemplateServices.RenderTemplateCredenciales(plantillaString, usuario, t.Nombre_completo);
                    string subject = "Credenciales para acceso al Portal CCA";

                    MailServices.SendMail(new List<String>() { t.Email }, null, subject, template,
                    new List<ModelFiles>
                    {
                        new ModelFiles
                        {
                            Name = "Portal CCA - Paso a paso.pdf",
                            Value = "wwwroot\\Media\\Portal CCA - Paso a paso.pdf",
                            Type = ".pdf"
                        }
                    });
                    t.Credenciales_Enviadas = true;
                    t.Update();
                    
                }
                catch (Exception ex)
                {                    
                    LoggerServices.AddMessageError("Error al enviar correo de credenciales: id:" + t.Id.ToString(), ex);
                }
            });
        }
    }
}
