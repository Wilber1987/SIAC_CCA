using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CAPA_NEGOCIO.Gestion_Pagos.Model;
using CAPA_NEGOCIO.Templates;
using CAPA_NEGOCIO.Utility;
using DataBaseModel;

namespace CAPA_NEGOCIO.Gestion_Pagos.Operations
{
	public class PagosTemplate
	{
		// Método en C# para generar el HTML con valores reemplazados
		public static string GenerarFacturaHtml(PagosRequest request)
		{
			//string html = htmlTemplate;
			DocumentsData documentos = new DocumentsData().GetGeneralFragments();
			string html = HtmlContentGetter.ReadHtmlFile("tpvFacturaTemplate.html", "Resources");

			// Reemplazo de los valores simples
			html = html.Replace("{{ HEADER }}", documentos.Header ?? "");
			
			
			html = html.Replace("{{ NoRecibo }}", request.Id_Pago_Request?.ToString("D9"));
			html = html.Replace("{{ Ruc }}", Config.pageConfig().RUC);
			
			html = html.Replace("{{ Referencia }}", request.Referencia ?? "");
			html = html.Replace("{{ Creador }}", request.Creador ?? "");
			html = html.Replace("{{ Fecha }}", request.Fecha?.ToString("yyyy-MM-dd") ?? "");
			html = html.Replace("{{ Estado }}", request.Estado ?? "");
			html = html.Replace("{{ Moneda }}", request.Moneda ?? "");
			html = html.Replace("{{ Concepto }}", request.Descripcion ?? "");
			
			var totalC = request.Monto * (request.TasaCambio ?? 1);
			html = html.Replace("{{ Monto_C }}", totalC?.ToString("F2") ?? "0.00");
			html = html.Replace("{{ Monto }}", request.Monto?.ToString("F2") ?? "0.00");
			html = html.Replace("{{ Monto_TC }}", request.TasaCambio?.ToString("F2") ?? "0.00");

			// Construcción de los detalles del pago
			string detallePagoHtml = "";
			if (request.Detalle_Pago != null)
			{
				foreach (var detalle in request.Detalle_Pago)
				{
					var totalDetalle = detalle.Total * (request.TasaCambio ?? 1);
					detallePagoHtml += $"<tr><td>{detalle.Concepto}</td><td>{totalDetalle?.ToString("F2")}</td></tr>";
				}
			}
			
			html = html.Replace("{{ Detalle_Pago }}", detallePagoHtml);

			return html;
		}
	}
}