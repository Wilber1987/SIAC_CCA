

//@ts-check

import {  Estudiantes } from "../Model/Estudiantes.js";
import { Clase_Group_ModelComponent, Estudiantes_ModelComponent } from "../Model/ModelComponent/Estudiantes_ModelComponent.js";
import { WAppNavigator } from "../WDevCore/WComponents/WAppNavigator.js";
import { WDetailObject } from "../WDevCore/WComponents/WDetailObject.js";
import { ComponentsManager, WRender } from "../WDevCore/WModules/WComponentsTools.js";
import { css } from "../WDevCore/WModules/WStyledRender.js";
import { ClasesDetails } from "./ClasesDetails.js";


const route = location.origin
const routeEstudiantes = location.origin + "/Media/Images/estudiantes/"

class EstudianteDetail extends HTMLElement {    
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
        this.append(
            this.CustomStyle
        )
        this.Draw();
    }
    Draw = async () => {
        this.append(new WDetailObject({
            ObjectDetail: this.Estudiante,
            ImageUrlPath: `${routeEstudiantes}`,
            ModelObject: new Estudiantes_ModelComponent()
        }));
        this.ComponentTab = new WAppNavigator({
            NavStyle: "tab", Inicialize: true, Elements: this.TabElements()
        });
        this.append(this.ComponentTab)
    }
    TabElements() {
        return [
            {
                name: "Clases",
                NavStyle: "tab",
                Inicialize: true,
                action: async (ev) => {                   
                    return new ClasesDetails({
                        ModelObject: new Clase_Group_ModelComponent(),
                        Dataset: this.Estudiante.Estudiante_clases ?? []
                    });
                }
            }
        ];
    }


    async MainComponent() { return {} }

    CustomStyle = css`
        .component{
           display: block;
        }       
        w-view-detail{
            text-transform: lowercase !important;
        }    
    `
}
customElements.define('w-estudiante-detail', EstudianteDetail);
export { EstudianteDetail }
