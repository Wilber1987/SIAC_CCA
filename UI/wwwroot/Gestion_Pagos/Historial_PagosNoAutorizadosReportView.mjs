//@ts-check
import { Detalle_Pago } from "../Model/Detalle_Pago.js";
import { PagosRequest_ModelComponent, PowerTranzTpvResponse_ModelComponent } from "../Model/ModelComponent/PagosRequest_ModelComponent.js";
import { PagosRequest, PowerTranzTpvResponse } from "../Model/PagosRequest.js";
import { StylesControlsV2, StylesControlsV3, StyleScrolls } from "../WDevCore/StyleModules/WStyleComponents.js";
import { WFilterOptions } from "../WDevCore/WComponents/WFilterControls.js";
import { WPrintExportToolBar } from "../WDevCore/WComponents/WPrintExportToolBar.mjs";
import { PageType } from "../WDevCore/WComponents/WReportComponent.js";
import { WCardTable } from "../WDevCore/WComponents/WTableComponent.js";
import { DateTime } from "../WDevCore/WModules/Types/DateTime.js";
import { ComponentsManager, ConvertToMoneyString, html, WRender } from "../WDevCore/WModules/WComponentsTools.js";
import { css } from "../WDevCore/WModules/WStyledRender.js";

/**
 * @typedef {Object} Historial_PagosNoAutorizadosReportViewConfig
 * * @property {Object} [propierty]
 */
class Historial_PagosNoAutorizadosReportView extends HTMLElement {
	/**
	 * @param {Historial_PagosNoAutorizadosReportViewConfig} [props] 
	 */
	constructor(props) {
		super();
		this.OptionContainer = WRender.Create({ className: "OptionContainer" });
		this.TabContainer = WRender.Create({ className: "TabContainer", id: 'TabContainer' });
		this.Manager = new ComponentsManager({ MainContainer: this.TabContainer, SPAManage: false });
		this.append(this.CustomStyle);
		this.Container = WRender.Create({ className: "component" });

		/**@type {PagosRequest} */
		// @ts-ignore
		this.EntityModel = new PagosRequest({
			// @ts-ignore
			Get: async () => {
				return await this.EntityModel.GetData("ApiPagosManage/GetPagosNoRealizados")
			}
		})
		this.Filter = new WFilterOptions({
			AutoSetDate: true,
			AutoFilter: true,
			EntityModel: this.EntityModel,
			ModelObject: new PagosRequest_ModelComponent(),
			UseEntityMethods: false,
			//UseManualControlForFiltering : true,
			Display: true,
			Dataset: [],
			FilterFunction: async (Filters) => {
				this.Filters = Filters;
			}
		});
		this.PrintTool = new WPrintExportToolBar({
			ExportPdfAction: (/**@type {WPrintExportToolBar} */ tool) => {
				const body = this.Container.cloneNode(true);
				body.appendChild(this.CustomStyle.cloneNode(true));
				tool.ExportPdf(body, PageType.OFICIO_HORIZONTAL, false, "Historial de pagos")
			}
		});
		this.OptionContainer.append(
			this.Filter,
			html`<button class="btn-go" onclick="${() => this.GetHistorialData()}">Filtrar</button>`
		);
		this.append(
			StylesControlsV2.cloneNode(true),
			StyleScrolls.cloneNode(true),
			StylesControlsV3.cloneNode(true),
			this.OptionContainer,
			this.TabContainer,
			this.PrintTool
		);
		this.Informes = {};
		this.Draw();
	}
	Draw = async () => {
		this.GetHistorialData();
	}

	async GetHistorialData() {
		this.Container.innerHTML = "";
		this.EntityModel.FilterData = this.Filters;
		const facturas = await this.EntityModel.Get();
		this.Container.append(this.DrawInformepagos(facturas));
		this.Manager.NavigateFunction("informe", this.Container)
	}

	/**
	 * @param {Array<PagosRequest>} pagos 
	 */
	DrawInformepagos(pagos) {
		// @ts-ignore                    
		const pagosRealizados = Object.groupBy(pagos, p => p.Year);
		const containerInforme = this.ViewPagosReport(pagosRealizados);
		return containerInforme;
	}

	/**
	 * @param {{ [x: string]: PagosRequest[]; }} pagos
	 */
	ViewPagosReport(pagos) {
		// @ts-ignore

		//console.log(facturaspago);
		const div = html`<div class="data-container">
		<style>
			.data-container {
				display: flex;
				flex-direction: column;
				gap: 10px;
				padding: 10px;
			}
			</style>            
		</div>`;
		const fechsFilter = this.Filter.FilterControls[0].querySelectorAll("input");


		for (const pagoYear in pagos) {
			/**@type {PagosRequest} */
			const pago = pagos[pagoYear][0];
			const pagoContainer = html`<div class="">
				<h2 style="color: #09559c">${localStorage.getItem("TITULO")}</h2>
				<h3 style="color: #0c3964;">Reporte de pasarela de pagos no aprobados del ${new DateTime(fechsFilter[0].value).toDDMMYYYY()} al ${new DateTime(fechsFilter[1].value).toDDMMYYYY()}</h3>				
				<hr/>
			 </div>`;
			const pagosMes = this.BuildPagosXMes(pagos[pagoYear]);
			pagoContainer.append(pagosMes);
			//});
			div.append(pagoContainer);
		}
		return div;
	}

