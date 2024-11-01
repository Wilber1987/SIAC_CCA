using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CAPA_DATOS;
using CAPA_NEGOCIO.UpdateModule.Model;
using CAPA_NEGOCIO.Utility;
using DataBaseModel;

namespace CAPA_NEGOCIO.Templates
{
	public class DocumentsData
	{
		public string? Header { get; set; }
		public string? WatherMark { get; set; }
		public string? Body { get; set; }
		public string? Footer { get; set; }
		public DocumentsData GetBoletinDataFragments()
		{
			var theme = new PageConfig();
			Header = HtmlContentGetter.ReadHtmlFile("header-boletin.html", "Resources/BoletinFragments");
			WatherMark = HtmlContentGetter.ReadHtmlFile("wathermark.html", "Resources/BoletinFragments");
			Footer = HtmlContentGetter.ReadHtmlFile("footer.html", "Resources/BoletinFragments");
			var periodo = Periodo_lectivos.PeriodoActivo();
			//build header
			Header = Header.Replace("{{ logo }}", theme.MEDIA_IMG_PATH + theme.LOGO_PRINCIPAL)
				.Replace("{{ titulo }}", theme.TITULO)
				.Replace("{{ sub-titulo }}", theme.SUB_TITULO)
				.Replace("{{ sub-titulo2 }}", theme.SUB_TITULO2)
				.Replace("{{ periodo-lectivo }}", periodo?.Nombre_corto);
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

			WatherMark = WatherMark.Replace("url-wattermark", theme.MEDIA_IMG_PATH + theme.WATHERMARK);

			return this;
		}

		public DocumentsData GetContratoFragment(UpdateData data)
		{
			var theme = new PageConfig();
			var contratos = new List<string>();

			var plantilla = HtmlContentGetter.ReadHtmlFile("contratotemplate.html", "Resources");

			var primerParienteConUserId = data.Parientes?.FirstOrDefault(p => p.User_id != null);
			DateTime fechaActual = DateTime.Now;

			plantilla = plantilla.Replace("{{ logo }}", theme.MEDIA_IMG_PATH + theme.LOGO_PRINCIPAL)
								 .Replace("{{ current_year }}", fechaActual.Year.ToString())
								 .Replace("{{ impresion }}", fechaActual.ToString("dd.MM.yyyy"));

			plantilla = plantilla.Replace("{{ nombre_responsable1 }}", primerParienteConUserId?.Nombre_completo ?? string.Empty)
								 .Replace("{{ cedula1 }}", primerParienteConUserId?.Identificacion ?? string.Empty);

			var segundoResponsable = data.Parientes?
				.FirstOrDefault(p => p.Estudiantes_responsables_familia?.Any(erf => erf.Parentesco_id == 10) == true)
				?? data.Parientes?.FirstOrDefault(p => p != primerParienteConUserId && p.User_id != null);

			plantilla = plantilla.Replace("{{ nombre_responsable2 }}", segundoResponsable?.Nombre_completo ?? string.Empty)
								 .Replace("{{ cedula2 }}", segundoResponsable?.Identificacion ?? string.Empty);

			foreach (var estudiante in data.Estudiantes ?? new List<Estudiantes_Data_Update>())
			{
				var contratoEstudiante = plantilla;
				contratoEstudiante = contratoEstudiante.Replace("{{ nombre_estudiante }}", estudiante?.Nombre_completo ?? string.Empty)
													   .Replace("{{ codigo_estudiante }}", estudiante?.Codigo ?? string.Empty)
													   .Replace("{{ codigo_familia }}", estudiante?.Id_familia?.ToString() ?? string.Empty);
				contratos.Add(contratoEstudiante);
			}

			Body = string.Join(Environment.NewLine, contratos);

			return this;
		}

		public DocumentsData GetBoletaFragment(UpdateData data)
		{
			var theme = new PageConfig();
			var contratos = new List<string>();

			var plantilla = HtmlContentGetter.ReadHtmlFile("contratotemplate.html", "Resources");

			var primerParienteConUserId = data.Parientes?.FirstOrDefault(p => p.User_id != null);
			DateTime fechaActual = DateTime.Now;

			plantilla = plantilla.Replace("{{ logo }}", theme.MEDIA_IMG_PATH + theme.LOGO_PRINCIPAL)
								 .Replace("{{ current_year }}", fechaActual.Year.ToString())
								 .Replace("{{ impresion }}", fechaActual.ToString("dd.MM.yyyy"));

			plantilla = plantilla.Replace("{{ nombre_responsable1 }}", primerParienteConUserId?.Nombre_completo ?? string.Empty)
								 .Replace("{{ cedula1 }}", primerParienteConUserId?.Identificacion ?? string.Empty);

			var segundoResponsable = data.Parientes?
				.FirstOrDefault(p => p.Estudiantes_responsables_familia?.Any(erf => erf.Parentesco_id == 10) == true)
				?? data.Parientes?.FirstOrDefault(p => p != primerParienteConUserId && p.User_id != null);

			plantilla = plantilla.Replace("{{ nombre_responsable2 }}", segundoResponsable?.Nombre_completo ?? string.Empty)
								 .Replace("{{ cedula2 }}", segundoResponsable?.Identificacion ?? string.Empty);

			foreach (var estudiante in data.Estudiantes ?? new List<Estudiantes_Data_Update>())
			{
				var contratoEstudiante = plantilla;
				contratoEstudiante = contratoEstudiante.Replace("{{ nombre_estudiante }}", estudiante?.Nombre_completo ?? string.Empty)
													   .Replace("{{ codigo_estudiante }}", estudiante?.Codigo ?? string.Empty)
													   .Replace("{{ codigo_familia }}", estudiante?.Id_familia?.ToString() ?? string.Empty);
				contratos.Add(contratoEstudiante);
			}

			Body = string.Join(Environment.NewLine, contratos);

			return this;
		}

	}
}