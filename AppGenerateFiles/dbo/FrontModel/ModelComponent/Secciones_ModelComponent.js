//@ts-check
import { EntityClass } from "../../../WDevCore/WModules/EntityClass.js";
import { WAjaxTools, BasicStates } from "../../../WDevCore/WModules/WComponentsTools.js";
//@ts-ignore
import { ModelProperty } from "../../../WDevCore/WModules/CommonModel.js";
import { Docente_materias_ModelComponent }  from './Docente_materias_ModelComponent.js'
import { Estudiante_clases_ModelComponent }  from './Estudiante_clases_ModelComponent.js'
import { Evaluaciones_ModelComponent }  from './Evaluaciones_ModelComponent.js'
class Secciones_ModelComponent extends EntityClass {
   /** @param {Partial<Secciones_ModelComponent>} [props] */
   constructor(props) {
       super(props, 'EntityDbo');
       for (const prop in props) {
           this[prop] = props[prop];
       }
   }
   /**@type {ModelProperty}*/ Id = { type: 'number', primary: true };
   /**@type {ModelProperty}*/ Nombre = { type: 'text' };
   /**@type {ModelProperty}*/ Clase_id = { type: 'number' };
   /**@type {ModelProperty}*/ Docente_id = { type: 'number' };
   /**@type {ModelProperty}*/ Observaciones = { type: 'text' };
   /**@type {ModelProperty}*/ Created_at = { type: 'date' };
   /**@type {ModelProperty}*/ Updated_at = { type: 'date' };
   /**@type {ModelProperty}*/ Docente_materias = { type: 'MasterDetail',  ModelObject: ()=> new Docente_materias_ModelComponent()};
   /**@type {ModelProperty}*/ Estudiante_clases = { type: 'MasterDetail',  ModelObject: ()=> new Estudiante_clases_ModelComponent()};
   /**@type {ModelProperty}*/ Evaluaciones = { type: 'MasterDetail',  ModelObject: ()=> new Evaluaciones_ModelComponent()};
}
export { secciones_ModelComponent }
