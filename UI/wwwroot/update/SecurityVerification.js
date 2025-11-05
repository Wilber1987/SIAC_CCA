//@ts-check
import { ModalMessage } from "../WDevCore/WComponents/ModalMessage.js";
import { ModalVericateAction } from "../WDevCore/WComponents/ModalVericateAction.js";
import { WAlertMessage } from "../WDevCore/WComponents/WAlertMessage.js";
import { WForm } from "../WDevCore/WComponents/WForm.js";
import { WModalForm } from "../WDevCore/WComponents/WModalForm.js";
//@ts-ignore
import { FilterData, ModelProperty } from "../WDevCore/WModules/CommonModel.js";
import { EntityClass } from "../WDevCore/WModules/EntityClass.js";
import { WArrayF } from "../WDevCore/WModules/WArrayF.js";
import { html } from "../WDevCore/WModules/WComponentsTools.js";
import { css } from "../WDevCore/WModules/WStyledRender.js";
import { securityOptions } from "./Model/Estudiantes_ModelComponent.js";


//modelo unico para mostrar el seguro
class Estudiantes_Data_Update_ModelComponent extends EntityClass {
    /** @param {Partial<Estudiantes_Data_Update_ModelComponent>} [props] */
    constructor(props) {
        super(props, 'Update');

        //@ts-ignore   
        for (const prop in props) { this[prop] = props[prop]; };
    }
    /**@type {ModelProperty}*/ SecurityOption = {
        type: 'WRADIO', label: "¿Este estudiante cuenta con seguro vigente hasta diciembre 2026?",
        Dataset: securityOptions,
        require: false
    };
}

class Estudiantes_Data_Update extends EntityClass {
    /** @param {Partial<Estudiantes_Data_Update>} [props] */
    constructor(props) {
        super(props, 'Update');
        //@ts-ignore   
        for (const prop in props) { this[prop] = props[prop]; };
    }
    /**@type {Object.<string, any>?} */  SecurityOption = null;
    /**@type {Number?} */ Id = null;
    /**@type {String?} */ Periodo_Lectivo_Update = null;
    /**@type {String?} */ Nombre_completo = null;
}

window.addEventListener('load', async () => {
    /**@type {Array<Estudiantes_Data_Update>} */
    const estudiantesActualizados = await new Estudiantes_Data_Update().Where(
        // @ts-ignore
        FilterData.In("Id", ...window.EstudiantesIds),
        FilterData.IsNull("SecurityOption")
    )
    console.log(estudiantesActualizados);
    if (estudiantesActualizados.length > 0) {
        const cssStyle = css` 
            .ModalHeader { text-transform: lowercase !important}
            .ModalHeader::first-letter, .group-title::first-letter, *::first-letter{ text-transform: capitalize !important}
        `;
        const forms = estudiantesActualizados.map(estudiante => new WForm({
            Title: `${estudiante.Nombre_completo}`,
            EditObject: estudiante,
            ModelObject: new Estudiantes_Data_Update_ModelComponent(),
            Options: false,
            // @ts-ignore
            CustomStyle: cssStyle.cloneNode(true)
        }))
        const modal = new WModalForm({
            title: "Confirmación de seguro estudiantil",
            // @ts-ignore
            CustomStyle: cssStyle.cloneNode(true),
            ObjectModal: html`<div class="forms-container">
                ${forms}
                <hr>
                <p>*Si selecciona la primera opción: "No, deseo que el colegio lo gestione para este estudiante", no debe presentar nada del seguro al momento de pagar matricular, únicamente la boleta de Autorización de Matrícula</p>
                <hr>
                <div class="options-container">
                    <button class="Btn" onclick="${() => {
                    document.body.append(ModalVericateAction(async () => {
                        for (const estudiante of estudiantesActualizados) {
                            if (!WArrayF.ValidateByModel(estudiante, new Estudiantes_Data_Update_ModelComponent({
                                SecurityOption: {
                                    type: 'WRADIO', require: true
                                }
                            }))) {
                                document.body.append(ModalMessage(`Los datos del estudiante ${estudiante.Nombre_completo}  incompletos`, undefined));
                                return;
                            }
                        }
                        for (const estudiante of estudiantesActualizados) {
                            const response = await estudiante.Update();
                            if (response.status != 200) {
                                WAlertMessage.Danger(response.message, true);
                                return;
                            }
                        }
                        WAlertMessage.Success("Datos confirmados", true);
                        modal.close();
                    }, "¿Está seguro de aceptar estas opciones?"))
                }}">Aceptar</button>
                </div>
            </div>`
        })
        document.body.append(modal)
    }
});