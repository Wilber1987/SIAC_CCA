//@ts-check
import { Estudiante_clases } from "../Model/Estudiante_clases.js";
import { Estudiante_Clases_View } from "../Model/Estudiante_Clases_View.js";
import { Asignatura_Group, Calificacion_Group, Clase_Group, Estudiante_Group, Estudiantes } from "../Model/Estudiantes.js";
import { Calificaciones_ModelComponent } from "../Model/ModelComponent/Calificaciones_ModelComponent.js";
import { Calificacion_Group_ModelComponent, Clase_Group_ModelComponent } from "../Model/ModelComponent/Estudiantes_ModelComponent.js";
import { StylesControlsV2 } from "../WDevCore/StyleModules/WStyleComponents.js";
import { WModalForm } from "../WDevCore/WComponents/WModalForm.js";
import { WTableComponent } from "../WDevCore/WComponents/WTableComponent.js";
import { WArrayF } from "../WDevCore/WModules/WArrayF.js";
import { html } from "../WDevCore/WModules/WComponentsTools.js";
import { WOrtograficValidation } from "../WDevCore/WModules/WOrtograficValidation.js";
import { css } from "../WDevCore/WModules/WStyledRender.js";
import { CalificacionesUtil } from "./CalificacionesUtil.js";
/**
 * @typedef {Object} Config 
    * @property {Clase_Group_ModelComponent} ModelObject
    * @property {Function} [action]
    * @property {Estudiante_clases} [Estudiante_Clase_Seleccionado]
    * @property {boolean} [FullEvaluation]
    * @property {boolean} [WithoutDocente]
    * @property {Array<Estudiante_clases>} [Dataset]
**/
class ClasesDetails extends HTMLElement {

    /**
    * @param {Config} Config 
    */
    constructor(Config) {
        super();
        this.Config = Config;
        this.attachShadow({ mode: 'open' });

        this.Acordeon = html`<div class="accordion"></div>`;
        this.shadowRoot?.append(this.CustomStyle, this.Acordeon);
        this.Draw();

    }
    connectedCallback() { }
    Draw = async () => {
        // ${Object.keys(element).map(key => this.BuildPropiertyDetail(element, key))}
        this.Config.Dataset?.forEach(element => {
            // @ts-ignore
            const content = html`<div class="element-content"  id="${element.Descripcion?.toString().replaceAll(" ", "")}">
            </div>`;
            // @ts-ignore

            this.Acordeon?.append(html`<div class="element-container">
                <div class="accordion-button" onclick="${(ev) => {
                    this.getClassGroup(ev, content, element);
                }}">${element.Descripcion}</div>
                ${content}
            </div>`)
        });
    }

    /**
     * @param {{ target: { className: string; }; }} ev
     * @param {HTMLElement} content
     * @param {Estudiante_clases} element
     */
    async getClassGroup(ev, content, element) {
        // @ts-ignore
        if (!content.dataElement) {
            let response = new Clase_Group();
            if (this.Config.FullEvaluation != true) {
                response = await new Estudiante_Clases_View({
                    Estudiante_id: element.Estudiante_id,
                    Clase_id: element.Clase_id
                }).GetClaseEstudianteConsolidado();
            } else {
                response = await new Estudiante_Clases_View({
                    Estudiante_id: element.Estudiante_id, Clase_id: element.Clase_id
                }).GetClaseEstudianteCompleta();
            }
            this.Config.Estudiante_Clase_Seleccionado = element;
            const classGroup = new ClaseGroup(response, this.Config);
            // @ts-ignore
            content.dataElement = response;
            content.innerHTML = "";
            content.append(classGroup);
        }
        ev.target.className = ev.target.className.includes("active-btn")
            ? "accordion-button" : "accordion-button active-btn";

        content.className = content.className.includes("active")
            ? "element-content" : "element-content active";
    }
    update() {
        this.Draw();
    }
    CustomStyle = css`@import url(/css/variables.css);
        *{ font-family:  Montserrat, sans-serif;}
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
        .element-content {
            overflow: hidden;
            max-height: 0px;
            padding: 0px 20px;
            transition: all 1s;
            display: flex;
            flex-wrap: wrap;
            gap: 30px;
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
       
     `
}
customElements.define('w-clases-details', ClasesDetails);
export { ClasesDetails }

