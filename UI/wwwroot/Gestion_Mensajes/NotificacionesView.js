//@ts-check
import { Notificaciones_ModelComponent } from "../Model/ModelComponent/Notificacion_ModelComponent.js";
import { Notificaciones } from "../Model/Notificaciones.js";
import { StylesControlsV2 } from "../WDevCore/StyleModules/WStyleComponents.js";
import { WAppNavigator } from "../WDevCore/WComponents/WAppNavigator.js";
import { WModalForm } from "../WDevCore/WComponents/WModalForm.js";
import { WAjaxTools } from "../WDevCore/WModules/WAjaxTools.js";
import { ComponentsManager, html, WRender } from "../WDevCore/WModules/WComponentsTools.js";
import { css } from "../WDevCore/WModules/WStyledRender.js";
class NotificacionesView extends HTMLElement {
    constructor() {
        super();
        this.TabContainer = WRender.createElement({ type: 'div', props: { class: 'TabContainer notifContainer', id: "TabContainer" } });
        this.DOMManager = new ComponentsManager({ MainContainer: this.TabContainer, SPAManage: true });
        
        this.append(
            this.Style,
            StylesControlsV2.cloneNode(true),
            this.View(),
            this.TabContainer
        );
    }
    View() {        
        return new NotificacionesElements({ DOMManager: this.DOMManager });
    }

    connectedCallback() { }
    Style = css`
        w-notificaciones-view{
            display: grid;
            grid-template-columns: 500px calc(100% - 520px);
            gap: 20px;            
        }
        w-app-navigator {
            display: block;
            max-height: calc(100vh - 220px);
            overflow-y: auto;
        }
        
        @media (max-width: 600px) {
            w-notificaciones-view{
                display: grid;
                grid-template-columns: 100%;
                gap: 20px;
            }
        }
    `
}
customElements.define('w-notificaciones-view', NotificacionesView);
export { NotificacionesView }



class NotificacionesReader extends HTMLElement {
    /**
     * @param {{ Leidas?: boolean }} Config
     */
    constructor(Config) {
        super();
        this.Config = Config;
        this.NotificacionesElements = new NotificacionesElements({ Leidas: this.Config?.Leidas });
        this.append(
            this.Style,
            StylesControlsV2.cloneNode(true)
        );
    }
    connectedCallback() {
        setTimeout(() => {
            this.NotificacionesElements.Notificaciones
                .filter(notificacion => notificacion.Leido == false)
                .forEach((notificacion, index) => {
                    if (index == 0) {
                        this.NotificacionesElements.VerDetalle(notificacion)
                    }
                })
        }, 1000);

    }
    Style = css``
}
customElements.define('w-notificaciones-reader', NotificacionesReader);
export { NotificacionesReader }

class NotificacionesElements extends HTMLElement {
    /**
     * @param {{ 
     * DOMManager?: ComponentsManager;
     * Leidas?: boolean;
     *  }} [Config]
     */
    constructor(Config) {
        super();
        this.Config = Config;
        this.append(
            this.Style,
            StylesControlsV2.cloneNode(true),
            this.StyleNotifications,
        );
        /**@type {Array<Notificaciones>} */
        this.Notificaciones = [];
        this.NavElements();
    }
    async NavElements() {
        this.Notificaciones = await new Notificaciones_ModelComponent({
            Leido: this.Config?.Leidas
        }).Get();
        // @ts-ignore
        const Notifications = this.Notificaciones.sort((a, b) => a.Leido - b.Leido).map(Notificacion => {
            return {
                Object: Notificacion,
                Titulo: Notificacion.Titulo ?? `Notificación de ${Notificacion.Tipo}`,
                Descripcion: Notificacion.Mensaje,
                Fecha: Notificacion.Fecha,
                Leido: Notificacion.Leido,
                Actions: [
                    { TextAction: "Ver", class: "BtnMini", Action: async (Event) => this.VerDetalle(Notificacion) },
                    /* {
                        TextAction: "Rechazar",  class: "btn-alert", Action: async (Event) => {
                            this.append(ModalVericateAction(async () => {
                                const Events = await WAjaxTools.PostRequest("../api/Events/RechazarParticipacion", Event);
                            }))
                        }
                    }*/
                ]
            };
        });
        this.append(this.DrawNotificaciones(Notifications));
    }

    connectedCallback() { }

    DrawNotificaciones = (Dataset = []) => {
        return WRender.Create({
            className: "GroupDiv", children: Dataset.map(n => {
                return this.CreateNotificacion(n)
            })
        })
    }
    CreateNotificacion = (Notificacion) => {
        const NotificacionContainer = this.BuildNotificationDetail(Notificacion)
        NotificacionContainer.appendChild(WRender.Create({
            className: "options", children: Notificacion.Actions.map(a => ({
                tagName: 'input', type: 'button', className: a.class, value: a.TextAction, onclick: async () => {
                    await a.Action(Notificacion.Object)
                }
            }))
        }))
        return NotificacionContainer;
    }
    BuildNotificationDetail(Notificacion) {
        return WRender.Create({
            className: "NotificationContainer " + (Notificacion.Leido == true ? "Leido" : "NoLeido"), children: [
                { tagName: 'label', className: "titulo", innerText: Notificacion.Titulo },
                { tagName: 'label', className: "fecha", innerText: Notificacion.Fecha?.toDateFormatEs() ?? "" },
                { tagName: 'p', innerHTML: Notificacion.Descripcion }
            ]
        });
    }

