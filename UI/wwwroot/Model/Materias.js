//@ts-check
// @ts-ignore
import { EntityClass } from '../WDevCore/WModules/EntityClass.js';
import { Asignaturas }  from './Asignaturas.js'
import { Clases }  from './Clases.js'
import { Docente_materias }  from './Docente_materias.js'
import { Evaluaciones }  from './Evaluaciones.js'
class Materias extends EntityClass {
   /** @param {Partial<Materias>} [props] */
   constructor(props) {
       super(props, 'EntityDbo');
       for (const prop in props) {
           this[prop] = props[prop];
       }
   }
   /**@type {Number}*/ Id;
   /**@type {String}*/ Observaciones;
   /**@type {String}*/ Config;
   /**@type {Date}*/ Created_at;
   /**@type {Date}*/ Updated_at;
   /**@type {Number}*/ Lock_version;
   /**@type {Asignaturas} ManyToOne*/ Asignaturas;
   /**@type {Clases} ManyToOne*/ Clases;
   /**@type {Array<Docente_materias>} OneToMany*/ Docente_materias;
   /**@type {Array<Evaluaciones>} OneToMany*/ Evaluaciones;
}
export { Materias }
