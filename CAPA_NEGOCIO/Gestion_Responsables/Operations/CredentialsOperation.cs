using APPCORE;
using APPCORE.Services;
using CAPA_NEGOCIO.Services;
using CAPA_NEGOCIO.Templates;
using AppCore.Services;
using DataBaseModel;
using APPCORE.Util;

namespace CAPA_NEGOCIO.Oparations
{
    public class CredentialsOperation : TransactionalClass
    {
        public static void sendInvitations()
        {
            var ultimoPeriodo = new Periodo_lectivos()
                .Get<Periodo_lectivos>()
                .OrderByDescending(p => p.Id)
                .FirstOrDefault();

            if (ultimoPeriodo == null) return;

            
            var estudiantesActivosIds = new Estudiante_clases()
                .Where<Estudiante_clases>(FilterData.Equal("Periodo_lectivo_id", ultimoPeriodo.Id))
                .Select(ec => ec.Estudiante_id)
                .Distinct()
                .ToArray();

            if (!estudiantesActivosIds.Any()) return;

           var parientesActivosIds = new Estudiantes_responsables_familia()
                .Where<Estudiantes_responsables_familia>(FilterData.In("Estudiante_id", estudiantesActivosIds))
                .Select(rf => rf.Pariente_id)
                .Where(id => id.HasValue)
                .Cast<int>()
                .Distinct()
                .ToList();

            if (!parientesActivosIds.Any()) return;

            string idsParientesString = string.Join(",", parientesActivosIds);

            var tutor = new Parientes();
            var filter = FilterData.And(
                FilterData.In("id", idsParientesString), 
                FilterData.Distinc("credenciales_enviadas", true),
                FilterData.NotNull("user_id"),
                FilterData.Equal("Responsable_Pago", true)
            );

            tutor.filterData?.Add(FilterData.Limit(200));
            var tutores = tutor.Where<Parientes>(filter);

            tutores.ForEach(t =>
            {
                try
                {
                    // 1. Buscamos el usuario
                    Security_Users? usuario = new Security_Users().Find<Security_Users>(FilterData.Equal("Id_User", t.User_id));
                    if (usuario == null || usuario.Estado != "ACTIVO")
                    {
                        LoggerServices.AddMessageInfo($"Salto de envío: El pariente {t.Id} ya no tiene un usuario activo.");
                        return; 
                    }

                    if (string.IsNullOrEmpty(t.Email) || !StringUtil.IsValidEmail(t.Email))
                    {
                        return;
                    }

                    var plantillaString = HtmlContentGetter.ReadHtmlFile("credencialesUsuario.html", "Resources");
                    var template = TemplateServices.RenderTemplateCredenciales(plantillaString, usuario, t.Nombre_completo);
                    string subject = "Renovación credenciales (Actualización de Portal CCA)";

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
