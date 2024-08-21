//@ts-check
import { EntityClass } from "../../../WDevCore/WModules/EntityClass.js";
import { WAjaxTools, BasicStates } from "../../../WDevCore/WModules/WComponentsTools.js";
//@ts-ignore
import { ModelProperty } from "../../../WDevCore/WModules/CommonModel.js";
import { Niveles_ModelComponent }  from './Niveles_ModelComponent.js'
import { Docente_asignaturas_ModelComponent }  from './Docente_asignaturas_ModelComponent.js'
import { Materias_ModelComponent }  from './Materias_ModelComponent.js'
class Asignaturas_ModelComponent extends EntityClass {
   /** @param {Partial<Asignaturas_ModelComponent>} [props] */
   constructor(props) {
       super(props, 'EntityDbo');
       for (const prop in props) {
           this[prop] = props[prop];
       }
   }
   /**@type {ModelProperty}*/ Id = { type: 'number', primary: true };
   /**@type {ModelProperty}*/ Nombre = { type: 'text' };
   /**@type {ModelProperty}*/ Nombre_corto = { type: 'text' };
   /**@type {ModelProperty}*/ Observaciones = { type: 'text' };
   /**@type {ModelProperty}*/ Habilitado = { type: 'checkbox' };
   /**@type {ModelProperty}*/ Created_at = { type: 'date' };
   /**@type {ModelProperty}*/ Updated_at = { type: 'date' };
   /**@type {ModelProperty}*/ Orden = { type: 'number' };
   /**@type {ModelProperty}*/ Niveles = { type: 'WSELECT',  ModelObject: ()=> new Niveles_ModelComponent(), ForeignKeyColumn: 'Nivel_id'};
   /**@type {ModelProperty}*/ Docente_asignaturas = { type: 'MasterDetail',  ModelObject: ()=> new Docente_asignaturas_ModelComponent()};
   /**@type {ModelProperty}*/ Materias = { type: 'MasterDetail',  ModelObject: ()=> new Materias_ModelComponent()};
}
export { asignaturas_ModelComponent }
