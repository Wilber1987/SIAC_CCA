
//@ts-check
// @ts-ignore
import { DocumentsData } from "../Model/DocumentsData.js";
import { Estudiante_clases } from "../Model/Estudiante_clases.js";
import { Estudiantes } from "../Model/Estudiantes.js";
import { Clase_Group } from "../Model/ModelComponent/Estudiantes_ModelComponent.js";
import { StylesControlsV2, StylesControlsV3, StyleScrolls } from "../WDevCore/StyleModules/WStyleComponents.js";
import { ModalMessege } from "../WDevCore/WComponents/WForm.js";
import { WModalForm } from "../WDevCore/WComponents/WModalForm.js";
import { WPrintExportToolBar } from "../WDevCore/WComponents/WPrintExportToolBar.mjs";
import { ComponentsManager, html, WRender } from "../WDevCore/WModules/WComponentsTools.js";
import { css } from "../WDevCore/WModules/WStyledRender.js";
import { ClaseGroup } from "./ClasesDetails.js";
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
            this.OptionContainer
        );
        this.Draw();
    }
    connectedCallback() { }
    Draw = async () => {
        const content = html`<section class="Historial">  
            <div class="options-container">
                ${new WPrintExportToolBar({ PrintAction: this.PrintAction, ExportPdfAction: this.ExportPdfAction })}
            </div>         
            <div class="alumnos-container">
                <h6 class="text-uppercase mb-3">Estudiantes</h6>
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
            html`<div class="estudiante-card-container" onclick="${() => this.VerEstudianteDetalles(Estudiante)}">            
                 <div class="d-flex title align-items-center">
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
    }
    PrintAction = () => {
        if (!this.EstudianteSeleccionado.Id) {
            this.append(ModalMessege("Seleccione un estudiante"));
            return;
        }
        this.append(new WModalForm({
            title: "Imprimir informe clase",
            StyleForm: "columnX1",
            ModelObject: {
                Seleccione: { type: "select", Dataset: this.EstudianteSeleccionado?.Estudiante_clases.map(c => ({ id: c.Clase_id, Descripcion: c.Descripcion})) }
            }, EditObject: {
                Estudiante_id: this.EstudianteSeleccionado.Id,               
            },  ObjectOptions: {
                SaveFunction: async (object) => {
                    const body = await this.GetActa(object);
                    //this.append(body); return;
                    const ventimp = window.open(' ', 'popimpr');
                    ventimp?.document.write(body.innerHTML);
                    ventimp?.focus();
                    setTimeout(() => {
                        ventimp?.print();
                        ventimp?.close();
                    }, 100)
                    return;
                }
            }
        }));
    };
    ExportPdfAction = () => {
        if (!this.EstudianteSeleccionado.Id) {
            this.append(ModalMessege("Seleccione un estudiante"));
            return;
        }
        this.append(new WModalForm({
            title: "Imprimir informe clase",
            StyleForm: "columnX1",
            ModelObject: {
                Seleccione: { type: "select", Dataset: this.EstudianteSeleccionado?.Estudiante_clases.map(c => ({ id: c.Clase_id, Descripcion: c.Descripcion})) }
            }, EditObject: {
                Estudiante_id: this.EstudianteSeleccionado.Id,               
            }, ObjectOptions: {
                SaveFunction: async (object) => {
                    const body = await this.GetActa(object);
                    // @ts-ignore
                    html2pdf().from(body).save();
                    return;
                }
            }
        }));
    };

    async GetActa(object) {
        /**@type {DocumentsData} */
        const documentsData = await new DocumentsData().GetBoletinDataFragments();

        const response = await new Estudiante_clases({
            Estudiante_id: object.Estudiante_id,
            Clase_id: object.Seleccione.Seleccione
        }).GetClaseEstudianteConsolidado();
        const body = new ClaseGroup(response, {ModelObject: new Clase_Group()});

        documentsData.Header.style.width = "100%";

        const data = html`<div class="page" style="position:relative">
            ${documentsData.Header}
            ${body.shadowRoot?.innerHTML}
            ${documentsData.WatherMark}
            ${this.PrintStyle.cloneNode(true)}
            ${documentsData.Footer}
        </div>`; 
        // @ts-ignore
        this.buildData(data, response);        
        return data;
    }    
    CustomStyle = css`
        .Historial{
            display: grid;
            grid-template-columns: 300px calc(100% - 320px);
            gap: 20px;
        }   
        .Historial .options-container {
            grid-column: span 2;
        }
        .estudiante-card-container {
            display: block;
            border: 1px solid #d6d6d6;;
            border-radius: 10px;
            cursor: pointer;
            padding: 10px;
        }
        .alumnos-container {
            display: flex;
            flex-direction: column;
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
    PrintStyle = css`@import url(/css/variables.css);
        *{ font-family:  Montserrat, sans-serif; color: #000 !important; }
        .page {   
            margin: 40px;
            display: flex;
            flex-wrap: wrap;
            gap: 10px;
        }   
        .prop {
            font-size: 11px !important;
            margin-right: 10px;
        }               
               
        .detail-content .container {
            width: -webkit-fill-available;
            & .element-description {
                font-size: 11px;
            }
            & .element-details {
                & .element-detail {
                    font-size: 11px;
                }
            }   
        }        
        .header {
            width: 100%;           
            font-size: 11px;
        }
        .value {            
            font-size: 11px;
        }   
        
        .detail-content { 
            display: flex;
            flex-direction: column;
            border-color: rgb(239, 240, 242);
            border-style: solid;
            border-width: 0.8px;
            border-radius: 0.3cm;
            width: 100%;
        } 

        .element-content.active {           
            max-height: unset;
            padding: 20px 20px;
        }

        .accordion {
            border: 1px solid #d2d2d2;
            border-radius: 20px;
            overflow: hidden;
        }
        .hidden {
            display: none !important;
        }       
        @media print{
            *{
                -webkit-print-color-adjust: exact !important;
                border-collapse: collapse;
            }
            .page{
                border: none; /* Optional: Remove border for printing */
                margin: 0;
                padding: 0;
                box-shadow: none; /* Optional: Remove any shadow for printing */
                page-break-after: always; /* Ensure each .page-container starts on a new page */
            }
            .detail-content .container, .detail-content .element-details {
                flex-direction: row
            }
            .hidden {
                display: none;
            }
        } 
    `
    /**
    * @param {HTMLElement} data 
    * @param {Clase_Group} info
    */
    buildData(data, info) {
        //console.log(this.EstudianteSeleccionado);
        
        data.innerHTML = data.innerHTML
            .replace("{{ nombre-completo }}", this.EstudianteSeleccionado.GetNombreCompleto())
            .replace("{{ codigo }}", this.EstudianteSeleccionado.Codigo)
            //.replace("{{ repite }}",info.Repite)
            .replace("{{ sexo }}", this.EstudianteSeleccionado.Sexo)
            //.replace("{{ nivel }}", info.Nivel)
            //.replace("{{ grado }}", info.Descripcion)
            //.replace("{{ seccion }}", info.Seccion)
            .replace("{{ nombre-padre }}", this.EstudianteSeleccionado.GetPadre().Name ?? "-")
            .replace("{{ nombre-madre }}", this.EstudianteSeleccionado.GetMadre().Name ?? "-")
            //.replace("{{ nombre-guia }}", info.Guia);
        const foto = data.querySelector(".foto");
        if (foto != null) {
            // @ts-ignore
            foto.src = `${routeEstudiantes}/${this.EstudianteSeleccionado.Id}/${this.EstudianteSeleccionado.Foto}`;
        }
    }
}
customElements.define('w-estudiantes-view', EstudiantesDetails);
export { EstudiantesDetails }

