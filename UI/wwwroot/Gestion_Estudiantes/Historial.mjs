//@ts-check
import { Estudiantes } from "../Model/Estudiantes.js";
import { Estudiantes_ModelComponent } from "../Model/ModelComponent/Estudiantes_ModelComponent.js";
import { StylesControlsV2, StylesControlsV3, StyleScrolls } from "../WDevCore/StyleModules/WStyleComponents.js";
import { WDetailObject } from "../WDevCore/WComponents/WDetailObject.js";
import { ComponentsManager, html, WRender } from "../WDevCore/WModules/WComponentsTools.js";
import { css } from "../WDevCore/WModules/WStyledRender.js";


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
            this.OptionContainer,
            this.TabContainer
        );
        this.Draw();
    }
    Draw = async () => {
        this.Manager.NavigateFunction("id", await this.MainHistorial());
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
            <div class="alumnos-detalle"></div>
        </section>`;
        return content;
    }

    CustomStyle = css`
        .Historial{
           display: block;
        }           
    `
    BuildEstudiantes(dataset) {
        return dataset.map((/** @type {Estudiantes} */ Estudiante) =>
            html`<div class="mb-2" onclick="${() => this.VerEstudianteDetalles(Estudiante)}">
            <div class="message-list mb-0 p-1">
                <div class="list">
                    <div class="col-mail col-mail-1">
                        <div class="checkbox-wrapper-mail">
                            <input type="checkbox" id="chk1">
                            <label for="chk1" class="toggle"></label>
                        </div>
                        <div class="d-flex title align-items-center">
                            <img src="${Estudiante.Foto}" class="avatar-sm rounded-circle" alt="">
                            <div class="flex-1 ms-2 ps-1">
                                <h5 class="font-size-14 mb-0"><a href="" class="text-body">${Estudiante.GetNombreCompleto()}</a></h5>
                                <a href="" class="text-muted text-uppercase font-size-12">${Estudiante.Fecha_nacimiento}</a>
                            </div>
                        </div>
                    </div>                
                </div>
            </div>
        </div>`);
    }
    VerEstudianteDetalles(Estudiante) {
        throw new Error("Method not implemented.");
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
        this.attachShadow({ mode: 'open' });
        this.OptionContainer = WRender.Create({ className: "OptionContainer" });
        this.TabContainer = WRender.Create({ className: "TabContainer", id: 'TabContainer' });
        this.Manager = new ComponentsManager({ MainContainer: this.TabContainer, SPAManage: false });
        this.Estudiante = Estudiante
        this.Draw();
    }
    Draw = async () => {
        this.append(new WDetailObject({
            ObjectDetail: this.Estudiante,
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

