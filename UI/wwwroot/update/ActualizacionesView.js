// @ts-check

import { Parientes_ModelComponent } from "../Model/ModelComponent/Parientes_ModelComponent.js";
import { Parientes } from "./Model/Parientes.js";
import { EstudiantesUpdateReportModel, EstudiantesUpdateReportModel_Modelcomponent } from "../Reportes/Model/EstudiantesUpdateReportModel.js";
import { StylesControlsV2, StylesControlsV3, StyleScrolls } from "../WDevCore/StyleModules/WStyleComponents.js";
import { WAlertMessage } from "../WDevCore/WComponents/WAlertMessage.js";
import { WAppNavigator } from "../WDevCore/WComponents/WAppNavigator.js";
import { WTableComponent } from "../WDevCore/WComponents/WTableComponent.js";
import { html } from "../WDevCore/WModules/WComponentsTools.js";
import { css } from "../WDevCore/WModules/WStyledRender.js";


/**
 * @typedef {Object} ComponentConfig
 * * @property {Object} [propierty]
 * * @property {boolean} [initialize]
 */
class ActualizacionesView extends HTMLElement {
    /**
     * @param {ComponentConfig} props 
     */
    constructor(props) {
        super();
        this.props = props ?? {};
        this.append(this.CustomStyle);
        /**@type {Object.<string, any>} */
        this.ParientesActulizacionData = {}
        this.Draw();
    }
    /**
     * @param {Parientes} parienteActualizado
     */
    async ReenviarBoleta(parienteActualizado) {
        const response = await parienteActualizado.ReenviarBoleta();
        WAlertMessage.ResponseMessage(response);
    }

    NavElements() {
        return [{
            name: `<div>Tutores que no actualizaron - <span>${this.ParientesActulizacionData?.NoActualizados}</span></div>`,
            action: () => {
                /**@type {Parientes} */
                const modelEntity = new Parientes({ Get: async () => modelEntity.GetParientesQueNoActulizaron() })
                return new WTableComponent({
                    ModelObject: new Parientes_ModelComponent(),
                    EntityModel: modelEntity,                   
                    Options: {
                        Filter: true,
                        UseManualControlForFiltering: true
                    }
                })
            }
        }, {
            name: `<div>Tutores que actualizaron - <span>${this.ParientesActulizacionData?.Actualizados}</span></div>`,
            action: () => {
                /**@type {Parientes} */
                const modelEntity = new Parientes({ Get: async () => modelEntity.GetParientesActualizados() })
                return new WTableComponent({
                    ModelObject: new Parientes_ModelComponent(),
                    EntityModel: modelEntity,
                    Options: {
                        Filter: true,
                        UseManualControlForFiltering: true,
                        UserActions: [
                            {
                                name: "Reenviar boleta",
                                action: async ( /**@type {Parientes} */ parienteActualizado) => {
                                    this.ReenviarBoleta(parienteActualizado)
                                }
                            }
                        ]
                    }
                })
            }
        }]
    }
    Draw = async () => {
        /**@type {Object.<string, any>} */
        this.ParientesActulizacionData = await new Parientes().GetParientesActulizacionData();
        this.NavManager = new WAppNavigator({
            NavStyle: "tab",
            Inicialize: true,
            Elements: this.NavElements()
        })
        this.Body = html`<div class="">
            <h2>Actualizaciones ${new Date().getFullYear()}</h2>                
            <hr/>
            ${this.NavManager}
        </div>`;
        this.append(
            StylesControlsV2.cloneNode(true),
            StyleScrolls.cloneNode(true),
            StylesControlsV3.cloneNode(true),
            this.Body
        );
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
customElements.define('w-reporte-recorridos', ActualizacionesView);
export { ActualizacionesView };



