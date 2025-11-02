using System.Security.Cryptography.Pkcs;
using APPCORE;
using CAPA_NEGOCIO.Gestion_Pagos.Model;
using CAPA_NEGOCIO.UpdateModule.Model;
using CAPA_NEGOCIO.Util;
using AppCore.Services;
using DataBaseModel;
using Microsoft.Extensions.Configuration;
using APPCORE.Util;
using CAPA_NEGOCIO.Templates.Model;
using Microsoft.IdentityModel.Tokens;
using CAPA_NEGOCIO.SystemConfig;
using APPCORE.Services;

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

			var template = new TemplateData { Descripcion = TemplatesDataType.CONTRATO_ACTUALIZACION }.Find<TemplateData>();
			
			//var plantilla = HtmlContentGetter.ReadHtmlFile("contratotemplate.html", "Resources");
			var plantilla = @$"<div style=""page-break-after: always; margin-bottom: 40px;"">{
				string.Join("", template?.Sections.Select(section => section.Body).ToList())
			}</div>";

			var primerParienteConUserId = data.Parientes?.FirstOrDefault(p => p.User_id != null);

			DateTime fechaActual = DateTime.Now;
			DateTime fechaManana = fechaActual.AddDays(1);

			var dia = fechaManana.Day;
			var mes = fechaManana.ToString("MMMM", new System.Globalization.CultureInfo("es-ES"));
			var anio = fechaManana.Year;

			int currentYear = (fechaActual.Month == 12) ? fechaActual.Year + 1 : fechaActual.Year;

			plantilla = plantilla.Replace("{{ logo }}", theme.MEDIA_IMG_PATH + theme.LOGO_PRINCIPAL)
								.Replace("{{ current_year }}", currentYear.ToString())
								.Replace("{{ impresion }}", fechaActual.ToString("dd.MM.yyyy"));

			plantilla = plantilla.Replace("{{ nombre_responsable1 }}", primerParienteConUserId?.Nombre_completo ?? string.Empty)
								 .Replace("{{ cedula1 }}", primerParienteConUserId?.Identificacion ?? string.Empty);

			var segundoResponsable = data.Parientes?
						   .FirstOrDefault(p => p.User_id == null && p.Id != primerParienteConUserId?.Id);
			plantilla = plantilla.Replace("{{ nombre_responsable2 }}", segundoResponsable?.Nombre_completo ?? string.Empty)
								 .Replace("{{ cedula2 }}", segundoResponsable?.Identificacion ?? string.Empty);
			plantilla = plantilla.Replace("{{ nombre_responsable2 }}", segundoResponsable?.Nombre_completo ?? string.Empty)
								 .Replace("{{ cedula2 }}", segundoResponsable?.Identificacion ?? string.Empty);
			plantilla = plantilla.Replace("{{ nombre_responsable2_firma }}", segundoResponsable?.Nombre_completo ?? string.Empty);
			var familia = new Familias().Where<Familias>(
										FilterData.Equal("id", primerParienteConUserId.Id_familia)
									).FirstOrDefault();
			foreach (var estudiante in data.Estudiantes ?? new List<Estudiantes_Data_Update>())
			{
				var contratoEstudiante = plantilla;
				contratoEstudiante = contratoEstudiante.Replace("{{ nombre_estudiante }}", estudiante?.Nombre_completo ?? string.Empty)
													   .Replace("{{ codigo_estudiante }}", estudiante?.Codigo ?? string.Empty)
													   .Replace("{{ codigo_familia }}", familia?.Idtfamilia?.ToString() ?? string.Empty);
				contratoEstudiante = contratoEstudiante.Replace("{{ dia }}", dia.ToString())
														.Replace("{{ mes }}", mes)
														.Replace("{{ anio }}", anio.ToString());
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

			foreach (var estudiante in data.Estudiantes ?? new List<Estudiantes_Data_Update>())
			{
				try
				{
					List<Viewestudiantesboletas> boletasMsql;
					var contratoEstudiante = plantillaBase;
					var anio = fechaActual.Year;
					var nexanio = fechaActual.Year + 1;
					if (SystemConfigImpl.IsWMachine())
					{
						boletasMsql = BoletasTestData.GetTestData();
					}
					else
                    {
                        boletasMsql = GetBoletas(fechaActual, estudiante);
                    }
                    if (boletasMsql != null)
					{
						var boletaMatricula = boletasMsql.Find(b => b.idservicio != 7);
						var boletaColegiatura = boletasMsql.Find(b => b.idservicio == 7);

						var fechaVencimiento = theme.FECHA_VENCIMIENTO_BOLETAS_ESTUDIANTES;
						var familia = new Familias().Where<Familias>(FilterData.Equal("id", estudiante.Id_familia)).FirstOrDefault();

						double? total = boletaMatricula?.ImporteNetoMD + boletaColegiatura?.ImporteNetoMD;
						string footer = Transactional_Configuraciones.GetBoletaFooter()?.Valor ?? "";

						contratoEstudiante = contratoEstudiante.Replace("{{ logo }}", theme.MEDIA_IMG_PATH + theme.LOGO_PRINCIPAL)
							.Replace("{{ ciclo }}", nexanio.ToString())
							.Replace("{{ nombre }}", $"{estudiante?.Nombre_completo}".Trim())
							.Replace("{{ no_expediente }}", familia?.Idtfamilia?.ToString() ?? string.Empty)
							.Replace("{{ curso_actual }}", $"{boletaMatricula?.Grado_Actual} {boletaMatricula?.Curso_Actual}".Trim())
							.Replace("{{ promueve }}", $"{boletaMatricula?.Grado_Siguiente} {boletaMatricula?.Curso_Siguiente}".Trim())
							.Replace("{{ moneda }}", "C$")
							.Replace("{{ importe_matricula }}", NumberUtility.ConvertToMoneyString(boletaMatricula?.ImporteNetoMD) )
							.Replace("{{ importe_colegiatura }}", NumberUtility.ConvertToMoneyString(boletaColegiatura?.ImporteNetoMD))
							.Replace("{{ importe_total }}", NumberUtility.ConvertToMoneyString(total))
							.Replace("{{ footer }}", footer)
							.Replace("{{ fecha_vencimiento }}", fechaVencimiento);
						boletas.Add(contratoEstudiante);
					}
				}
				catch (System.Exception ex)
				{
					LoggerServices.AddMessageError("ERROR: GetBoletaFragment para el estudiante con código " + estudiante.Codigo, ex);
					continue;
				}
			}

			Body = string.Join(Environment.NewLine, boletas);

			return this;
		}

        private List<Viewestudiantesboletas> GetBoletas(DateTime fechaActual, Estudiantes_Data_Update estudiante)
        {
            List<Viewestudiantesboletas> boletasMsql;
            using (var client = _sshTunnelService.GetSshClient("Bellacom"))
            {
                client.Connect();
                var forwardedPort = _sshTunnelService.GetForwardedPort("Bellacom", client, 3308);
                forwardedPort.Start();
                var boleta = new Viewestudiantesboletas();
                boleta.SetConnection(MySqlConnections.BellacomTest);
                boleta.IdTEstudiante = Convert.ToInt32(estudiante.Codigo);
                boleta.Ejercicio = fechaActual.Year;
                boleta.IdTPeriodoAcademico = fechaActual.Year + 1;
                boletasMsql = boleta.GetBoletas();
                forwardedPort.Stop();
                client.Disconnect();

            }
            return boletasMsql;
        }

        public DocumentsData GetGeneralFragments()
		{
			var theme = new PageConfig();
			Header = HtmlContentGetter.ReadHtmlFile("header.html", "Resources");
			WatherMark = HtmlContentGetter.ReadHtmlFile("wathermark.html", "Resources/BoletinFragments");
			Footer = HtmlContentGetter.ReadHtmlFile("footer.html", "Resources/BoletinFragments");
			//build header
			Header = Header.Replace("{{ logo }}", theme.MEDIA_IMG_PATH + theme.LOGO_PRINCIPAL)
				.Replace("{{ titulo }}", theme.TITULO)
				.Replace("{{ sub-titulo }}", "Calificaciones");

			WatherMark = WatherMark.Replace("url-wattermark", theme.MEDIA_IMG_PATH + theme.WATHERMARK);

			return this;
		}

	}
}