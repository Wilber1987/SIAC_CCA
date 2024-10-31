//@ts-check
//@ts-check
import { StylesControlsV2, StylesControlsV3, StyleScrolls } from "../WDevCore/StyleModules/WStyleComponents.js"
import { css } from "../WDevCore/WModules/WStyledRender.js";
import { WAppNavigator } from "../WDevCore/WComponents/WAppNavigator.js";
import { UpdateData } from "./Model/UpdateData.js";
import { ComponentsManager, html, WRender } from "../WDevCore/WModules/WComponentsTools.js";
import { Adress, Estudiantes } from "./Model/Estudiantes.js";
import { ModalVericateAction, WForm } from "../WDevCore/WComponents/WForm.js";
import { Adress_ModelComponent, Estudiantes_ModelComponent } from "./Model/Estudiantes_ModelComponent.js";
import { Parientes_ModelComponent } from "./Model/Parientes_ModelComponent.js";
import { Parientes } from "./Model/Parientes.js";
import { WModalForm } from "../WDevCore/WComponents/WModalForm.js";
/**
 * @typedef {Object} ComponentConfig
 * * @property {Object} [propierty]
 */
class UpdateView extends HTMLElement {
    /**
     * 
     * @param {ComponentConfig} props 
     */
    constructor(props) {
        super();
        this.append(this.CustomStyle);
        this.TabContainer = WRender.Create({ className: "TabContainer", id: "content-container" });
        this.Manager = new ComponentsManager({ MainContainer: this.TabContainer, SPAManage: false });
        this.append(
            StylesControlsV2.cloneNode(true),
            StyleScrolls.cloneNode(true),
            StylesControlsV3.cloneNode(true)
        );
        this.UpdateData = new UpdateData();
        this.Draw();
    }
    Draw = async () => {
        // @ts-ignore
        this.UpdateData = await new UpdateData().Get();
        this.NavManager = new WAppNavigator({
            NavStyle: "tab",
            Inicialize: true,
            TabContainer: this.TabContainer,
            Elements: this.NavElements()
        })
        this.append(this.NavManager);
    }


    NavElements() {
        return [{
            name: "Tutores", action: () => {
                return html`<div class="element-container">
                    ${this.UpdateData?.Parientes?.map(pariente =>
                    this.TutorCard(pariente))}
                      ${this.CustomStyle.cloneNode(true)}
                      ${StylesControlsV2.cloneNode(true)}
                </div>`;
            }
        }, {
            name: "Hijos", action: () => {
                return html`<div class="element-container">
                    ${this.UpdateData?.Estudiantes?.map(estudiante =>
                    this.EstudianteCard(estudiante))}
                    ${this.CustomStyle.cloneNode(true)}
                    ${StylesControlsV2.cloneNode(true)}
                </div>`;
            }
        }]
    }
    /**
     * Function to draw a card element for each tutor in the update form
     * @param {Parientes} pariente - The object containing the tutor data
     * @returns {HTMLElement} The card element
     */
    TutorCard(pariente) {
        return html`<div class="element-detail" >
            <div class="element-title">${pariente.Nombre_completo}
                <button class="Btn" onclick="${() => {
                // @ts-ignore
                if (pariente.Form) {
                    // @ts-ignore
                    pariente.Form.DrawComponent();
                }
                this.EditTutor(pariente)
            }}">Actualizar datos</button>
            </div>
        </div>`;
    }

    /**
     * Function to draw a card element for each estudiante in the update form
     * @param {Estudiantes} estudiante - The estudiante object to draw the card
     * @returns {HTMLElement} - The card element
     */
    EstudianteCard(estudiante) {
        const card = html`<div class="element-detail">
            <div class="element-title">
                ${estudiante.Nombre_completo} - ${estudiante.Codigo}
                <button class="Btn" onclick="${() => {
                if (estudiante.IdaVueltaForm && estudiante.IdaVueltaForm.Form) {
                    estudiante.IdaVueltaForm.Form.DrawComponent();
                }
                this.EditEstudiante(estudiante)
            }}">Actualizar datos</button>
            </div>            
        </div>`
        return card;
    }

