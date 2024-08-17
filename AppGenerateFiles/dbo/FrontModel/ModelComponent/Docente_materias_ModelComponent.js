//@ts-check
import { EntityClass } from "../../../WDevCore/WModules/EntityClass.js";
import { WAjaxTools, BasicStates } from "../../../WDevCore/WModules/WComponentsTools.js";
//@ts-ignore
import { ModelProperty } from "../../../WDevCore/WModules/CommonModel.js";
import { Docentes_ModelComponent }  from './Docentes_ModelComponent.js'
import { Materias_ModelComponent }  from './Materias_ModelComponent.js'
import { Secciones_ModelComponent }  from './Secciones_ModelComponent.js'
class Docente_materias_ModelComponent extends EntityClass {
   /** @param {Partial<Docente_materias_ModelComponent>} [props] */
   constructor(props) {
       super(props, 'EntityDbo');
       for (const prop in props) {
           this[prop] = props[prop];
       }
   }
   /**@type {ModelProperty}*/ Id = { type: 'number', primary: true };
   /**@type {ModelProperty}*/ Created_at = { type: 'date' };
   /**@type {ModelProperty}*/ Updated_at = { type: 'date' };
   /**@type {ModelProperty}*/ Docentes = { type: 'WSELECT',  ModelObject: ()=> new Docentes_ModelComponent(), ForeignKeyColumn: 'Docente_id'};
   /**@type {ModelProperty}*/ Materias = { type: 'WSELECT',  ModelObject: ()=> new Materias_ModelComponent(), ForeignKeyColumn: 'Materia_id'};
   /**@type {ModelProperty}*/ Secciones = { type: 'WSELECT',  ModelObject: ()=> new Secciones_ModelComponent(), ForeignKeyColumn: 'Seccion_id'};
}
export { docente_materias_ModelComponent }
