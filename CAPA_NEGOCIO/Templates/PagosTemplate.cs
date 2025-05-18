using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CAPA_DATOS.Services;
using CAPA_NEGOCIO.Gestion_Pagos.Model;
using CAPA_NEGOCIO.Templates;
using CAPA_NEGOCIO.Utility;
using DataBaseModel;

namespace CAPA_NEGOCIO.Gestion_Pagos.Operations
{
	public class PagosTemplate
	{
		// Método en C# para generar el HTML con valores reemplazados
		public static string GenerarFacturaHtml(PagosRequest request, bool isPrint = false)
		{
			if (request == null)
			{
				return "";
			}
			//string html = htmlTemplate;
			DocumentsData documentos = new DocumentsData().GetGeneralFragments();
			string html = HtmlContentGetter.ReadHtmlFile("tpvFacturaTemplate.html", "Resources");

			// Reemplazo de los valores simples
			html = html.Replace("{{ HEADER }}", documentos.Header ?? "");


			html = html.Replace("{{ NoRecibo }}", request.Id_Pago_Request?.ToString("D9"));
			html = html.Replace("{{ Ruc }}", Config.pageConfig().RUC);

			html = html.Replace("{{ Referencia }}", request.Referencia ?? "");
			html = html.Replace("{{ Creador }}", request.Creador ?? "");
			html = html.Replace("{{ Fecha }}", request.Fecha?.ToString("dd-MM-yyyy HH:mm") ?? "");
			html = html.Replace("{{ Estado }}", request.Estado?.ToString() ?? "");
			html = html.Replace("{{ Moneda }}", request.Moneda ?? "");
			html = html.Replace("{{ Concepto }}", request.Descripcion ?? "");



			var totalC = request.Moneda == MoneyEnum.DOLARES.ToString() ? request.Monto * (request.TasaCambio ?? 1) : request.Monto;
			var totalDolares = request.Moneda == MoneyEnum.DOLARES.ToString() ? request.Monto : request.Monto / (request.TasaCambio ?? 1);


			html = html.Replace("{{ Monto_C }}", NumberUtility.ConvertToMoneyString(totalC) ?? "0.00");
			html = html.Replace("{{ Monto }}", NumberUtility.ConvertToMoneyString(totalDolares) ?? "0.00");
			//html = html.Replace("{{ Moneda }}", request.Moneda == MoneyEnum.DOLARES.ToString() ? "DÓLARES" : "CORDOBAS");
			html = html.Replace("{{ Monto_TC }}", NumberUtility.ConvertToMoneyString(request.TasaCambio) ?? "0.00");

			// Construcción de los detalles del pago
			string detallePagoHtml = "";
			if (request.Detalle_Pago != null)
			{
				foreach (var detalle in request.Detalle_Pago)
				{
					var totalDetalle = detalle.Total;
					detallePagoHtml += $@"<tr>
						<td>{detalle.Concepto} - Est. {detalle?.Pago?.Estudiante?.Codigo}</td>
						<td style=""text-align: right; min-width: 180px""> {(request.Moneda == MoneyEnum.DOLARES.ToString() ? "$" : "C$")} 
						 {NumberUtility.ConvertToMoneyString(totalDetalle)}</td>
					</tr>";
				}
			}

			html = html.Replace("{{ Detalle_Pago }}", detallePagoHtml);
			if (!isPrint)
			{
				html += @"<div style=""text-align: center; margin-top: 20px;"">
					<a href=""{{ printURL }}"" class=""btn-success"">Imprimir</a>										
				</div>";
			}
			html = html.Replace("{{ printURL }}", "https://portal.cca.edu.ni/api/ApiPagos/GetFactura/" + request.Id_Pago_Request.ToString() ?? "");
			//html = html.Replace("{{ historialURL }}", "https://portal.cca.edu.ni/Gestion_Pagos/Historial_Pagos");
			return html;
		}
	}
}