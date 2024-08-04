//@ts-check
import { EntityClass } from "../../../WDevCore/WModules/EntityClass.js";
import { WAjaxTools, BasicStates } from "../../../WDevCore/WModules/WComponentsTools.js";
//@ts-ignore
import { ModelProperty } from "../../../WDevCore/WModules/CommonModel.js";
import { Niveles }  from './Niveles.js'
import { Docente_asignaturas }  from './Docente_asignaturas.js'
import { Materias }  from './Materias.js'
class Asignaturas extends EntityClass {
   /** @param {Partial<Asignaturas>} [props] */
   constructor(props) {
       super(props, 'EntityDbo');
       for (const prop in props) {
           this[prop] = props[prop];
       }
   }
   /**@type {Number}*/ Id;
   /**@type {String}*/ Nombre;
   /**@type {String}*/ Nombre_corto;
   /**@type {String}*/ Observaciones;
   /**@type {Boolean}*/ Habilitado;
   /**@type {Date}*/ Created_at;
   /**@type {Date}*/ Updated_at;
   /**@type {Number}*/ Orden;
   /**@type {Niveles} ManyToOne*/ Niveles;
   /**@type {Array<Docente_asignaturas>} OneToMany*/ Docente_asignaturas;
   /**@type {Array<Materias>} OneToMany*/ Materias;
}
export { Asignaturas }
