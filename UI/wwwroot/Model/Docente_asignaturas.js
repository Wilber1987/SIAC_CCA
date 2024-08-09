//@ts-check
// @ts-ignore
import { EntityClass } from '../WDevCore/WModules/EntityClass.js';
import { Asignaturas }  from './Asignaturas.js'
import { Docentes }  from './Docentes.js'
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
   /**@type {Asignaturas} ManyToOne*/ Asignaturas;
   /**@type {Docentes} ManyToOne*/ Docentes;
}
export { Docente_asignaturas }