	/**
	 * Construye los pagos del mes y los agrega al contenedor
	 * @param {PagosRequest[]} Pagos - Lista de pagos
	 */
	BuildPagosXMes(Pagos) {
		const div = html`<div class="data-mes-container"></div>`;
		// @ts-ignore
		const PagosGroup = Object.groupBy(Pagos, p => p.Month);

		let totalCargos = 0;
		let totalCargosD = 0;
		for (const pagosMes in PagosGroup) {
			div.append(html`<h3>${pagosMes.toUpperCase()}</h3>`)
			div.append(html`<hr>`);

			// @ts-ignore
			const PagosGroupDays = Object.groupBy(PagosGroup[pagosMes], p => new DateTime(p.Fecha).toDDMMYYYY());
			for (const pagosDay in PagosGroupDays) {
				const mesContainer = html`<table class="mes-container">				
					<tr class="data-details-container">
						<td class="data-title">Fecha</td>
						<td class="data-title">Respuesta de Pasarela</td>
						<td class="data-title">Documento</td>
						<td class="data-title">CodFamilia</td>
						<td class="data-title">Estudiante</td>
						<td class="data-title">Concepto</td>
						<td class="data-title">Monto</td>
						<td class="data-title">No Tarjeta</td>
					</tr>
				</table>`;
				div.append(html`<h4>${pagosDay}</h4>`);
				div.append(mesContainer);
				PagosGroupDays[pagosDay].forEach((/** @type {PagosRequest} */ pago) => {
					pago.Detalle_Pago.forEach((detalle, index) => {
						const card = this.PagosRow(pago, detalle, index);
						mesContainer.append(card);
					});
				});
				const subTotalAbonosDay = PagosGroupDays[pagosDay]?.filter(p => p.Moneda == "CORDOBAS")?.reduce((acc, pago) => acc + pago.Monto, 0);
				const subTotalAbonosDayD = PagosGroupDays[pagosDay]?.filter(p => p.Moneda == "DOLARES")?.reduce((acc, pago) => acc + pago.Monto, 0);
				div.append(html`<div class="data-details-container total-container">
					<div class="pago-title" style="margin-right: 40px; width: 70%">Sub-total diario</div>
					<div class="pago-title value">C$ ${ConvertToMoneyString(subTotalAbonosDay) ?? "0.00"}</div>					
				</div>`);
			}

			const subTotalAbonos = PagosGroup[pagosMes]?.filter(p => p.Moneda == "CORDOBAS")?.reduce((acc, pago) => acc + pago.Monto, 0);
			const subTotalAbonosD = PagosGroup[pagosMes]?.filter(p => p.Moneda == "DOLARES")?.reduce((acc, pago) => acc + pago.Monto, 0);


			div.append(html`<div class="data-details-container total-container">
				<div class="pago-title" style="margin-right: 40px; width: 70%">Sub-Total mensual</div>
				<div class="pago-title value">C$ ${ConvertToMoneyString(subTotalAbonos) ?? "0.00"}</div>
				<!-- <div class="pago-title value">$ ${ConvertToMoneyString(subTotalAbonosD) ?? "0.00"}</div> -->
			</div>`)
			totalCargos += subTotalAbonos;
			totalCargosD += subTotalAbonosD;

		}
		div.append(html`<div class="data-details-container total-container">
			<div class="pago-title" style="margin-right: 40px; width: 70%">Total</div>
			<div class="pago-title value total-cargos">C$ ${ConvertToMoneyString(totalCargos)}</div>			
		</div>`);
		return div;
	}
	/**
	 * @param {PagosRequest} pagosRequest
	 * @param {Detalle_Pago} detallePago
	* @param {Number} index
	 */
	PagosRow(pagosRequest, detallePago, index) {
		
		const row = {
			/**@type {String} */  tagName: "tr",
			/**@type {String} */ className: "data-details-container",
			/**@type {Array} */ children: []
		}
		if (index == 0) {
			const card = this.BuildTpvCard(pagosRequest);	
			row.children.push({
				tagName: "td",
				rowSpan : pagosRequest.Detalle_Pago.length,
				class: "data-value", 
				innerText: new DateTime(pagosRequest.Fecha).toDDMMYYYY()
			});
			row.children.push({
				tagName: "td", 
				class: "data-value",
				rowSpan: pagosRequest.Detalle_Pago.length.toString(), children: [card]
			});
		}

		row.children.push({ tagName: "td", class: "data-value", innerText: detallePago.Pago?.Documento });
		row.children.push({ tagName: "td", class: "data-value", innerText: detallePago.Pago?.Estudiante?.Idtfamilia ?? " - " });
		row.children.push({ tagName: "td", class: "data-value", innerText: detallePago?.Pago?.Estudiante?.Nombre_completo + " - " + detallePago?.Pago?.Estudiante?.Codigo });
		row.children.push({ tagName: "td", class: "data-value", innerText: detallePago.Pago?.Concepto });
		row.children.push({ tagName: "td", class: "data-value value", innerText: ConvertToMoneyString(detallePago.Total, detallePago.Pago.Money) });
		row.children.push({
			tagName: "td",
			class: "data-value",
			innerText: pagosRequest.CardNumber ? `**** **** **** ${pagosRequest.CardNumber}` : "-"
		});
		return WRender.Create(row);
	}
	/**
	 * @param {PagosRequest} pagosRequest
	 */
	BuildTpvCard(pagosRequest) {
		const card = new PowerTranzTpvResponse({
			TotalAmount: pagosRequest.TpvInfo.TotalAmount,
			CurrencyCode: pagosRequest.Moneda,
			Mensaje: pagosRequest.TpvInfo.ResponseMessage,
			Identificador_de_transaccion: pagosRequest.TpvInfo.TransactionIdentifier,
			Estado: pagosRequest.TpvInfo.Approved ? "-": "No aprobado",
			Errores: pagosRequest.TpvInfo.Errors.map( error => `${error.Code} - ${error.Message}`).join(", ")
		});
		return new WCardTable(card, new PowerTranzTpvResponse_ModelComponent());
	}

