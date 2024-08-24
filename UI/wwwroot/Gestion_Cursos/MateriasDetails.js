
//@ts-check
// @ts-ignore
import { Materias } from "../Model/Materias.js";
import { DocumentsData } from "../Model/DocumentsData.js";
import { StylesControlsV2, StylesControlsV3, StyleScrolls } from "../WDevCore/StyleModules/WStyleComponents.js";
import { ModalMessege } from "../WDevCore/WComponents/WForm.js";
import { WModalForm } from "../WDevCore/WComponents/WModalForm.js";
import { WPrintExportToolBar } from "../WDevCore/WComponents/WPrintExportToolBar.mjs";
import { ComponentsManager, html, WRender } from "../WDevCore/WModules/WComponentsTools.js";
import { css } from "../WDevCore/WModules/WStyledRender.js";
import { MateriaDetail } from "./MateriaDetail.js";
import { Docente_materias } from "../Model/Docente_materias.js";

const route = location.origin
const routeMaterias = location.origin + "/Media/Images/Materias/"
/**
 * @typedef {Object} MateriasDetailsConfig 
    * @property {Array<Materias>} Dataset
**/
class MateriasDetails extends HTMLElement {
    /**
    * @param {MateriasDetailsConfig} Config 
    */
    constructor(Config) {
        super();
        this.Config = Config      
        this.TabContainer = WRender.Create({ className: "TabContainer", id: 'TabContainer' });
        this.MainContent = WRender.Create({ className: "MainContent", id: 'MainContent' });
        this.Manager = new ComponentsManager({ MainContainer: this.TabContainer, SPAManage: false });
        this.Docentes_materiasSeleccionado = new Docente_materias();
        this.append(this.CustomStyle);
        this.append(
            StylesControlsV2.cloneNode(true),
            StyleScrolls.cloneNode(true),
            StylesControlsV3.cloneNode(true),
            this.MainContent
        );
        this.Draw();
    }
    connectedCallback() { }
    Draw = async () => {
        const content = html`<section class="Historial">  
            <div class="options-container">              
            </div>
            <div class="Materias-container">
                <h6 class="text-uppercase mb-3">Materias</h6>
                ${this.BuildMaterias(this.Config.Dataset)}
            </div>
            ${this.TabContainer}
        </section>`;
        this.MainContent.append(content);
    }
    update() {
        this.MainContent.innerHTML = "";
        this.Draw();
    }
    BuildMaterias(dataset) {
        return dataset.map((/** @type {Materias} */ Materia) =>
            html`<div class="materia-card-container ${Materia?.Clases?.Niveles?.Nombre}">
                <label class="text-uppercase font-size-12 description">
                    <i class="bx bxs-book-content icon nav-icon"></i>
                    ${Materia.Descripcion}</label>
                   <div class="card-options">
                       ${Materia?.Docentes_materias?.map(mdoc => html`<label class="btn-go" onclick="${() => this.VerDocentes_materiasDetalles(mdoc)}">
                           Sección: ${mdoc?.Secciones?.Nombre}
                       </label>`)}
                </div>
            </div>`);
    }
    /**
    * @param {Docente_materias} Docentes_materias 
    */
    async VerDocentes_materiasDetalles(Docentes_materias) {
        /**@type {Docentes_materias} */
        this.Docentes_materiasSeleccionado = await new Docente_materias(Docentes_materias).Find();
        this.Manager.NavigateFunction("MateriaDetail_" + Docentes_materias.Id, new MateriaDetail(this.Docentes_materiasSeleccionado));
    }

    CustomStyle = css`
        .Historial{
            display: grid;
            grid-template-columns: 400px calc(100% - 420px);
            gap: 20px;
        } 
        .Historial .options-container {
            grid-column: span 2;
        }
        .Materias-container, .card-options {
            display: flex;
            flex-direction: column;
            gap: 10px;
        }
        .card-options {
            margin-left: 50px;
            padding: 10px;
        }
        .materia-card-container {
            display: block;
            border: 1px solid #1f58c7;
            border-radius: 10px;
            cursor: pointer;
            cursor: pointer;
        }
        .description {
            border-bottom: 1px solid #1f58c7;
            background-color: #1f58c7;
            font-weight: 600;
            width: 100%;
            margin: 0px;
            font-size: 1rem !important;
            padding: 10px;
            display: flex;
            align-items: center;
            gap: 10px;
            color: #fff;
        }
        .PREESCOLAR, .PREESCOLAR .description {
           border-color: rgb(40 183 101) !important;
        }
        .PRIMARIA, .PRIMARIA .description {
           border-color: rgb(237 85 85) !important;
        }
        .PREESCOLAR .description, .PREESCOLAR .btn-go::after{
            background-color: rgb(40 183 101) !important;
        }
        .PRIMARIA .description,.PRIMARIA .btn-go::after  {
           background-color: rgb(237 85 85) !important;
        }
        
    `
}
customElements.define('w-materias-details', MateriasDetails);
export { MateriasDetails }

