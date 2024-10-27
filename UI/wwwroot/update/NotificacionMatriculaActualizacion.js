//@ts-check
import { StylesControlsV2, StylesControlsV3, StyleScrolls } from "../WDevCore/StyleModules/WStyleComponents.js"
import { css } from "../WDevCore/WModules/WStyledRender.js";
import { WAppNavigator } from "../WDevCore/WComponents/WAppNavigator.js";
import { WTableComponent } from "../WDevCore/WComponents/WTableComponent.js";
import { Parientes_ModelComponent } from "../Model/ModelComponent/Parientes_ModelComponent.js";
import { Parientes } from "../Model/Parientes.js";
import { html } from "../WDevCore/WModules/WComponentsTools.js";
/**
 * @typedef {Object} ComponentConfig
 * * @property {Object} [propierty]
 */
class NotificacionMatriculaActualizacion extends HTMLElement {
    /**
     * 
     * @param {ComponentConfig} props 
     */
    constructor(props) {
        super();
        this.props = props;
        this.append(this.CustomStyle);
        this.NavManager = new WAppNavigator({
            NavStyle: "tab",
            Inicialize: true,
            Elements: this.NavElements()
        })
        this.append(
            StylesControlsV2.cloneNode(true),
            StyleScrolls.cloneNode(true),
            StylesControlsV3.cloneNode(true),
            this.NavManager,
        );
        this.Draw();
    }
    Draw = async () => {

    }


    NavElements() {
        return [{
            name: "Envio de notificaciones", action: () => {
                this.ParientesTable = new WTableComponent({
                    ModelObject: new Parientes_ModelComponent(),
                    EntityModel: new Parientes(),                    
                    Options: {
                        Filter: true,
                        FilterDisplay: true,
                        MultiSelect: true
                    }
                });
                return html`<div class="w-table-container">
                    <h3>Parientes</h3>
                    <div class="OptionsContainer">
                        <button class="BtnPrimary" onclick="${(/** @type {any} */ ev) => this.SendNotificaciones(this.ParientesTable)}">Enviar</button>
                    </div>
                    ${this.ParientesTable}
                </div>`
            }
        }, {
            name: "Actualizaciones de parientes", action: () => {
                return "";
            }
        }, {
            name: "Actualizaciones de estudiantes", action: () => {
                return "";
            }
        }]
    }
    /**
     * Env a notificaciones a los parientes seleccionados en la tabla ParientesTable
     * @param {WTableComponent} [ParientesTable] La tabla de parientes a la que se le van a enviar notificaciones
     */
    SendNotificaciones(ParientesTable) {
        throw new Error("Method not implemented.");
    }

    CustomStyle = css`
        .component{
           display: block;
        }           
    `
}
customElements.define('w-notif-mat-actualizacion', NotificacionMatriculaActualizacion);
export { NotificacionMatriculaActualizacion }