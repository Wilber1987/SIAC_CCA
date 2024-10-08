//@ts-check

import { ClaseGroup } from "../Gestion_Estudiantes/ClaseGroup.js";
import { Docente_materias } from "../Model/Docente_materias.js";
import { DocumentsData } from "../Model/DocumentsData.js";
import { Estudiante_clases } from "../Model/Estudiante_clases.js";
import { Estudiante_Clases_View } from "../Model/Estudiante_Clases_View.js";
import { Clase_Group, Estudiantes } from "../Model/Estudiantes.js";
import { Clase_Group_ModelComponent, Estudiantes_ModelComponent } from "../Model/ModelComponent/Estudiantes_ModelComponent.js";
import { StylesControlsV2, StylesControlsV3, StyleScrolls } from "../WDevCore/StyleModules/WStyleComponents.js";
import { WAppNavigator } from "../WDevCore/WComponents/WAppNavigator.js";
import { WPrintExportToolBar } from "../WDevCore/WComponents/WPrintExportToolBar.mjs";
import { WTableComponent } from "../WDevCore/WComponents/WTableComponent.js";
import { ComponentsManager, html, WRender } from "../WDevCore/WModules/WComponentsTools.js";
import { css } from "../WDevCore/WModules/WStyledRender.js";

const route = location.origin
const routeDocentes = location.origin + "/Media/Images/docentes"
const routeEstudiantes = location.origin + "/Media/Images/estudiantes"

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
        this.OptionContainer = WRender.Create({ className: "" });
        this.TabContainer = WRender.Create({ className: "", id: 'TabContainer' });
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
        this.append(html`<div class="detalles-container">
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
                name: "Estudiantes", action: async () => {
                    return await this.GetEstudiantes()
                }
            },
            {
                name: "Calificaciones", action: async () => {
                    return await this.GetCalificaciones()
                }
            }, {
                name: "Evaluaciones", action: async () => {
                    return await this.GetCalificacionesCompletas()
                }
            }
        ];
    }
    async GetCalificaciones() {
        if (this.Docente_Materia?.Materias?.Id == undefined) {
            return;
        }
        if (!this.calificacionesResponse) {
            /**@type {Clase_Group} */
            this.calificacionesResponse = await new Estudiante_Clases_View({
                Materia_id: this.Docente_Materia?.Materias?.Id,
                Seccion_id: this.Docente_Materia?.Seccion_id
            }).GetClaseMateriaConsolidado();
        }
        const classGroup = new ClaseGroup(this.calificacionesResponse, { GroupBy: "Estudiante", ModelObject: new Clase_Group_ModelComponent() });
        const optionsBar = await this.buildOptionsBar(classGroup);
        return html`<div style=" display: flex;
            flex-direction: column;
            gap: 10px;">${optionsBar}${classGroup}</div>`;
    }

    async GetCalificacionesCompletas() {
        if (this.Docente_Materia?.Materias?.Id == undefined) {
            return;
        }
        if (!this.calificacionesCompletasResponse) {
            /**@type {Clase_Group} */
            this.calificacionesCompletasResponse = await new Estudiante_Clases_View({
                Materia_id: this.Docente_Materia?.Materias?.Id,
                Seccion_id: this.Docente_Materia?.Seccion_id
            }).GetClaseMateriaCompleta();
        }
        const classGroup = new ClaseGroup(this.calificacionesCompletasResponse, { GroupBy: "Estudiante", ModelObject: new Clase_Group_ModelComponent(), IsComplete: true });
        const optionsBar = await this.buildOptionsBar(classGroup);
        return html`<div style=" display: flex;
            flex-direction: column;
            gap: 10px;">${optionsBar}${classGroup}</div>`;
    }
    /**
     * @param {ClaseGroup} classGroup
     */
    async buildOptionsBar(classGroup) {
        /**@type {DocumentsData} */
        const documentsData = await new DocumentsData().GetDataFragments();
        documentsData.Header.style.width = "100%";
        return new WPrintExportToolBar({
            /*PrintAction: ( toolBar) => {
                 toolBar.Print(html`<div class="page">
                    ${documentsData.Header}   
                    ${documentsData.WatherMark}
                    ${classGroup.shadowRoot?.innerHTML}
                    ${documentsData.Footer}         
                </div>`);
                return;
            }, */ExportPdfAction: (/** @type {WPrintExportToolBar} */ toolBar) => {
               const body = html`<div class="page" style="position:relative">
                    ${documentsData.Header}                    
                    ${classGroup.shadowRoot?.innerHTML}
                    ${documentsData.WatherMark}
                    ${documentsData.Footer}
                </div>`
                
                
                toolBar.ExportPdf(body);
                return;
            },
        });
    }
    async GetEstudiantes() {
        if (this.Docente_Materia?.Materias?.Clase_id == undefined) {
            return;
        }
        if (!this.response) {
            /**@type {Array<Estudiantes>} */
            this.response = await new Estudiantes().GetEstudianBySectionClass(new Estudiante_clases({
                Clase_id: this.Docente_Materia?.Materias?.Clase_id,
                Seccion_id: this.Docente_Materia?.Seccion_id
            }));
        }

        return new WTableComponent({
            Dataset: this.response?.map(e => e, Estudiantes),
            ModelObject: new Estudiantes_ModelComponent(),
            EntityModel: new Estudiantes(),
            ImageUrlPath: routeEstudiantes,
            Options: {
                Search: true,
                UserActions: [{ name: "Ver detalles", action: () => { } }]
            }
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
        .informe-container {
            display: flex;
            flex-direction: column;
            gap: 10px;
        }
        w-app-navigator {
            padding: 30px 0px;
        }
    `
}
customElements.define('w-materia-detail', MateriaDetail);
export { MateriaDetail };