    VerDetalle(Notificacion) {
        try {
            const attachs = WRender.Create({ className: "attachs", style: "text-align: center" });
            Notificacion.Media?.forEach(attach => {
                if (attach.Type.toUpperCase().includes("JPG") || attach.Type.toUpperCase().includes("JPEG") || attach.Type.toUpperCase().includes("PNG")) {
                    attachs.append(WRender.Create({
                        tagName: "img", src: attach.Value.replace("wwwroot", ""), style: {
                            width: "auto",
                            objectFit: "cover",
                            height: "calc(100% - 20px)",
                            maxWidth: "100%",
                            overflow: "hidden",
                            borderRadius: "20px"
                        }
                    }));
                } else if (attach.Type.toUpperCase().includes("PDF")) {
                    attachs.append(WRender.Create({
                        tagName: "iframe", src: attach.Value.replace("wwwroot", ""), style: {
                            height: "-webkit-fill-available",
                            width: "100%",
                            minHeight: "500px"
                        }
                    }));
                }
            });
            const notificacionContainer = html`<div class="notification-viewer">
                ${this.StyleNotifications.cloneNode(true)}
                ${this.BuildNotificationDetail({
                TextAction: Notificacion.Estado,
                Object: Notificacion,
                Titulo: Notificacion.Titulo ?? `Notificación de ${Notificacion.Tipo}`,
                Descripcion: Notificacion.Mensaje,
                Fecha: Notificacion.Fecha,
                Leido: Notificacion.Leido
            })}
                ${attachs}
            </div>`;
            if (this.Config?.DOMManager) {
                this.Config.DOMManager.NavigateFunction("notif"+Notificacion.Id, notificacionContainer);
                return;
            }
            document.body.appendChild(new WModalForm({
                ObjectModal: notificacionContainer}))
        } catch (error) {
            console.log(error);
        } finally {
            new Notificaciones({ Id: Notificacion.Id }).MarcarComoLeido();
        }

    }
    StyleNotifications = css`
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
        w-notificaciones-view {
            margin-top: 20px;
        }
        .notifContainer {
            min-height: 700px;
        }
        
        .titulo{
            font-size: 20px;
            font-weight: 500;
            color: #335888;   
            white-space: nowrap;
            text-overflow: ellipsis;
            overflow: hidden;
        }
        
        .fecha{
            padding: 10px 0px;
            font-size: 12px;
            border-bottom: 1px solid #f0f0f0;

        }
        .NotificationContainer p{
            padding: 10px 0px;
            margin: 0px;
            border-bottom: 1px solid #f0f0f0;
            font-size: 14px;
            white-space: nowrap;
            text-overflow: ellipsis;
            overflow: hidden;
            height: 20px;
        } 
        .notification-viewer {
            & .NotificationContainer {
                padding: 0px;
                box-shadow: unset;
                & .fecha{
                    border-bottom: unset;
                }
                & p {
                    white-space: unset;
                    text-overflow: unset;
                    overflow: unset;
                    height: auto;
                }
            }
            & .Leido 
            {
                background-color: unset;
            }
            & .titulo {
                font-size: 30px;
                text-align: center;
            }
        }
        .NotificationContainer .options{
            display: flex;
            justify-content: flex-end;
            margin-top: 10px;
        } 
        .options {
            display: flex;
            justify-content: flex-end;
        }
        .Leido {
            background-color: rgb(248, 249, 250)
        }
        .NoLeido {
            position: relative;
        }
        .NoLeido::after {
            display: block;
            content: " ";
            width: 30px;
            height: 10px;
            position: absolute;
            background-color: rgb(40, 183, 101);
            border-radius: 5px;
            top: 20px;
            right: 20px;
        }
        .BtnMini {
            background-color: #1c4786;
            border: none;
            outline: none;
            border-radius: 8px;
            color: #fff;
            font-weight: 600;
            margin: 0px;
            margin-right: 5px;
            cursor: pointer;
            transition: 0.5s;
            font-size: 12px;
            padding: 8px;
            width: 200px;
        }
        .ContainerFormWModal {
            display: grid;
            grid-template-rows: 10px calc(100% - 10px);           
        }
    `
    Style = css`
        
    `
    GetDescription(E) {
        return `${E.Tbl_Evento?.Tbl_InvestigatorProfile?.Nombres} ${E.Tbl_Evento?.Tbl_InvestigatorProfile?.Apellidos} Indico que participarias en el evento ${E.Tbl_Evento?.Nombre} que se realizara de forma ${E.Tbl_Evento?.Modalidad}, con el rol de ${E.Cat_Tipo_Participacion_Eventos?.Descripcion} de ${E.Titulo} el ${E.Fecha_Participacion?.toDateFormatEs()}.
        
        ¿Desea confirmar su participación?`;
    }

}
customElements.define('w-notificaciones-elements', NotificacionesElements);
export { NotificacionesElements }