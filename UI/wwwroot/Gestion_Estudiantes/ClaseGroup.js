//@ts-check
import { DocumentsData } from "../Model/DocumentsData.js";
import { Estudiante_clases } from "../Model/Estudiante_clases.js";
import { Estudiante_Clases_View } from "../Model/Estudiante_Clases_View.js";
import { Asignatura_Group, Clase_Group, Estudiante_Group, Estudiantes } from "../Model/Estudiantes.js";
import { Calificacion_Group_ModelComponent, Clase_Group_ModelComponent } from "../Model/ModelComponent/Estudiantes_ModelComponent.js";
import { StylesControlsV2 } from "../WDevCore/StyleModules/WStyleComponents.js";
import { WModalForm } from "../WDevCore/WComponents/WModalForm.js";
import { WTableComponent } from "../WDevCore/WComponents/WTableComponent.js";
import { WArrayF } from "../WDevCore/WModules/WArrayF.js";
import { html } from "../WDevCore/WModules/WComponentsTools.js";
import { css } from "../WDevCore/WModules/WStyledRender.js";
import { CalificacionesUtil } from "./CalificacionesUtil.js";
import { BuildHeaderData } from "./EstudiantesComponents.js";
const routeEstudiantes = location.origin + "/Media/Images/estudiantes/";
const HeaderEvaluaciones = ["IB", "IIB", "IS", "IIIB", "IVB", "IIS", "F"];
class ClaseGroup extends HTMLElement {
    /**
     * @param {Clase_Group} response
     * @param {{ 
     *  ModelObject?: Clase_Group_ModelComponent, 
     *  GroupBy?: String,
     *  Estudiante?: Estudiantes,
     *  IsComplete?: Boolean,
     *  WithoutDocente?: Boolean; 
     * Estudiante_Clase_Seleccionado?: Estudiante_clases
     * }} Config
     */
    constructor(response, Config) {
        super();
        this.attachShadow({ mode: 'open' });
        this.shadowRoot?.append(this.CustomStyle);
        this.Config = Config ?? { ModelObject: new Clase_Group_ModelComponent() };
        //this.shadowRoot?.append(this.BuildAsignaturasDetail(modelClass, element, ObjectF, prop, maxDetails))
        const propsData = Object.keys(response)
            .filter(prop => response[prop] != null && response[prop] != undefined && this.isDrawable(response, prop))
            .map(prop => this.BuildPropiertyDetail(response, prop))
        const dataContainer = html`<div class="data-container"></div>`
        propsData.forEach(data => {
            dataContainer.append(data);
        })
        this.shadowRoot?.append(StylesControlsV2.cloneNode(true));
        this.shadowRoot?.append(dataContainer);
    }
    isDrawable(response, prop) {
        if (prop.toUpperCase() == "REPITE" || prop.toUpperCase() == "NIVEL") {
            return false;
        }
        return true;
    }
    connectedCallback() { }
    BuildPropiertyDetail(ObjectF, prop) {
        //console.log(html`<div>${WOrtograficValidation.es(prop)}: ${ObjectF[prop]}</div>`);
        // return html`<div>${WOrtograficValidation.es(prop)}: ${ObjectF[prop]}</div>`;
        // @ts-ignore
        switch (this.Config.ModelObject[prop]?.type.toUpperCase()) {
            case "MASTERDETAIL":
                const isEstudiante = this.Config.GroupBy?.toUpperCase() == "ESTUDIANTE";
                // @ts-ignore
                const modelClass = this.Config.ModelObject[prop].ModelObject.__proto__ == Function.prototype ? this.Config.ModelObject[prop].ModelObject() : this.Config.ModelObject[prop].ModelObject;
                //console.log(this.Config.ModelObject, prop, this.Config.ModelObject[prop], this.Config.ModelObject[prop].ModelObject, ObjectF[prop]);
                const maxDetails = this.Config.IsComplete != true ? 7 : ObjectF[prop].reduce((max, detail) => {
                    const DetailsLength = new modelClass.constructor(detail).Details
                        ? new modelClass.constructor(detail).Details.length : 0;
                    return Math.max(max, DetailsLength);
                }, 0);
                const maxDetailsHeaders = this.Config.IsComplete == true ? null : HeaderEvaluaciones;

                const evaluaciones = CalificacionesUtil.UpdateCalificaciones(ObjectF[prop], maxDetails, maxDetailsHeaders);
                return html`<div class="detail-content">                   
                    ${ObjectF[prop].map(element => {
                    return isEstudiante
                        ? this.BuildEstudianteDetail(modelClass, element, ObjectF, prop, maxDetails)
                        : this.BuildAsignaturasDetail(modelClass, element, ObjectF, prop, maxDetails);
                })}
                <!-- <span class="break-page"></span>-->     
                 <div class="container promedio">
                    <div class="element-description"><span class="value">PROMEDIO</span></div>   
                    <div class="element-details" style="width: 70%; grid-template-columns: repeat(${maxDetails}, ${100 / maxDetails}%);">
                        ${evaluaciones.map(element => html`<label class="element-detail"><span class="value">${element.Promedio.toFixed(1)}</span></label>`)}
                    </div> 
                    <div style="width: 80px"></div> 
                 </div>
                 <div class="details-options container">
                    <div class="element-description"><span class="value">-</span></div>                                  
                    <div class="element-details" style="width: 70%; grid-template-columns: repeat(${maxDetails}, ${100 / maxDetails}%);">
                        ${evaluaciones.map(element => html`<label class="Btn-Mini detalle-btn" onclick="${() => this.ShowEvaluationDetails(element)}">detalle</label>`)}
                    </div>
                    <div style="width: 80px"></div> 
                 </div>               
                </div>`;
            //${this.BuildConsolidado(ObjectF[prop])} TODO REVISAR A POSTERIORI
            default:
                return ""
            //return html`<div class="prop">${WOrtograficValidation.es(prop)}: ${ObjectF[prop]}</div>`;
        }

    }
    BuildConsolidado(Dataset) {
        const consolidado = WArrayF.GroupBy(Dataset.flatMap(c => c.Calificaciones), "EvaluacionCompleta", "Resultado")
        const consolidadoContainer = html`<div class="consolidado-container"></div>`;
        consolidado.forEach(element => {
            if (element.EvalProperty.toUpperCase().includes("EVA") || element.EvalProperty.length < 3) {
                return;
            }
            consolidadoContainer.append(html`<div class="consolidado-detail">
                <div class="eval-prop">${element.EvalProperty}</div>
                <div class="eval-result">
                    <div class="detail"><span class="title">Promedio: </span><span class="value-consolidado">${element.avg.toFixed(2)}</span></div>
                    <div class="detail"><span class="title">Nota máxima: </span><span class="value-consolidado">${element.Max}</span></div>
                    <div class="detail"><span class="title">Nota mínima: </span><span class="value-consolidado">${element.Min}</span></div>
                </div>               
            </div>`)
        });
        return consolidadoContainer;
    }

