//@ts-check
import { DocumentsData } from "../Model/DocumentsData.js";
import { Estudiante_clases } from "../Model/Estudiante_clases.js";
import { Estudiante_Clases_View } from "../Model/Estudiante_Clases_View.js";
import { Clase_Group, Estudiantes } from "../Model/Estudiantes.js";
import { Clase_Group_ModelComponent } from "../Model/ModelComponent/Estudiantes_ModelComponent.js";
import { WPrintExportToolBar } from "../WDevCore/WComponents/WPrintExportToolBar.mjs";
import { PageType } from "../WDevCore/WComponents/WReportComponent.js";
import { html } from "../WDevCore/WModules/WComponentsTools.js";
import { css } from "../WDevCore/WModules/WStyledRender.js";
import { ClaseGroup } from "./ClaseGroup.js";
import { BuildHeaderData } from "./EstudiantesComponents.js";

const routeEstudiantes = location.origin + "/Media/Images/estudiantes/";
const HeaderEvaluaciones = ["IB", "IIB", "IS", "IIIB", "IVB", "IIS", "F"];
/**
 * @typedef {Object} Config 
	* @property {Clase_Group_ModelComponent} ModelObject
	* @property {Function} [action]
	* @property {Estudiantes} [Estudiante]
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
		this.Config.Dataset?.forEach((element, index) => {
			// @ts-ignore
			const content = html`<div class="element-content"  id="${element.Descripcion?.toString().replaceAll(" ", "")}">
			</div>`;
			const btn = html`<div class="accordion-button" onclick="${(ev) => {
				this.getClassGroup(ev.target, content, element);
			}}">${element.Descripcion}</div>`
			this.Acordeon?.append(html`<div class="element-container">
				
				${content}
			</div>`)
			if (index == 0) {
				// @ts-ignore
				this.getClassGroup(btn, content, element);
			}
		});
	}

	/**
	 * @param {HTMLElement} control
	 * @param {HTMLElement} content
	 * @param {Estudiante_clases} element
	 */
	async getClassGroup(control, content, element) {
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
			content.append(new WPrintExportToolBar({ ExportPdfAction: (toolBar) => this.ExportPdfAction(toolBar, element) }), classGroup);
		}
		control.className = control.className.includes("active-btn")
			? "accordion-button" : "accordion-button active-btn";

		content.className = content.className.includes("active")
			? "element-content" : "element-content active";
	}
	update() {
		this.Draw();
	}

	CustomStyle = css`@import url(/css/variables.css);
		*{ font-family:  Montserrat, sans-serif;}
		w-class-detail {
			width: 100%;
			width: -webkit-fill-available;
		}
		w-tool-bar {
			width: 100%;
			display: block;
		}
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
	/* @param {WPrintExportToolBar} toolBar 
	 
	PrintAction = (toolBar) => {
		if (!this.EstudianteSeleccionado.Id) {
			this.append(ModalMessege("Seleccione un estudiante"));
			return;
		}
		this.append(new WModalForm({
			title: "Imprimir informe clase",
			StyleForm: "columnX1",
			ModelObject: {
				Seleccione: { type: "select", Dataset: this.EstudianteSeleccionado?.Estudiante_clases.map(c => ({ id: c.Clase_id, Descripcion: c.Descripcion })) }
			}, EditObject: {
				Estudiante_id: this.EstudianteSeleccionado.Id,
			}, ObjectOptions: {
				SaveFunction: async (object) => {
					const body = await this.GetActa(object);
					toolBar.Print(body);
					return;
				}
			}
		}));
	};*/
	/**
	* @param {WPrintExportToolBar} toolBar 
	* @param {Estudiante_clases} object 
	*/
	ExportPdfAction = async (toolBar, /** @type {Estudiante_clases} */ object) => {
		const body = await this.GetActa(object);
		toolBar.ExportPdf(body, PageType.A4, false);
		return;
	};

	/**
	 * @param {Estudiante_clases} object
	 */
	async GetActa(object) {
		/**@type {DocumentsData} */
		const documentsData = await new DocumentsData().GetBoletinDataFragments();

		const response = await new Estudiante_Clases_View({
			Estudiante_id: object.Estudiante_id,
			Clase_id: object.Clase_id
		}).GetClaseEstudianteConsolidado();

		const body = new ClaseGroup(response, { ModelObject: new Clase_Group_ModelComponent() });

		documentsData.Header.style.width = "100%";

		const data = html`<div class="page" style="position:relative">
			${documentsData.Header}
			${body.shadowRoot?.innerHTML}
			${documentsData.WatherMark}
			${this.PrintStyle.cloneNode(true)}
			${documentsData.Footer}
		</div>`;
		BuildHeaderData(data, this.Config.Estudiante);
		return data;
	}

	PrintStyle = css`@import url(/css/variables.css);
		*{ font-family:  Montserrat, sans-serif; color: #000 !important; }
		.page {   
			margin: 40px;
			display: flex;
			flex-wrap: wrap;
			gap: 10px;
			font-size: 12px !important;
		}           
		.prop {
			font-size: 12px !important;
			margin-right: 10px;
		}               
			
		.detail-content .container {
			width: -webkit-fill-available;
			display: flex;
			& .element-description {
				font-size: 12px;
			}
			& .element-details {
				& .element-detail {
					font-size: 12px;
				}
			}   
		}        
		.header {
			width: 100%;           
			font-size: 12px;
		}
		.value {            
			font-size: 12px;
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
			flex-direction: row !important;
		}
		.details-options {
			display: none !important;
		}

		.element-content.active {           
			max-height: unset;
			padding: 20px 20px;
		}
		h2, h3, h4 {
			width: 100%;            
		}

		h2 {
			font-size: 16px;
			text-align: center;
		}
		h3 {
			font-size: 14px;
			text-align: center;
		}
		h4 {
			font-size: 12px;
			text-align: left;
		}
		.accordion {
			border: 1px solid #d2d2d2;
			border-radius: 20px;
			overflow: hidden;
		}
		.hidden, .option {
			display: none !important;
		}       
		@media print{
			*{
				-webkit-print-color-adjust: exact !important;
				border-collapse: collapse;
			}
			.page{
				border: none; /* Optional: Remove border for printing */
				margin: 0;
				padding: 0;
				box-shadow: none; /* Optional: Remove any shadow for printing */
				page-break-after: always; /* Ensure each .page-container starts on a new page */
			}
			.detail-content .container, .detail-content .element-details {
				flex-direction: row
			}
			.hidden {
				display: none;
			}
		} 
	`

}
customElements.define('w-clases-details', ClasesDetails);
export { ClasesDetails };

