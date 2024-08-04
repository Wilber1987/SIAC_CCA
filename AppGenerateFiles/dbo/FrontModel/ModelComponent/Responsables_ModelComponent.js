//@ts-check
import { EntityClass } from "../../../WDevCore/WModules/EntityClass.js";
import { WAjaxTools, BasicStates } from "../../../WDevCore/WModules/WComponentsTools.js";
//@ts-ignore
import { ModelProperty } from "../../../WDevCore/WModules/CommonModel.js";
import { Estudiantes_ModelComponent }  from './Estudiantes_ModelComponent.js'
import { Parientes_ModelComponent }  from './Parientes_ModelComponent.js'
class Responsables_ModelComponent extends EntityClass {
   /** @param {Partial<Responsables_ModelComponent>} [props] */
   constructor(props) {
       super(props, 'EntityDbo');
       for (const prop in props) {
           this[prop] = props[prop];
       }
   }
   /**@type {ModelProperty}*/ Id = { type: 'number', primary: true };
   /**@type {ModelProperty}*/ Created_at = { type: 'date' };
   /**@type {ModelProperty}*/ Updated_at = { type: 'date' };
   /**@type {ModelProperty}*/ Parentesco = { type: 'text' };
   /**@type {ModelProperty}*/ Estudiantes = { type: 'WSELECT',  ModelObject: ()=> new Estudiantes_ModelComponent(), ForeignKeyColumn: 'Estudiante_id'};
   /**@type {ModelProperty}*/ Parientes = { type: 'WSELECT',  ModelObject: ()=> new Parientes_ModelComponent(), ForeignKeyColumn: 'Pariente_id'};
}
export { responsables_ModelComponent }