    BuildAsignaturasDetail(modelClass, element, ObjectF, prop, maxDetails) {
        ///**@type {Asignatura_Group} */
        const instance = new modelClass.constructor(element);
        //this.UpdateCalificaciones(instance, maxDetails);
        const index = ObjectF[prop].indexOf(element);
        //${this.AddTeacherDetail(index, instance)}      se quita porque no quieren la columna
        return html`<div class="container">
            <div class="element-description">
                ${index == 0 ? html`<span class="header">Asignatura</span>` : ""} 
                <span class="value">${instance.Descripcion}</span>                     
            </div>                
            <div class="element-details" style="${"width: 70%;" /*se quita porque no queiren la columna this.Config.WithoutDocente == true ? "width: 70%;" : ""*/} grid-template-columns: repeat(${maxDetails}, ${100 / maxDetails}%);">
                ${instance.Details.map((detail, indexDetail) => { return this.buildDetail(detail, indexDetail, maxDetails, index); })}
            </div>
            <div class="${index == 0 ? "element-options option" : "option"} " > 
                ${index == 0 ? html`<span class="header">-</span>` : ""}
                <label class="Btn-Mini detalle-btn" onclick="${() => this.ShowDetails(element)}">Detalle</label>
            </div>
        </div>`;
    }
    /**
     * @param {Asignatura_Group} instance
     * 
     */
    async ShowDetails(instance) {
        const response = await new Estudiante_Clases_View({
            Estudiante_id: this.Config.Estudiante_Clase_Seleccionado?.Estudiante_id,
            Clase_id: this.Config.Estudiante_Clase_Seleccionado?.Clase_id,
            Nombre_asignatura: instance.Descripcion
        }).GetClaseEstudianteCompleta();

        //const instanced = new modelClass.constructor(response[0]);        
        CalificacionesUtil.UpdateCalificaciones(response.Asignaturas, instance.Calificaciones.length);
        let lastIndex = 0;
        HeaderEvaluaciones.forEach(header => {
            const index = response.Asignaturas[0].Calificaciones.findIndex(c => c.Evaluacion.toUpperCase() == header.toUpperCase());
            console.log(index, header);
            if (index != -1) {
                for (let i = lastIndex; i <= index; i++) {
                    response.Asignaturas[0].Calificaciones[i].Periodo = header;
                }
                lastIndex = index + 1;
            }
        });

        const MateriaDetailEvaluations = html`<div class="MateriaDetailEvaluations"></div>`;
        response.Asignaturas.forEach(asignatura => {
            MateriaDetailEvaluations.append(html`<div class="materia-details-calificaciones"></div>`);
            MateriaDetailEvaluations.append(new WTableComponent({
                Dataset: asignatura.Calificaciones,
                ModelObject: new Calificacion_Group_ModelComponent(),
                maxElementByPage: 100,
                paginate: false,
                isActiveSorts: false,
                CustomStyle: css`.WTable td label {
                    text-transform: uppercase;
                }`,
                Options: {}
            }))
            document.body.append(new WModalForm({
                title: asignatura.Descripcion,
                ObjectModal: MateriaDetailEvaluations
            }));
        })
    }
    AddTeacherDetail(index, instance) {
        return this.Config.WithoutDocente == true ? "" : (html`<div class="element-description">
                ${index == 0 ? html`<span class="header">Docente</span>` : ""} 
                <span class="value">${instance.Docente ?? "--"}</span>
            </div>`);
    }

