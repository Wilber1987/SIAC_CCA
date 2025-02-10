//@ts-check

import { Detalle_Pago } from "../Model/Detalle_Pago.js";
import { Estudiantes } from "../Model/Estudiantes.js";
import { Tbl_Pagos_ModelComponent } from "../Model/ModelComponent/Tbl_Pagos_ModelComponent.js";
import { PagosRequest } from "../Model/PagosRequest.js";
import { Tbl_Pago } from "../Model/Tbl_Pago.js";
import { StylesControlsV2, StylesControlsV3, StyleScrolls } from "../WDevCore/StyleModules/WStyleComponents.js";
import { WFilterOptions } from "../WDevCore/WComponents/WFilterControls.js";
import { WPrintExportToolBar } from "../WDevCore/WComponents/WPrintExportToolBar.mjs";
import { PageType } from "../WDevCore/WComponents/WReportComponent.js";
import { DateTime } from "../WDevCore/WModules/Types/DateTime.js";
import { WArrayF } from "../WDevCore/WModules/WArrayF.js";
import { ComponentsManager, html, WRender } from "../WDevCore/WModules/WComponentsTools.js";
import { css } from "../WDevCore/WModules/WStyledRender.js";

const route = location.origin
const routeEstudiantes = location.origin + "/Media/Images/estudiantes/"

/**
 * @typedef {Object} Historial_PagosViewConfig
 * * @property {Object} [propierty]
 */
