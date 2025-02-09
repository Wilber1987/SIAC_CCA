//@ts-check
import { Detalle_Pago } from "../Model/Detalle_Pago.js";
import { PagosRequest_ModelComponent } from "../Model/ModelComponent/PagosRequest_ModelComponent.js";
import { PagosRequest } from "../Model/PagosRequest.js";
import { StylesControlsV2, StylesControlsV3, StyleScrolls } from "../WDevCore/StyleModules/WStyleComponents.js";
import { WFilterOptions } from "../WDevCore/WComponents/WFilterControls.js";
import { WPrintExportToolBar } from "../WDevCore/WComponents/WPrintExportToolBar.mjs";
import { PageType } from "../WDevCore/WComponents/WReportComponent.js";
import { DateTime } from "../WDevCore/WModules/Types/DateTime.js";
import { ComponentsManager, html, WRender } from "../WDevCore/WModules/WComponentsTools.js";
import { css } from "../WDevCore/WModules/WStyledRender.js";

const route = location.origin
const routeEstudiantes = location.origin + "/Media/Images/estudiantes/"

/**
 * @typedef {Object} Historial_PagosReportViewConfig
 * * @property {Object} [propierty]
 */
class Historial_PagosReportView extends HTMLElement {
	/**
	 * @param {Historial_PagosReportViewConfig} [props] 
	 */
	constructor(props) {
		super();
		this.OptionContainer = WRender.Create({ className: "OptionContainer" });
		this.TabContainer = WRender.Create({ className: "TabContainer", id: 'TabContainer' });
		this.Manager = new ComponentsManager({ MainContainer: this.TabContainer, SPAManage: false });
		this.append(this.CustomStyle);
		const container = WRender.Create({ className: "component" });

		const EntityModel = new PagosRequest({
			// @ts-ignore
			Get: async () => {
				return await EntityModel.GetData("ApiPagosManage/GetPagosRealizados")
			}
		})
		this.Filter = new WFilterOptions({
			AutoSetDate: true,
			AutoFilter: true,
			EntityModel: EntityModel,
			ModelObject: new PagosRequest_ModelComponent(),
			UseEntityMethods: true,
			Display: true,
			Dataset: [],
			FilterFunction: async (filterData) => {
				container.innerHTML = "";
				//console.log(this.Filter.ModelObject);
				/*const facturas = await new pagosRequest().Where(
					this.Filter.ModelObject.FilterData[0]
				)*/
				//this.DrawInformepagos(filterData);
				container.append(this.DrawInformepagos(filterData));
				this.Manager.NavigateFunction("informe", container)
			}
		});
		this.OptionContainer.append(
			this.Filter,
			new WPrintExportToolBar({
			ExportPdfAction: (/**@type {WPrintExportToolBar} */ tool) => {
				const body = container.cloneNode(true);
				body.appendChild(this.CustomStyle.cloneNode(true));
				tool.ExportPdf(body, PageType.OFICIO_HORIZONTAL)
			}
		}));
		this.append(
			StylesControlsV2.cloneNode(true),
			StyleScrolls.cloneNode(true),
			StylesControlsV3.cloneNode(true),
			this.OptionContainer,
			
			this.TabContainer
		);
		this.Informes = {};
		this.Draw();
	}
	Draw = async () => {
		this.MainComponent();
	}

	async SetOption() {

	}
	async MainComponent() {

		//return container;
	}

