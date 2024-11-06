//@ts-check
import { StylesControlsV2, StylesControlsV3, StyleScrolls } from "../WDevCore/StyleModules/WStyleComponents.js"
import { css } from "../WDevCore/WModules/WStyledRender.js";
import { WAppNavigator } from "../WDevCore/WComponents/WAppNavigator.js";
import { WTableComponent } from "../WDevCore/WComponents/WTableComponent.js";
import { html } from "../WDevCore/WModules/WComponentsTools.js";
import { Parientes_ModelComponent } from "./Model/Parientes_ModelComponent.js";
import { Parientes } from "./Model/Parientes.js";
import { UpdateData } from "./Model/UpdateData.js";
import { ModalMessege } from "../WDevCore/WComponents/WForm.js";
import { WModalForm } from "../WDevCore/WComponents/WModalForm.js";
import { Estudiantes } from "./Model/Estudiantes.js";
import { sacramentos } from "./Model/Estudiantes_ModelComponent.js";
/**
 * @typedef {Object} ComponentConfig
 * * @property {Object} [propierty]
 */
class NotificacionMatriculaActualizacion extends HTMLElement {
    /**
     * 
     * @param {ComponentConfig} props 
     */
    constructor(props) {
        super();
        this.props = props;
        this.append(this.CustomStyle);
        this.NavManager = new WAppNavigator({
            NavStyle: "tab",
            Inicialize: true,
            Elements: this.NavElements()
        })
        this.append(
            StylesControlsV2.cloneNode(true),
            StyleScrolls.cloneNode(true),
            StylesControlsV3.cloneNode(true),
            this.NavManager,
        );
        this.Draw();
    }
    Draw = async () => {

    }


