//@ts-check
import { EntityClass } from "../../../WDevCore/WModules/EntityClass.js";
import { WAjaxTools, BasicStates } from "../../../WDevCore/WModules/WComponentsTools.js";
//@ts-ignore
import { ModelProperty } from "../../../WDevCore/WModules/CommonModel.js";
import { Docentes }  from './Docentes.js'
class Escolaridades extends EntityClass {
   /** @param {Partial<Escolaridades>} [props] */
   constructor(props) {
       super(props, 'EntityDbo');
       for (const prop in props) {
           this[prop] = props[prop];
       }
   }
   /**@type {Number}*/ Id;
   /**@type {String}*/ Nombre;
   /**@type {Array<Docentes>} OneToMany*/ Docentes;
}
export { Escolaridades }