    BuildEstudianteDetail(modelClass, element, ObjectF, prop, maxDetails) {
        /**@type {Estudiante_Group} */
        const instance = new modelClass.constructor(element);
        const index = ObjectF[prop].indexOf(element);
        return html`<div class="container">
            <div class="element-description">
                ${index == 0 ? html`<span class="header">Estudiante</span>` : ""} 
                <span class="value">${instance.Descripcion}</span>
            </div>
            <div class="element-details" style="width: 70%; grid-template-columns: repeat(${maxDetails}, ${100 / maxDetails}%);">
                ${instance.Details.map((detail, indexDetail) => { return this.buildDetail(detail, indexDetail, maxDetails, index); })}                
            </div>
        </div>`;
    }
    /**
     * Muestra los detalles de las evaluaciones de un estudiante en una clase, 
     * para una asignatura determinada.
     * @param {{ Evaluacion: string, ev:string, EvaluacionCompleta: string }} evalElement - objeto de evaluacion { Evaluacion: "IB", ev:"IB" }
     * contiene las calificaciones del estudiante.
     */
    async ShowEvaluationDetails(evalElement) {
        /**@type {DocumentsData} */
        const documentsData = await new DocumentsData().GetBoletinDataFragments();

        const response = await new Estudiante_Clases_View({
            Estudiante_id: this.Config.Estudiante_Clase_Seleccionado?.Estudiante_id,
            Clase_id: this.Config.Estudiante_Clase_Seleccionado?.Clase_id
        }).GetClaseEstudianteCompleta();
        const asignaturas = []

        response.Asignaturas.forEach(asignatura => {
            const calificaciones = [];
            CalificacionesUtil.UpdateCalificaciones([asignatura], asignatura.Calificaciones.length);
            let lastIndex = 0;
            HeaderEvaluaciones.forEach(header => {
                const index = asignatura.Calificaciones.findIndex(c => c.Evaluacion.toUpperCase() == header.toUpperCase());
                if (index != -1) {
                    for (let i = lastIndex; i <= index; i++) {
                        asignatura.Calificaciones[i].Periodo = header;
                        if (header == evalElement.Evaluacion) {
                            calificaciones.push(asignatura.Calificaciones[i]);
                        }
                    }
                    lastIndex = index + 1;
                }
            });
            asignaturas.push({ Asignatura: asignatura.Descripcion, Calificaciones: calificaciones });
        })
        BuildHeaderData(documentsData.Header, this.Config.Estudiante)
        const columna1 = html`<div class="columna"></div>`
        const columna2 = html`<div class="columna"></div>`
        const containerCalificaciones = html`<div class="consolidado-container-calificaciones">            
            ${documentsData.Header}
            <div class="consolidado-columns-container">
                ${columna1}
                ${columna2}
            </div>           
            <style>
                .consolidado-container-calificaciones {
                    overflow: hidden;  
                    &  .consolidado-columns-container {
                        display: grid;  
                        grid-template-columns: 1fr 1fr;
                        gap: 20px;
                    }                  
                }
                .MateriaDetailEvaluations {     
                    box-sizing: border-box;
                }
            </style>
        </div>`
        // @ts-ignore
        containerCalificaciones.querySelector(".header").append(` - ${evalElement.EvaluacionCompleta}`);
        asignaturas.forEach((asignatura, indexAssignatura) => {
            if (asignatura.Calificaciones.length == 0) {
                return;
            }
            const MateriaDetailEvaluations = html`<div class="MateriaDetailEvaluations">
                <h4>INDICADORES DE LOGROS</h4>
            </div>`;
            asignatura.Consolidados = [];
            const total = asignatura.Calificaciones[asignatura.Calificaciones.length - 1];
            asignatura.Calificaciones.filter(c => c.Resultado != null).forEach((c, index) => {
                const consolidado = {
                    No: index + 1,
                    Resultado: c.Resultado,
                    Pocentage: (parseInt(c.Resultado) / parseInt(total.Resultado)) * 100
                }
                if (c.EvaluacionCompleta == total.EvaluacionCompleta) {
                    consolidado.No = "";
                    consolidado[asignatura.Asignatura] = "Total";
                } else {

                    consolidado[asignatura.Asignatura] = `${c.EvaluacionCompleta} (${consolidado.Pocentage.toFixed(0)}%)`;
                }
                asignatura.Consolidados.push(consolidado);
            })
            MateriaDetailEvaluations.append(html`<div class="materia-details-calificaciones"></div>`);
            const consolidadoModel = {
                No: { type: "text" }
            }
            consolidadoModel[asignatura.Asignatura] = { type: "text" };
            consolidadoModel.Resultado = { type: "text" };
            /* const table = html`<table class="WTable">
                <tr><td>No</td><td>INDICADORES DE LOGROS</td><td></td></tr>
                <tr><td>No</td><td>${asignatura.Asignatura}</td><td></td></tr>
            </table>`
            asignatura.Consolidados.forEach(element => {
                table.append(WRender.Create({
                    tagName: "tr", children: [
                        { tagName: "td", innerText: element.No },
                        { tagName: "td", className: parseInt(element.Pocentage) < 40 ? "text-success" : "text-danger",innerText: element.Evaluacion },
                        { tagName: "td", innerText: element.Resultado }
                    ]
                }))

            });
            MateriaDetailEvaluations.append(table) */
            MateriaDetailEvaluations.append(new WTableComponent({
                Dataset: asignatura.Consolidados,
                ModelObject: consolidadoModel,
                maxElementByPage: 100,
                paginate: false,
                isActiveSorts: false,
                CustomStyle: css`.WTable td label {
                    text-transform: uppercase;
                }
                 .WTable tbody tr:last-child { font-weight: bold !important; }`,
                Options: {}
            }))
            if (indexAssignatura % 2 == 0) {
                columna1.append(MateriaDetailEvaluations);
            } else {
                columna2.append(MateriaDetailEvaluations);
            }
            //containerCalificaciones.append(MateriaDetailEvaluations);
        })
        document.body.append(new WModalForm({
            title: "",
            ObjectModal: containerCalificaciones
        }));
    }

