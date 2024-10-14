//@ts-check

import { PagosRequest_ModelComponent } from "../Model/ModelComponent/PagosRequest_ModelComponent.js";
import { PagosRequest } from "../Model/PagosRequest.js";
import { StylesControlsV2, StylesControlsV3, StyleScrolls } from "../WDevCore/StyleModules/WStyleComponents.js";
import { WTableComponent } from "../WDevCore/WComponents/WTableComponent.js";
import { ComponentsManager, WRender } from "../WDevCore/WModules/WComponentsTools.js";
import { css } from "../WDevCore/WModules/WStyledRender.js";

/**
 * @typedef {Object} Historial_PagosViewConfig
 * * @property {Object} [propierty]
 */
class Historial_PagosView extends HTMLElement {
    /**
     * @param {Historial_PagosViewConfig} props 
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
        this.SetOption();
    } 

    async SetOption() {
        this.OptionContainer.append(WRender.Create({
            tagName: 'button', className: 'Block-Primary', innerText: 'Hitorial de pagos realizados',
            onclick: async () => this.Manager.NavigateFunction("id", await this.MainComponent())
        }))
        this.Manager.NavigateFunction("id", await this.MainComponent());          
    }
    async MainComponent() { 
        return new WTableComponent({
            ModelObject: new PagosRequest_ModelComponent(),
            EntityModel: new PagosRequest({}),
            Options: { Filter: true, Show: true },
        });
     }
   
    CustomStyle = css`
        .component{
           display: block;
        }           
    `
}
customElements.define('w-component', Historial_PagosView);
export { Historial_PagosView }