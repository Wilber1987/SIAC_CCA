//@ts-check
import { WRender, ComponentsManager, html } from "../WDevCore/WModules/WComponentsTools.js";
import { StylesControlsV2, StylesControlsV3, StyleScrolls } from "../WDevCore/StyleModules/WStyleComponents.js"
import { css } from "../WDevCore/WModules/WStyledRender.js";
import { WTableComponent } from "../WDevCore/WComponents/WTableComponent.js";
import { TemplateData, TemplateData_ModelComponent, WTemplateBuilder } from "../WDevCore/WComponents/WTemplateBuilder.js";
import { PageType } from "../WDevCore/WComponents/WDocumentViewer.js";

/**
 * @typedef {Object} ComponentConfig
 * * @property {Object} [propierty]
 */
class TemplateView extends HTMLElement {
    /**
     * 
     * @param {ComponentConfig} props 
     */
    constructor(props) {
        super();
        this.attachShadow({ mode: 'open' });
        this.OptionContainer = WRender.Create({ className: "OptionContainer" });
        this.TabContainer = WRender.Create({ className: "TabContainer", id: 'TabContainer' });
        this.Manager = new ComponentsManager({ MainContainer: this.TabContainer, SPAManage: false });
        this.shadowRoot?.append(this.CustomStyle);
        this.shadowRoot?.append(
            StylesControlsV2.cloneNode(true),
            StyleScrolls.cloneNode(true),
            StylesControlsV3.cloneNode(true),
            this.OptionContainer,
            this.TabContainer
        );
        this.Draw();
    }
    Draw = async () => {
        this.SetOption();
    }

    async SetOption() {
        this.OptionContainer.append(WRender.Create({
            tagName: 'button', className: 'Btn-Mini-Success', innerText: 'Plantillas',
            onclick: async () => this.Manager.NavigateFunction("id", await this.MainComponent())
        }))
        this.Manager.NavigateFunction("id", await this.MainComponent());
    }
    async MainComponent() {
        return new WTableComponent({
            ModelObject: new TemplateData_ModelComponent(),
            EntityModel: new TemplateData(),
            Options: {
                UserActions: [{
                    name: 'edit', action: (/** @type {TemplateData} */ TableElement) => {
                        this.Manager.NavigateFunction("idtemplate" + TableElement.Id_Template, this.TemplateEditor(TableElement));
                    }
                }]
            }
        })
    }
    /**
     * @param {TemplateData} TableElement
     */
    TemplateEditor(TableElement) {
        return html`<div class="template-element-editor">
            <div class="diccionary">
                ${this.GetDiccionary()}
            </div>
            ${new WTemplateBuilder({ Data: TableElement, PageType: PageType.OFICIO })}
        </div>`;
    }
    GetDiccionary() {
        return html`<ul>
            <h2>Parametros</h2>
           <li>logo</li>
           <li>codigo_familia</li>
           <li>codigo_estudiante</li>
           <li>nombre_estudiante</li>
           <li>impresion</li>
           <li>current_year</li>
           <li>nombre_responsable1</li>
           <li>nombre_responsable2</li>
           <li>cedula1</li>
           <li>cedula2</li>
           <li>dia</li>
           <li>mes</li>
           <li>anio</li>
        </ul>`;
    }

    CustomStyle = css`
        .OptionContainer {
            margin-bottom: 10px;
        }
        
        .template-element-editor{
            display: grid;
            height: 100%;
            grid-template-columns: 350px calc(100% - 370px);
            gap: 20px;
            max-height: 700px;
            w-template-builder, .diccionary {
                padding: 10px 20px;
                border-radius: 10px;
                border: solid 1px #b9b9b9;
                max-height: 680px;
            }
        }           
    `
}
customElements.define('w-component', TemplateView);
export { TemplateView  }