	/**
	 * @param {Array<PagosRequest>} pagos 
	 */
	DrawInformepagos(pagos) {
		// @ts-ignore                    
		//const pagospago = Object.groupBy(pagos, p => p.Year);
		const pagosRealizados = Object.groupBy(pagos, p => p.Year);
		console.log(pagosRealizados)
		const containerInforme = this.ViewPagosReport(pagosRealizados);
		//this.Manager.NavigateFunction("informe",);
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

		for (const pagoYear in pagos) {
			/**@type {PagosRequest} */
			const pago = pagos[pagoYear][0];
			const pagoContainer = html`<div class="">
				<h1 style="color: #09559c">${localStorage.getItem("TITULO")}</h1>
				<h3 style="color: #0c3964;">Informe de pagos enviadas ${pago.Year}</h3>
				
				<hr/><!-- <div class="pago">
					<div class="data-container">
						<label class="pago-prop">AÑO:</label>
						<label>{pago.Year} - </label>
					</div>
				</div>				 -->
			 </div>`;

			//pagoContainer.append(html`<h3>Cargo</h3>`);
			//pagospago[pagoYear].forEach((/** @type {Tbl_pago} */ pago) => {
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

		console.log(PagosGroup);
		let totalCargos = 0;
		let totalCargosC = 0;
		for (const pagosMes in PagosGroup) {
			div.append(html`<h3>${pagosMes.toUpperCase()}</h3>`)
			const mesContainer = html`<table class="mes-container">				
				<tr class="data-details-container">
					<td class="data-title">Responsable</td>
					<td class="data-title">Estudiante</td>
					<td class="data-title">Concepto</td>
					<td class="data-title">Documento</td>
					<td class="data-title">Referencia</td>
					<td class="data-title">Fecha</td>
					<td class="data-title">Moneda</td>
					<td class="data-title">Monto</td>
				</tr>
			</table>`;
			//-------------------->
			//mesContainer.append(html`<h3>pagoes</h3>`);
			PagosGroup[pagosMes].forEach((/** @type {PagosRequest} */ pago) => {
				pago.Detalle_Pago.forEach(detalle => {
					const card = this.PagosRow(pago, detalle);
					mesContainer.append(card);
				});

			});
			const subTotalAbonos = PagosGroup[pagosMes].flatMap(p => p.Detalle_Pago)?.filter(d => d.Pago.Moneda == "DOLARES")?.reduce((acc, pago) => acc + pago.Monto, 0);
			const subTotalAbonosC = PagosGroup[pagosMes].flatMap(p => p.Detalle_Pago)?.filter(d => d.Pago.Moneda == "CORDOBAS")?.reduce((acc, pago) => acc + pago.Monto, 0);
			
			div.append(mesContainer);
			/*div.append(html`<div class="data-details-container total-container">
				<div class="data-title" style="grid-column: span 12">Cantidad</div>
				<div class="data-title value">${PagosGroup[pagosMes].flatMap(p => p.Detalle_Pago).length}</div>			
			</div>`);*/
			div.append(html`<div class="data-details-container total-container">
				<div class="pago-title" style="margin-right: 40px">Sub-Total mensual</div>
				<div class="pago-title value">C$ ${subTotalAbonosC.toFixed(2)  ?? "0.00"}</div>
			</div>`);
			div.append(html`<div class="data-details-container total-container">
				<div class="pago-title" style="margin-right: 40px">Sub-Total mensual</div>
				<div class="pago-title value">$ ${subTotalAbonos.toFixed(2)  ?? "0.00"}</div>
			</div>`);
			//-------------------->
			//mesContainer.append(html`<h3>Resumen</h3>`);
			totalCargos += subTotalAbonos;
			totalCargosC += subTotalAbonosC;

		}
		div.append(html`<div class="data-details-container total-container">
			<div class="pago-title" style="margin-right: 40px">Total</div>
			<div class="pago-title value total-cargos">C$ ${totalCargosC.toFixed(2)}</div>
		</div>`);
		div.append(html`<div class="data-details-container total-container">
			<div class="pago-title" style="margin-right: 40px">Total</div>
			<div class="pago-title value total-cargos">$ ${totalCargos.toFixed(2)}</div>
		</div>`);
		return div;
	}
	/**
	 * @param {PagosRequest} pagosRequest
	 * @param {Detalle_Pago} detallePago
	 */
	PagosRow(pagosRequest, detallePago) {		
		return WRender.Create({
			tagName: "tr", className: "data-details-container",
			children: [
				{ tagName: "td", class: "data-value", innerText: pagosRequest.Creador },
				{ tagName: "td", class: "data-value", innerText: detallePago?.Pago?.Estudiante?.Nombre_completo  + " - " +   detallePago?.Pago?.Estudiante?.Codigo},
				{ tagName: "td", class: "data-value", innerText: detallePago.Pago?.Concepto},
				{ tagName: "td", class: "data-value", innerText: detallePago.Pago?.Documento},
				{ tagName: "td", class: "data-value", innerText: pagosRequest.TpvInfo?.TransactionIdentifier ?? "-"},
				{ tagName: "td", class: "data-value", innerText: new DateTime(pagosRequest.Fecha).toISO()},
				{ tagName: "td", class: "data-value", innerText: pagosRequest.Moneda},
				{ tagName: "td", class: "data-value", innerText: detallePago.Total.toFixed(2)},
			]
		});
	}

	CustomStyle = css`
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
			/* display: grid;
			grid-template-columns: repeat(14, 1fr); */
			gap: 5px;
			& h3 {
				grid-column: span 14;
				font-size: 1em;
				border-bottom: 1px solid #919191;
			}
			
		}   
		table.mes-container {
			gap: 5px;
			border-collapse: collapse;
			width: 100%;
		}
		.data-details-container {
			/* display: contents; */
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
				display: flex;
				justify-content: flex-start;
				/* border-bottom: 1px solid #d6d6d6; */
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
customElements.define('w-component', Historial_PagosReportView);
export { Historial_PagosReportView };
