//@ts-check
import { Notificaciones_ModelComponent } from "../Model/ModelComponent/Notificacion_ModelComponent.js";
import { StylesControlsV2 } from "../WDevCore/StyleModules/WStyleComponents.js";
import { WAppNavigator } from "../WDevCore/WComponents/WAppNavigator.js";
import { ModalVericateAction } from "../WDevCore/WComponents/WForm.js";
import { WAjaxTools } from "../WDevCore/WModules/WAjaxTools.js";
import { ComponentsManager, WRender } from "../WDevCore/WModules/WComponentsTools.js";
import { css } from "../WDevCore/WModules/WStyledRender.js";
class NotificacionesView extends HTMLElement {
    constructor() {
        super();
        this.attachShadow({ mode: 'open' });
        this.TabContainer = WRender.createElement({ type: 'div', props: { class: 'TabContainer', id: "TabContainer" } });
        this.DOMManager = new ComponentsManager({ MainContainer: this.TabContainer });
        this.shadowRoot?.append(
            this.Style,
            StylesControlsV2.cloneNode(true),
            this.NotificationsNav,
            this.TabContainer
        );
        this.DrawNotificacionesView();
    }
    connectedCallback() { }
    DrawNotificacionesView = async () => { }
    NotificationsNav = new WAppNavigator({
        NavStyle: "tab",
        Inicialize: true,
        Elements: [ {
                name: "PARTICIPACIÓN EN EVENTOS", url: "#",
                action: async (ev) => {
                    const Events = await new Notificaciones_ModelComponent().Get();
                    const Notifications = Events.filter(E => E.Leido == true).map(E => {
                        return {
                            TextAction: E.Estado,
                            Object: E,
                            Titulo: `Notificación de ${E.Tipo}`,
                            Descripcion: E.Mensaje,
                            Fecha: E.Fecha,
                            Actions: [
                                /*{
                                    TextAction: "Aceptar",  class: "btn", Action: async (Event) => {
                                        this.append(ModalVericateAction(async () => {
                                            const Events = await WAjaxTools.PostRequest("../api/Events/AprobarParticipacion", Event);
                                        }))                                       
                                    }
                                }, {
                                    TextAction: "Rechazar",  class: "btn-alert", Action: async (Event) => {
                                        this.append(ModalVericateAction(async () => {
                                            const Events = await WAjaxTools.PostRequest("../api/Events/RechazarParticipacion", Event);
                                        }))    
                                    }
                                }*/
                            ]
                        }
                    });
                    this.DOMManager.NavigateFunction("Tab-eventos", this.DrawNotificaciones(Notifications));
                }
            }
        ]
    })
    DrawNotificaciones = (Dataset = []) => {
        return WRender.Create({
            className: "GroupDiv", children: Dataset.map(n => {
                return this.CreateNotificacion(n)
            })
        })
    }
    CreateNotificacion = (Notificacion) => {
        const NotificacionContainer = WRender.Create({
            className: "NotificationContainer", children: [
                { tagName: 'label', className: "titulo", innerText: Notificacion.Titulo },
                { tagName: 'label',className: "fecha", innerText: Notificacion.Fecha },
                { tagName: 'p', innerText: Notificacion.Descripcion },
                Notificacion.Actions.map(a => ({
                    tagName: 'input', type: 'button', className: a.class, value: a.TextAction, onclick: async () => {
                        await a.Action(Notificacion.Object)
                    }
                }))
            ]
        })
        return NotificacionContainer;
    }
    Style = css`
        .NotificationContainer{
            padding: 20px;
            border-radius: 0.3cm;
            display: flex;
            flex-direction: column;
            box-shadow: 0 0 3px 0 rgba(0,0,0,0.2);
            background-color: #fff;
            margin: 10px; 
            color: #444;   
        }        
        .titulo{
            font-size: 20px;
            font-weight: 500;
            color: #335888;   
        }
        .fecha{
            padding: 10px 0px;
            font-size: 12px;
            border-bottom: 1px solid #9999;

        }
        .NotificationContainer p{
            padding: 10px 0px;
            margin: 0px;
            border-bottom: 1px solid #9999;
            font-size: 14px;
        } 
        .NotificationContainer div{
            display: flex;
            justify-content: flex-end;
            margin-top: 10px;
        } 
    `
    GetDescription(E) {
        return `${E.Tbl_Evento?.Tbl_InvestigatorProfile?.Nombres} ${E.Tbl_Evento?.Tbl_InvestigatorProfile?.Apellidos} Indico que participarias en el evento ${E.Tbl_Evento?.Nombre} que se realizara de forma ${E.Tbl_Evento?.Modalidad}, con el rol de ${E.Cat_Tipo_Participacion_Eventos?.Descripcion} de ${E.Titulo} el ${E.Fecha_Participacion?.toDateFormatEs()}.
        
        ¿Desea confirmar su participación?`;
    }
    
}
customElements.define('w-notificaciones-view', NotificacionesView);
export { NotificacionesView }