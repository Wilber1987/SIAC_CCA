//@ts-check
import { StylesControlsV2, StylesControlsV3, StyleScrolls } from "../WDevCore/StyleModules/WStyleComponents.js"
import { css } from "../WDevCore/WModules/WStyledRender.js";
import { WAppNavigator } from "../WDevCore/WComponents/WAppNavigator.js";
import { WTableComponent } from "../WDevCore/WComponents/WTableComponent.js";
import { html, WRender } from "../WDevCore/WModules/WComponentsTools.js";
import { ModalMessege } from "../WDevCore/WComponents/WForm.js";
import { WModalForm } from "../WDevCore/WComponents/WModalForm.js";
import { DateTime } from "../WDevCore/WModules/Types/DateTime.js";
import { Estudiantes } from "../update/Model/Estudiantes.js";
import { Estudiantes_ModelComponent } from "../update/Model/Estudiantes_ModelComponent.js";
import { PageType, WReportComponent } from "../WDevCore/WComponents/WReportComponent.js";
import { WAjaxTools } from "../WDevCore/WModules/WAjaxTools.js";
/**
 * @typedef {Object} ComponentConfig
 * * @property {Object} [propierty]
 */
class ReporteRecorridosView extends HTMLElement {
    /**
     * 
     * @param {ComponentConfig} props 
     */
    constructor(props) {
        super();
        this.props = props;
        this.append(this.CustomStyle);

        this.Draw();

    }
    Draw = async () => {
        this.dataEntityRecorridos = await new Estudiantes().GetEstudiantesConRecorridos();
        this.Header = html`<div class="">
            <h2>Informe de Recorridos 2025</h2> 
            <hr/>
        </div>`;

        this.Report = ReportEstudiantesRecorridos(this.dataEntityRecorridos);
        this.append(
            StylesControlsV2.cloneNode(true),
            StyleScrolls.cloneNode(true),
            StylesControlsV3.cloneNode(true),
            this.Header,
            this.Report,
        );
    }

    /**
     * Env a notificaciones a los parientes seleccionados en la tabla ParientesTable
     * @param {WTableComponent} [ParientesTable] La tabla de parientes a la que se le van a enviar notificaciones
     */
    async SendNotificaciones(ParientesTable) {

        //const response = await new UpdateData({ Parientes: ParientesTable?.selectedItems }).Save();
        //this.append(ModalMessege(response.message, undefined, true));
    }

    CustomStyle = css`
        .component{
           display: block;
        }       
        .element-card {
            display: flex;
            flex-direction: column;
            margin: 5px;
            border: 1px solid #888888;
            border-radius: 0.2cm;
            overflow: hidden;
            padding: 10px;
        }
        .element-title {
            font-weight: bold;
            font-size: 16px;
            color: var(--font-secundary-color);
        }
        .element-data-container {
            display: grid;
            grid-template-columns: repeat(3, 1fr);
            gap: 10px;
        }
        .element-data {
            display: flex;
            flex-direction: column;
            font-weight: 500;
            font-size: 16px;
            & span {
                font-size: 12px;
            }
        }
        .tab .elementNavActive, .tab .elementNav {
            & span {
                padding-left: 10px;
                font-weight: bold;
            }
        }
    `
}
customElements.define('w-reporte-recorridos', ReporteRecorridosView);
export { ReporteRecorridosView }

