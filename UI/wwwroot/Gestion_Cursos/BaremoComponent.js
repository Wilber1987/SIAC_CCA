//@ts-check
import { CalificacionesUtil } from "../Gestion_Estudiantes/CalificacionesUtil.js";
import { Clases } from "../Model/Clases.js";
import { Asignatura_Group, Calificacion_Group, Clase_Group } from "../Model/Estudiantes.js";
import { StylesControlsV2 } from "../WDevCore/StyleModules/WStyleComponents.js";
import { WPrintExportToolBar } from "../WDevCore/WComponents/WPrintExportToolBar.mjs";
import { WArrayF } from "../WDevCore/WModules/WArrayF.js";
import { html, WRender } from "../WDevCore/WModules/WComponentsTools.js";
import { css } from "../WDevCore/WModules/WStyledRender.js";
/**
 * @typedef {Object} BaremoComponentConfig 
    * @property {Array<Clase_Group>} Dataset
    * @property {Clases} Clase
**/
class BaremoComponent extends HTMLElement {


    /**
    * @param {BaremoComponentConfig} Config 
    */
    constructor(Config) {
        super();
        this.Config = Config;
        this.append(this.CustomStyle);
        this.OptionContainer = WRender.Create({ className: "baremo-header" });
        this.append(StylesControlsV2.cloneNode(true), this.OptionContainer)
        this.Draw();
    }
    connectedCallback() { }
    Draw = async () => {

        const data = []
        //HEADER
        this.OptionContainer.append(html`<div class="header-container">
            <h3>BAREMO ${this.Config.Clase.Nombre_Grado} GRADO ${this.Config.Clase.Periodo_lectivos.Nombre_corto}</h3>
            ${new WPrintExportToolBar({ ExportCvsAction: (tool) => this.ExportCvsAction(tool, data) })}
            <div class="header-detail"><label class="detail">Nivel:</label><label  class="value">${this.Config.Clase.Niveles.Nombre}</label></div>
            <div class="header-detail"><label class="detail">Grado:</label><label  class="value">${this.Config.Clase.Nombre_Grado}</label></div>
        </div>`);

        //BODY
        const containerEstudiante = html`<div class="baremo-estudiante"></div>`
        const asignaturas = this.Config.Dataset?.flatMap(d => d.Estudiantes).flatMap(e => e.Asignaturas);

        const groupAsignatura = WArrayF.GroupBy(asignaturas ?? [], "Descripcion_Corta");
        const repeatColumns = `grid-template-columns: repeat(${groupAsignatura.length},auto)`;

        const containerBaremo1 = html`<div class="container-baremo baremo1" style="${repeatColumns}"></div>`
        const containerBaremo2 = html`<div class="container-baremo baremo2" style="${repeatColumns}"></div>`
        const containerBaremoResult = html`<div class="container-baremo baremo-result" style="${repeatColumns}"></div>`
        //botones
        const btnDesplegarB1 = html`<button class="vertical-acordeon-btn" onclick="${(ev) => displayVerticalAcordeon(ev)}">Desplegar</button>`;
        const btnDesplegarB2 = html`<button class="vertical-acordeon-btn" onclick="${(ev) => displayVerticalAcordeon(ev)}">Desplegar</button>`;
        const btnDesplegarResult = html`<button class="vertical-acordeon-btn" onclick="${(ev) => displayVerticalAcordeon(ev)}">BAREMO</button>`;
        containerBaremo1.append(btnDesplegarB1);
        containerBaremo2.append(btnDesplegarB2);
        containerBaremoResult.append(btnDesplegarResult);

        const container = html`<div class="baremo-container">
            ${containerEstudiante}
            ${containerBaremo1}
            ${containerBaremo2}
            ${containerBaremoResult}
        </div>`
        this.Config.Dataset?.forEach((clase_Group, groupIndex) => {

            clase_Group.Estudiantes.forEach((estudiante, estudianteIndex) => {
                const headers = []
                const headersB1 = []
                const headersB2 = []
                const headersBR = []
                if (groupIndex == 0) {
                    containerEstudiante.append(html`<label class="estudiante-header">Sección</label>`)
                    headers.push({ value: "Sección" });
                    headers.push({ value: "Nombre y apellidos" });
                    headers.push({ value: "Sexo" });
                    headers.push({ value: "Estado" });

                }
                const row = []
                const rowB1 = []
                const rowB2 = []
                const rowBR = []
                const styleLabel = estudianteIndex % 2 == 0 ? "label1" : "label2";

                containerEstudiante.append(html`<div class="estudiante-name ${styleLabel}">
                    <label>${clase_Group.Seccion}</label> 
                    <label>${estudiante.Descripcion}</label> 
                    <label>${estudiante.Sexo.toUpperCase()}</label> 
                    <label>${estudiante.Estado}</label> 
                </div>`);
                row.push({ value: clase_Group.Seccion })
                row.push({ value: estudiante.Descripcion })
                row.push({ value: estudiante.Sexo.toUpperCase() })
                row.push({ value: estudiante.Estado })

                CalificacionesUtil.UpdateCalificaciones(estudiante.Asignaturas);
                //console.log(estudiante.Asignaturas, groupAsignatura);

                groupAsignatura.forEach(asignaturasG => {
                    const asignatura = estudiante.Asignaturas.find(ea => ea.Descripcion_Corta == asignaturasG?.EvalProperty) ?? new Asignatura_Group();

                    const asignaturaColumnB1 = html`<div class="baremo-asignatura b1-asignatura"></div>`
                    const asignaturaColumnB2 = html`<div class="baremo-asignatura b2-asignatura"></div>`
                    const asignaturaColumnB3 = html`<div class="baremo-asignatura br-asignatura"></div>`
                    const calificacion = this.GetBaremoCal(asignatura);

                    if (estudianteIndex == 0 && groupIndex == 0) {
                        asignaturaColumnB1.append(html`<label class="asig-name">${asignatura.Descripcion_Corta}</label>`)
                        asignaturaColumnB2.append(html`<label class="asig-name">${asignatura.Descripcion_Corta}</label>`)
                        asignaturaColumnB3.append(html`<label class="asig-name">${asignatura.Descripcion_Corta}</label>`)
                        headersB1.push({ value: asignatura.Descripcion_Corta });
                        headersB2.push({ value: asignatura.Descripcion_Corta });
                        headersBR.push({ value: asignatura.Descripcion_Corta });
                        if (calificacion.length == 2) {
                            btnDesplegarB1.innerText = calificacion[0].EvaluacionCompleta;
                            btnDesplegarB2.innerText = calificacion[1].EvaluacionCompleta;
                        }
                    }
                    if (calificacion.length == 2) {
                        const result = -2 * (calificacion[0].Resultado + (calificacion[1].Resultado / 2) - (120))
                        asignaturaColumnB1.append(html`<label class="asig-value ${styleLabel} ${asignatura.Descripcion_Corta} ${calificacion[0].Resultado >= 60 ? "aprob" : "reprob"}">
                            ${calificacion[0].Resultado}</label>`)
                        asignaturaColumnB2.append(html`<label class="asig-value b2 ${styleLabel} ${asignatura.Descripcion_Corta}  ${calificacion[1].Resultado >= 60 ? "aprob" : "reprob"}">
                            ${calificacion[1].Resultado}</label>`)
                        asignaturaColumnB3.append(html`<label class="asig-value br ${styleLabel} ${asignatura.Descripcion_Corta} ${result >= 60 ? "resultMayor60" : "resultMenor60"}">
                            ${result}</label>`)
                        rowB1.push({ value: calificacion[0].Resultado })
                        rowB2.push({ value: calificacion[1].Resultado })
                        rowBR.push({ value: result })
                    } else if (calificacion.length == 1) {
                        asignaturaColumnB1.append(html`<label class="asig-value ${styleLabel} ${asignatura.Descripcion_Corta}">${calificacion[0].Resultado}</label>`)
                        asignaturaColumnB2.append(html`<label class="asig-value b2 ${styleLabel} ${asignatura.Descripcion_Corta}">-</label>`)
                        asignaturaColumnB3.append(html`<label class="asig-value br ${styleLabel} ${asignatura.Descripcion_Corta}">-</label>`)
                        rowB1.push({ value: calificacion[0].Resultado })
                        rowB2.push({ value: "-" })
                        rowBR.push({ value: "-" })
                    } else {
                        asignaturaColumnB1.append(html`<label class="asig-value ${styleLabel} ${asignatura.Descripcion_Corta}">-</label>`)
                        asignaturaColumnB2.append(html`<label class="asig-value b2 ${styleLabel} ${asignatura.Descripcion_Corta}">-</label>`)
                        asignaturaColumnB3.append(html`<label class="asig-value br ${styleLabel} ${asignatura.Descripcion_Corta}">-</label>`)
                        rowB1.push({ value: "-" })
                        rowB2.push({ value: "-" })
                        rowBR.push({ value: "-" })
                    }
                    containerBaremo1.append(asignaturaColumnB1)
                    containerBaremo2.append(asignaturaColumnB2)
                    containerBaremoResult.append(asignaturaColumnB3)
                });
                if (estudianteIndex == 0 && groupIndex == 0) {
                    data.push([...headers,
                    ...headersB1,
                    ...headersB2,
                    ...headersBR])
                }
                data.push([...row,
                ...rowB1,
                ...rowB2,
                ...rowBR])
            });
        })
        console.log(data);

        this.append(container);

        // @ts-ignore
        //const estudiantes = Object.groupBy(this.Config.Dataset?.Estudiantes, (groupData) => groupData.Descripcion)
        //console.log(estudiantes);

    }
    /**
     * @param {import("../Model/Estudiantes.js").Asignatura_Group} asignatura
     * @returns {Array<Calificacion_Group>}
     */
    GetBaremoCal(asignatura) {
        const NotaSemestral = asignatura.Calificaciones?.filter(c => c.Evaluacion.toUpperCase().includes("S") && c.Resultado != null) ?? [];
        //console.log(NotaSemestral);

        if (NotaSemestral.length == 2) {
            return NotaSemestral;
        } else if (NotaSemestral.length == 1) {
            const NotaBimestral = asignatura.Calificaciones.filter(c => c.Evaluacion.toUpperCase().includes("B") && c.Resultado != null);
            if (NotaBimestral.length > 2) {
                /**si ya se evaluo el primer semestre y el tercer bimestre retorno el primer semestre y tercer bimestre */
                return [NotaSemestral[0], NotaBimestral[2]]
            }
            return [NotaSemestral[0]];
        }
        return [];
    }
    /**
     * @param {WPrintExportToolBar} tool
     * @param {Array} data
     */
    ExportCvsAction(tool, data) {
        tool.exportToCsv("classReport", data)
    }

