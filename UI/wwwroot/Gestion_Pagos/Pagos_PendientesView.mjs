//@ts-check
import { Detalle_Pago } from "../Model/Detalle_Pago.js";
import { PagosRequest } from "../Model/PagosRequest.js";
import { Tbl_Pago } from "../Model/Tbl_Pago.js";
import { StylesControlsV2 } from "../WDevCore/StyleModules/WStyleComponents.js";
import { ModalMessage } from "../WDevCore/WComponents/ModalMessage.js";
import { DateTime } from "../WDevCore/WModules/Types/DateTime.js";
import { WArrayF } from "../WDevCore/WModules/WArrayF.js";
import { ConvertToMoneyString, html, WRender } from "../WDevCore/WModules/WComponentsTools.js";
import { WOrtograficValidation } from "../WDevCore/WModules/WOrtograficValidation.js";
import { css } from "../WDevCore/WModules/WStyledRender.js";
import { WAlertMessage } from "../WDevCore/WComponents/WAlertMessage.js";
class Pagos_PendientesView extends HTMLElement {
	constructor() {
		super();
		this.TabContainer = WRender.Create({
			class: 'TabContainer', id: "TabContainer"
		});
		//this.DOMManager = new ComponentsManager({ MainContainer: this.TabContainer, SPAManage: true });
		// this.NotificationsNav = new WAppNavigator({
		// 	NavStyle: "tab",
		// 	Inicialize: true,
		// 	Elements: this.NavElements()
		// })
		this.DetailContainer = html`<div class="pago-detail"></div>`;
		this.TotalContainer = html`<div class="pago-monto ">
				<div class="pago-details-container">
				<div>Total</div>                 
				<div>0.00</div>                
			</div>
		</div>`;

		this.TabContainer.append(
			html`<h3>Detalle de pagos</h3>`,
			this.DetailContainer,
			this.TotalContainer,
			this.PayButton()
		);
		/**@type {Array<Detalle_Pago>} */
		this.pagosSeleccionados = [];
		this.pagosSeleccionadosProxy = new Proxy(this.pagosSeleccionados, {
			get: (target, property) => {
				return target[property];
			}, set: (target, property, value, receiver) => {
				target[property] = value;
				this.UpdatePagos();
				return true;
			}
		});
		this.PagosContainer = WRender.Create({ className: "PagosContainer" });
		this.append(
			this.Style,
			StylesControlsV2.cloneNode(true),
			this.PagosContainer,
			this.TabContainer
		);
		this.Controls = [];
		this.DrawPagos();
	}
	UpdatePagos() {
		let total = 0;
		//console.log(this.pagosSeleccionados);
		this.DetailContainer.innerHTML = "";
		this.TotalContainer.innerHTML = "";
console.log(this.pagosSeleccionados);

		this.pagosSeleccionados.forEach(detalle => {
			this.DetailContainer.append(html`<div class="pago-details-container">
				<div>${detalle.Pago?.Concepto} Est. ${detalle.Pago.Estudiante?.Codigo}</div>                 
				<div>${WOrtograficValidation.es(detalle.Pago?.Money)} ${ConvertToMoneyString(detalle.Monto ?? 0)}</div>                
			</div>`);
			total += detalle.Total ?? 0;
		});
		this.TotalContainer.append(html`<div class="pago-details-container">
			<div>Total</div>                 
			<div>${WOrtograficValidation.es(this.pagosSeleccionados[0]?.Pago?.Money)} ${ConvertToMoneyString(total)}</div>                
		</div>`);
	}

