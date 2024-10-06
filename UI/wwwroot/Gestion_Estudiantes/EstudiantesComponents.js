//@ts-check
import { Estudiantes } from "../Model/Estudiantes.js";
import { html } from "../WDevCore/WModules/WComponentsTools.js";
const routeEstudiantes = location.origin + "/Media/Images/estudiantes/";
/**
* @param {HTMLElement} data 
*/
const BuildHeaderData = (data, Estudiante) => {
    //console.log(this.EstudianteSeleccionado);

    //.replace("{{ nombre-guia }}", info.Guia);
    const foto = data.querySelector(".foto");
    if (foto != null) {
        // @ts-ignore
        foto.src = `${routeEstudiantes}/${Estudiante?.Id}/${Estudiante?.Foto}`;
    }
    if (Estudiante) {
        const datos = data.querySelector(".datos-generales-header");
        if (datos != null) {
            // @ts-ignore
            datos?.append(DatosGenerales(Estudiante));
        }
    }

}
export { BuildHeaderData };
/**
 * @function DatosGenerales
 * @description Renderiza los datos generales del estudiante
 * @param {Estudiantes} estudiante 
 * @returns {HTMLElement}
 */
const DatosGenerales = (estudiante) => {
    return html`<div class="estudiante-detail">
        <h4 class="header">Datos Generales</h4>
        <div class="estudiante-detail-container">                
            <div class="estudiante-detail-element">
                <label class="text-description">Código:</label> <label>${estudiante.Codigo}</label>
            </div>
            <div class="estudiante-detail-element">
                <label class="text-description">Nivel:</label> <label>${estudiante.Estudiante_clases[0]?.Clases?.Niveles?.Nombre}</label>
            </div>
            <div class="estudiante-detail-element">
                <label class="text-description">Estudiante:</label> <label>${estudiante.Nombre_completo}</label>
            </div>
            <div class="estudiante-detail-element">
                <label class="text-description">Grado:</label> <label>${estudiante.Estudiante_clases[0]?.Clases?.Nombre_Grado}</label>
            </div>
            <div class="estudiante-detail-element">
                <label class="text-description">Docente Guía:</label> <label>${estudiante.Estudiante_clases[0]?.Secciones?.Guia?.Nombre_completo}</label>
            </div>
            <div class="estudiante-detail-element">
                <label class="text-description">Sección:</label> <label>${estudiante.Estudiante_clases[0]?.Secciones?.Nombre}</label>
            </div>
        </div>
        <style>
            .estudiante-detail{
                display: block;
                width: 100%;
                height: 100%;
                box-sizing: border-box;
                margin-bottom: 10px;
                & .estudiante-detail-container{
                    display: grid;
                    grid-template-columns: 1fr 1fr;
                    grid-gap: 10px; 
                    border-radius: 5px;
                    border: 1px solid #c9c9c9;
                    padding: 10px;
                    & label {
                        font-size: 12px;
                        margin: 0;
                    }
                }
            }  
        </style>
    </div>`
}
export { DatosGenerales };
