//@ts-check
import { Estudiante_clases } from "../Model/Estudiante_clases.js";
import { Estudiantes } from "../Model/Estudiantes.js";
import { Estudiantes_ModelComponent } from "../Model/ModelComponent/Estudiantes_ModelComponent.js";
import { StylesControlsV2, StylesControlsV3, StyleScrolls } from "../WDevCore/StyleModules/WStyleComponents.js";
import { WDetailObject } from "../WDevCore/WComponents/WDetailObject.js";
import { ModalMessege } from "../WDevCore/WComponents/WForm.js";
import { WModalForm } from "../WDevCore/WComponents/WModalForm.js";
import { WPrintExportToolBar } from "../WDevCore/WComponents/WPrintExportToolBar.mjs";
import { ComponentsManager, html, WRender } from "../WDevCore/WModules/WComponentsTools.js";
import { css } from "../WDevCore/WModules/WStyledRender.js";

const route = location.origin
const routeEstudiantes = location.origin + "/Media/Images/estudiantes/"

/**
 * @typedef {Object} HistorialConfig
 * * @property {Object} [propierty]
 */
class HistorialView extends HTMLElement {
    /**
     * 
     * @param {HistorialConfig} props 
     */
    constructor(props) {
        super();
        this.OptionContainer = WRender.Create({ className: "OptionContainer" });
        this.TabContainer = WRender.Create({ className: "TabContainer", id: 'TabContainer' });
        this.Manager = new ComponentsManager({ MainContainer: this.TabContainer, SPAManage: false });
        this.append(
            StylesControlsV2.cloneNode(true),
            StyleScrolls.cloneNode(true),
            StylesControlsV3.cloneNode(true),
            this.OptionContainer
        );
        this.Draw();
    }

    async Draw() {
        const entityModel = new Estudiantes();
        const dataset = await entityModel.GetOwEstudiantes();
        this.append(new EstudiantesView({ Dataset: dataset }));
    }

}
customElements.define('w-historial', HistorialView);
export { HistorialView }

/**
 * @typedef {Object} EstudiantesViewConfig 
    * @property {Array<Estudiantes>} Dataset
**/
class EstudiantesView extends HTMLElement {
    /**
    * @param {EstudiantesViewConfig} Config 
    */
    constructor(Config) {
        super();
        this.Config = Config
        this.OptionContainer = WRender.Create({ className: "OptionContainer" });
        this.TabContainer = WRender.Create({ className: "TabContainer", id: 'TabContainer' });
        this.Manager = new ComponentsManager({ MainContainer: this.TabContainer, SPAManage: false });
        this.append(this.CustomStyle);
        this.append(
            StylesControlsV2.cloneNode(true),
            StyleScrolls.cloneNode(true),
            StylesControlsV3.cloneNode(true),
            this.OptionContainer
        );
        this.Draw();
    }
    connectedCallback() { }
    Draw = async () => {
        const content = html`<section class="Historial">  
            <div class="options-container">
                ${new WPrintExportToolBar({ PrintAction: this.PrintAction, ExportPdfAction: this.ExportPdfAction })}
            </div>         
            <div class="alumnos-container">
                <h6 class="text-muted text-uppercase mb-3">Today</h6>
                ${this.BuildEstudiantes(this.Config.Dataset)}
            </div>
            ${this.TabContainer}
        </section>`;
        this.append(content);
    }

    update() {
        this.Draw();
    }


    BuildEstudiantes(dataset) {
        return dataset.map((/** @type {Estudiantes} */ Estudiante) =>
            html`<div class="estudiante-card-container" onclick="${() => this.VerEstudianteDetalles(Estudiante)}">            
                 <div class="d-flex title align-items-center">
                     <img src="${Estudiante.Foto ? `${routeEstudiantes}/${Estudiante.Id}/${Estudiante.Foto}`
                    : route + "/media/image/avatar-estudiante.png"}" class="avatar-est rounded-circle" alt="">
                     <div class="flex-1 ms-2 ps-1">
                     <h5 class="font-size-14 mb-0">${Estudiante.GetNombreCompleto()}</h5>
                     <label class="text-muted text-uppercase font-size-12">${Estudiante.Codigo}</label>
                     </div>
                 </div>
             </div>`);
    }
    /**
    * @param {Estudiantes} Estudiante 
    */
    async VerEstudianteDetalles(Estudiante) {
        this.EstudianteSeleccionado = Estudiante;
        this.Manager.NavigateFunction("EstDetail_" + Estudiante.Id,
            new HistorialDetailView(await Estudiante.Find()));
    }
    PrintAction = () => {
        if (!this.EstudianteSeleccionado) {
            this.append(ModalMessege("Seleccione un estudiante"));
            return;
        }
        this.append(new WModalForm({
            title: "Imprimir informe clase",
            StyleForm: "columnX1",
            ModelObject: {
                Seleccione: { type: "select", Dataset: this.EstudianteSeleccionado?.Clase_Group.map(c => c.Descripcion) }
            }, EditObject: {}, ObjectOptions: {
                SaveFunction: async (object) => {
                    const body = await this.GetActa(object);
                    const ventimp = window.open(' ', 'popimpr');
                    ventimp?.document.write(body.innerHTML);
                    ventimp?.focus();
                    setTimeout(() => {
                        ventimp?.print();
                        ventimp?.close();
                    }, 100)
                    return;
                }
            }
        }));
    };
    ExportPdfAction = () => {
        if (!this.EstudianteSeleccionado) {
            this.append(ModalMessege("Seleccione un estudiante"));
            return;
        }
        this.append(new WModalForm({
            title: "Imprimir informe clase",
            StyleForm: "columnX1",
            ModelObject: {
                Seleccione: { type: "select", Dataset: this.EstudianteSeleccionado?.Clase_Group.map(c => c.Descripcion) }
            }, EditObject: {}, ObjectOptions: {
                SaveFunction: async (object) => {
                    const body = await this.GetActa(object);
                    console.log(body);
                    
                    // @ts-ignore
                    html2pdf().from(body).save();
                    return;
                }
            }
        }));
    };