	PayButton() {
		return html`<button class="btn btn-success" onclick="${(/** @type {any} */ ev) => this.Pay()}">Pagar</button>`
	}
	async Pay() {
		if (this.pagosSeleccionados.length == 0) {
			document.body.appendChild(ModalMessage("Seleccione las mensualidades a pagar"));
			return;
		}
		for (const key in this.pagosSeleccionados) {
			if (this.pagosSeleccionados[key].Monto == 0) {
				document.body.appendChild(ModalMessage("El monto de un pago no puede ser 0"));
				return;
			}
		}
		const response = await new PagosRequest({ Detalle_Pago: this.pagosSeleccionados }).Save();
		if (response.status == 200) {
			window.location.href = "/Gestion_Pagos/Tpv";
			return;
		}
	}
	async DrawPagos() {
		const div = html`<div class="pago-container">
						<style>
							.pago-container {
								display: flex;
								flex-direction: column;
								gap: 10px;
								padding: 10px;
							}
						</style>
						${this.Style.cloneNode(true)}
					</div>`;
		/**@type {Array<Tbl_Pago>} */
		const pagos = await new Tbl_Pago().Get();
		// @ts-ignore                    
		const pagosEstudiante = Object.groupBy(pagos, p => p.Estudiante_Id);
		for (const estudianteId in pagosEstudiante) {
			/**@type {Tbl_Pago} */
			const pago = pagosEstudiante[estudianteId][0];
			const estudianteContainer = html`<div class="estudiante-container">
							<h3>${pago.Estudiante?.Nombre_completo}</h3>
							<hr/>
						</div>`;
			//pagosEstudiante[estudianteId].forEach((/** @type {Tbl_Pago} */ pago) => {
			const pagoMes = this.BuildPagosMes(pagosEstudiante[estudianteId]);
			estudianteContainer.append(pagoMes);
			//});
			div.append(estudianteContainer);
		}
		//const pagoMes = this.BuildPagosMes(pagos);
		this.PagosContainer.append(div);
	}


	/**
	 * Construye los pagos del mes y los agrega al contenedor
	 * @param {Tbl_Pago[]} pagos - Lista de pagos
	 * @returns {HTMLElement} - Contenedor de los pagos
	 */
	BuildPagosMes(pagos) {
		const div = html`<div class="pago-mes-container"></div>`;
		// @ts-ignore
		const pagosGroup = Object.groupBy(pagos, p => WArrayF.Capitalize(DateTime.Meses[p.Mes - 1]) + " " + new Date(p.Fecha).getFullYear());
		//pagos.sort((a, b) => new Date(a.Fecha) - new Date(b.Fecha))

		for (const pagosMes in pagosGroup) {
			const mesContainer = html`<div class="mes-container">
				<h3>${pagosMes}</h3>
			</div>`;

			pagosGroup[pagosMes].forEach((/** @type {Tbl_Pago} */ pago) => {
				const card = this.PagosCard(pago, this.pagosSeleccionadosProxy, pagos);
				this.Controls.push(card);
				mesContainer.append(card);
			});
			div.append(mesContainer);
		}
		return div;
	}

