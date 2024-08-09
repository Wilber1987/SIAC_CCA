//@ts-check
import { EntityClass } from '../WDevCore/WModules/EntityClass.js';
import { Estudiante_clases }  from './Estudiante_clases.js'
import { Evaluaciones }  from './Evaluaciones.js'
import { Tipo_notas }  from './Tipo_notas.js'
class Calificaciones extends EntityClass {
   /** @param {Partial<Calificaciones>} [props] */
   constructor(props) {
       super(props, 'EntityDbo');
       for (const prop in props) {
           this[prop] = props[prop];
       }
   }
   /**@type {Number}*/ Id;
   /**@type {Number}*/ Resultado;
   /**@type {String}*/ Observaciones;
   /**@type {Date}*/ Created_at;
   /**@type {Date}*/ Updated_at;
   /**@type {Number}*/ Consolidado_id;
   /**@type {Number}*/ Materia_id;
   /**@type {Number}*/ Periodo;
   /**@type {Estudiante_clases} ManyToOne*/ Estudiante_clases;
   /**@type {Evaluaciones} ManyToOne*/ Evaluaciones;
   /**@type {Tipo_notas} ManyToOne*/ Tipo_notas;
}
export { Calificaciones }