    update() {
        this.Draw();
    }
    CustomStyle = css`
        w-baremo-component {           
            display: block;
            font-size: 11px;
            width: 100%;
            box-sizing: border-box;
            overflow-x: auto;
            & label {
                margin: 0px;
                padding: 8px;
            }  
            & label.reprob {
                color: red;
            }  
            & label.resultMayor60 {
                background-color: #f5caca;
            }  
            & .baremo-header {
                border-bottom: solid 1px #999;
            }
        }  
        
        .baremo-container {
            display: grid;
            grid-template-columns: repeat(4,auto);
            min-height: 400px;
            width: max-content;
        } 
        .baremo-estudiante {
            display: flex;
            flex-direction: column;
            padding-top: 30px;
        } 
        .estudiante-name {
            display: grid;
            grid-template-columns: 30px calc(100% - 100px) 30px 30px;
            gap: 10px;
            box-sizing: border-box;
            width: auto;
            min-width: 100%;
        }
        .container-baremo {
            position: relative;
            display: grid;
            border-left: solid 1px #999;
            width: max-content;
            overflow-x: hidden;
            transition: all 0.5s;
            padding-top: 30px;
            & .vertical-acordeon-btn {
                position: absolute;
                top: 0px;
            }
        }
        .label1 {
            background-color: #eee;
        }
        .baremo-asignatura {
           
            display: flex;
            flex-direction: column;
            & .asig-name {
                background-color: #005ea1;
                color: #fff;
            }
        }
     `
}
customElements.define('w-baremo-component', BaremoComponent);
export { BaremoComponent }

/**
 * @param {{ target: { parentNode: any; className: string; }; }} ev
 */
function displayVerticalAcordeon(ev) {
    const parentNode = ev.target.parentNode;
    ev.target.className = ev.target.className.includes("btnactive") ? "vertical-acordeon-btn" : "vertical-acordeon-btn btnactive";
    parentNode.style.maxWidth = parentNode.style.maxWidth.includes("150px") ? "max-content" : "150px";
}