	connectedCallback() { }
	/**
	 * @param {Tbl_Pago} pago
	 * @param {Detalle_Pago[]} pagosSeleccionados
	 * @param {Tbl_Pago[]} pagosPendientes
	 */
	PagosCard(pago, pagosSeleccionados, pagosPendientes) {
		const newDetalle = Detalle_Pago.CrearPago(pago);
		const pagoInput = html`<input value="${pago.Monto_Pendiente}" min="0" max="${pago.Monto_Pendiente}" style="display: none" type="number" onchange="${(ev) => {
			// @ts-ignore
			if (parseFloat(pagoInput.value) > pago.Monto_Pendiente) {
				// @ts-ignore
				pagoInput.value = pago.Monto_Pendiente;
			}
			Detalle_Pago.ActualizarDetalle(newDetalle, parseFloat(ev.target.value))			
			this.UpdatePagos();
		}}" />`
		const checkPago = html`<input type="checkbox" class="check-pago Btn pago-control${pago.Id_Pago}" id="pago${pago.Id_Pago}" onchange="${(ev) => {
			// @ts-ignore
			pagoInput.value = pago.Monto_Pendiente;

			this.AgregarPago(ev.target, pago, pagosSeleccionados, pagosPendientes, newDetalle)
			Detalle_Pago.ActualizarDetalle(newDetalle, pago.Monto_Pendiente)
			this.UpdatePagos();
		}}" />`
		return html`<div class="pago-card" id="pago${pago.Id_Pago}">
		<div class="pago-detail">
			<div class="pago-title">${pago.Concepto}</div>		   
		</div>  
		<div class="pago-options pago-parcial-check">
			<label for="pago-parcial${pago.Id_Pago}">Pago parcial</label>
			<input type="checkbox" class="check-pago" id="pago-parcial${pago.Id_Pago}"  onchange="${(ev) => {
				if (ev.target.checked) {
					// @ts-ignore
					checkPago.checked = ev.target.checked;
					// @ts-ignore
					pagoInput.value = 0;
					// @ts-ignore
					this.AgregarPago(checkPago, pago, pagosSeleccionados, pagosPendientes, newDetalle)
					Detalle_Pago.ActualizarDetalle(newDetalle, 0)
				}
				else {
					// @ts-ignore
					pagoInput.value = pago.Monto_Pendiente;
					// @ts-ignore
					checkPago.checked = false;
					// @ts-ignore
					this.AgregarPago(checkPago, pago, pagosSeleccionados, pagosPendientes, newDetalle)
					Detalle_Pago.ActualizarDetalle(newDetalle, pago.Monto_Pendiente)
				}
				pagoInput.style.display = ev.target.checked ? "block" : "none";
				this.UpdatePagos();

			}}"/>
		</div>  
		<div class="pago-options">
			<label for="pago${pago.Id_Pago}" class="pago-monto">${WOrtograficValidation.es(pago.Money)} ${ConvertToMoneyString(pago.Monto_Pendiente)}</label>
			 ${pagoInput}       
			 ${checkPago}
			</div>       
	</div>`;
	}

