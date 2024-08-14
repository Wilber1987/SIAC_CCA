//@ts-check
import { EntityClass } from "../../../WDevCore/WModules/EntityClass.js";
import { WAjaxTools, BasicStates } from "../../../WDevCore/WModules/WComponentsTools.js";
//@ts-ignore
import { ModelProperty } from "../../../WDevCore/WModules/CommonModel.js";
import { Materias }  from './Materias.js'
import { Secciones }  from './Secciones.js'
import { Docentes }  from './Docentes.js'
class Docente_materias extends EntityClass {
   /** @param {Partial<Docente_materias>} [props] */
   constructor(props) {
       super(props, 'EntityDbo');
       for (const prop in props) {
           this[prop] = props[prop];
       }
   }
   /**@type {Number}*/ Id;
   /**@type {Date}*/ Created_at;
   /**@type {Date}*/ Updated_at;
   /**@type {Materias} ManyToOne*/ Materias;
   /**@type {Secciones} ManyToOne*/ Secciones;
   /**@type {Docentes} ManyToOne*/ Docentes;
}
export { Docente_materias }
