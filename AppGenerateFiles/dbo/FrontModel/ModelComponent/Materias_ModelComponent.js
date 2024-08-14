//@ts-check
import { EntityClass } from "../../../WDevCore/WModules/EntityClass.js";
import { WAjaxTools, BasicStates } from "../../../WDevCore/WModules/WComponentsTools.js";
//@ts-ignore
import { ModelProperty } from "../../../WDevCore/WModules/CommonModel.js";
import { Clases_ModelComponent }  from './Clases_ModelComponent.js'
import { Asignaturas_ModelComponent }  from './Asignaturas_ModelComponent.js'
import { Docente_materias_ModelComponent }  from './Docente_materias_ModelComponent.js'
import { Evaluaciones_ModelComponent }  from './Evaluaciones_ModelComponent.js'
class Materias_ModelComponent extends EntityClass {
   /** @param {Partial<Materias_ModelComponent>} [props] */
   constructor(props) {
       super(props, 'EntityDbo');
       for (const prop in props) {
           this[prop] = props[prop];
       }
   }
   /**@type {ModelProperty}*/ Id = { type: 'number', primary: true };
   /**@type {ModelProperty}*/ Observaciones = { type: 'text' };
   /**@type {ModelProperty}*/ Config = { type: 'text' };
   /**@type {ModelProperty}*/ Created_at = { type: 'date' };
   /**@type {ModelProperty}*/ Updated_at = { type: 'date' };
   /**@type {ModelProperty}*/ Lock_version = { type: 'number' };
   /**@type {ModelProperty}*/ Clases = { type: 'WSELECT',  ModelObject: ()=> new Clases_ModelComponent(), ForeignKeyColumn: 'Clase_id'};
   /**@type {ModelProperty}*/ Asignaturas = { type: 'WSELECT',  ModelObject: ()=> new Asignaturas_ModelComponent(), ForeignKeyColumn: 'Asignatura_id'};
   /**@type {ModelProperty}*/ Docente_materias = { type: 'MasterDetail',  ModelObject: ()=> new Docente_materias_ModelComponent()};
   /**@type {ModelProperty}*/ Evaluaciones = { type: 'MasterDetail',  ModelObject: ()=> new Evaluaciones_ModelComponent()};
}
export { materias_ModelComponent }
