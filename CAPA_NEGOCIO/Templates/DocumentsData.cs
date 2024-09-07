using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CAPA_DATOS;
using CAPA_NEGOCIO.Utility;
using DataBaseModel;

namespace CAPA_NEGOCIO.Templates
{
    public class DocumentsData
    {
        public string? Header { get; set; }
        public string? WatherMark { get; set; }
        public string? Footer { get; set; }
        public DocumentsData GetBoletinDataFragments()
        {
            var theme = new PageConfig();
            Header = HtmlContentGetter.ReadHtmlFile("header-boletin.html", "Resources/BoletinFragments");
            WatherMark = HtmlContentGetter.ReadHtmlFile("wathermark.html", "Resources/BoletinFragments");
            Footer = HtmlContentGetter.ReadHtmlFile("footer.html", "Resources/BoletinFragments");
            //build header
            Header = Header.Replace("{{ logo }}", theme.MEDIA_IMG_PATH + theme.LOGO_PRINCIPAL)
                .Replace("{{ titulo }}", theme.TITULO)
                .Replace("{{ sub-titulo }}", "Calificaciones");
            //build header
            WatherMark = WatherMark.Replace("url-wattermark", theme.MEDIA_IMG_PATH + theme.WATHERMARK);

            return this;
        }
        public DocumentsData GetDataFragments()
        {
            var theme = new PageConfig();
            Header = HtmlContentGetter.ReadHtmlFile("header.html", "Resources/BoletinFragments");
            WatherMark = HtmlContentGetter.ReadHtmlFile("wathermark.html", "Resources/BoletinFragments");
            Footer = HtmlContentGetter.ReadHtmlFile("footer.html", "Resources/BoletinFragments");
            //build header
            Header = Header.Replace("{{ logo }}", theme.MEDIA_IMG_PATH + theme.LOGO_PRINCIPAL)
                .Replace("{{ titulo }}", theme.TITULO)
                .Replace("{{ sub-titulo }}", "Calificaciones");
            //build header
            WatherMark = WatherMark.Replace("url-wattermark", theme.MEDIA_IMG_PATH + theme.WATHERMARK);

            return this;
        }
    }
}