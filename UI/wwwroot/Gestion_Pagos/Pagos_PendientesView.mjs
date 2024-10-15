//@ts-check
import { Detalle_Pago } from "../Model/Detalle_Pago.js";
import { PagosRequest } from "../Model/PagosRequest.js";
import { Tbl_Pago } from "../Model/Tbl_Pago.js";
import { StylesControlsV2 } from "../WDevCore/StyleModules/WStyleComponents.js";
import { WAppNavigator } from "../WDevCore/WComponents/WAppNavigator.js";
import { ModalMessege } from "../WDevCore/WComponents/WForm.js";
import { DateTime } from "../WDevCore/WModules/Types/DateTime.js";
import { WArrayF } from "../WDevCore/WModules/WArrayF.js";
import { ConvertToMoneyString, html, WRender } from "../WDevCore/WModules/WComponentsTools.js";
import { WOrtograficValidation } from "../WDevCore/WModules/WOrtograficValidation.js";
import { css } from "../WDevCore/WModules/WStyledRender.js";
class Pagos_PendientesView extends HTMLElement {
    constructor() {
        super();
        this.TabContainer = WRender.Create({
            class: 'TabContainer', style: {
                display: "flex",
                alignItems: "center",
                justifyContent: "flex-end"
            }, id: "TabContainer"
        });
        //this.DOMManager = new ComponentsManager({ MainContainer: this.TabContainer, SPAManage: true });
        this.NotificationsNav = new WAppNavigator({
            NavStyle: "tab",
            Inicialize: true,
            Elements: this.NavElements()
        })
        this.label = html`<label class="pago-monto">Total: $ 0.00</label>`;
        this.TabContainer.append(this.label, this.PayButton());
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
        //const ObjectProxy = new Proxy(FormObject, ObjHandler);

        this.append(
            this.Style,
            StylesControlsV2.cloneNode(true),
            this.NotificationsNav,
            this.TabContainer
        );
        this.Controls = [];
    }
    UpdatePagos() {
        let total = 0;
        console.log(this.pagosSeleccionados);

        this.pagosSeleccionados.forEach(detalle => {
            total += detalle.Total ?? 0;
        });
        this.label.innerHTML = `Total: ${WOrtograficValidation.es(this.pagosSeleccionados[0]?.Pago?.Money)} ${ConvertToMoneyString(total)}`;
    }

