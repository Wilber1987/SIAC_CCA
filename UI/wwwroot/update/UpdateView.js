//@ts-check
//@ts-check
import { StylesControlsV2, StylesControlsV3, StyleScrolls } from "../WDevCore/StyleModules/WStyleComponents.js"
import { css } from "../WDevCore/WModules/WStyledRender.js";
import { WAppNavigator } from "../WDevCore/WComponents/WAppNavigator.js";
/**
 * @typedef {Object} ComponentConfig
 * * @property {Object} [propierty]
 */
class NotificacionMatriculaActualizacion extends HTMLElement {
    /**
     * 
     * @param {ComponentConfig} props 
     */
    constructor(props) {
        super();   
        this.append(this.CustomStyle);  
        this.NavManager = new WAppNavigator({
                NavStyle: "tab",
                Inicialize: true,
                Elements: this.NavElements()
            })      
        this.append(
            StylesControlsV2.cloneNode(true),
            StyleScrolls.cloneNode(true),
            StylesControlsV3.cloneNode(true),
            this.NavManager,
        );
        this.Draw();
    }
    Draw = async () => {
        
    }
  

    NavElements() {
        return [{
                name: "Element", action: () => {
                    return "";
                }
        }]   
    }
   
    CustomStyle = css`
        .component{
           display: block;
        }           
    `
}
customElements.define('w-notif-mat-actualizacion', NotificacionMatriculaActualizacion);
export { NotificacionMatriculaActualizacion }