class ClaseGroup extends HTMLElement {
    /**
     * @param {Clase_Group} response
     * @param {{ 
     *  ModelObject?: Clase_Group_ModelComponent, 
     *  GroupBy?: String,
     *  WithoutDocente?: Boolean; 
     * Estudiante_Clase_Seleccionado?: Estudiante_clases
     * }} Config
     */
    constructor(response, Config) {
        super();
        this.attachShadow({ mode: 'open' });
        this.shadowRoot?.append(this.CustomStyle);
        this.Config = Config ?? { ModelObject: new Clase_Group_ModelComponent() };
        //this.shadowRoot?.append(this.Builddetail(modelClass, element, ObjectF, prop, maxDetails))
        const propsData = Object.keys(response)
            .filter(prop => response[prop] != null && response[prop] != undefined && this.isDrawable(response, prop))
            .map(prop => this.BuildPropiertyDetail(response, prop))
        const dataContainer = html`<div class="data-container"></div>`
        propsData.forEach(data => {
            dataContainer.appendChild(data);
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
                const maxDetails = ObjectF[prop].reduce((max, detail) => {
                    const DetailsLength = new modelClass.constructor(detail).Details
                        ? new modelClass.constructor(detail).Details.length : 0;
                    return Math.max(max, DetailsLength);
                }, 0);

                CalificacionesUtil.UpdateCalificaciones(ObjectF[prop], maxDetails);
                return html`<div class="detail-content">                   
                    ${ObjectF[prop].map(element => {
                    return isEstudiante
                        ? this.BuildEstudianteDetail(modelClass, element, ObjectF, prop, maxDetails)
                        : this.Builddetail(modelClass, element, ObjectF, prop, maxDetails);
                })}
                <!-- <span class="break-page"></span>-->                    
                </div>`;
            //${this.BuildConsolidado(ObjectF[prop])} TODO REVISAR A POSTERIORI
            default:
                return html`<div class="prop">${WOrtograficValidation.es(prop)}: ${ObjectF[prop]}</div>`;
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

    Builddetail(modelClass, element, ObjectF, prop, maxDetails) {
        ///**@type {Asignatura_Group} */
        const instance = new modelClass.constructor(element);
        //this.UpdateCalificaciones(instance, maxDetails);
        const index = ObjectF[prop].indexOf(element);
        return html`<div class="container">
            <div class="element-description">
                ${index == 0 ? html`<span class="header">Asignatura</span>` : ""} 
                <span class="value">${instance.Descripcion}</span>
                <label class="Btn-Mini detalle-btn" onclick="${() => this.ShowDetails(element)}">Detalle</label>                
            </div>
            ${this.AddTeacherDetail(index, instance)}          
            <div class="element-details" style="${this.Config.WithoutDocente == true ? "width: 70%;" : ""} grid-template-columns: repeat(${maxDetails}, ${100 / maxDetails}%);">
                ${instance.Details.map((detail, indexDetail) => {
            return this.buildDetail(detail, indexDetail, maxDetails, index);
        })}</div>
        </div>`;
    }
    /**
     * @param {Asignatura_Group} instance
     */
    async ShowDetails(instance) {
        console.log(instance);

        const response = await new Estudiante_Clases_View({
            Estudiante_id: this.Config.Estudiante_Clase_Seleccionado?.Estudiante_id,
            Clase_id: this.Config.Estudiante_Clase_Seleccionado?.Clase_id,
            Nombre_asignatura: instance.Descripcion
        }).GetClaseEstudianteCompleta();
        
        const MateriaDetailEvaluations = html`<div class="MateriaDetailEvaluations"></div>`;
        response.Asignaturas.forEach(asignatura => {
            MateriaDetailEvaluations.append(html`<div class="materia-details-calificaciones"></div>`);
            MateriaDetailEvaluations.append(new WTableComponent({
                Dataset: asignatura.Calificaciones,
                ModelObject: new Calificacion_Group_ModelComponent(),
                maxElementByPage: 100,
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
                ${instance.Details.map((detail, indexDetail) => {
            return this.buildDetail(detail, indexDetail, maxDetails, index);
        })}</div>
        </div>`;
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
            position: absolute;
            right: 10px;
            bottom: 10px;
            height: 15px;
            font-weight: 500 !important;
            font-size: 10px !important;
            padding: 5px 5px !important;
            text-transform: capitalize;
            cursor: pointer;
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
            font-size: 10px !important;
            & .prop {
                font-size: 11px !important;
                margin-right: 10px;
            }
            & .detalle-btn {
                display: none  ;
            }
        } 
            
        @media (max-width: 800px) {
            .detail-content .container, .detail-content .element-details {
                flex-direction: column;
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
export { ClaseGroup }

