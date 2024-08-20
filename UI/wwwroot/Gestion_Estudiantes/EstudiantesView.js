
//@ts-check
// @ts-ignore
import { DocumentsData } from "../Model/DocumentsData.js";
import { Clase_Group, Estudiantes } from "../Model/Estudiantes.js";
import { StylesControlsV2, StylesControlsV3, StyleScrolls } from "../WDevCore/StyleModules/WStyleComponents.js";
import { ModalMessege } from "../WDevCore/WComponents/WForm.js";
import { WModalForm } from "../WDevCore/WComponents/WModalForm.js";
import { WPrintExportToolBar } from "../WDevCore/WComponents/WPrintExportToolBar.mjs";
import { ComponentsManager, html, WRender } from "../WDevCore/WModules/WComponentsTools.js";
import { css } from "../WDevCore/WModules/WStyledRender.js";
import { EstudianteDetail } from "./EstudianteDetail.js";

const route = location.origin
const routeEstudiantes = location.origin + "/Media/Images/estudiantes/"
/**
 * @typedef {Object} EstudiantesViewConfig 
    * @property {Array<Estudiantes>} Dataset
**/
class EstudiantesView extends HTMLElement {
    /**
    * @param {EstudiantesViewConfig} Config 
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
                <h6 class="text-muted text-uppercase mb-3">Today</h6>
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
                Seleccione: { type: "select", Dataset: this.EstudianteSeleccionado?.Estudiante_clases.map(c => c.Descripcion) }
            }, EditObject: {}, ObjectOptions: {
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
        if (!this.EstudianteSeleccionado) {
            this.append(ModalMessege("Seleccione un estudiante"));
            return;
        }
        this.append(new WModalForm({
            title: "Imprimir informe clase",
            StyleForm: "columnX1",
            ModelObject: {
                Seleccione: { type: "select", Dataset: this.EstudianteSeleccionado?.Estudiante_clases.map(c => c.Descripcion) }
            }, EditObject: {}, ObjectOptions: {
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
        const id = object.Seleccione?.toString().replaceAll(" ", "");
        /**@type {DocumentsData} */
        const documentsData = await new DocumentsData().GetBoletinDataFragments();
        /**@type {EstudianteDetail} */
        const detail = this.Manager.DomComponents.find(c => c.id === "EstDetail_" + this.EstudianteSeleccionado?.Id);
        // @ts-ignore
        const content = detail.ComponentTab?.Manager?.DomComponents[0];
        const class_element = content.shadowRoot.querySelector(`#${id}`);
        const body = class_element?.cloneNode(true);
        // @ts-ignore
        body.append(content.CustomStyle.cloneNode(true));
        body.append(this.PrintStyle.cloneNode(true));
        documentsData.Header.style.width = "100%"
        const detailContent = html`<div class="detail-content">`;
        class_element.querySelectorAll("w-asignatura-detail").forEach((asig, index) => {
            if (index == 0) {
                detailContent.append(asig.shadowRoot.querySelector(".container").cloneNode(true), asig.shadowRoot.querySelector("style").cloneNode(true))
            } else {
                detailContent.append(asig.shadowRoot.querySelector(".container").cloneNode(true))
            }           
        });      
        body.append(detailContent)  
        const data = html`<div class="page" style="position:relative">
            ${documentsData.Header}
            ${body.innerHTML}
            ${documentsData.WatherMark}
            ${documentsData.Footer}
        </div>`; 
        this.buildData(data, class_element.dataElement);        
        return data;
    }
    /**<td>
                <div>Estudiante: {{ nombre-completo }}</div>
                <div>
                    <label for="">Código: {{ codigo }}</label>
                    <label for="">Repite: {{ repite }}</label>
                    <label for="">Sexo: {{ sexo }}</label>
                </div>
                <div>
                    <label for="">Nivel: {{ nivel }}</label>
                    <label for="">Grado: {{ grado }}</label>
                    <label for="">Sección: {{ seccion }}</label>
                </div>
            </td>
            <td>
                <div>Padre: {{ nombre-padre }}</div>
                <div>Madre: {{ nombre-madre }}</div>
                <div>Guía: {{ nombre-guia }}</div>
            </td> */
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
            border: 1px solid #ede9e9;
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
            border-left: 1px solid #ede9e9;
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
    PrintStyle = css`
    
    @import url(/css/variables.css);
        *{ font-family:  Montserrat, sans-serif;}
        .page {   
            margin: 40px;
            display: flex;
            flex-wrap: wrap;
            gap: 10px;
        }   
        .prop {
            font-size: 12px !important;
            margin-right: 10px;
        }               
               
        .detail-content .container {
            width: -webkit-fill-available;
            & .element-description {
                font-size: 12px;
            }
            & .element-details {
                & .element-detail {
                    font-size: 12px;
                }
            }   
        }        
        .header {
            width: 100%;           
            font-size: 12px;
        }
        .value {            
            font-size: 12px;
        }    
        .accordion-button {
            cursor: pointer;
            position: relative;
            display: -webkit-box;
            display: -ms-flexbox;
            display: flex;
            -webkit-box-align: center;
            -ms-flex-align: center;
            align-items: center;
            padding: 20px 20px;
            font-size: .925rem;
            color: #282c2f;
            text-align: left;
            background-color: var(--bs-accordion-btn-bg);
            border: 0;
            border-radius: 0;
            overflow-anchor: none;
            -webkit-transition: color 0.15s ease-in-out, background-color 0.15s ease-in-out, border-color 0.15s ease-in-out, box-shadow 0.15s ease-in-out, border-radius 0.15s ease;
            transition: var(--bs-accordion-transition);            
            justify-content: space-between;
            text-transform: uppercase;
            font-weight: 600;
            transition: all 0.5s;
        }
        .accordion-button::after {
            -ms-flex-negative: 0;
            flex-shrink: 0;
            width: 14px;
            height: 14px;
            margin-left: auto;
            content: "";
            background-image: url("data:image/svg+xml,%3csvg xmlns='http://www.w3.org/2000/svg' viewBox='0 0 16 16' fill='%23282c2f'%3e%3cpath fill-rule='evenodd' d='M1.646 4.646a.5.5 0 0 1 .708 0L8 10.293l5.646-5.647a.5.5 0 0 1 .708.708l-6 6a.5.5 0 0 1-.708 0l-6-6a.5.5 0 0 1 0-.708z'/%3e%3c/svg%3e");
            background-repeat: no-repeat;
            background-size: 14px;
            transition: all 0.5s;
        }
        
        .active-btn {
            background-color: rgb(210, 222, 244);            
        }
        .active-btn::after {
            transform: rotate(180deg)
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
        .accordion .accordion-button{
            border-bottom: 1px solid #d2d2d2;
        }
        .prop {            
            text-transform: capitalize;
            font-size: 1rem;
        }
        
        .element-content {
            overflow: hidden;
            max-height: 0px;
            padding: 0px 20px;
            transition: all 1s;
            display: flex;
            flex-wrap: wrap;
            gap: 30px;
        }
        .hidden {
            display: none;
        }        
       
        .container:nth-of-type(even) {
            background-color: #f8f8f8;
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
            .replace("{{ repite }}",info.Repite)
            .replace("{{ sexo }}", this.EstudianteSeleccionado.Sexo)
            .replace("{{ nivel }}", info.Nivel)
            .replace("{{ grado }}", info.Descripcion)
            .replace("{{ seccion }}", info.Seccion)
            .replace("{{ nombre-padre }}", this.EstudianteSeleccionado.GetPadre().Name ?? "-")
            .replace("{{ nombre-madre }}", this.EstudianteSeleccionado.GetMadre().Name ?? "-")
            .replace("{{ nombre-guia }}", info.Guia);
        const foto = data.querySelector(".foto");
        if (foto != null) {
            // @ts-ignore
            foto.src = `${routeEstudiantes}/${this.EstudianteSeleccionado.Id}/${this.EstudianteSeleccionado.Foto}`;
        }
    }
}
customElements.define('w-estudiantes-view', EstudiantesView);
export { EstudiantesView }

