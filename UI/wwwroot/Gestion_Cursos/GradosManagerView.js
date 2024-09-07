//@ts-check
import { Clases } from "../Model/Clases.js";
import { Materias } from "../Model/Materias.js";
import { Clases_ModelComponent } from "../Model/ModelComponent/Clases_ModelComponent.js";
import { Materias_ModelComponent } from "../Model/ModelComponent/Materias_ModelComponent.js";
import { StylesControlsV2, StylesControlsV3, StyleScrolls } from "../WDevCore/StyleModules/WStyleComponents.js";
import { FilterDateRange, WFilterOptions } from "../WDevCore/WComponents/WFilterControls.js";
import { WTableComponent } from "../WDevCore/WComponents/WTableComponent.js";
import { ComponentsManager, html, WRender } from "../WDevCore/WModules/WComponentsTools.js";
import { MateriasDetails } from "./MateriasDetails.js";


/**
 * @typedef {Object} GradosManagerViewConfig
 * * @property {Object} [propierty]
 */
class GradosManagerView extends HTMLElement {
    /**
     * 
     * @param {GradosManagerViewConfig} props 
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
            this.OptionContainer,
            this.TabContainer
        );
        this.Draw();
    }

    async Draw() {
        const MainTable = new WTableComponent({
            ModelObject: new Clases_ModelComponent(),
            EntityModel: new Clases(),
            Options: {
                Filter: true,
                AutoSetDate: false,
                FilterDisplay: true,
                UserActions: [
                    {
                        name: "Baremo", action: async (/** @type {Clases} */ Clases) => {

                        }
                    }
                ]
            }
        });
        this.Manager.NavigateFunction("Main-Table", MainTable);        
    }

}
customElements.define('w-grados-manager-view', GradosManagerView);
export { GradosManagerView };