	formatNumber(num) {
		return num.toString().padStart(8, '0');
	}

	CustomStyle = css`
		.btn-go {
			height: 50px;
			align-self: center;
		}
		.OptionContainer {			
			display: flex;
			justify-content: space-between;
			margin-bottom: 10px;
			& w-filter-option {
				flex: 2
			}
			& w-tool-bar {
				flex: 1
			}
		}
		.component{
		   display: block;
		   width: 100%;		   
		}   
		.component table{
		   width: 100%;		   
		}   
		.CANCELADO {
			color: green;
		}  
		.PENDIENTE {
			color: red
		}  
		.mes-container {
			gap: 5px;
			width: 100%;			
		}   
		hr {
			margin: 10px 0px;
		}
		h3 {
			grid-column: span 14;
			font-size: 1.1em;
		}
		h4 { 
			grid-column: span 14;
			font-size: 1em;
			margin-bottom: 10px;
			margin-top: 10px;
			
		}
		table.mes-container {
			gap: 5px;
			border-collapse: collapse;
			width: 100%;
		}
		.data-details-container {
			grid-column: span 14;
		}
		.total-container {
			display: flex;
			justify-content: space-between;
			background-color: #f1f1f1;
			font-weight: bold;
			padding: 10px;
			font-size: 1.2em
		}
		.data-title {
			font-size: 0.8em;
			padding: 8px;
			font-weight: bold;
			background-color: #f1f1f1;
		}
		.data-value {
			font-size: 0.8em;
			padding: 8px;
		}
		.total-container {			
			font-size: 1em;
			text-align: right;
		} 
		.value, .total-cargos {
			text-align: right;
		}  
		.Historial{
			display: flex;
			flex-direction: column;            
			gap: 20px;
		}   
		.Historial .options-container {
			display: flex;
			align-items: center;
			justify-content: space-between;
			grid-column: span 2;
		}
		.pago-card-container {
			display: flex;
			border: 1px solid #d6d6d6;;
			border-radius: 10px;
			cursor: pointer;
			padding: 10px;
			max-width: 400px; 
		}
		.pago-card {
			display: flex;         
			gap: 10px;
			min-width: 400px;
			align-items: center;
		}
		.alumnos-container {
			display: flex;
			flex-direction: row;
			flex-wrap: wrap;
			gap: 10px;
		}
		.TabContainer {
			min-height: 400px;
		}
		.avatar-est{
			height: 100px;
			width: 80px;
			min-width: 80px;
			border-radius: 10px !important;
			object-fit: cover;
		}
		
		.aside-container {            
			padding: 0;
			border-radius: 0;
			box-shadow: unset
		}
		.pago-container {
			display: flex;
			flex-direction: column;
			gap: 0px;
			padding: 10px;
			& .pago {
				margin-bottom: 20px;
			}
			& .data-container {
				width: 100%;
				display: flex;
				justify-content: flex-start;
				& .pago-prop {
					background-color: #f1f1f1;
					width: 100px;
				}
				& label {
					padding: 10px;
					margin-bottom: 0;
				}
			}
		}
		
		@media (max-width: 768px) {
			.Historial{               
				grid-template-columns: 100%;
			} 
			.Historial .options-container {
				grid-column: span 1;
			}
			.TabContainer {
				border-left: unset;
				padding-left: unset;                
			}
		}
	`
}
customElements.define('w-component', Historial_PagosNoAutorizadosReportView);
export { Historial_PagosNoAutorizadosReportView };
