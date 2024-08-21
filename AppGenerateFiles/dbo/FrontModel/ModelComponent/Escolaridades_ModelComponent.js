//@ts-check
import { EntityClass } from "../../../WDevCore/WModules/EntityClass.js";
import { WAjaxTools, BasicStates } from "../../../WDevCore/WModules/WComponentsTools.js";
//@ts-ignore
import { ModelProperty } from "../../../WDevCore/WModules/CommonModel.js";
import { Docentes_ModelComponent }  from './Docentes_ModelComponent.js'
class Escolaridades_ModelComponent extends EntityClass {
   /** @param {Partial<Escolaridades_ModelComponent>} [props] */
   constructor(props) {
       super(props, 'EntityDbo');
       for (const prop in props) {
           this[prop] = props[prop];
       }
   }
   /**@type {ModelProperty}*/ Id = { type: 'number', primary: true };
   /**@type {ModelProperty}*/ Nombre = { type: 'text' };
   /**@type {ModelProperty}*/ Docentes = { type: 'MasterDetail',  ModelObject: ()=> new Docentes_ModelComponent()};
}
export { escolaridades_ModelComponent }
