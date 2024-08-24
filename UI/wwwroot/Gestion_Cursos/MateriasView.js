//@ts-check
import { Clases } from "../Model/Clases.js";
import { Estudiantes } from "../Model/Estudiantes.js";
import { Materias } from "../Model/Materias.js";
import { Materias_ModelComponent } from "../Model/ModelComponent/Materias_ModelComponent.js";
import { StylesControlsV2, StylesControlsV3, StyleScrolls } from "../WDevCore/StyleModules/WStyleComponents.js";
import { FilterDateRange, WFilterOptions } from "../WDevCore/WComponents/WFilterControls.js";
import { ComponentsManager, WRender } from "../WDevCore/WModules/WComponentsTools.js";
import { MateriasDetails } from "./MateriasDetails.js";


/**
 * @typedef {Object} MateriasConfig
 * * @property {Object} [propierty]
 */
class MateriasView extends HTMLElement {
    /**
     * 
     * @param {MateriasConfig} props 
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
        //const entityModel = new Materias();
        //const dataset = await entityModel.Get();
        const main = new MateriasDetails({ Dataset: [] });
        this.FilterOptions = new WFilterOptions({
            // @ts-ignore
            Dataset: this.Dataset,
            AutoSetDate: true,
            DateRange: FilterDateRange.YEAR,
            AutoFilter: true,
            UseEntityMethods: false,
            Display: true,
            ModelObject: new Materias_ModelComponent(),
            FilterFunction: async (DFilt) => {
                const dataset = await new Materias({ FilterData: DFilt }).Get();
                main.Config.Dataset = dataset;
                main.update();
            }
        });
        this.OptionContainer.appendChild(this.FilterOptions);
        this.append(main);
    }

}
customElements.define('w-materias-view', MateriasView);
export { MateriasView };

