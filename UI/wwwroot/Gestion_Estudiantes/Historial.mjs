//@ts-check
import { Estudiantes } from "../Model/Estudiantes.js";
import { Estudiantes_ModelComponent } from "../Model/ModelComponent/Estudiantes_ModelComponent.js";
import { StylesControlsV2, StylesControlsV3, StyleScrolls } from "../WDevCore/StyleModules/WStyleComponents.js";
import { WDetailObject } from "../WDevCore/WComponents/WDetailObject.js";
import { ComponentsManager, html, WRender } from "../WDevCore/WModules/WComponentsTools.js";
import { css } from "../WDevCore/WModules/WStyledRender.js";

const route = location.origin

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
        this.append(this.CustomStyle);
        this.append(
            StylesControlsV2.cloneNode(true),
            StyleScrolls.cloneNode(true),
            StylesControlsV3.cloneNode(true),
            this.OptionContainer
        );
        this.Draw();
    }
    Draw = async () => {
        this.append(await this.MainHistorial());        
    }

    async MainHistorial() {
        const entityModel = new Estudiantes();
        const dataset = await entityModel.GetOwEstudiantes();
        const content = html`<section class="Historial">  
            <div class="options-container"></div>         
            <div class="alumnos-container">
                <h6 class="text-muted text-uppercase mb-3">Today</h6>
                ${this.BuildEstudiantes(dataset)}
            </div>
            ${this.TabContainer}
        </section>`;
        return content;
    }

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
    BuildEstudiantes(dataset) {
        return dataset.map((/** @type {Estudiantes} */ Estudiante) =>
            html`<div class="estudiante-card-container" onclick="${() => this.VerEstudianteDetalles(Estudiante)}">            
                <div class="d-flex title align-items-center">
                    <img src="${route + Estudiante.Foto}" class="avatar-sm rounded-circle" alt="">
                    <div class="flex-1 ms-2 ps-1">
                    <h5 class="font-size-14 mb-0">${Estudiante.GetNombreCompleto()}</h5>
                    <label class="text-muted text-uppercase font-size-12">${Estudiante.Codigo}</label>
                    </div>
                </div>
            </div>`);
    }
    VerEstudianteDetalles(Estudiante) {
        console.log(Estudiante);
        
        this.Manager.NavigateFunction("EstDetail_" + Estudiante.Id , new HistorialDetailView(Estudiante));
    }
}
customElements.define('w-historial', HistorialView);
export { HistorialView }


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
            ImageUrlPath: route,
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
customElements.define('w-component', HistorialDetailView);
export { HistorialDetailView }