    PayButton() {
        return html`<button class="btn btn-success" onclick="${(/** @type {any} */ ev) => this.Pay()}">Pagar</button>`
    }
    async Pay() {
        if (this.pagosSeleccionados.length == 0) {
            document.body.appendChild(ModalMessege("Seleccione las mensualidades a pagar"));
            return;
        }
        const response = await new PagosRequest({ Detalle_Pago: this.pagosSeleccionados }).Save();
        if (response.status == 200) {
            window.location.href = "/Gestion_Pagos/Tpv";
            return;
        }
    }
    NavElements() {
        return [
            {
                name: "Pagos pendientes", url: "#",
                action: async (ev) => {
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
                    const pagosGroup = Object.groupBy(pagos, p => p.Mes);

                    for (const pagosMes in pagosGroup) {
                        const mesContainer = html`<div class="mes-container">
                            <h3>${pagosMes}</h3>
                        </div>`;
                        pagosGroup[pagosMes].forEach(pago => {
                            const card = this.PagosCard(pago, this.pagosSeleccionadosProxy, pagos);
                            this.Controls.push(card);
                            mesContainer.append(card);
                        });
                        div.append(mesContainer);
                    }
                    return div;
                }
            }, {
                name: "Pagos realizados", url: "#",
                action: async (ev) => {
                    window.location.href = "/Gestion_Pagos/Historial_Pagos";
                }
            }
        ];
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
            Detalle_Pago.ActualizarDetalle(newDetalle, parseFloat(ev.target.value))
            this.UpdatePagos();
        }}" />`
        return html`<div class="pago-card" id="pago${pago.Id_Pago}">
        <div class="pago-detail">
            <div class="pago-title">${pago.Estudiante?.Nombre_completo}</div>
            <div class="pago-subtitle">${pago.Concepto}</div>
            <div class="pago-details-container">
                <div class="pago-subtitle">Documento: ${pago.Documento}</div>
                <div class="pago-subtitle">Fecha de pago: ${new DateTime(pago.Fecha).toDateFormatEs()}</div>
                <div class="pago-subtitle">Fecha de limite: ${new DateTime(pago.Fecha_Limite).toDateFormatEs()}</div>
            </div>
           
        </div>  
        <div class="pago-options pago-parcial-check">
            <label for="pago-parcial${pago.Id_Pago}">Pago parcial</label>
            <input type="checkbox" class="check-pago" id="pago-parcial${pago.Id_Pago}"  onchange="${(ev) => {
                pagoInput.style.display = ev.target.checked ? "block" : "none";
                Detalle_Pago.ActualizarDetalle(newDetalle, pago.Monto_Pendiente)
                this.UpdatePagos();
            }}"/>
        </div>  
        <div class="pago-options">
            <label for="pago${pago.Id_Pago}" class="pago-monto">${WOrtograficValidation.es(pago.Money)} ${ConvertToMoneyString(pago.Monto_Pendiente)}</label>
             ${pagoInput}       
            <input type="checkbox" class="check-pago Btn pago-control${pago.Id_Pago}" id="pago${pago.Id_Pago}" onchange="${(ev) => this.AgregarPago(ev, pago, pagosSeleccionados, pagosPendientes, newDetalle)}" />                                
        </div>       
    </div>`;
    }

    /**
     * Agrega o elimina un pago de la lista de pagos seleccionados
     * @param {Event} ev - Evento del checkbox
     * @param {Tbl_Pago} pago - Pago seleccionado o deseleccionado
     * @param {Detalle_Pago[]} pagosSeleccionados - Pagos ya seleccionados
     * @param {Tbl_Pago[]} pagosPendientes - Pagos pendientes de pago
     */
    AgregarPago(ev, pago, pagosSeleccionados, pagosPendientes, newDetalle) {
        /**@type {HTMLInputElement} */
        // @ts-ignore
        const control = ev.target;
        const fechaPagoSeleccionado = new Date(pago.Fecha);

        // Comprobar si hay pagos anteriores no seleccionados (si se está seleccionando el pago)
        if (control.checked) {
            let pagosAnterioresNoSeleccionados = false;
            const pagosAnteriores = pagosPendientes.filter(p => new Date(p.Fecha) < fechaPagoSeleccionado);
            pagosAnteriores.forEach(pagoSeleccionado => {
                if (!pagosSeleccionados.some(p => p.Pago?.Id_Pago === pagoSeleccionado?.Id_Pago)) {
                    pagosAnterioresNoSeleccionados = true;
                }
            });
            if (pagosAnterioresNoSeleccionados) {
                document.body.appendChild(ModalMessege('Debe seleccionar los pagos anteriores antes de agregar este.'));
                control.checked = false;  // Desmarcar el checkbox
                return;
            }
            // Si no hay pagos anteriores no seleccionados, agregar el pago
            WArrayF.AddOrRemove(newDetalle, pagosSeleccionados, control.checked);

        } else {
            // Si se está deseleccionando el pago, eliminar los pagos con fecha posterior
            // Filtrar todos los pagos seleccionados que tengan una fecha mayor a la del pago deseleccionado
            // @ts-ignore
            const pagosASerRemovidos = pagosSeleccionados.filter(p => new Date(p.Pago.Fecha) > fechaPagoSeleccionado);
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
        }
        .pago-title, .pago-monto {
            font-size: 20px;
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
        .pago-details-container {
            display: flex;
            gap: 20px;
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
           
        }
    `
}
customElements.define('w-pagos-view', Pagos_PendientesView);
export { Pagos_PendientesView };


