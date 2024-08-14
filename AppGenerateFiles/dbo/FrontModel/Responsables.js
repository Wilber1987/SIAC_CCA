//@ts-check
import { EntityClass } from "../../../WDevCore/WModules/EntityClass.js";
import { WAjaxTools, BasicStates } from "../../../WDevCore/WModules/WComponentsTools.js";
//@ts-ignore
import { ModelProperty } from "../../../WDevCore/WModules/CommonModel.js";
import { Estudiantes }  from './Estudiantes.js'
import { Parientes }  from './Parientes.js'
class Responsables extends EntityClass {
   /** @param {Partial<Responsables>} [props] */
   constructor(props) {
       super(props, 'EntityDbo');
       for (const prop in props) {
           this[prop] = props[prop];
       }
   }
   /**@type {Number}*/ Id;
   /**@type {Date}*/ Created_at;
   /**@type {Date}*/ Updated_at;
   /**@type {String}*/ Parentesco;
   /**@type {Estudiantes} ManyToOne*/ Estudiantes;
   /**@type {Parientes} ManyToOne*/ Parientes;
}
export { Responsables }
