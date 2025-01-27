//@ts-check

import { Detalle_Pago } from "../Model/Detalle_Pago.js";
import { PagosRequest_ModelComponent } from "../Model/ModelComponent/PagosRequest_ModelComponent.js";
import { Tbl_Pagos_ModelComponent } from "../Model/ModelComponent/Tbl_Pagos_ModelComponent.js";
import { PagosRequest } from "../Model/PagosRequest.js";
import { Tbl_Pago } from "../Model/Tbl_Pago.js";
import { StylesControlsV2, StylesControlsV3, StyleScrolls } from "../WDevCore/StyleModules/WStyleComponents.js";
import { WFilterOptions } from "../WDevCore/WComponents/WFilterControls.js";
import { WPrintExportToolBar } from "../WDevCore/WComponents/WPrintExportToolBar.mjs";
import { PageType } from "../WDevCore/WComponents/WReportComponent.js";
import { WTableComponent } from "../WDevCore/WComponents/WTableComponent.js";
import { FilterData } from "../WDevCore/WModules/CommonModel.js";
import { DateTime } from "../WDevCore/WModules/Types/DateTime.js";
import { WArrayF } from "../WDevCore/WModules/WArrayF.js";
import { ComponentsManager, ConvertToMoneyString, html, WRender } from "../WDevCore/WModules/WComponentsTools.js";
import { WOrtograficValidation } from "../WDevCore/WModules/WOrtograficValidation.js";
import { css } from "../WDevCore/WModules/WStyledRender.js";

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
        this.append(
            StylesControlsV2.cloneNode(true),
            StyleScrolls.cloneNode(true),
            StylesControlsV3.cloneNode(true),
            //this.OptionContainer,
            //this.TabContainer
        );
        this.Draw();
    }
    Draw = async () => {
        this.MainComponent();
    }

    async SetOption() {

    }
    async MainComponent() {
        const container = WRender.Create({ className: "component" });
        this.Filter = new WFilterOptions({
            AutoSetDate: true,
            AutoFilter: true,
            ModelObject: new Tbl_Pagos_ModelComponent(),
            UseEntityMethods: true,
            Display: true,
            Dataset: [],
            FilterFunction: async (filterData) => {
                container.innerHTML = "";
                //console.log(this.Filter.ModelObject);
                const facturas = await new PagosRequest().Where(
                    this.Filter.ModelObject.FilterData[0]
                )
                //console.log(facturas);
                container.appendChild(this.DrawInformePagos(filterData, facturas));
            }
        });
        this.append(new WPrintExportToolBar({
            ExportPdfAction: (/**@type {WPrintExportToolBar} */ tool)=> {
                const body = container.cloneNode(true);
                body.appendChild(this.CustomStyle.cloneNode(true));
                tool.ExportPdf(body, PageType.A4)
            }
        }), this.Filter, container);
        //return container;
    }

    /**
     * @param {Array<Tbl_Pago>} pagos 
     * @param {Array<PagosRequest>} facturas 
     * @returns {HTMLElement}
     */
    DrawInformePagos(pagos, facturas) {
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

        // @ts-ignore                    
        const pagosEstudiante = Object.groupBy(pagos, p => p.Estudiante_Id);
        // @ts-ignore                    
        const facturasEstudiante = Object.groupBy(facturas.flatMap(p => p.Detalle_Pago), p => p.Pago.Estudiante_Id);
        //console.log(facturasEstudiante);

        for (const estudianteId in pagosEstudiante) {
            /**@type {Tbl_Pago} */
            const pago = pagosEstudiante[estudianteId][0];
            const estudianteContainer = html`<div class="estudiante-container">
                <h3>${pago.Estudiante?.Nombre_completo}</h3>
            </div>`;

            //estudianteContainer.append(html`<h3>Cargo</h3>`);

            //pagosEstudiante[estudianteId].forEach((/** @type {Tbl_Pago} */ pago) => {
            const  pagoMes = this.BuildPagosMes(pagosEstudiante[estudianteId], facturasEstudiante[estudianteId]);
            estudianteContainer.append(pagoMes);
            //});


            div.append(estudianteContainer);
        }
        //const pagoMes = this.BuildPagosMes(pagos);
        return div;
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
        const facturaGroup = Object.groupBy(facturasEstudiante, p => p.Pago.Mes);

        let totalCargos = 0;

        for (const pagosMes in pagosGroup) {
            const mesContainer = html`<div class="mes-container">
                <h3>${WArrayF.Capitalize(DateTime.Meses[// @ts-ignore
                pagosMes - 1])}
                </h3> 
                <div class="pago-details-container">
                    <div class="pago-title">Fecha</div>
                    <div class="pago-title">Tipo</div>
                    <div class="pago-title">Documento</div>
                    <div class="pago-title">Concepto</div>
                    <div class="pago-title">MD</div>
                    <div class="pago-title">Importe</div>
                    <div class="pago-title">Estado</div>
                </div>
            </div>`;
            //-------------------->
            mesContainer.append(html`<h3>Cargo</h3>`);
            pagosGroup[pagosMes].forEach((/** @type {Tbl_Pago} */ pago) => {
                const card = this.PagosCard(pago);
                mesContainer.append(card);
            });
            const subTotalCargos = pagosGroup[pagosMes].reduce((acc, pago) => acc + pago.Monto, 0);
            mesContainer.append(html`<div class="pago-details-container">
                <div class="pago-title" style="grid-column: span 5"></div>
                <div class="pago-title value">${subTotalCargos}</div>
                <div class="pago-title"></div>
            </div>`)
            //-------------------->
            mesContainer.append(html`<h3>Abono</h3>`);
            facturaGroup[pagosMes]?.forEach((/** @type {Detalle_Pago} */ pago) => {
                const card = this.PagosRequestCard(pago);
                mesContainer.append(card);
            });
            const subTotalAbonos = facturaGroup[pagosMes]?.reduce((acc, pago) => acc + pago.Monto, 0);
            mesContainer.append(html`<div class="pago-details-container">
                <div class="pago-title" style="grid-column: span 5"> </div>
                <div class="pago-title value">${subTotalAbonos}</div>
                <div class="pago-title"></div>
            </div>`);
            const total = subTotalCargos - subTotalAbonos;
            mesContainer.append(html`<div class="pago-details-container">
                <div class="pago-title" style="grid-column: span 5">Sub-Total mensual</div>
                <div class="pago-title value">${total}</div>
                <div class="pago-title"></div>
            </div>`);

            div.append(mesContainer);
            
            totalCargos += total;

        }
        
        div.append(html`<div class="mes-container total-container">
            <div class="pago-title" style="grid-column: span 5">Total</div>
            <div class="pago-title total-cargos">${totalCargos}</div>
            <div class="pago-title"></div>
        </div>`);
        return div;
    }
    /**
     * @param {Tbl_Pago} pago
     */
    PagosCard(pago) {
        return html`<div class="pago-details-container">
            <div class="pago-value">${new DateTime(pago.Fecha).toISO()}</div>
            <div class="pago-value">${pago.Tipo}</div>
            <div class="pago-value">${pago.Documento}</div>
            <div class="pago-value">${pago.Concepto}</div>
            <div class="pago-value">${pago.Money}</div>
            <div class="pago-value value">${pago.Monto}</div>  
            <div class="pago-value ${pago.Monto_Pendiente == 0 ? "CANCELADO" : "PENDIENTE"}">
                ${pago.Monto_Pendiente == 0 ? "CANCELADO" : "PENDIENTE"}
            </div>                   
        </div>`;
    }
    /**
     * @param {Detalle_Pago} pago
     */
    PagosRequestCard(pago) {
        return html`<div class="pago-details-container">
            <div class="pago-value">${new DateTime(pago.Pago.Fecha).toISO()}</div>
            <div class="pago-value">${pago.Pago.Tipo}</div>
            <div class="pago-value">${pago.Pago.Documento}</div>
            <div class="pago-value">${pago.Pago.Concepto}</div>
            <div class="pago-value">${pago.Pago.Money}</div>
            <div class="pago-value value">${pago.Monto}</div>
            <div class="pago-value CANCELADO">
                EFECTUADO
            </div>
        </div>`;
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
            display: grid;
            grid-template-columns: repeat(7, 1fr);
            gap: 5px;
            & h3 {
                grid-column: span 7;
                font-size: 1em;
                border-bottom: 1px solid #919191;
            }
            & .pago-details-container {
                display: contents;
                grid-column: span 7;
            }
            .pago-title {
                font-size: 0.8em;
                padding: 5px;
                font-weight: bold;
                background-color: #f1f1f1;
            }
            .pago-value {
                font-size: 0.8em;
            }
        }   
        .total-container {
            border-top: 1px solid #919191;
        } 
        .value, .total-cargos {
            text-align: right;
        }  
    `
}
customElements.define('w-component', Historial_PagosView);
export { Historial_PagosView }