
//@ts-check
// @ts-ignore
import { Estudiantes } from "../Model/Estudiantes.js";
import { StylesControlsV2, StylesControlsV3, StyleScrolls } from "../WDevCore/StyleModules/WStyleComponents.js";
import { ComponentsManager, html, WRender } from "../WDevCore/WModules/WComponentsTools.js";
import { css } from "../WDevCore/WModules/WStyledRender.js";
import { EstudianteDetail } from "./EstudianteDetail.js";

const route = location.origin
const routeEstudiantes = location.origin + "/Media/Images/estudiantes/"
/**
 * @typedef {Object} EstudiantesDetailsConfig 
    * @property {Array<Estudiantes>} Dataset
**/
class EstudiantesDetails extends HTMLElement {
    /**
    * @param {EstudiantesDetailsConfig} Config 
    */
    constructor(Config) {
        super();
        this.Config = Config
        this.OptionContainer = WRender.Create({ className: "OptionContainer" });
        this.TabContainer = WRender.Create({ className: "TabContainer", id: 'TabContainer' });
        this.Manager = new ComponentsManager({ MainContainer: this.TabContainer, SPAManage: false });
        this.EstudianteSeleccionado = new Estudiantes();
        this.append(this.CustomStyle);
        this.append(
            StylesControlsV2.cloneNode(true),
            StyleScrolls.cloneNode(true),
            StylesControlsV3.cloneNode(true),
            //this.OptionContainer
        );
        this.Draw();
    }
    connectedCallback() { }
    Draw = async () => {
        const content = html`<section class="Historial"> 
            <h3 class="text-uppercase">Estudiantes</h3> 
            <div class="alumnos-container aside-container">                
                ${this.BuildEstudiantes(this.Config.Dataset)}
            </div>
            ${this.TabContainer}
        </section>`;
        this.append(content);
    }

    update() {
        this.Draw();
    }


    BuildEstudiantes(dataset) {
        return dataset.map((/** @type {Estudiantes} */ Estudiante) =>
            html`<div class="estudiante-card-container" id="card-estudiante${Estudiante.Id}" onclick="${(ev) => this.VerEstudianteDetalles(Estudiante)}">            
                     <div class="estudiante-card">
                         <img src="${Estudiante.Foto ? `${routeEstudiantes}/${Estudiante.Id}/${Estudiante.Foto}`
                    : route + "/media/image/avatar-estudiante.png"}" class="avatar-est rounded-circle" alt="">
                         <div class="flex-1 ms-2 ps-1">
                           <h5 class="font-size-14 mb-0">${Estudiante.GetNombreCompleto()}</h5>
                           <label class="text-muted text-uppercase font-size-12">${Estudiante.Codigo}</label>
                         </div>
                     </div>
                 </div>`);
    }
    /**
    * @param {Estudiantes} Estudiante 
    */
    async VerEstudianteDetalles(Estudiante) {
        /**@type {Estudiantes} */
        this.EstudianteSeleccionado = await Estudiante.Find();
        this.Manager.NavigateFunction("EstDetail_" + Estudiante.Id, new EstudianteDetail(this.EstudianteSeleccionado));
        this.querySelectorAll(".estudiante-card-container")?.forEach(n => {
            n.classList.remove("card-active");
        });
        this.querySelector(`#card-estudiante${Estudiante.Id}`)?.classList.add("card-active");
    }

    CustomStyle = css`
        .card-active {
            background-color: #f1f1f1;
        }
        .Historial{
            display: flex;
            flex-direction: column;            
            gap: 20px;
        }   
        .Historial .options-container {
            display: flex;
            align-items: center;
            justify-content: space-between;
            grid-column: span 2;
        }
        .estudiante-card-container {
            display: flex;
            border: 1px solid #d6d6d6;;
            border-radius: 10px;
            cursor: pointer;
            padding: 10px;
            max-width: 400px; 
        }
        .estudiante-card {
            display: flex;         
            gap: 10px;
            min-width: 400px;
            align-items: center;
        }
        .alumnos-container {
            display: flex;
            flex-direction: row;
            flex-wrap: wrap;
            gap: 10px;
        }
        .TabContainer {
            border-left: 1px solid #d6d6d6;;
            padding-left: 20px;
            min-height: 400px;
        }
        .avatar-est{
            height: 100px;
            width: 80px;
            min-width: 80px;
            border-radius: 10px !important;
            object-fit: cover;
        }
        @media (max-width: 768px) {
            .Historial{               
                grid-template-columns: 100%;
            } 
            .Historial .options-container {
                grid-column: span 1;
            }
            .TabContainer {
                border-left: unset;
                padding-left: unset;                
            }
        }
    `
}
customElements.define('w-estudiantes-view', EstudiantesDetails);
export { EstudiantesDetails };

