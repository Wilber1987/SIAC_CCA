//@ts-check

import { Docente_materias } from "../Model/Docente_materias.js";
import { Estudiante_clases } from "../Model/Estudiante_clases.js";
import { Estudiantes } from "../Model/Estudiantes.js";
import { Estudiantes_ModelComponent } from "../Model/ModelComponent/Estudiantes_ModelComponent.js";
import { StylesControlsV2, StylesControlsV3, StyleScrolls } from "../WDevCore/StyleModules/WStyleComponents.js";
import { WAppNavigator } from "../WDevCore/WComponents/WAppNavigator.js";
import { WTableComponent } from "../WDevCore/WComponents/WTableComponent.js";
import { ComponentsManager, html, WRender } from "../WDevCore/WModules/WComponentsTools.js";
import { css } from "../WDevCore/WModules/WStyledRender.js";

const route = location.origin
const routeDocentes = location.origin + "/Media/Images/docentes"

/**
 * @typedef {Object} MateriaDetailConfig
 * * @property {Object} [propierty]
 */
class MateriaDetail extends HTMLElement {
    /**
     * 
     * @param {Docente_materias} Docente_Materia 
     */
    constructor(Docente_Materia) {
        super();
        this.Docente_Materia = Docente_Materia;
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
        this.append(html`<div class="mt-3 mt-xl-3 ps-xl-5 detalles-container">
            <h2 class="font-size-20 mb-3">${this.Docente_Materia?.Materias?.Descripcion?.toUpperCase()}</h2>   
            <h4 class="text-primary mt-4 py-2 mb-0">Seccion: ${this.Docente_Materia?.Secciones.Nombre.toUpperCase()}</h4>           
            <div class="detalles-data-container">             
                <div class="detail-column">
                    <img src="${this.Docente_Materia?.Docentes?.Foto ? `${routeDocentes}/${this.Docente_Materia?.Docentes?.Id}/${this.Docente_Materia?.Docentes?.Foto}`
                : route + "/media/image/avatar-estudiante.png"}" class="avatar-est rounded-circle" alt="">
                </div>
                <div class="detail-column docentes-data">
                    <h5>Docente : ${this.Docente_Materia?.Docentes?.Nombre_completo}</h5>
                    <label>Cargo:  ${this.Docente_Materia?.Docentes?.Cargo ?? "--"}</label>
                    <label>Escolaridad: ${this.Docente_Materia?.Docentes?.Escolaridades?.Nombre ?? "--"}</label>
                    <label>Sexo: ${this.Docente_Materia?.Docentes?.Sexo?.toUpperCase()}</label>
                    <label>Teléfono: ${this.Docente_Materia?.Docentes?.Telefono?.toUpperCase() ?? "--"}</label>
                </div>  
                <div class="detail-column">
                    <h5>Datos de la materia:</h5>
                    <ul class="list-unstyled ps-0 mb-0 mt-3">
                        <li><i class="mdi mdi-circle-medium align-middle text-primary me-1"></i> 
                        Nota mínima: ${this.Docente_Materia?.ThisConfig?.nota_minima ?? "--"} </li>
                        <li><i class="mdi mdi-circle-medium align-middle text-primary me-1"></i> 
                        Periodo de inicio: ${this.Docente_Materia?.ThisConfig?.periodo_inicio ?? "--"} </li>
                        <li><i class="mdi mdi-circle-medium align-middle text-primary me-1"></i> 
                        Periodo de finalización: ${this.Docente_Materia?.ThisConfig?.periodo_fin ?? "--"}</li>
                    </ul>                              
                </div>  
            </div>  
        </div>`);
        this.ComponentTab = new WAppNavigator({
            NavStyle: "tab", Inicialize: true, Elements: this.TabElements()
        });
        this.append(this.ComponentTab)
    }
    TabElements() {
        return [
            {
                name: "Estudiantes", Inicialize: true, action: async () => {
                    return await this.GetEstudiantes()
                }
            },
            {
                name: "Calificaciones", Inicialize: true, action: async () => {
                    return await this.GetCalificaciones()
                }
            }
        ];
    }
    async GetCalificaciones() {
        throw new Error("Method not implemented.");
    }
    async GetEstudiantes() {
        /**@type {Array<Estudiante_clases>} */
        const response = await new Estudiantes().GetBySectionClass({
            Clase_id: this.Docente_Materia?.Materias?.Clase_id,
            Seccion_id: this.Docente_Materia?.Seccion_id
        });
        return new WTableComponent({
            Dataset: response.map(e => e, Estudiantes),
            ModelObject: new Estudiantes_ModelComponent(),
            Options: {}
        })
    }

    CustomStyle = css`
        .detalles-container {
            border: 1px solid #d6d6d6;;
            border-radius: 10px;
            padding: 20px 0px !important;
            & .detail-column img{
                margin: auto;
                display: block;
            }
            & h2, h4 {
                margin: 0px 20px;
            }
        }
        .docentes-data{
           display: flex;
           flex-direction: column;     
        }   
        .detalles-data-container {
            display: grid;
            grid-template-columns: repeat(3, auto);
            gap: 20px;
            border-top: 1px solid #d6d6d6;
            padding: 20px;
        }        
    `
}
customElements.define('w-materia-detail', MateriaDetail);
export { MateriaDetail };
