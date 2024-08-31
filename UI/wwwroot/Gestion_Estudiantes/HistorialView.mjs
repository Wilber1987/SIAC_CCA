//@ts-check
import { Estudiantes } from "../Model/Estudiantes.js";
import { StylesControlsV2, StylesControlsV3, StyleScrolls } from "../WDevCore/StyleModules/WStyleComponents.js";
import { ComponentsManager, WRender } from "../WDevCore/WModules/WComponentsTools.js";
import { EstudiantesDetails } from "./EstudiantesDetails.js";


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
            //this.OptionContainer
        );
        this.Draw();
    }

    async Draw() {
        const entityModel = new Estudiantes();
        const dataset = await entityModel.GetOwEstudiantes();
        this.append(new EstudiantesDetails({ Dataset: dataset }));
    }

}
customElements.define('w-historial', HistorialView);
export { HistorialView };

