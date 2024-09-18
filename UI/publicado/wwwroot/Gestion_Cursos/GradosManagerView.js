//@ts-check
import { Clases } from "../Model/Clases.js";
import { Estudiante_Clases_View } from "../Model/Estudiante_Clases_View.js";
import { Materias } from "../Model/Materias.js";
import { Clases_ModelComponent } from "../Model/ModelComponent/Clases_ModelComponent.js";
import { Materias_ModelComponent } from "../Model/ModelComponent/Materias_ModelComponent.js";
import { StylesControlsV2, StylesControlsV3, StyleScrolls } from "../WDevCore/StyleModules/WStyleComponents.js";
import { LoadinModal } from "../WDevCore/WComponents/LoadinModal.js";
import { FilterDateRange, WFilterOptions } from "../WDevCore/WComponents/WFilterControls.js";
import { WTableComponent } from "../WDevCore/WComponents/WTableComponent.js";
import { ComponentsManager, html, WRender } from "../WDevCore/WModules/WComponentsTools.js";
import { css } from "../WDevCore/WModules/WStyledRender.js";
import { BaremoComponent } from "./BaremoComponent.js";
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
        this.OptionContainer = WRender.Create({ className: "" });
        this.TabContainer = WRender.Create({ className: "TabContainer", id: 'TabContainer' });
        this.Manager = new ComponentsManager({ MainContainer: this.TabContainer, SPAManage: false });
        this.append(
            StylesControlsV2.cloneNode(true),
            StyleScrolls.cloneNode(true),
            StylesControlsV3.cloneNode(true),
            this.OptionContainer,
            this.TabContainer,
            this.CustomStyle
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
                            if (this.Manager.DomComponents.find(c => c.id == `Clase${Clases.Id}`)) {
                                this.Manager.NavigateFunction(`Clase${Clases.Id}`);
                            }                           
                            const loadinModal = new LoadinModal();
                            this.append(loadinModal);
                            this.claseResponse = await new Estudiante_Clases_View({
                                Grado: Clases.Grado,
                                Nombre_corto_periodo: Clases.Periodo_lectivos.Nombre_corto,
                                Nombre_corto_nivel: Clases.Niveles.Nombre_corto
                            }).GetClasesCompleta();
                            loadinModal.close();                            
                            this.Manager.NavigateFunction(`Clase${Clases.Id}`, new BaremoComponent({ Clase: Clases ,  Dataset: this.claseResponse }));
                        }
                    }
                ]
            }
        });
        this.Manager.NavigateFunction("Main-Table", MainTable);
    }
    CustomStyle = css`
        w-grados-manager-view {
            display: flex;
            flex-direction: column;
            gap: 20px;
        }
    `

}
customElements.define('w-grados-manager-view', GradosManagerView);
export { GradosManagerView };