    /**
     * Generates a form for selecting transportation options (Ida, Vuelta, Ida y Vuelta, or Sin Transporte) for a student. 
     * Updates the student's transport points based on the selected option and allows input of a shared address.
     * 
     * @param {Estudiantes} estudiante - The student object for which the form is being generated.
     * @returns {HTMLElement} - The HTML element containing the transportation form.
     */
    IdaVueltaForm(estudiante) {
        estudiante.Puntos_Transportes = estudiante.Puntos_Transportes ?? [];

        const estudianteIda = estudiante.Puntos_Transportes.find(punto => punto.Trayecto == "IDA") ?? new Adress({ Trayecto: "IDA" });
        const estudianteVuelta = estudiante.Puntos_Transportes.find(punto => punto.Trayecto == "VUELTA") ?? new Adress({ Trayecto: "VUELTA" });

        const inputDireccion = estudiante.IdaVueltaForm?.Direccion
            ? estudiante.IdaVueltaForm.Direccion.cloneNode(true) : html`<textarea placeholder="Dirección" style="display:none">`;

        estudianteIda.Direccion = inputDireccion.value;
        estudianteVuelta.Direccion = inputDireccion.value;

        inputDireccion.onchange = (e) => {
            estudianteIda.Direccion = e.target.value;
            estudianteVuelta.Direccion = e.target.value;
        };
        const radioIda = estudiante.IdaVueltaForm?.Ida
            ? estudiante.IdaVueltaForm.Ida.cloneNode(true) : html`<input id="radioIda" name="idavuelta" type="radio">`;
        const radioVuelta = estudiante.IdaVueltaForm?.Vuelta
            ? estudiante.IdaVueltaForm.Vuelta.cloneNode(true) : html`<input id="radioVuelta" name="idavuelta"  type="radio" >`;
        const radioIdaVuelta = estudiante.IdaVueltaForm?.IdaVuelta
            ? estudiante.IdaVueltaForm.IdaVuelta.cloneNode(true) : html`<input id="radioIdaVuelta" name="idavuelta" type="radio">`;
        const radiioSinIdaVuelta = estudiante.IdaVueltaForm?.SinIdaVuelta
            ? estudiante.IdaVueltaForm.SinIdaVuelta.cloneNode(true) : html`<input id="radiioSinIdaVuelta" name="idavuelta" type="radio">`;

        radioIda.checked = estudiante.Puntos_Transportes.find(punto => punto.Trayecto == "IDA") && !estudiante.Puntos_Transportes.find(punto => punto.Trayecto == "VUELTA");
        radioVuelta.checked = !estudiante.Puntos_Transportes.find(punto => punto.Trayecto == "IDA") && estudiante.Puntos_Transportes.find(punto => punto.Trayecto == "VUELTA");
        radioIdaVuelta.checked = estudiante.Puntos_Transportes.find(punto => punto.Trayecto == "IDA") && estudiante.Puntos_Transportes.find(punto => punto.Trayecto == "VUELTA");
        radiioSinIdaVuelta.checked = !estudiante.Puntos_Transportes.find(punto => punto.Trayecto == "IDA") && !estudiante.Puntos_Transportes.find(punto => punto.Trayecto == "VUELTA");

        estudiante.IdaVueltaForm = {
            Ida: radioIda,
            Vuelta: radioVuelta,
            IdaVuelta: radioIdaVuelta,
            SinIdaVuelta: radiioSinIdaVuelta,
            Direccion: inputDireccion
        }
        const idaVuelta = () => {
            estudiante.Puntos_Transportes = [];
            // @ts-ignore
            if (radioIda.checked == true) {
                inputDireccion.style.display = "block";
                estudiante.Puntos_Transportes = [estudianteIda];
                // @ts-ignore
            } else if (radioVuelta.checked == true) {
                inputDireccion.style.display = "block";
                estudiante.Puntos_Transportes = [estudianteVuelta];
                // @ts-ignore
            } else if (radioIdaVuelta.checked == true) {
                inputDireccion.style.display = "block";
                estudiante.Puntos_Transportes = [estudianteIda, estudianteVuelta];
                //estudiante.Puntos_Transportes.push(estudianteIda);
                // @ts-ignore
            } else if (radiioSinIdaVuelta.checked == true) {
                inputDireccion.style.display = "none";
                estudiante.Puntos_Transportes = [];
            }
            console.log(estudiante.Puntos_Transportes);
        }
        radioIda.onchange = idaVuelta;
        radioVuelta.onchange = idaVuelta;
        radioIdaVuelta.onchange = idaVuelta;
        radiioSinIdaVuelta.onchange = idaVuelta;

        const formIdaYVuelta = html`<div class="form-container">           
            <div class="radio-options-container">          
                <div class="element-option">
                    <label for="radiioSinIdaVuelta">Sin Transporte</label>
                    ${radiioSinIdaVuelta}
                </div>
                <div class="element-option">
                    <label for="radioIda">Ida</label>
                    ${radioIda}
                </div>
                <div class="element-option">
                    <label for="radioVuelta">Vuelta</label>
                    ${radioVuelta}                
                </div>
                <div class="element-option">
                    <label for="radioIdaVuelta">Ida y Vuelta</label>
                    ${radioIdaVuelta}
                </div>            
            </div>
            <div class="element-title">${inputDireccion}</div>
        </div>`;
        return formIdaYVuelta;
    }

