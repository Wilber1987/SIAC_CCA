using System.Reflection;
using CAPA_DATOS;
using CAPA_DATOS.Security;
using DataBaseModel;

namespace CAPA_NEGOCIO.Templates
{
    public class TemplateServices
    {

        public static string ClaseBoletin(Clase_Group clase_Group)
        {

            return "";
        }

        public static string RenderTemplate(string templateContent, object model)
        {
            PropertyInfo[] properties = model.GetType().GetProperties();
            string renderedTemplate = templateContent;
            foreach (PropertyInfo property in properties)
            {
                string propertyName = property.Name;
                object propertyValue = property.GetValue(model, null);

                string placeholder = $"{{{{ {propertyName} }}}}";

                if (propertyValue != null)
                {
                    renderedTemplate = renderedTemplate.Replace(placeholder, propertyValue.ToString());
                }
                else
                {
                    renderedTemplate = renderedTemplate.Replace(placeholder, "");
                }
            }
            LoggerServices.AddMessageInfo("FIN DE RENDER PROPS");
            return renderedTemplate;
        }

        public static string RenderTemplateInvitacion(string templateContent, Security_Users model, string? nombre_completo)
        {            
            var theme = new PageConfig();						
			
			templateContent = templateContent.Replace("{{ logo }}", theme.MEDIA_IMG_PATH + theme.LOGO_PRINCIPAL)
				.Replace("{{ link }}", theme.URL_BASE)
				.Replace("{{ usuario }}", model.Mail)
				.Replace("{{ contrasena }}", EncrypterServices.Encrypt(model.Password))
				.Replace("{{ nombre }}", nombre_completo);

			return templateContent;          
        }
    }
}