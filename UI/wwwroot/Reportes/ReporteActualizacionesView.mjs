// @ts-check

import { Periodo_lectivos } from "../Model/Periodo_lectivos.js";
import { StylesControlsV2, StylesControlsV3, StyleScrolls } from "../WDevCore/StyleModules/WStyleComponents.js";
import { WAlertMessage } from "../WDevCore/WComponents/WAlertMessage.js";
import { WFilterOptions } from "../WDevCore/WComponents/WFilterControls.js";
import { WPrintExportToolBar } from "../WDevCore/WComponents/WPrintExportToolBar.mjs";
import { PageType, WReportComponent } from "../WDevCore/WComponents/WReportComponent.js";
import { WTableComponent } from "../WDevCore/WComponents/WTableComponent.js";
import { DateTime } from "../WDevCore/WModules/Types/DateTime.js";
import { html } from "../WDevCore/WModules/WComponentsTools.js";
import { css } from "../WDevCore/WModules/WStyledRender.js";
import { EstudiantesUpdateReportModel, EstudiantesUpdateReportModel_Modelcomponent } from "./Model/EstudiantesUpdateReportModel.js";


/**
 * @typedef {Object} ComponentConfig
 * * @property {Object} [propierty]
 * * @property {boolean} [initialize]
 */
class ReporteActualizacionesView extends HTMLElement {
    /**
     * 
     * @param {ComponentConfig} props 
     */
    constructor(props) {
        super();
        this.props = props ?? {};
        this.append(this.CustomStyle);
        this.MainComponent = new WTableComponent({
            ModelObject: new EstudiantesUpdateReportModel_Modelcomponent(),
            EntityModel: new EstudiantesUpdateReportModel(),
            Options: {
                Filter: true,
                UserActions: [
                    {
                        name: "Reenviar boleta",
                        action: async ( /**@type {EstudiantesUpdateReportModel} */ estudainteActualizado) => {
                            this.ReenviarBoleta(estudainteActualizado)
                        }
                    }
                ]
            }
        })
        this.Draw();
    }
    /**
     * @param {EstudiantesUpdateReportModel} estudainteActualizado
     */
    async ReenviarBoleta(estudainteActualizado) {
        const response = await estudainteActualizado.ReenviarBoleta();
        WAlertMessage.ResponseMessage(response);
    }
    Draw = async () => {
        const periodosLectivos = await new Periodo_lectivos().Get();
        const model = new EstudiantesUpdateReportModel_Modelcomponent();
        model.Periodo_Lectivo_Update.Dataset = periodosLectivos.map(p => p.Nombre_corto);
        this.ReportWrapper = html`<div class="report-wrapper"></div>`
        this.Filter = new WFilterOptions({
            ModelObject: model,
            EntityModel: new EstudiantesUpdateReportModel(),
            UseManualControlForFiltering: true,
            UseEntityMethods: true,
            Display: true,
            // @ts-ignore
            FilterFunction: async (Dfilter) => {
                const encodedText = "Colegio Centro Am&#xE9;rica";
                const parser = new DOMParser();
                const decodedText = parser.parseFromString(encodedText, "text/html").documentElement.textContent;
                // @ts-ignore
                this.ReportWrapper.innerHTML = "";
                const reportHeader = html`<div class="report-header">
                        <style>
                            .report-header {
                                display: block;
                                align-items: center;
                                width: 100%;
                                vertical-align: top;
                            
                            }
                            .report-header div {
                                min-width: calc(100% - 250px);
                                width: 1100px;
                                display: inline-block;
                                vertical-align: top;
                            }
                            .repot-header-logo {
                                height: 100px; 
                                width: auto;
                                display: inline-block;
                                vertical-align: top;
                            }
                            .report-header h1, .report-header h2 {
                                font-size: 20px;
                                width: 100%;
                                margin: 0;
                                padding: 0px 0px 10px 0px;
                                text-align: center;                    
                            }
                            .report-header h2 {
                                font-size: 18px;
                            }
                        </style>
                        <img class="repot-header-logo" src="${location.origin}${localStorage.getItem("MEDIA_IMG_PATH")}${localStorage.getItem("LOGO_PRINCIPAL")}">
                        <div>
                            <h1>${decodedText}</h1> 
                            <h2>${`Informe de actualizaciones ${new Date().getFullYear()}`}</h2>                        
                        </div>      
                    </div>`;

                this.ReportWrapper?.append(
                    new WReportComponent({
                        title: `Estudiantes`,
                        ModelObject: model,
                        Dataset: Dfilter,
                        Header: reportHeader,
                        PageType: PageType.OFICIO_HORIZONTAL,
                        exportXls: true,
                        exportPdf: true,
                        exportPdfApi: true,
                        DocumentViewFirst: true,
                        exportXlsAction: (/**@type {WPrintExportToolBar} */ tool) => {
                            // @ts-ignore
                            tool.exportToXls(Dfilter, reportHeader,
                                "report" + new Date().getFullYear().toString(),
                                undefined,
                                model)
                        }
                    })
                )
            }
        });
        // <h2>Informe de actualizaciones ${new Date().getFullYear()}</h2>
        this.Body = html`<div class="">           
            ${this.Filter}
        <hr/>
        </div>`;
        this.append(
            StylesControlsV2.cloneNode(true),
            StyleScrolls.cloneNode(true),
            StylesControlsV3.cloneNode(true),
            this.Body,
            this.ReportWrapper
        );
        this.Filter.filterFunction();

    }

    /**
     * Env a notificaciones a los parientes seleccionados en la tabla ParientesTable
     * @param {WTableComponent} [ParientesTable] La tabla de parientes a la que se le van a enviar notificaciones
     */
    async SendNotificaciones(ParientesTable) {

        //const response = await new UpdateData({ Parientes: ParientesTable?.selectedItems }).Save();
        //this.append(ModalMessage(response.message, undefined, true));
    }

    CustomStyle = css`
        .component{
           display: block;
        }       
        .element-card {
            display: flex;
            flex-direction: column;
            margin: 5px;
            border: 1px solid #888888;
            border-radius: 0.2cm;
            overflow: hidden;
            padding: 10px;
        }
        .element-title {
            font-weight: bold;
            font-size: 16px;
            color: var(--font-secundary-color);
        }
        .element-data-container {
            display: grid;
            grid-template-columns: repeat(3, 1fr);
            gap: 10px;
        }
        .element-data {
            display: flex;
            flex-direction: column;
            font-weight: 500;
            font-size: 16px;
            & span {
                font-size: 12px;
            }
        }
        .tab .elementNavActive, .tab .elementNav {
            & span {
                padding-left: 10px;
                font-weight: bold;
            }
        }
    `
}
customElements.define('w-reporte-actualizaciones', ReporteActualizacionesView);
export { ReporteActualizacionesView };