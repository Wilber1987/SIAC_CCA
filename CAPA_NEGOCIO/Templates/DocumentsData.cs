using CAPA_DATOS;
using CAPA_NEGOCIO.Gestion_Pagos.Model;
using CAPA_NEGOCIO.UpdateModule.Model;
using CAPA_NEGOCIO.Util;
using CAPA_NEGOCIO.Utility;
using DataBaseModel;
using Microsoft.Extensions.Configuration;

namespace CAPA_NEGOCIO.Templates
{
	public class DocumentsData
	{
		public string? Header { get; set; }
		public string? WatherMark { get; set; }
		public string? Body { get; set; }
		public string? Footer { get; set; }

		private readonly SshTunnelService _sshTunnelService;

		public DocumentsData()
		{
			_sshTunnelService = new SshTunnelService(LoadConfiguration());
		}

		private IConfigurationRoot LoadConfiguration()
		{
			return new ConfigurationBuilder()
				.SetBasePath(Directory.GetCurrentDirectory())
				.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
				.Build();
		}
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
								 .Replace("{{ current_year }}", (fechaActual.Year+1).ToString())
								 .Replace("{{ impresion }}", fechaActual.ToString("dd.MM.yyyy"));

			plantilla = plantilla.Replace("{{ nombre_responsable1 }}", primerParienteConUserId?.Nombre_completo ?? string.Empty)
								 .Replace("{{ cedula1 }}", primerParienteConUserId?.Identificacion ?? string.Empty);

			 var segundoResponsable = data.Parientes?
       					 .FirstOrDefault(p => p.User_id == null && p.Id != primerParienteConUserId?.Id);


			plantilla = plantilla.Replace("{{ nombre_responsable2 }}", segundoResponsable?.Nombre_completo ?? string.Empty)
								 .Replace("{{ cedula2 }}", segundoResponsable?.Identificacion ?? string.Empty);

			var familia = new Familias().Where<Familias>(
										FilterData.Equal("id", primerParienteConUserId.Id_familia)
									).FirstOrDefault();

			foreach (var estudiante in data.Estudiantes ?? new List<Estudiantes_Data_Update>())
			{
				var contratoEstudiante = plantilla;
				contratoEstudiante = contratoEstudiante.Replace("{{ nombre_estudiante }}", estudiante?.Nombre_completo ?? string.Empty)
													   .Replace("{{ codigo_estudiante }}", estudiante?.Codigo ?? string.Empty)
													   .Replace("{{ codigo_familia }}", familia?.Idtfamilia?.ToString() ?? string.Empty);
				contratos.Add(contratoEstudiante);
			}

			Body = string.Join(Environment.NewLine, contratos);

			return this;
		}

		public DocumentsData GetBoletaFragment(UpdateData data)
		{
			var theme = new PageConfig();
			var boletas = new List<string>();

			var plantillaBase = HtmlContentGetter.ReadHtmlFile("boleta.html", "Resources");
			DateTime fechaActual = DateTime.Now;

			using (var client = _sshTunnelService.GetSshClient("Bellacom"))
			{
				client.Connect();
				var forwardedPort = _sshTunnelService.GetForwardedPort("Bellacom", client, 3308);
				forwardedPort.Start();

				try
				{
					var boleta = new Viewestudiantesboletas();
					boleta.SetConnection(MySqlConnections.BellacomTest);

					foreach (var estudiante in data.Estudiantes ?? new List<Estudiantes_Data_Update>())
					{
						var contratoEstudiante = plantillaBase;

						var boletaMsql = boleta.Where<Viewestudiantesboletas>(
										FilterData.Equal("idtestudiante", estudiante.Codigo),
										FilterData.Equal("ejercicio", fechaActual.Year),
										FilterData.Equal("idtperiodoacademico", fechaActual.Year + 1)
									).FirstOrDefault();

						var fechaVencimiento = theme.FECHA_VENCIMIENTO_BOLETAS_ESTUDIANTES;

						var familia = new Familias().Where<Familias>(FilterData.Equal("id", boletaMsql.idfamilia)).FirstOrDefault();

						contratoEstudiante = contratoEstudiante.Replace("{{ logo }}", theme.MEDIA_IMG_PATH + theme.LOGO_PRINCIPAL)
															   .Replace("{{ ciclo }}", (fechaActual.Year+1).ToString())
															   .Replace("{{ nombre }}", $"{boletaMsql?.Nombres} {boletaMsql?.Apellidos}".Trim())
															   .Replace("{{ no_expediente }}", familia?.Idtfamilia.ToString() ?? string.Empty)
															   .Replace("{{ curso_actual }}", $"{boletaMsql?.Grado_Actual} {boletaMsql?.Curso_Actual}".Trim())
															   .Replace("{{ promueve }}", $"{boletaMsql?.Grado_Siguiente} {boletaMsql?.Curso_Siguiente}".Trim())
															   .Replace("{{ moneda }}", boletaMsql?.IdTMoneda.ToString() ?? string.Empty)
															   .Replace("{{ importe_matricula }}", boletaMsql?.ImporteNetoMD.ToString() ?? string.Empty)
															   .Replace("{{ fecha_vencimiento }}", fechaVencimiento);

						boletas.Add(contratoEstudiante);
					}

					Body = string.Join(Environment.NewLine, boletas);
				}
				catch (System.Exception ex)
				{
					LoggerServices.AddMessageError("ERROR: GetBoletaFragment.", ex);
					forwardedPort.Stop();
					client.Disconnect();
					throw;
				}
				finally
				{
					forwardedPort.Stop();
					client.Disconnect();
				}
			}

			return this;
		}


	}
}