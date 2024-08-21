//@ts-check
import { EntityClass } from "../../../WDevCore/WModules/EntityClass.js";
import { WAjaxTools, BasicStates } from "../../../WDevCore/WModules/WComponentsTools.js";
//@ts-ignore
import { ModelProperty } from "../../../WDevCore/WModules/CommonModel.js";
import { Familias }  from './Familias.js'
import { Parientes }  from './Parientes.js'
import { Estudiantes }  from './Estudiantes.js'
class Estudiantes_responsables_familias extends EntityClass {
   /** @param {Partial<Estudiantes_responsables_familias>} [props] */
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
   /**@type {Familias} ManyToOne*/ Familias;
   /**@type {Parientes} ManyToOne*/ Parientes;
   /**@type {Estudiantes} ManyToOne*/ Estudiantes;
}
export { Estudiantes_responsables_familias }