    /**
     * Navigates to the detailed view of the given student for editing purposes.
     * It creates a form based on the student's data, allowing the user to update the student's details.
     * Provides options to either save the changes or revert to the original data.
     * 
     * @param {Estudiantes} estudiante - The student object to be edited.
     */
    EditEstudiante(estudiante) {
        const original = new Estudiantes(estudiante);

        const idaVueltaForm = this.IdaVueltaForm(estudiante);
        const form = new WForm({
            ModelObject: new Estudiantes_ModelComponent(),
            EntityModel: new Estudiantes(),
            EditObject: estudiante,
            AutoSave: false,
            StyleForm: "ColumnX1",
            Options: false
        });
        //estudiante.IdaVueltaForm.Form = form;
        this.Manager.NavigateFunction("EstDetail_" + Date.now(), html`<div class="TabContainer">      
            ${this.CustomStyle.cloneNode(true)}      
            <h3>${estudiante.Nombre_completo}</h3>  
            ${form}
            ${idaVueltaForm}
            <div  class="form-options">
                <button class="Btn" onclick="${() => this.regresarEstudiantes(estudiante, original)}">Regresar</button>
                <button class="Btn" onclick="${() => this.GuardarEstudinte(estudiante, original)}">Actualizar datos</button>
            </div>
        </div>`);
    }
    GuardarEstudinte(estudiante, original) {
        this.append(ModalVericateAction(() => {
            for (const prop in estudiante) {
                original[prop] = estudiante[prop];
            }
            this.NavManager?.ActiveTab("Hijos");
            console.log(estudiante);
        }, "¿Esta seguro que desea actualizar los datos del estudiante?"));
    }
    /**
     * Restaura el objeto estudiante con los datos originales antes de editar.
     * @param {Estudiantes} estudiante - El objeto estudiante a ser restaurado.
     * @param {Estudiantes} original - El objeto estudiante con los datos originales.
     */
    regresarEstudiantes(estudiante, original) {
        this.append(ModalVericateAction(() => {
            for (const prop in original) {
                estudiante[prop] = original[prop];
            }
            this.NavManager?.ActiveTab("Hijos");
        }, "¿Esta seguro que desea descartar los cambios?"));
    }

    /**
     * Edita la informacion de un tutor.
     * @param {Parientes} pariente - El objeto pariente a ser editado.
     */
    EditTutor(pariente) {
        const original = new Parientes(pariente);
        const form = new WForm({
            ModelObject: new Parientes_ModelComponent({
                Primer_nombre: undefined,
                Segundo_nombre: undefined,
                Primer_apellido: undefined,
                Segundo_apellido: undefined
            }),
            EntityModel: new Parientes(),
            EditObject: pariente,
            AutoSave: false,
            StyleForm: "ColumnX1",
            Options: false
        })

        this.Manager.NavigateFunction("ParDetail_" + Date.now().toString(), html`<div class="TabContainer">  
            ${this.CustomStyle.cloneNode(true)}          
            <h3>${pariente.Nombre_completo}</h3>
            ${form}
            <div  class="form-options">
                <button class="Btn" onclick="${() => this.regresarPariente(pariente, original)}">Regresar</button>
                <button class="Btn" onclick="${() => this.GuardarPariente(pariente, original)}">Actualizar datos</button>
            </div>
        </div>`);
    }
    GuardarPariente(pariente, original) {
        this.append(ModalVericateAction(() => {
            for (const prop in pariente) {
                original[prop] = pariente[prop];
            }
            this.NavManager?.ActiveTab("Tutores");
        }, "¿Esta seguro que desea actualizar los datos del tutor?"));
    }
    /**
   * Restores the estudiante object with the original data before editing.
   * @param {Parientes} pariente - The estudiante object to be restored.
   * @param {Parientes} original - The estudiante object containing the original data.
   */

    regresarPariente(pariente, original) {
        this.append(ModalVericateAction(() => {
            for (const prop in original) {
                pariente[prop] = original[prop];
            }
            this.NavManager?.ActiveTab("Tutores");
        }, "¿Esta seguro que desea descartar los cambios?"));
    }

    CustomStyle = css`
        w-app-navigator {
            display: flex;
            max-width: 100%;
            max-width: 1000px;
            margin  : auto;
            flex-direction: column;
            gap: 10px;
        }
        w-form, .form-container, .form-options {
            padding: 20px;
            border: #d6d6d6 solid 1px;
            border-radius: 0.2cm;
            background-color: #fff;
            margin-bottom: 10px;
        }
        .form-container {
            height: 80px;
        }
        .form-container, .radio-options-container {
            display: flex;
            gap: 10px;
            align-items: center;
            justify-content: space-between;
        }
        .radio-options-container {
            width: 60%;
        }
        .form-container .element-title {
            width: calc(40% - 10px);
            height: 100%;
            & textarea {
                height: 100%;
            }
        }
        .element-container .element-detail{
           display: block;
           margin: 15px 0px;
           border: #d6d6d6 solid 1px;
           border-radius: 0.2cm;
           padding: 15px;
           background-color: #fff;
           cursor: pointer;
           & .element-title{
               display: flex;
               justify-content: space-between;
               align-items: center;
           }
        }           
    `
}
customElements.define('w-notif-update-actualizacion', UpdateView);
export { UpdateView }