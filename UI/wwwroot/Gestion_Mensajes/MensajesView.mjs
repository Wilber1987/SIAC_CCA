//@ts-check
import { Conversacion } from "../Model/Conversacion.js";
import { Conversacion_usuarios } from "../Model/Conversacion_usuarios.js";
import { Mensajes_ModelComponent } from "../Model/ModelComponent/Mensajes_ModelComponent.js";
import { WSecurity } from "../WDevCore/Security/WSecurity.js";
import { StylesControlsV2, StylesControlsV3, StyleScrolls } from "../WDevCore/StyleModules/WStyleComponents.js";
import { WCommentsComponent } from "../WDevCore/WComponents/WCommentsComponent.js";
import { ComponentsManager, html, WRender } from "../WDevCore/WModules/WComponentsTools.js";
import { css } from "../WDevCore/WModules/WStyledRender.js";
import { Contacto } from "./Contacto.js";
const route = location.origin
const routeContacto = location.origin + "/Media/Images/profile/"


/**
 * @typedef {Object} MensajesConfig
 * * @property {Object} [propierty]
 */
class MensajesView extends HTMLElement {
    /**
     * 
     * @param {MensajesConfig} Config 
     */
    constructor(Config) {
        super();
        this.Config = Config
        this.OptionContainer = WRender.Create({ className: "OptionContainer" });
        this.TabContainer = WRender.Create({ className: "TabContainer", id: 'TabContainer' });
        this.Manager = new ComponentsManager({ MainContainer: this.TabContainer, SPAManage: false });
        this.append(
            StylesControlsV2.cloneNode(true),
            StyleScrolls.cloneNode(true),
            StylesControlsV3.cloneNode(true),
            //this.OptionContainer,
            this.CustomStyle
        );
        this.Draw();
    }

    async Draw() {
        /**@type {Array<Conversacion>} */
        const conversaciones = await new Conversacion().Get();
        const conversacionesContact = html`<section class="contacts-container aside-container">  
            <div class="options-container">            
            </div>         
            <div class="contact-container">
                <h6 class="text-uppercase mb-3">Conversaciones</h6>
                ${conversaciones.map(conversacion => this.BuildConversacion(conversacion))}
            </div>
        </section>`;
        /**@type {Array<Contacto>} */
        const contactos = await new Contacto().Get();
        const contentContact = html`<section class="contacts-container aside-container">  
            <div class="options-container">            
            </div>         
            <div class="contact-container">
                <h6 class="text-uppercase mb-3">Contactos</h6>
                ${contactos.map(contact => this.BuildContacto(contact))}
            </div>
        </section>`;
        this.append(html`<div class="main-container">${[conversacionesContact, this.TabContainer, contentContact ]}</div>`);
        
        this.VerConversacion(conversaciones[0])

    }
    /**
     * @param {Conversacion} conversacion
     */
    BuildConversacion(conversacion) {
        return html`<div class="conversacion-card-container" onclick="${() => this.VerConversacion(conversacion)}">            
            <div class="d-flex title align-items-center">               
                     <div class="flex-1 ms-2 ps-1">
                     <h5 class="font-size-14 mb-0">${conversacion.Descripcion}</h5>
                     <label class="text-muted text-uppercase font-size-12">${conversacion.Mensajes}</label>
                     </div>
                </div>
        </div>`;
    }
    /**
     * @param {Contacto} contacto
     */
    BuildContacto(contacto) {
        return html`<div class="estudiante-card-container" onclick="${() => this.VerConversacionContacto(contacto)}">            
            <div class="d-flex title align-items-center">
                <img src="${contacto.Foto ? `${routeContacto}/${contacto.Id_User}/${contacto.Foto}`
                : route + "/media/image/avatar-estudiante.png"}" class="avatar-est rounded-circle" alt="">
                     <div class="flex-1 ms-2 ps-1">
                     <h5 class="font-size-14 mb-0">${contacto.Nombre_Completo}</h5>
                     <label class="text-muted text-uppercase font-size-12">${contacto.Mensajes}</label>
                     </div>
                </div>
        </div>`;
    }
    /**
     * @param {Contacto} contacto
     */
    async VerConversacionContacto(contacto) {
        this.ContactoSeleccionado = contacto;
        /**@type {Conversacion} */
        const conversacion = await new Conversacion({ Conversacion_usuarios : [
           new Conversacion_usuarios({ Id_usuario: contacto.Id_User })
        ]}).Find();
        this.VerConversacion(conversacion)
    }
     /**
     * @param {Conversacion} conversacion
     */
     async VerConversacion(conversacion) {
        this.ContactoSeleccionado = conversacion;        
      
        const mensajes = []
        const commentsContainer = new WCommentsComponent({
            Dataset: [],
            ModelObject: new Mensajes_ModelComponent(),
            User: WSecurity.UserData,
            UserIdProp: "Id_User",
            CommentsIdentify: conversacion.Id_conversacion,
            CommentsIdentifyName: "Id_conversacion",
            UrlSearch: route + "/api/ApiMessageManager/getMensajes",
            UrlAdd: route + "/api/ApiMessageManager/saveMensajes",
            AddObject: true, 
            UseDestinatarios: false,
            UseAttach: false
        });
        this.Manager.NavigateFunction("EstDetail_" + conversacion.Id_conversacion, commentsContainer);
    }
    CustomStyle = css`
        .main-container {
            display: grid;
            grid-template-columns: 300px calc(100% - 330px);
            grid-template-rows: 300px auto;
            gap: 20px;
        }
        .TabContainer {
            grid-row: span 2;
            grid-column: span 2;
            grid-column-start: 2;
            min-height: 600px;
            max-height: 700px;
        }
        
        @media (max-width: 600px) {
            .main-container {
                display: flex;
                flex-direction: column;
              
            }
        }
    `

}
customElements.define('w-historial', MensajesView);
export { MensajesView };
