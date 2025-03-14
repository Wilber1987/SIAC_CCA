//@ts-check
//@ts-check
import { StylesControlsV2, StylesControlsV3, StyleScrolls } from "../WDevCore/StyleModules/WStyleComponents.js"
import { css } from "../WDevCore/WModules/WStyledRender.js";
import { WAppNavigator } from "../WDevCore/WComponents/WAppNavigator.js";
import { UpdateData } from "./Model/UpdateData.js";
import { ComponentsManager, html, WRender } from "../WDevCore/WModules/WComponentsTools.js";
import { Adress, Estudiantes } from "./Model/Estudiantes.js";

import { Adress_ModelComponent, Estudiantes_ModelComponent } from "./Model/Estudiantes_ModelComponent.js";
import { Parientes_ModelComponent } from "./Model/Parientes_ModelComponent.js";
import { Parientes } from "./Model/Parientes.js";
import { WModalForm } from "../WDevCore/WComponents/WModalForm.js";
import { UpdateDataRequest } from "./Model/UpdateDataRequest.js";
import { WArrayF } from "../WDevCore/WModules/WArrayF.js";
import { WForm } from "../WDevCore/WComponents/WForm.js";
import { ModalVericateAction } from "../WDevCore/WComponents/ModalVericateAction.js";
import { ModalMessage } from "../WDevCore/WComponents/ModalMessage.js";
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
            StylesControlsV3.cloneNode(true),
            html`<h3>Actualización de Datos de Familia</h3>`
        );
        this.UpdateData = new UpdateData();
        /**@type {Array<WForm>} */
        this.Forms = [];
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
    OptionsContainer() {
        return WRender.Create({
            className: "OptionsContainer", children: [
                html`<button class="Btn check-icon" onclick="${() => {
                    this.Manager.NavigateFunction("finalizacio-proceso", this.ActualizacionForm());
                }}">Finalizar proceso de actualización</button>`,
            ]
        });
    }


    NavElements() {
        return [{
            name: "Tutores", action: () => {
                return html`<div class="element-container">
                    <div class="element-data">
                        ${this.UpdateData?.Parientes?.map(pariente => this.TutorCard(pariente))}
                    </div>                    
                    ${this.CustomStyle.cloneNode(true)}
                    ${StylesControlsV2.cloneNode(true)}
                    ${this.OptionsContainer()}
                </div>`;
            }
        }, {
            name: "Hijos", action: () => {
                return html`<div class="element-container">
                    <div class="element-data"> 
                        ${this.UpdateData?.Estudiantes?.map(estudiante => this.EstudianteCard(estudiante))}
                    </div>      
                    ${this.CustomStyle.cloneNode(true)}
                    ${StylesControlsV2.cloneNode(true)}
                    ${this.OptionsContainer()}
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
        const original = new Parientes(pariente);
        const form = this.buildUpdateParForm(pariente);
        this.Forms.push(form)
        return html`<div class="element-detail" >
            <div class="element-title">${pariente.Nombre_completo}
                <button class="Btn" onclick="${() => {
                // @ts-ignore
                if (pariente.Form) {
                    // @ts-ignore
                    pariente.Form.DrawComponent();
                }
                this.EditTutor(pariente, original, form)
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
        const original = new Estudiantes(estudiante);
        const idaVueltaForm = this.IdaVueltaForm(estudiante);
        const form = this.buildUpdateEstForm(estudiante);
        this.Forms.push(form);
        const card = html`<div class="element-detail">
            <div class="element-title">
                ${estudiante.Nombre_completo} - ${estudiante.Codigo}
                <button class="Btn" onclick="${() => {
                if (estudiante.IdaVueltaForm && estudiante.IdaVueltaForm.Form) {
                    estudiante.IdaVueltaForm.Form.DrawComponent();
                }
                this.EditEstudiante(estudiante, original, idaVueltaForm, form)
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
                    <label for="radioIda">Solo Ida</label>
                    ${radioIda}
                </div>
                <div class="element-option">
                    <label for="radioVuelta">Solo Vuelta</label>
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
     * Navigates to a new tab with a form to edit the given student's details.
     * The form is created with the student's data and allows the user to update the student's details.
     * The form also has buttons to go back to the list of students and to save the changes.
     * @param {Estudiantes} estudiante - The student object to be edited.
     * @param {Estudiantes} original - The student object with the original data.
     * @param {HTMLElement} idaVueltaForm - The form element for selecting transportation options.
     * @param {WForm} form - The form object for editing the student's details.
     */
    EditEstudiante(estudiante, original, idaVueltaForm, form) {

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
    /**
     * Builds a form for updating the given student's details.
     * It creates a form with the student's data, allowing the user to update the student's details.
     * The form is configured to not auto-save changes and to have a style of a single column (ColumnX1).
     * The form also doesn't display any options.
     * @param {Estudiantes} estudiante - The student object to be edited.
     * @returns {WForm} - The form object.
     */
    buildUpdateEstForm(estudiante) {
        return new WForm({
            ModelObject: new Estudiantes_ModelComponent(),
            EntityModel: new Estudiantes(),
            EditObject: estudiante,
            AutoSave: false,
            StyleForm: "ColumnX1",
            Options: false
        });
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


    EditTutor(pariente, original, form) {

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
    /**
     * Builds and returns a form for updating the information of a given pariente object.
     * @param {Parientes} pariente - The pariente object containing the data to be edited.
     * @returns {WForm} An instance of WForm configured with the pariente's data.
     */
    buildUpdateParForm(pariente) {
        return new WForm({
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
        });
    }

    GuardarPariente(pariente, original) {
        console.log(pariente, original);

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

    ActualizacionForm() {
        const inputTerminosYCondiciones = html`<input type="checkbox" class="inputChecked" id="terminos" name="terminos" value="terminos">`;
        return html`<div class="OptionsContainer">
            ${this.CustomStyle.cloneNode(true)}
            ${StylesControlsV2.cloneNode(true)}
            ${[
                html`<h3>Estimados padres de familia, el contrato debe aceptar por cada estudiante matriculado!</h3>`,
                html`<div class="options-container">
                    ${inputTerminosYCondiciones}
                    <label for="terminos" class="check-label">Acepto contrato para todos mis estudiantes</label>
                </div>`,
                html`<div class="element-container">
                    ${this.UpdateData?.Parientes?.map((pariente, index) => html`<div class="element-detail" >
                    <div class="element-title">${pariente.Nombre_completo}</div>
                </div>`)}
                </div>`,
                html`<section class="WOptionsSection">
                <button class="Btn" onclick="${() => {
                        this.NavManager?.ActiveTab("Hijos");
                    }}">Atrás</button>
                <button class="Btn check-icon" onclick="${() => {
                        document.body.append(new WModalForm({
                            title: "Contrato",
                            ObjectModal: html`<div class="WModalForm">
                                ${this.UpdateData?.Contrato}                                
                            </div>`,
                        }))
                    }}">Ver contrato</button>
                <button class="Btn check-icon" onclick="${() => {
                        document.body.append(new WModalForm({
                            title: "Boleta",
                            ObjectModal: html`<div class="WModalForm">                              
                                ${this.UpdateData?.Boleta == "" ? "No se encuentra boleta de matrícula, ponerse en contacto con el colegio" : this.UpdateData?.Boleta}
                            </div>`,
                        }))
                    }}">Ver boleta</button>
                <button class="Btn check-icon" onclick="${async () => {
                        // @ts-ignore
                        if (inputTerminosYCondiciones.checked != true) {
                            this.append(ModalMessage("Debe aceptar los terminos y condiciones", undefined));
                            return;
                        }
                        for (const pariente of this.UpdateData?.Parientes) {
                            if (!WArrayF.ValidateByModel(pariente, new Parientes_ModelComponent())) {
                                this.append(ModalMessage(`Los datos del pariente ${pariente.Nombre_completo}  incompletos`, undefined));
                                return;
                            }
                        }
                        for (const estudiante of this.UpdateData?.Estudiantes) {
                            if (!WArrayF.ValidateByModel(estudiante, new Estudiantes_ModelComponent())) {
                                this.append(ModalMessage(`Los datos del estudiante ${estudiante.Nombre_completo}  incompletos`, undefined));
                                return;
                            }
                        }
                        /* for (const form of this.Forms) {
                             //await form.DrawComponent();
                             if (!form.Validate()) {
                                 this.append(ModalMessage(`Los datos de ${form.FormObject.Nombre_completo}  incompletos`, undefined));
                                 return;
                             }
                         }*/
                        this.append(ModalVericateAction(async () => {
                            const response = await new UpdateDataRequest({
                                Parientes: this.UpdateData?.Parientes,
                                Estudiantes: this.UpdateData?.Estudiantes,
                                // @ts-ignore
                                AceptaTerminosYCondiciones: inputTerminosYCondiciones.checked
                            }).Save();
                            this.append(ModalMessage(response.message, undefined, true));
                        }, "Está a punto de finalizar el proceso de actualización de datos familiares y de aceptar los terminos y condiciones del contrato. ¿Desea continuar?"));

                    }}">Aceptar</button>
            </section>`
            ]}
        </div>`;
    }

    CustomStyle = css`
        w-app-navigator, .OptionsContainer, h2, h3, h4 {
            display: flex;
            max-width: 100%;
            max-width: 1000px;
            margin  : auto;
            flex-direction: column;
            gap: 10px;
            margin-bottom: 20px;
        }
        w-form, .form-container, .form-options {
            padding: 20px;
            border: #d6d6d6 solid 1px;
            border-radius: 0.2cm;
            background-color: #fff;
            margin-bottom: 10px;
        }
        .element-data {
            min-height: 300px;
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
        .options-container {
            display: flex;
            gap: 10px;
            align-items: center;
            font-size: 16px;
        }
        .inputChecked {
            height: 20px;
            width: 20px;
            background-color: #fff;
            color: #fff;
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