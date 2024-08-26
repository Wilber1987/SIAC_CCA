//@ts-check
import { Estudiante_clases } from "../Model/Estudiante_clases.js";
import { Estudiante_Clases_View } from "../Model/Estudiante_Clases_View.js";
import { Asignatura_Group, Calificacion_Group, Estudiantes } from "../Model/Estudiantes.js";
import { Clase_Group } from "../Model/ModelComponent/Estudiantes_ModelComponent.js";
import { html } from "../WDevCore/WModules/WComponentsTools.js";
import { WOrtograficValidation } from "../WDevCore/WModules/WOrtograficValidation.js";
import { css } from "../WDevCore/WModules/WStyledRender.js";
/**
 * @typedef {Object} Config 
    * @property {Clase_Group} ModelObject
    * @property {Function} [action]
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
            const response = await new Estudiante_Clases_View({
                Estudiante_id: element.Estudiante_id,
                Clase_id: element.Clase_id
            }).GetClaseEstudianteConsolidado();

            const classGroup = new ClaseGroup(response, this.Config);
            // @ts-ignore
            content.dataElement = response;
            content.innerHTML = "";
            content.append(classGroup);
        }
        ev.target.className = content.className.includes("active")
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
    constructor(response, Config) {
        super();
        this.attachShadow({ mode: 'open' });
        this.shadowRoot?.append(this.CustomStyle);
        this.Config = Config ?? { ModelObject: new Clase_Group() };
        //this.shadowRoot?.append(this.Builddetail(modelClass, element, ObjectF, prop, maxDetails))
        const propsData = Object.keys(response).map(prop => this.BuildPropiertyDetail(response, prop))
        const dataContainer = html`<div class="data-container"></div>`
        propsData.forEach(data => {
            dataContainer.appendChild(data);
        })
        this.shadowRoot?.append(dataContainer);
    }
    connectedCallback() { }
    BuildPropiertyDetail(ObjectF, prop) {
        //console.log(html`<div>${WOrtograficValidation.es(prop)}: ${ObjectF[prop]}</div>`);
        // return html`<div>${WOrtograficValidation.es(prop)}: ${ObjectF[prop]}</div>`;
        switch (this.Config.ModelObject[prop]?.type.toUpperCase()) {
            case "MASTERDETAIL":
                const modelClass = this.Config.ModelObject[prop].ModelObject.__proto__ == Function.prototype ? this.Config.ModelObject[prop].ModelObject() : this.Config.ModelObject[prop].ModelObject;
                //console.log(this.Config.ModelObject, prop, this.Config.ModelObject[prop], this.Config.ModelObject[prop].ModelObject, ObjectF[prop]);
                const maxDetails = ObjectF[prop].reduce((max, detail) => {
                    const DetailsLength = new modelClass.constructor(detail).Details
                        ? new modelClass.constructor(detail).Details.length : 0;
                    return Math.max(max, DetailsLength);
                }, 0);
                return html`<div class="detail-content">${ObjectF[prop].map(element => {
                    return this.Builddetail(modelClass, element, ObjectF, prop, maxDetails);
                })}</div>`;
            /*new WTableComponent({
                Dataset: ObjectF[prop],
                ModelObject: this.Config.ModelObject[prop].ModelObject,
                Options: {}
            })*/
            default:
                return html`<div class="prop">${WOrtograficValidation.es(prop)}: ${ObjectF[prop]}</div>`;
        }
    }
    Builddetail(modelClass, element, ObjectF, prop, maxDetails) {
        /**@type {Asignatura_Group} */
        const instance = new modelClass.constructor(element);
        this.UpdateCalificaciones(instance, maxDetails);
        const index = ObjectF[prop].indexOf(element);
        return html`<div class="container">
            <div class="element-description">
                ${index == 0 ? html`<span class="header">Asignatura</span>` : ""} 
                <span class="value">${instance.Descripcion}</span>
            </div>
            <div class="element-description">
                ${index == 0 ? html`<span class="header">Docente</span>` : ""} 
                <span class="value">${instance.Docente ?? "--"}</span>
            </div>
            <div class="element-details" style=" grid-template-columns: repeat(${maxDetails}, ${100 / maxDetails}%);">
                ${instance.Details.map((detail, indexDetail) => {
            return this.buildDetail(detail, indexDetail, maxDetails, index);
        })}</div>
        </div>`;
    }


    /**
     * @param {Asignatura_Group} instance
     * @param {number} maxDetails
     */
    UpdateCalificaciones(instance, maxDetails) {
        //console.log(instance.Calificaciones.length, maxDetails);  
        if (instance.Calificaciones.length < maxDetails) {
            let isFirstOrder = instance.Calificaciones[0].Order == 1
            for (let index = 0; index <= (maxDetails - instance.Calificaciones.length + 1); index++) {
                if (isFirstOrder) {
                    instance.Calificaciones.push(new Calificacion_Group({ Evaluacion: "", Resultado: "-" }));
                } else {
                    instance.Calificaciones.unshift(new Calificacion_Group({ Evaluacion: "", Resultado: "-" }));
                }
            }
            const indexF = instance.Calificaciones.findIndex(calificacion => calificacion.Evaluacion === "F");

            if (indexF !== -1) {
                // Sacar el objeto del array
                const objetoF = instance.Calificaciones.splice(indexF, 1)[0];
                // Insertar el objeto al final del array
                instance.Calificaciones.push(objetoF);
            }
        }
        function toRoman(num) {
            const roman = ['I', 'II', 'III', 'IV', 'V', 'VI', 'VII', 'VIII', 'IX', 'X'];
            return roman[num - 1];
        }

        const counters = {};
        // Mapear sobre las calificaciones para modificar la propiedad "Evaluacion"
        const updatedCalificaciones = instance.Calificaciones.map(calificacion => {
            const letra = calificacion.Evaluacion;

            // Incrementar el contador para la letra correspondiente
            if (!counters[letra]) {
                counters[letra] = 1;
            } else {
                counters[letra]++;
            }

            // Actualizar la evaluación con el número romano
            const numeroRomano = toRoman(counters[letra]);
            if (letra.toUpperCase() != "F") {
                calificacion.Evaluacion = `${numeroRomano}${letra}`;
            }
            return calificacion;
        });
        instance.Calificaciones = updatedCalificaciones;
    }

    buildDetail(detail, indexDetail, maxDetails, index) {
        let columStyle = detail.Order == 1
            ? "" : `grid-column-start: ${indexDetail + 1 + ((maxDetails % 2 !== 0 ? maxDetails - 1 : maxDetails) / 2)}`;
        columStyle = detail.Evaluacion.toUpperCase().includes("F") ? `grid-column-end: ${maxDetails + 1}` : columStyle;
        return html`<div class="element-detail" style="">
            <span class="header ${index == 0 ? "" : "hidden"}"><span>${detail.Evaluacion}</span></span>
            <span class="value">${detail.Resultado}</span>
        </div>`;
    }

    CustomStyle = css`   
        .data-container {
            display: flex;
            row-gap: 20px;
            column-gap: 25px;
            flex-wrap: wrap;
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
        .container {
            display: flex;
            flex: 1;
            & .element-description {
                width: 30%;
                display: grid;
                grid-template-rows: 50% 50%;
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
        }        
     `
}
customElements.define('w-class-detail', ClaseGroup);
export { ClaseGroup }