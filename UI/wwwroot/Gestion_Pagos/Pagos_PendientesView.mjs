//@ts-check
import { Pagos } from "../Model/Pagos.js";
import { PagosRequest } from "../Model/PagosRequest.js";
import { StylesControlsV2 } from "../WDevCore/StyleModules/WStyleComponents.js";
import { WAppNavigator } from "../WDevCore/WComponents/WAppNavigator.js";
import { ModalMessege, ModalVericateAction } from "../WDevCore/WComponents/WForm.js";
import { WModalForm } from "../WDevCore/WComponents/WModalForm.js";
import { DateTime } from "../WDevCore/WModules/Types/DateTime.js";
import { WAjaxTools } from "../WDevCore/WModules/WAjaxTools.js";
import { WArrayF } from "../WDevCore/WModules/WArrayF.js";
import { ComponentsManager, ConvertToMoneyString, html, WRender } from "../WDevCore/WModules/WComponentsTools.js";
import { WOrtograficValidation } from "../WDevCore/WModules/WOrtograficValidation.js";
import { css } from "../WDevCore/WModules/WStyledRender.js";
class Pagos_PendientesView extends HTMLElement {
    constructor() {
        super();
        this.TabContainer = WRender.createElement({ type: 'div', props: { class: 'TabContainer', id: "TabContainer" } });
        //this.DOMManager = new ComponentsManager({ MainContainer: this.TabContainer, SPAManage: true });
        this.NotificationsNav = new WAppNavigator({
            NavStyle: "tab",
            Inicialize: true,
            Elements: this.NavElements()
        })
        this.label = html`<label class="pago-monto">Total: $ 0.00</label>`;
        this.TabContainer.append(this.label, this.PayButton());
        /**@type {Array<Pagos>} */
        this.pagosSeleccionados = [];
        this.pagosSeleccionadosProxy = new Proxy(this.pagosSeleccionados, {
            get: (target, property) => {
                return target[property];
            }, set: (target, property, value, receiver) => {
                target[property] = value;
                let total = 0;
                this.pagosSeleccionados.forEach(pago => {
                    total += pago.Monto;
                });
                this.label.innerHTML = `Total: ${WOrtograficValidation.es(this.pagosSeleccionados[0]?.Money)} ${ConvertToMoneyString(total)}`;
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
    }
    PayButton() {
        return html`<button class="btn btn-success" onclick="${(/** @type {any} */ ev) => this.Pay()}">Pagar</button>`
    }
    async Pay() {
        if (this.pagosSeleccionados.length == 0) {
            document.body.appendChild(ModalMessege("Seleccione las mensualidades a pagar"));
            return;
        }
        const response = await new PagosRequest({ Pagos: this.pagosSeleccionados }).Save();
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
                    </div>`;
                    /**@type {Array<Pagos>} */
                    const pagos = await new Pagos().Get();
                    pagos.forEach(pago => {
                        const card = PagosCard(pago, this.pagosSeleccionadosProxy);
                        div.append(card)
                    });
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
    Style = css`
        w-app-navigator {
            display: block;
            max-height: calc(100vh - 280px);
            overflow-y: auto;
            margin-bottom:30px;
        }    
        .TabContainer {
            display: flex;
            justify-content: flex-end;
            align-items: center;
            gap: 10px;
        }
        @media (max-width: 600px) {
           
        }
    `
}
customElements.define('w-pagos-view', Pagos_PendientesView);
export { Pagos_PendientesView };

/**
 * @param {Pagos} pago
 * @param {any[]} pagosSeleccionados
 */
function PagosCard(pago, pagosSeleccionados) {
    return html`<div class="pago-card">
        <div class="pago-detail">
            <div class="pago-title">${pago.Estudiante.Nombre_completo}</div>
            <div class="pago-subtitle">Fecha de pago: ${new DateTime(pago.Fecha).toDateFormatEs()}</div>
            <div class="pago-subtitle">Fecha de limite: ${new DateTime(pago.Fecha_Limite).toDateFormatEs()}</div>
        </div>   
        <div class="pago-options">
            <label for="pago${pago.Id_Pago}" class="pago-monto">${WOrtograficValidation.es(pago.Money)} ${ConvertToMoneyString(pago.Monto)}</label>
            <input type="checkbox" class="check-pago Btn" id="pago${pago.Id_Pago}" onchange="${(ev) => {
            const control = ev.target;
            //const pagoBuscado = WArrayF.FindInArray(pago, pagosSeleccionados);
            WArrayF.AddOrRemove(pago, pagosSeleccionados, control.checked);
            //console.log(pagosSeleccionados);
        }}" />                                
        </div>
        <style>
            .pago-card {
                background-color: #FFFFFF;
                padding: 20px;
                border-radius: 5px;
                box-shadow: 0px 0px 5px #00000033;
                display: grid;
                grid-template-columns: calc(100% - 170px) 150px;
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
            .pago-subtitle {
                font-size: 12px;
                color: #000000;
            }
            .pago-options {
                display: flex;
                flex-direction: column;
                align-items: center;
                justify-content: center;
                gap: 5px;
            }
        </style>
    </div>`;
}

export { PagosCard };