class Historial_PagosView extends HTMLElement {
	/**
	 * @param {Historial_PagosViewConfig} props 
	 */
	constructor(props) {
		super();
		this.OptionContainer = WRender.Create({ className: "OptionContainer" });
		this.TabContainer = WRender.Create({ className: "TabContainer", id: 'TabContainer' });
		this.Manager = new ComponentsManager({ MainContainer: this.TabContainer, SPAManage: false });
		this.append(this.CustomStyle);
		const container = WRender.Create({ className: "component" });
		this.Filter = new WFilterOptions({
			AutoSetDate: true,
			AutoFilter: true,
			ModelObject: new Tbl_Pagos_ModelComponent(),
			UseEntityMethods: true,
			Display: true,
			Dataset: [],
			FilterFunction: async (filterData) => {
				//container.innerHTML = "";
				//console.log(this.Filter.ModelObject);
				const facturas = await new PagosRequest().Where(
					this.Filter.ModelObject.FilterData[0]
				)
				this.DrawInformePagos(filterData, facturas);
				//container.appendChild(this.DrawInformePagos(filterData, facturas));
			}
		});
		this.OptionContainer.append(new WPrintExportToolBar({
			ExportPdfAction: (/**@type {WPrintExportToolBar} */ tool) => {
				const body = this.Informes[this.selectedID].cloneNode(true);
				body.appendChild(this.CustomStyle.cloneNode(true));
				tool.ExportPdf(body, PageType.A4_HORIZONTAL)
			}
		}));
		this.append(
			StylesControlsV2.cloneNode(true),
			StyleScrolls.cloneNode(true),
			StylesControlsV3.cloneNode(true), this.OptionContainer, this.Filter,
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
	 * @param {Array<Tbl_Pago>} pagos 
	 * @param {Array<PagosRequest>} facturas 
	 */
	DrawInformePagos(pagos, facturas) {

		// @ts-ignore                    
		const pagosEstudiante = Object.groupBy(pagos, p => p.Estudiante_Id);

		if (!this.containerEstudiantes) {
			const studiantesCard = [];
			let i = 0;
			for (const estudianteId in pagosEstudiante) {
				if (i == 0) {
					this.selectedID = estudianteId;
				}
				const estudiante = pagosEstudiante[estudianteId][0].Estudiante;
				studiantesCard.push(this.BuildEstudiantes(new Estudiantes(estudiante), facturas, pagosEstudiante, estudianteId));
				i++;
			}
			this.containerEstudiantes = html`<section class="Historial">             
				<div class="alumnos-container aside-container">                
					${studiantesCard}
				</div>
			</section>`;
			this.OptionContainer.append(this.containerEstudiantes);
		}

		if (!this.Informes[this.selectedID]) {
			this.Informes[this.selectedID] = this.ViewEstudianteInforme(facturas, pagosEstudiante);
		} else {
			const informeActualizado = this.ViewEstudianteInforme(facturas, pagosEstudiante);
			this.Informes[this.selectedID].innerHTML = "";
			this.Informes[this.selectedID].innerHTML = informeActualizado.innerHTML;
		}
		this.Manager.NavigateFunction("informe" + this.selectedID, this.Informes[this.selectedID]);
	}
	ViewEstudianteInforme(facturas, pagosEstudiante) {
		// @ts-ignore
		const facturasEstudiante = Object.groupBy(facturas.flatMap(p => p.Detalle_Pago), p => p.Pago.Estudiante_Id);
		//console.log(facturasEstudiante);
		const div = html`<div class="pago-container">
		<style>
			.pago-container {
				display: flex;
				flex-direction: column;
				gap: 10px;
				padding: 10px;
			}
			</style>            
		</div>`;
		if (this.selectedID && pagosEstudiante[this.selectedID ?? 0]) {
			/**@type {Tbl_Pago} */
			const pago = pagosEstudiante[this.selectedID ?? 0][0];
			const estudianteContainer = html`<div class="estudiante-container">
				<div class="estudiante">
					<div class="data-container">
						<label class="estudiante-prop">NOMBRE:</label>
						<label>${pago.Estudiante?.Codigo} </label>
						<label>${pago.Estudiante?.Nombre_completo}</label>
					</div>
					<div class="data-container">
						<label class="estudiante-prop">NIVEL:</label>
						<label>${pago.Estudiante?.Nombre_nivel}</label>
						<label class="estudiante-prop">CLASE:</label>
						<label>${pago.Estudiante?.Descripcion}</label>
					</div>
				</div>
				
			 </div>`;

			//estudianteContainer.append(html`<h3>Cargo</h3>`);
			//pagosEstudiante[estudianteId].forEach((/** @type {Tbl_Pago} */ pago) => {
			const pagoMes = this.BuildPagosMes(pagosEstudiante[this.selectedID ?? 0], facturasEstudiante[this.selectedID ?? 0]);
			estudianteContainer.append(pagoMes);
			//});
			div.append(estudianteContainer);
		}
		//for (const estudianteId in pagosEstudiante) {

		//}
		//const pagoMes = this.BuildPagosMes(pagos);
		return div;
	}

	BuildEstudiantes(/** @type {Estudiantes} */ Estudiante, facturas, pagosEstudiante, estudianteId) {
		return html`<div class="estudiante-card-container" onclick="${() => {
			this.selectedID = estudianteId;
			if (!this.Informes[this.selectedID]) {
				this.Informes[this.selectedID] = this.ViewEstudianteInforme(facturas, pagosEstudiante);
			}
			this.Manager.NavigateFunction("informe" + this.selectedID, this.Informes[estudianteId])
		}}">            
			<div class="estudiante-card">
			<img src="${Estudiante.Foto ? `${routeEstudiantes}/${Estudiante.Id}/${Estudiante.Foto}`
				: route + "/media/image/avatar-estudiante.png"}" class="avatar-est rounded-circle" alt="">
			<div class="flex-1 ms-2 ps-1">
				<h5 class="font-size-14 mb-0">${Estudiante.GetNombreCompleto()}</h5>
				<label class="text-muted text-uppercase font-size-12">${Estudiante.Codigo}</label>
				</div>
			</div>
		</div>`
	}
	/**
	 * Construye los pagos del mes y los agrega al contenedor
	 * @param {Tbl_Pago[]} pagos - Lista de pagos
	 * @param {PagosRequest[]} facturasEstudiante - Lista de pagos
	 */
	BuildPagosMes(pagos, facturasEstudiante) {
		const div = html`<div class="pago-mes-container"></div>`;
		// @ts-ignore
		const pagosGroup = Object.groupBy(pagos, p => p.Mes);
		// @ts-ignore
		const facturaGroup = Object.groupBy(facturasEstudiante ?? [], p => p.Pago.Mes);

		let totalCargos = 0;

		for (const pagosMes in pagosGroup) {
			div.append(html`<h3>${WArrayF.Capitalize(DateTime.Meses[// @ts-ignore
				pagosMes - 1])}</h3>`);
			div.append(html`<hr/>`);
			const mesContainer = html`<table class="mes-container">				 
				<tr class="pago-details-container">
					<td class="pago-title">Fecha</td>
					<td class="pago-title">Documento</td>
					<td class="pago-title">Concepto</td>
					<td class="pago-title">MD</td>
					<td class="pago-title">Importe</td>
					<td class="pago-title">Estado</td>
				</tr>
			</table>`;
			//-------------------->

			div.append(html`<h5>Cargo</h5>`);
			pagosGroup[pagosMes].forEach((/** @type {Tbl_Pago} */ pago) => {
				const card = this.PagosCard(pago);
				mesContainer.append(card);
			});
			const subTotalCargos = pagosGroup[pagosMes].reduce((acc, pago) => acc + pago.Monto, 0);
			div.append(html`<table class="pago-details-container mes-container">
				<tr>
					<td class="pago-title value">${subTotalCargos?.toFixed(2) ?? "0.00"}</td>
				</tr>
			</table>`)
			//-------------------->
			div.append(html`<h5>Abono</h5>`);
			facturaGroup[pagosMes]?.forEach((/** @type {Detalle_Pago} */ pago) => {
				const card = this.PagosRequestCard(pago);
				mesContainer.append(card);
			});
			const subTotalAbonos = facturaGroup[pagosMes]?.reduce((acc, pago) => acc + pago.Monto, 0);
			div.append(html`<table class="pago-details-container mes-container">
				<tr>
					<td class="pago-title value">${subTotalAbonos?.toFixed(2) ?? "0.00"}</td>
				</tr>				
			</table>`);
			const total = (subTotalCargos ?? 0) - (subTotalAbonos ?? 0);
			div.append(html`<table class="pago-details-container mes-container">
				<tr>
					<td class="pago-title" style="grid-column: span 5">Sub-Total mensual</td>
					<td class="pago-title value">${total.toFixed(2) ?? "0.00"}</td>
				</tr>				
			</table>`);

			div.append(mesContainer);
			totalCargos += total;

		}

		div.append(html`<table class="mes-container total-container">
			<tr>
				<td class="pago-title" style="grid-column: span 5">Total</td>
				<td class="pago-title total-cargos">${totalCargos.toFixed(2) ?? "0.00"}</td>
			</tr>
		</table>`);
		return div;
	}
	/**
	 * @param {Tbl_Pago} pago
	 */
	PagosCard(pago) {
		return WRender.Create({
			tagName: "tr", className: "data-details-container",
			children: [
				{ tagName: "td", class: "pago-value", innerText: new DateTime(pago.Fecha).toISO() },
				//{ tagName: "td", class: "pago-value", innerText: pago.Tipo },
				{ tagName: "td", class: "pago-value", innerText: pago.Documento },
				{ tagName: "td", class: "pago-value", innerText: pago.Concepto },
				{ tagName: "td", class: "pago-value", innerText: pago.Money },
				{ tagName: "td", class: "pago-value", innerText: pago.Monto.toFixed(2) ?? "0.00" },
				{
					tagName: "td", class: `pago-value ${pago.Monto_Pendiente == 0 ? "CANCELADO" : "PENDIENTE"}`,
					innerText: (pago.Monto_Pendiente == 0 ? "CANCELADO" : "PENDIENTE")
				}
			]
		});
	}
	/**
	 * @param {Detalle_Pago} pago
	 */
	PagosRequestCard(pago) {
		return WRender.Create({
			tagName: "tr", className: "data-details-container",
			children: [
				{ tagName: "td", class: "pago-value", innerText: new DateTime(pago.Pago.Fecha).toISO() },
				{ tagName: "td", class: "pago-value", innerText: pago.Pago.Tipo },
				{ tagName: "td", class: "pago-value", innerText: pago.Pago.Documento },
				{ tagName: "td", class: "pago-value", innerText: pago.Pago.Concepto },
				{ tagName: "td", class: "pago-value", innerText: pago.Pago.Money },
				{ tagName: "td", class: "pago-value value", innerText: pago.Monto.toFixed(2) },
				{ tagName: "td", class: "pago-value CANCELADO", innerText: "EFECTUADO" }
			]
		});
	}

	CustomStyle = css`
		.component{
		   display: block;
		}    
		.CANCELADO {
			color: green;
		}  
		.PENDIENTE {
			color: red
		}  
		.mes-container {
			/* display: grid;
			grid-template-columns: repeat(7, 1fr);
			gap: 5px; */
			width: 100%;
			& h3 {
				grid-column: span 7;
				font-size: 1em;
			}
			& .pago-details-container {
				display: contents;
				grid-column: span 7;
			}
			.pago-title {
				/* font-size: 0.8em; */
				padding: 5px;
				font-weight: bold;
				background-color: #f1f1f1;
			}
			.pago-value {
				
			}
		}   
		td {
			padding: 10px;
		}
		.total-container {
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
		.estudiante-card-container {
			display: flex;
			border: 1px solid #d6d6d6;;
			border-radius: 10px;
			cursor: pointer;
			padding: 10px;
			max-width: 400px; 
		}
		.estudiante-card {
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
			border-left: 1px solid #d6d6d6;;
			padding-left: 20px;
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
		.estudiante-container {		
			width: 100%;	
			padding: 10px;
			border-radius: 10px;
			
		}
		.estudiante {
			margin-bottom: 20px;
		}
		.data-container {				
			display: flex;
			justify-content: flex-start;
			border-top: 1px solid #d6d6d6;
			border-bottom: 1px solid #d6d6d6;	
			text-transform: uppercase;			
		}
		.estudiante-prop {
			background-color: #f1f1f1;
			width: 100px;
		}
		.data-container label {
			padding: 10px;
			margin-bottom: 0;
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
customElements.define('w-component', Historial_PagosView);
export { Historial_PagosView };