    buildDetail(detail, indexDetail, maxDetails, index) {
        let columStyle = detail.Order == 1
            ? "" : `grid-column-start: ${indexDetail + 1 + ((maxDetails % 2 !== 0 ? maxDetails - 1 : maxDetails) / 2)}`;

        columStyle = detail.Evaluacion.toUpperCase().includes("F") ? `grid-column-end: ${maxDetails + 1}` : columStyle;

        return html`<div class="element-detail" style="">
            <span class="header ${index == 0 ? "" : "hidden"}">
                <span class="tooltip">${detail.EvaluacionCompleta}</span>
                <span>${detail.Evaluacion}</span>
            </span>
            <span class="value">${detail.Resultado}</span>
        </div>`;
    }
    CustomStyle = css`          
        .data-container {
            display: flex;
            row-gap: 20px;
            column-gap: 25px;
            flex-wrap: wrap;
            width: 100%;
            position: relative;
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
        .detalle-btn {
            margin-top: 5px !important;
            display: block;
            height: 15px;
            align-self: center;
            justify-self: center;
            font-weight: 500 !important;
            font-size: 12px !important;
            padding: 5px 5px !important;
            text-transform: capitalize;
            width: 70px !important;
            text-align: center;
            cursor: pointer;
        }
        .element-options {
            display: grid;
            height: 100%;
            grid-template-rows: 50% 50%;
        }
        .promedio {
            font-weight: 700;
        }
        
        .container {
            display: flex;
            flex: 1;
            & .element-description {
                width: 30%;
                display: grid;
                grid-template-rows: 50% 50%;
                position: relative;
            }
            & .element-details {
                display: grid;
                grid-template-columns: repeat(auto-fit, minmax(min-content, 1fr));
                width: 40%;
                & .element-detail {
                    display: flex;
                    flex-direction: column;
                    flex: 1;
                    border-right: solid 1px rgb(239, 240, 242);
                    border-left: solid 1px rgb(239, 240, 242);
                    & .value {
                        position: relative;
                        text-align: right;
                    }
                }
                & .element-detail:last-child { 
                    border-right: none;
                }
            }   
        }        
        .header {
            flex: 1;
            padding: 5px;
            border-bottom: 1px solid #999;
            font-weight: 700;
            text-transform: uppercase;
            padding: 10px;
            position: relative;
           /* & span {
                position: absolute;
                transform: rotate(-50deg) translateY(-0px) translateX(20px);
                background-color: #fff;
                border-bottom: solid 1px #999;
                width: 80px;
                display: flex;
                justify-content: center;
            }*/
        }
       
        .tooltip {
            position: absolute;
            background-color: rgba(0, 0, 0, 0.8);
            color: #fff;
            border-radius: 5px;
            padding: 5px;
            font-size: 0.8rem;
            display: none;
            transform: translateY(100%);
            width: 150px;
            text-align: center;
        }
        
        .header:hover .tooltip {
            display: block;
        }
        .value {
            flex: 1;
            padding: 10px;
        }
        .hidden {
            display: none;
        }
        .prop {            
            text-transform: capitalize;
            font-size: 1rem;
            font-weight: 600;

        }
        .container:nth-of-type(even) {
            background-color: #f8f8f8;
        }  
        .consolidado-container { 
            display: grid;
            flex-direction: column;
            grid-template-columns: calc(50% - 10px) calc(50% - 10px);
            gap: 10px;
            margin-top: 20px;
            padding: 20px;
            width: 100%;
            box-sizing: border-box;
            & .consolidado-detail {
                width: 100%;
                max-width: 600px;                  
                text-transform: uppercase;
                display: grid;
                grid-template-columns:  150px calc(100% - 150px);             
                & .eval-prop {     
                    border: solid 1px #eee;  
                    padding: 5px 10px;         
                }
                & .eval-result { 
                    display: flex;
                    flex-direction: column;  
                    border: solid 1px #eee;
                    & .detail {
                        border-bottom: solid 1px #eee; 
                        display: flex;
                        justify-content: space-between;
                        align-items: center;  
                        & .title { 
                            padding: 5px 10px;
                        }                     
                        & .value-consolidado {
                            text-align: right;  
                            padding: 5px 10px;                          
                        }
                    }                     
                }                
            }
           
        }
        
        .break-page {
            page-break-after: always;
        }
        .page {   
            margin: 40px;
            display: flex;
            flex-wrap: wrap;
            gap: 10px;
            font-size: 12px !important;
            & .prop {
                font-size: 12px !important;
                margin-right: 10px;
            }
            & .detalle-btn {
                display: none  ;
            }
        } 
            
        @media (max-width: 800px) {
            .detail-content .container, .detail-content .element-details {
                flex-direction: column;
                width: 100% !important;
            }
            .option {
                display: flex;
                justify-content: flex-end;
                padding: 5px;          
         }
            .hidden {
                display: block;
            }
            .element-detail {
                flex-direction: row;
            }
            .detail-content .container {
                border: solid 1px rgb(239, 240, 242);
                display: flex;
                flex: 1;
                & .element-description {
                    width: 100%;
                    border: solid 1px rgb(239, 240, 242);
                }
                & .element-details {
                    width: 100%; 
                    border: solid 1px rgb(239, 240, 242);
                }
            }
            .page .hidden{                
                display: none !important;
            }  
        } 
             
     `
}
customElements.define('w-class-detail', ClaseGroup);
export { ClaseGroup };

