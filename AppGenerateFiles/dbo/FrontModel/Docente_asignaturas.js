//@ts-check
import { EntityClass } from "../../../WDevCore/WModules/EntityClass.js";
import { WAjaxTools, BasicStates } from "../../../WDevCore/WModules/WComponentsTools.js";
//@ts-ignore
import { ModelProperty } from "../../../WDevCore/WModules/CommonModel.js";
import { Docentes }  from './Docentes.js'
import { Asignaturas }  from './Asignaturas.js'
class Docente_asignaturas extends EntityClass {
   /** @param {Partial<Docente_asignaturas>} [props] */
   constructor(props) {
       super(props, 'EntityDbo');
       for (const prop in props) {
           this[prop] = props[prop];
       }
   }
   /**@type {Number}*/ Id;
   /**@type {Boolean}*/ Jefe;
   /**@type {String}*/ Observaciones;
   /**@type {Date}*/ Created_at;
   /**@type {Date}*/ Updated_at;
   /**@type {Docentes} ManyToOne*/ Docentes;
   /**@type {Asignaturas} ManyToOne*/ Asignaturas;
}
export { Docente_asignaturas }
