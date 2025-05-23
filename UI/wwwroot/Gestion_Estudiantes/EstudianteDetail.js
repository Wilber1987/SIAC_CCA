

//@ts-check

import {  Estudiantes } from "../Model/Estudiantes.js";
import { Clase_Group_ModelComponent, Estudiantes_ModelComponent } from "../Model/ModelComponent/Estudiantes_ModelComponent.js";
import { WAppNavigator } from "../WDevCore/WComponents/WAppNavigator.js";
import { WDetailObject } from "../WDevCore/WComponents/WDetailObject.js";
import { ComponentsManager, html, WRender } from "../WDevCore/WModules/WComponentsTools.js";
import { css } from "../WDevCore/WModules/WStyledRender.js";
import { ClasesDetails } from "./ClasesDetails.js";
import { DatosGenerales } from "./EstudiantesComponents.js";


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
        this.append(DatosGenerales(this.Estudiante));        
        this.ComponentTab = new WAppNavigator({
            NavStyle: "tab", Inicialize: true, Elements: this.TabElements()
        });
        //this.append(this.ComponentTab)
        this.append(this.BuildClassDetail());
    }
    TabElements() {
        return [
            {
                name: "Clases",
                NavStyle: "tab",
                Inicialize: true,
                action: async (ev) => {                   
                    return this.BuildClassDetail();
                }
            }
        ];
    }

    BuildClassDetail() {
        return new ClasesDetails({
            ModelObject: new Clase_Group_ModelComponent(),
            Dataset: this.Estudiante.Estudiante_clases ?? [],
            Estudiante: this.Estudiante
        });
    }

    async MainComponent() { return {} }

    CustomStyle = css`
        .component{
            display: block;
        }        
         .header {
                text-align: center!important;
            }     
        w-view-detail{
            text-transform: uppercase !important;
        }    
    `
}
customElements.define('w-estudiante-detail', EstudianteDetail);
export { EstudianteDetail }
