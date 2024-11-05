//@ts-check
import { StylesControlsV2, StylesControlsV3, StyleScrolls } from "../WDevCore/StyleModules/WStyleComponents.js"
import { css } from "../WDevCore/WModules/WStyledRender.js";
import { WAppNavigator } from "../WDevCore/WComponents/WAppNavigator.js";
import { WTableComponent } from "../WDevCore/WComponents/WTableComponent.js";
import { html } from "../WDevCore/WModules/WComponentsTools.js";
import { Parientes_ModelComponent } from "./Model/Parientes_ModelComponent.js";
import { Parientes } from "./Model/Parientes.js";
import { UpdateData } from "./Model/UpdateData.js";
import { ModalMessege } from "../WDevCore/WComponents/WForm.js";
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
            name: "Tutores invitados", action: () => {
                const modelEntity = new Parientes({ Get: () => modelEntity.GetParientesInvitados() })
                this.ParientesTable = new WTableComponent({
                    ModelObject: new Parientes_ModelComponent(),
                    EntityModel: modelEntity,
                    Options: {
                        Filter: true,
                        //FilterDisplay: true,
                        MultiSelect: true
                    }
                });
                return html`<div class="w-table-container">                    
                    <div class="OptionsContainer">
                        <button class="BtnPrimary" onclick="${(/** @type {any} */ ev) => this.SendNotificaciones(this.ParientesTable)}">
                            Reenviar invitación</button>
                    </div>
                    ${this.ParientesTable}
                </div>`
            }
        }, {
            name: "Tutores que ingresaron", action: () => {
                const modelEntity = new Parientes({ Get: () => modelEntity.GetParientesQueLoguearon() })
                this.ParientesTable = new WTableComponent({
                    ModelObject: new Parientes_ModelComponent(),
                    EntityModel: modelEntity,
                    Options: {
                        Filter: true,
                        MultiSelect: true
                    }
                });
                return html`<div class="w-table-container">
                    ${this.ParientesTable}
                </div>`
            }
        }, {
            name: "Tutores que actualizarón", action: () => {
                const modelEntity = new Parientes({ Get: () => modelEntity.GetParientesQueActulizaron() })
                this.ParientesTable = new WTableComponent({
                    ModelObject: new Parientes_ModelComponent(),
                    EntityModel: modelEntity,
                    Options: {
                        Filter: true,
                        MultiSelect: true,
                        UserActions: [{
                            name: "Ver detalles", action: async ( /** @type {Parientes} */ Pariente) => {
                                const response = await new Parientes({ Id: Pariente.Id }).GetUpdatedData();

                            }
                        }]
                    }
                });
                return html`<div class="w-table-container">                    
                    ${this.ParientesTable}
                </div>`
            }
        }, {
            name: "Envio de invitaciones", action: () => {
                this.ParientesTable = new WTableComponent({
                    ModelObject: new Parientes_ModelComponent(),
                    EntityModel: new Parientes(),
                    Options: {
                        Filter: true,
                        MultiSelect: true
                    }
                });
                return html`<div class="w-table-container">
                    <div class="OptionsContainer">
                        <button class="BtnPrimary" onclick="${(/** @type {any} */ ev) => this.SendNotificaciones(this.ParientesTable)}">Enviar</button>
                    </div>
                    ${this.ParientesTable}
                </div>`
            }
        },]
    }
    /**
     * Env a notificaciones a los parientes seleccionados en la tabla ParientesTable
     * @param {WTableComponent} [ParientesTable] La tabla de parientes a la que se le van a enviar notificaciones
     */
    async SendNotificaciones(ParientesTable) {
        if (ParientesTable?.selectedItems.length == 0) {
            this.append(ModalMessege("No hay parientes seleccionados", undefined, true));
            return;
        }
        const response = await new UpdateData({ Parientes: ParientesTable?.selectedItems }).Save();
        this.append(ModalMessege(response.message, undefined, true));
    }

    CustomStyle = css`
        .component{
           display: block;
        }           
    `
}
customElements.define('w-notif-mat-actualizacion', NotificacionMatriculaActualizacion);
export { NotificacionMatriculaActualizacion }