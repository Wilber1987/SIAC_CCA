//@ts-check

import { Clases } from "../Model/Clases.js";
import { Clases_ModelComponent } from "../Model/ModelComponent/Clases_ModelComponent.js";
import { NotificationRequest_ModelComponent, NotificationTypeEnum } from "../Model/ModelComponent/NotificacionRequest_ModelComponent.js";
import { Parientes_ModelComponent } from "../Model/ModelComponent/Parientes_ModelComponent.js";
import { Responsables_ModelComponent } from "../Model/ModelComponent/Responsables_ModelComponent.js";
import { Secciones_ModelComponent } from "../Model/ModelComponent/Secciones_ModelComponent.js";
import { Niveles } from "../Model/Niveles.js";
import { NotificationRequest } from "../Model/NotificationRequest.js";
import { Parientes } from "../Model/Parientes.js";
import { Periodo_lectivos } from "../Model/Periodo_lectivos.js";
import { Responsables } from "../Model/Responsables.js";
import { Secciones } from "../Model/Secciones.js";
import { StylesControlsV2, StylesControlsV3, StyleScrolls } from "../WDevCore/StyleModules/WStyleComponents.js";
import { ModalMessage } from "../WDevCore/WComponents/ModalMessage.js";
import { ModalVericateAction } from "../WDevCore/WComponents/ModalVericateAction.js";
import { WAppNavigator } from "../WDevCore/WComponents/WAppNavigator.js";
import { WModalForm } from "../WDevCore/WComponents/WModalForm.js";
import { WTableComponent } from "../WDevCore/WComponents/WTableComponent.js";
import { WRender } from "../WDevCore/WModules/WComponentsTools.js";
import { css } from "../WDevCore/WModules/WStyledRender.js";


/**
 * @typedef {Object} NotificacionesManagerViewConfig
 * * @property {Object} [propierty]
 */
class NotificacionesManagerView extends HTMLElement {
    /**
     * @param {NotificacionesManagerViewConfig} props 
     */
    constructor(props) {
        super();
        this.OptionContainer = WRender.Create({ className: "OptionContainer" });
        //this.Manager = new ComponentsManager({ MainContainer: this.TabContainer, SPAManage: false });
        this.append(this.CustomStyle);
        this.append(
            StylesControlsV2.cloneNode(true),
            StyleScrolls.cloneNode(true),
            StylesControlsV3.cloneNode(true),
            this.OptionContainer
        );
        this.Draw();
        this.NotificationType = NotificationTypeEnum.CLASE
    }
    Draw = async () => {
        this.SetOption();
    }


    async SetOption() {
        this.OptionContainer.append(WRender.Create({
            tagName: 'button', className: 'Block-Primary', innerText: 'NUEVA NOTIFICACIÓN',
            onclick: async () => this.ProcessRequest()
        }))
        this.Navigator = new WAppNavigator({
            DarkMode: false,
            //Direction: "row",
            NavStyle: "tab",
            Inicialize: true,
            Elements: [
                {
                    name: "Clase", action: async () => {
                        return await this.ClasesComponent();
                    }
                }, {
                    name: "Secciones", action: () => {
                        return this.SeccionesComponent();
                    }
                }, {
                    name: "Responsables", action: () => {
                        return this.ResponsablesComponent();
                    }
                }
            ]
        });
        this.Navigator.className = "TabContainer"
        this.append(this.Navigator);
    }
    async ClasesComponent() {
        const model = new Clases_ModelComponent();
        //model.Periodo_lectivos.Dataset = await new Periodo_lectivos().Get();
        model.Niveles.Dataset = await new Niveles().Get();
        this.NotificationType = NotificationTypeEnum.CLASE;
        if (!this.ClaseComponent) {
            this.ClaseComponent = new WTableComponent({
                ModelObject: model,
                EntityModel: new Clases(),
                AutoSave: true,
                Options: {
                    Filter: true,
                    FilterDisplay: true,
                    AutoSetDate: false,
                    MultiSelect: true
                }
            })
        }
        return this.ClaseComponent;
    }
    SeccionesComponent() {
        this.NotificationType = NotificationTypeEnum.SECCION;
        if (!this.SeccionComponent) {
            this.SeccionComponent = new WTableComponent({
                ModelObject: new Secciones_ModelComponent(),
                EntityModel: new Secciones(),
                AutoSave: true,
                Options: {
                    Filter: true,
                    FilterDisplay: true,
                    AutoSetDate: false,
                    MultiSelect: true
                }
            })
        }
        return this.SeccionComponent;
    }
    ResponsablesComponent() {
        this.NotificationType = NotificationTypeEnum.RESPONSABLE;
        if (!this.ResponsableComponent) {
            this.ResponsableComponent = new WTableComponent({
                ModelObject: new Responsables_ModelComponent(),
                EntityModel: new Responsables(),
                AutoSave: true,
                Options: {
                    Filter: true,
                    FilterDisplay: true,
                    AutoSetDate: false,
                    MultiSelect: true
                }
            })
        }
        return this.ResponsableComponent;
    }

    CustomStyle = css`
        .TabContainer{
           margin-top: 20px;
        }           
    `
    ProcessRequest = () => {
        const modal = new WModalForm({
            ModelObject: new NotificationRequest_ModelComponent(),
            title: "NUEVA NOTIFICACIÓN",
            StyleForm: "columnX1",
            ObjectOptions: {
                SaveFunction: async (/**@type {NotificationRequest} */ entity) => {
                    let mensaje = ""
                    console.log(this.NotificationType);

                    if (this.NotificationType == NotificationTypeEnum.SECCION) {
                        entity.NotificationType = NotificationTypeEnum.SECCION;
                        // @ts-ignore
                        entity.Secciones = this.SeccionComponent?.selectedItems.map(s => s.Id);
                        // @ts-ignore
                        entity.Clases = this.SeccionComponent?.selectedItems.map(s => s.Clases?.Id);
                        mensaje = entity.Secciones.length == 0 ? "a todas las secciones" : "a las secciones selecionadas";


                    } else if (this.NotificationType == NotificationTypeEnum.CLASE) {
                        entity.NotificationType = NotificationTypeEnum.CLASE;
                        // @ts-ignore
                        entity.Clases = this.ClaseComponent?.selectedItems.map(s => s.Id);
                        mensaje = entity.Clases.length == 0 ? "a todas las clases" : "a las clases selecionadas";

                    } else if (this.NotificationType == NotificationTypeEnum.RESPONSABLE) {
                        entity.NotificationType = NotificationTypeEnum.RESPONSABLE;
                        // @ts-ignore
                        entity.Responsables = this.ResponsableComponent?.selectedItems.map(s => s.User_id);
                        mensaje = entity.Responsables.length == 0 ? "a todos los responsables" : "a los responsables selecionados";

                    }
                    document.body.appendChild(ModalVericateAction(async () => {
                        const response = await new NotificationRequest(entity).Save();
                        document.body.appendChild(ModalMessage(response.message, undefined, true));
                        modal.close();
                    }, `¿Desea enviar la notificación ${mensaje}?`));
                }
            }
        });
        document.body.append(modal);
    }
}
customElements.define('w-notificaciones-manager-view', NotificacionesManagerView);
export { NotificacionesManagerView }