	/**
	 * Agrega o elimina un pago de la lista de pagos seleccionados
	 * @param {HTMLInputElement} control - Evento del checkbox
	 * @param {Tbl_Pago} pago - Pago seleccionado o deseleccionado
	 * @param {Detalle_Pago[]} pagosSeleccionados - Pagos ya seleccionados
	 * @param {Tbl_Pago[]} pagosPendientes - Pagos pendientes de pago
	 */
	AgregarPago(control, pago, pagosSeleccionados, pagosPendientes, newDetalle) {
		/**@type {HTMLInputElement} */
		// @ts-ignore
		//const control = ev.target;
		const fechaPagoSeleccionado = new Date(pago.Fecha);

		// Comprobar si hay pagos anteriores no seleccionados (si se está seleccionando el pago)
		if (control.checked) {
			let pagosAnterioresNoSeleccionados = false;
			// @ts-ignore
			const pagosAnteriores = pagosPendientes.filter(p => new Date(p.Fecha).getMonth() < fechaPagoSeleccionado.getMonth());
			pagosAnteriores.forEach(pagoSeleccionadoAnterior => {
				if (!pagosSeleccionados.some(p => p.Pago?.Id_Pago == pagoSeleccionadoAnterior?.Id_Pago)) {
					pagosAnterioresNoSeleccionados = true;
				} else {
					/**@type {Detalle_Pago|undefined} */
					const pagoSeleccionado = pagosSeleccionados.find(p => p.Pago?.Id_Pago == pagoSeleccionadoAnterior?.Id_Pago);
					// @ts-ignore
					if (pagoSeleccionado?.Monto < pagoSeleccionado?.Pago?.Monto_Pendiente) {
						pagosAnterioresNoSeleccionados = true;
					}
				}
			});
			if (pagosAnterioresNoSeleccionados) {
				document.body.appendChild(ModalMessage('Debe cancelar los pagos anteriores antes de agregar este.'));
				control.checked = false;  // Desmarcar el checkbox
				return;
			}
			// Si no hay pagos anteriores no seleccionados, agregar el pago
			WArrayF.AddOrRemove(newDetalle, pagosSeleccionados, control.checked);

		} else {
			// Si se está deseleccionando el pago, eliminar los pagos con fecha posterior
			// Filtrar todos los pagos seleccionados que tengan una fecha mayor a la del pago deseleccionado
			// @ts-ignore
			const pagosASerRemovidos = pagosSeleccionados.filter(p => new Date(p.Pago.Fecha).getMonth() > fechaPagoSeleccionado.getMonth());
			// Eliminar los pagos con fechas posteriores
			pagosASerRemovidos.forEach(p => {
				WArrayF.AddOrRemove(p, pagosSeleccionados, false);  // Elimina el pago
				const controlF = this.Controls.find(c => c.id === `pago${p.Pago?.Id_Pago}`);
				controlF.querySelector(`.pago-control${p.Pago?.Id_Pago}`).checked = false;
			});
			// Finalmente, eliminar el pago deseleccionado
			WArrayF.AddOrRemove(newDetalle, pagosSeleccionados, control.checked);
		}
	}
	Style = css`
		w-pagos-view {
			display: grid;
			grid-template-columns: 70% 30%;
			gap: 30px;
			padding-top: 30px;
		}
		w-app-navigator {
			display: block;
			max-height: calc(100vh - 280px);
			overflow-y: auto;
			margin-bottom:30px;
		}    
		.pago-card {
			background-color: #FFFFFF;
			padding: 20px;
			border-radius: 5px;
			box-shadow: 0px 0px 5px #00000033;
			display: grid;
			grid-template-columns: calc(100% - 380px) 150px 150px;
			gap: 20px;
		}

		.pago-detail {
			display: flex;
			flex-direction: column;
			gap: 5px;
			flex: 1;
			font-size: 12px;
			justify-content: flex-start !important;
			border-radius: 10px;
			padding: 10px;
		}
		.TabContainer {
				display: flex;
				flex-direction: column;
				justify-content: flex-end;
				gap: 15px;
				transition: all 0.5s; 
				max-height: calc(100vh - 280px);
			
		}
		.PagosContainer {
			overflow-y: auto;
			padding: 10px;
			max-height: calc(100vh - 280px);
		}
		.mes-container {
			display: flex;
			flex-direction: column;
			gap: 10px;	
			margin-bottom: 15px;
		}
		
		.pago-title, .pago-monto {
			font-size: 16px;
			font-weight: bold;
		}
		.pago-monto, .check-pago { 
			cursor: pointer;
		}
		.check-pago {
			height: 20px;
			min-width: 20px;
			margin: 5px;
		}
		.TabContainer .pago-details-container {
			display: grid;
			grid-template-columns: calc(100% - 120px) 100px;
			gap: 20px;
			& div:nth-child(2) {
				text-align: right;
			}
		}
		.pago-subtitle {
			color: #000000;
		}
		.pago-options {
			display: flex;
			flex-direction: column;
			align-items: center;
			justify-content: center;
			gap: 5px;
		}
		@media (max-width: 600px) {
			w-pagos-view {
				grid-template-columns: 100%;
			
			}
			.PagosContainer {
				padding: 0px;
				max-height: unset;
			}
			.pago-container {
				padding: 5px;
			}
			.pago-card {
				padding: 10px;
				grid-template-columns: 48% 48%;
			}
			.pago-detail {
				grid-column: span 2;
				font-size: 11px;
			}
			.pago-title {
				font-size: 12px;
			}
			.pago-monto {
				font-size: 14px;
			}		
			
		}
		@media (max-width: 400px) {
			w-pagos-view {
				grid-template-columns: 100%;			
			}
			.PagosContainer {
				padding: 0px;
				max-height: unset;
			}
			.pago-container {
				padding: 4px;
			}
			.pago-card {
				padding: 8px;
				grid-template-columns: 48% 48%;
			}
			.pago-detail {
				grid-column: span 2;
				font-size: 9px;
			}
			.pago-title {
				font-size: 9px;
			}
			.pago-monto {
				font-size: 10px;
			}		
			
		}
	`
}
customElements.define('w-pagos-view', Pagos_PendientesView);
export { Pagos_PendientesView };


