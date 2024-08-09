//@ts-check
// @ts-ignore
import { EntityClass } from '../WDevCore/WModules/EntityClass.js';
import { Docente_materias } from './Docente_materias.js';
import { Estudiante_clases } from './Estudiante_clases.js';
import { Evaluaciones } from './Evaluaciones.js';
class Secciones extends EntityClass {
   /** @param {Partial<Secciones>} [props] */
   constructor(props) {
       super(props, 'EntityDbo');
       for (const prop in props) {
           this[prop] = props[prop];
       }
   }
   /**@type {Number}*/ Id;
   /**@type {String}*/ Nombre;
   /**@type {Number}*/ Clase_id;
   /**@type {Number}*/ Docente_id;
   /**@type {String}*/ Observaciones;
   /**@type {Date}*/ Created_at;
   /**@type {Date}*/ Updated_at;
   /**@type {Array<Docente_materias>} OneToMany*/ Docente_materias;
   /**@type {Array<Estudiante_clases>} OneToMany*/ Estudiante_clases;
   /**@type {Array<Evaluaciones>} OneToMany*/ Evaluaciones;
}
export { Secciones };

