//@ts-check
import { Clases } from "../Model/Clases.js";
import { Estudiantes } from "../Model/Estudiantes.js";
import { Materias } from "../Model/Materias.js";
import { StylesControlsV2, StylesControlsV3, StyleScrolls } from "../WDevCore/StyleModules/WStyleComponents.js";
import { ComponentsManager, WRender } from "../WDevCore/WModules/WComponentsTools.js";
import { MateriasDetails } from "./MateriasDetails.js";


/**
 * @typedef {Object} MateriasPropiasConfig
 * * @property {Object} [propierty]
 */
class MateriasPropiasView extends HTMLElement {
    /**
     * 
     * @param {MateriasPropiasConfig} props 
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
        const entityModel = new Materias();
        const dataset = await entityModel.GetOw();
        this.append(new MateriasDetails({ Dataset: dataset }));
    }

}
customElements.define('w-materias-propios', MateriasPropiasView);
export { MateriasPropiasView };