/**
* @param {Array<Estudiantes>} dataEntityRecorridos 
* @returns {any}
*/
export function ReportEstudiantesRecorridos(dataEntityRecorridos) {
    const dataSet = dataEntityRecorridos?.map((estudiante, index) => {
        const responsable = estudiante.Responsables.find(r => r.Parientes.User_id != null).Parientes
        const Clase = estudiante.Estudiante_clases[0].Clases
        const nombre_nivel = `${Clase.Nombre_Grado} ${Clase.Niveles.Nombre_grado}`
        return {
            No: (index + 1).toString(),
            Tutor: responsable.Nombre_completo,
            Nombre_del_Alumno: estudiante.Nombre_completo,
            Correo: responsable.Email,
            Telefono: responsable.Celular,
            Nivel: nombre_nivel,
            Direccion: estudiante.Puntos_Transportes[0].Direccion
        }
    })

    const table = html`<table class="table table-striped mb-0">
        <thead>
            <th>N°</th><th>Tutor</th><th>Nombre del alumno</th><th>Correo</th><th>Télefono</th><th>Nivel</th><th>Dirección</th>           
        </thead>
        <tbody></tbody>
    </table>`
    dataSet.forEach(element => {
        table.append(WRender.Create({
            tagName: "tr", children: [
                { tagName: "td", innerText: element.No },
                { tagName: "td", innerText: element.Tutor },
                { tagName: "td", innerText: element.Nombre_del_Alumno },
                { tagName: "td", innerText: element.Correo },
                { tagName: "td", innerText: element.Telefono },
                { tagName: "td", innerText: element.Nivel },
                { tagName: "td", innerText: element.Direccion }
            ]
        }))
    });
    const encodedText = "Colegio Centro Am&#xE9;rica";
    const parser = new DOMParser();
    const decodedText = parser.parseFromString(encodedText, "text/html").documentElement.textContent;

    return html`<div class="w-table-container">                    
        <div class="OptionsContainer">
            
        </div>
        <div class="">
             ${new WReportComponent({
        Dataset: dataSet,
        Logo: `${localStorage.getItem("MEDIA_IMG_PATH")}${localStorage.getItem("LOGO_PRINCIPAL")}`,
        Header: decodedText ?? "",
        SubHeader: "Listado de alumnos que requieren Recorrido 2025",
        PageType: PageType.OFICIO_HORIZONTAL,
        /*exportXlsAction2: async (htmlNode, filename = "reporteRecorridos") => {
             const htmlString = htmlNode.outerHTML;
             // Codificar la cadena en Base64
             const htmlBase64 = btoa(unescape(encodeURIComponent(htmlString.replace(/rgb\((\d+),\s*(\d+),\s*(\d+)\)/g, (match) => {
                 return rgbToHex(match); // Convierte rgb a hexadecimal
             }))));
             // Crear el objeto para enviar al endpoint
             console.log(htmlBase64);
 
             const payload = {
                 DocumentHtml: htmlBase64
             };
             //const response = await WAjaxTools.PostRequest("../api/ApiReportes/ExportToExcel", payload)
             const response = await fetch('../api/ApiReportes/ExportToExcel', {
                 method: 'POST',
                 headers: {
                     'Content-Type': 'application/json'
                 },
                 body: JSON.stringify(payload)
             });
 
             // Verificar si la respuesta es correcta
             if (!response.ok) {
                 throw new Error(`Error en la exportación: ${response.statusText}`);
             }
 
             // Convertir la respuesta en un blob (archivo)
             const blob = await response.blob();
 
             // Crear un enlace para descargar el archivo
             const url = window.URL.createObjectURL(blob);
             const a = document.createElement('a');
             a.href = url;
             a.download = filename;
             document.body.appendChild(a);
             a.click();
             a.remove();
 
             // Liberar la URL del objeto
             window.URL.revokeObjectURL(url);
         }*/
        //ModelObject: model
    })}
        </div>
       
    </div>`
}

function rgbToHex(rgb) {
    // Regex para extraer valores de RGB
    const result = rgb.match(/^rgb\((\d+),\s*(\d+),\s*(\d+)\)$/);
    if (result) {
        // Convertir valores de RGB a Hexadecimal
        const r = parseInt(result[1]).toString(16).padStart(2, '0');
        const g = parseInt(result[2]).toString(16).padStart(2, '0');
        const b = parseInt(result[3]).toString(16).padStart(2, '0');
        return `#${r}${g}${b}`;
    }
    return rgb; // Si no es RGB, retorna el valor original
}