    CustomStyle = css`
        .Historial{
        display: grid;
        grid-template-columns: 300px calc(100% - 320px);
        gap: 20px;
        }   
        .Historial .options-container {
            grid-column: span 2;
        }
        .estudiante-card-container {
            display: block;
            border: 1px solid #ede9e9;
            border-radius: 10px;
            cursor: pointer;
            padding: 10px;
        }
        .alumnos-container {
            display: flex;
            flex-direction: column;
            gap: 10px;
        }
        .TabContainer {
            border-left: 1px solid #ede9e9;
            padding-left: 20px;
            min-height: 400px;
        }
        .avatar-est{
        height: 80px;
        width: 80px;
        min-width: 80px;
        border-radius: 50%;
        object-fit: cover;
        }
        @media (max-width: 768px) {
            .Historial{               
                grid-template-columns: 100%;
            } 
            .Historial .options-container {
                grid-column: span 1;
            }
            .TabContainer {
                border-left: unset;
                padding-left: unset;                
            }
        }
    `

    async GetActa(object) {
        const id = object.Seleccione?.toString().replaceAll(" ", "");
        // new Estudiante_clases({ Clase_id: object.Id, Estudiante_id: this.EstudianteSeleccionado?.Id })
        //  .ExportClaseBoletin({ data: body });
        /**@type {HTMLElement} */
        const detail = this.Manager.DomComponents.find(c => c.id === "EstDetail_" + this.EstudianteSeleccionado?.Id);
        // @ts-ignore
        const content = detail.querySelector("w-view-detail")?.shadowRoot?.querySelector("w-app-navigator")?.Manager?.DomComponents[0];
        const body = content.shadowRoot.querySelector(`#${id}`);
        // @ts-ignore
        body.append(content.CustomStyle.cloneNode(true));
        body.append(this.PrintStyle.cloneNode(true));
        return html`<div class="page">${body.innerHTML}</div>`;
    }
    PrintStyle = css`
        .page {   
           margin: 40px;
        }   
        .prop {
            font-size: 12px;
        }               
        .detail-content {
            width: 100%;
            border: none;
            border-radius: unset;
            padding: 0;            
        }        
        .detail-content .container {
            width: -webkit-fill-available;
            & .element-description {
                font-size: 12px;
            }
            & .element-details {
                & .element-detail {
                    font-size: 12px;
                }
            }   
        }        
        .header {
            width: 100%;           
            font-size: 12px;
        }
        .value {            
            font-size: 12px;
        }        
        @media print{
            *{
                -webkit-print-color-adjust: exact !important;
                border-collapse: collapse;
            }
            .page{
                border: none; /* Optional: Remove border for printing */
                margin: 0;
                padding: 0;
                box-shadow: none; /* Optional: Remove any shadow for printing */
                page-break-after: always; /* Ensure each .page-container starts on a new page */
            }
            .detail-content .container, .detail-content .element-details {
                flex-direction: row
            }
            .hidden {
                display: none;
            }
        } 
    `
}
customElements.define('w-estudiantes', EstudiantesView);
export { EstudiantesView }

class HistorialDetailView extends HTMLElement {
    /**
     * 
     * @param {Estudiantes} Estudiante 
     */
    constructor(Estudiante) {
        super();
        //this.attachShadow({ mode: 'open' });
        this.OptionContainer = WRender.Create({ className: "OptionContainer" });
        this.TabContainer = WRender.Create({ className: "TabContainer", id: 'TabContainer' });
        this.Manager = new ComponentsManager({ MainContainer: this.TabContainer, SPAManage: false });
        this.Estudiante = Estudiante;
        this.Draw();
    }
    Draw = async () => {
        this.append(new WDetailObject({
            ObjectDetail: this.Estudiante,
            ImageUrlPath: `${routeEstudiantes}/${this.Estudiante.Id}/`,

            ModelObject: new Estudiantes_ModelComponent()
        }))
    }


    async MainComponent() { return {} }

    CustomStyle = css`
        .component{
           display: block;
        }           
    `
}
customElements.define('w-historial-detail', HistorialDetailView);
export { HistorialDetailView }

