//@ts-check
import { EntityClass } from '../WDevCore/WModules/EntityClass.js';
import { Estudiante_clases }  from './Estudiante_clases.js'
import { Materias }  from './Materias.js'
class Clases extends EntityClass {
   /** @param {Partial<Clases>} [props] */
   constructor(props) {
       super(props, 'EntityDbo');
       for (const prop in props) {
           this[prop] = props[prop];
       }
   }
   /**@type {Number}*/ Id;
   /**@type {Number}*/ Grado;
   /**@type {Number}*/ Nivel_id;
   /**@type {Number}*/ Periodo_lectivo_id;
   /**@type {String}*/ Observaciones;
   /**@type {Date}*/ Created_at;
   /**@type {Date}*/ Updated_at;
   /**@type {Array<Estudiante_clases>} OneToMany*/ Estudiante_clases;
   /**@type {Array<Materias>} OneToMany*/ Materias;
}
export { Clases }