    NavElements() {
        return [{
            name: "Tutores invitados", action: () => {
                const modelEntity = new Parientes({ Get: () => modelEntity.GetParientesInvitados() })
                this.ParientesTable = new WTableComponent({
                    ModelObject: new Parientes_ModelComponent({ Ip_ingreso : {type: "text"}}),
                    EntityModel: modelEntity,
                    Options: {
                        Filter: true,
                        //FilterDisplay: true,
                        MultiSelect: true
                    }
                });
                return html`<div class="w-table-container">                    
                    <div class="OptionsContainer">
                        <button class="BtnPrimary" onclick="${(/** @type {any} */ ev) => this.SendNotificaciones(this.ParientesTable)}">
                            Reenviar invitación</button>
                    </div>
                    ${this.ParientesTable}
                </div>`
            }
        }, {
            name: "Tutores que ingresaron", action: () => {
                const modelEntity = new Parientes({ Get: () => modelEntity.GetParientesQueLoguearon() })
                this.ParientesTable = new WTableComponent({
                    ModelObject: new Parientes_ModelComponent({ Ip_ingreso : {type: "text"}}),
                    EntityModel: modelEntity,
                    Options: {
                        Filter: true,
                        MultiSelect: true
                    }
                });
                return html`<div class="w-table-container">
                    ${this.ParientesTable}
                </div>`
            }
        },{
            name: "Tutores que no ingresaron", action: () => {
                const modelEntity = new Parientes({ Get: () => modelEntity.GetParientesQueNoLoguearon() })
                this.ParientesTable = new WTableComponent({
                    ModelObject: new Parientes_ModelComponent({ Ip_ingreso : {type: "text"}}),
                    EntityModel: modelEntity,
                    Options: {
                        Filter: true,
                        MultiSelect: true
                    }
                });
                return html`<div class="w-table-container">
                    ${this.ParientesTable}
                </div>`
            }
        }, {
            name: "Tutores que actualizarón", action: () => {
                const modelEntity = new Parientes({ Get: () => modelEntity.GetParientesQueActulizaron() })
                this.ParientesTable = new WTableComponent({
                    ModelObject: new Parientes_ModelComponent({ Ip_ingreso : {type: "text"}}),
                    EntityModel: modelEntity,
                    Options: {
                        Filter: true,
                        MultiSelect: true,
                        UserActions: [{
                            name: "Ver detalles", action: async ( /** @type {Parientes} */ Pariente) => {
                                /**@type {UpdateData} */
                                // @ts-ignore
                                const response = await new Parientes({ Id: Pariente.Id }).GetUpdatedData();
                                const actualizaciones = html`<div class="element-container">
                                    <h2>Actualizaciones de tutores</h2>
                                    <div class="element-data"> 
                                        ${response?.Parientes?.map(pariente => this.TutorCard(pariente))}
                                    </div>
                                    <h2>Actualizaciones de estudiantes</h2>
                                    <div class="element-data"> 
                                        ${response?.Estudiantes?.map(estudiante => this.EstudianteCard(estudiante))}
                                    </div>
                                    ${this.CustomStyle.cloneNode(true)}
                                    ${StylesControlsV2.cloneNode(true)}                               
                                </div>`;
                                this.append(new WModalForm({
                                    title: "Detalles de actualización",
                                    ObjectModal: actualizaciones
                                }));
                            }
                        }]
                    }
                });
                return html`<div class="w-table-container">                    
                    ${this.ParientesTable}
                </div>`
            }
        }, {
            name: "Envio de invitaciones", action: () => {
                this.ParientesTable = new WTableComponent({
                    ModelObject: new Parientes_ModelComponent(),
                    EntityModel: new Parientes(),
                    Options: {
                        Filter: true,
                        MultiSelect: true
                    }
                });
                return html`<div class="w-table-container">
                    <div class="OptionsContainer">
                        <button class="BtnPrimary" onclick="${(/** @type {any} */ ev) => this.SendNotificaciones(this.ParientesTable)}">Enviar</button>
                    </div>
                    ${this.ParientesTable}
                </div>`
            }
        },]
    }
    /**
     * @param {Estudiantes} estudiante
     */
    EstudianteCard(estudiante) {
        return html`<div class="element-card">
            <div class="element-title"> 
                ${estudiante.Codigo} - ${estudiante.Nombre_completo}
            </div>
            <div class="element-data-container">
                <div class="element-data">
                    <span>Religión:</span>
                    ${estudiante.Religion?.Texto ?? "No especificado"}
                </div>
                <div class="element-data">
                    <span>País:</span>
                    ${estudiante.Pais?.Texto ?? "No especificado"}
                </div>
                <div class="element-data">
                    <span>Región:</span>
                    ${estudiante.Region?.Texto ?? "No especificado"}
                </div>
                <div class="element-data">
                    <span>Vive con:</span>
                    ${estudiante.Vive_con ?? "No especificado" }                    
                </div>
                <div class="element-data">
                    <span>Colegio de procedencia:</span>
                    ${estudiante.Colegio_procede ?? "No especificado"}                    
                </div>
                <div class="element-data">
                    <span>Sacramento:</span>
                    ${sacramentos.find(sacramento => sacramento.id == estudiante.Sacramento)?.descripcion ?? "No especificado"}                    
                </div>
                <div class="element-data">
                    <span>Año de Sacramento:</span>
                    ${estudiante.SacramentoA ?? "No especificado"}                    
                </div> 
                <div class="element-data">
                    <span>Transporte:</span>
                    ${estudiante.Puntos_Transportes?.map(punto => punto.Trayecto).join(" Y ") ?? "No especificado"}                    
                </div>               
                <div class="element-data">
                    <span>Dirección:</span>
                    ${estudiante.Direccion ?? "No especificado"}                    
                </div>  
            </div>        
        </div>`;
    }
    /**
     * @param {Parientes} pariente
     */
    TutorCard(pariente) {
        return html`<div class="element-card">
            <div class="element-title">
                ${pariente.Nombre_completo}
            </div>
            <div class="element-data">
                <span>Identificación:</span>
                ${pariente.Identificacion ?? "No especificado"}
            </div>
            <div class="element-data-container">
                <div class="element-data">
                    <span>Religión:</span>
                    ${pariente.Religion?.Texto ?? "No especificado"}
                </div>
                <div class="element-data">
                    <span>País:</span>
                    ${pariente.Pais?.Texto ?? "No especificado"}
                </div>
                <div class="element-data">
                    <span>Región:</span>
                    ${pariente.Region?.Texto ?? "No especificado"}
                </div>
                <div class="element-data">
                    <span>Título:</span>
                    ${pariente.Titulo?.Texto ?? "No especificado"}
                </div>
                <div class="element-data">
                    <span>Estado civil:</span>
                    ${pariente.Estado_civil?.Texto ?? "No especificado"}
                </div>
                <div class="element-data">
                    <span>Teléfono:</span>
                    ${pariente.Telefono ?? "No especificado"}
                </div>
                <div class="element-data">
                    <span>Celular:</span>
                    ${pariente.Celular ?? "No especificado"}
                </div>
                <div class="element-data">
                    <span>Email:</span>
                    ${pariente.Email ?? "No especificado"}
                </div>
                <div class="element-data">
                    <span>Lugra de trabajo:</span>
                    ${pariente.Lugar_trabajo ?? "No especificado"}
                </div>
                <div class="element-data">
                    <span>Teléfono de trabajo:</span>
                    ${pariente.Telefono_trabajo ?? "No especificado"}
                </div>
                <div class="element-data">
                    <span>Exalumno:</span>
                    ${pariente.Ex_Alumno ?? "No especificado"}                    
                </div>
                <div class="element-data">
                    <span>Año de egreso:</span>
                    ${pariente.EgresoExAlumno ?? "No especificado"}                    
                </div>
                <div class="element-data">
                    <span>Dirección:</span>
                    ${pariente.Direccion ?? "No especificado"}                    
                </div>  
               
            </div>
        </div>`;
    }
    /**
     * Env a notificaciones a los parientes seleccionados en la tabla ParientesTable
     * @param {WTableComponent} [ParientesTable] La tabla de parientes a la que se le van a enviar notificaciones
     */
    async SendNotificaciones(ParientesTable) {
        if (ParientesTable?.selectedItems.length == 0) {
            this.append(ModalMessege("No hay parientes seleccionados", undefined, true));
            return;
        }
        const response = await new UpdateData({ Parientes: ParientesTable?.selectedItems }).Save();
        this.append(ModalMessege(response.message, undefined, true));
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
    `
}
customElements.define('w-notif-mat-actualizacion', NotificacionMatriculaActualizacion);
export { NotificacionMatriculaActualizacion }