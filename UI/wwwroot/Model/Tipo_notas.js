//@ts-check
// @ts-ignore
import { EntityClass } from '../WDevCore/WModules/EntityClass.js';
import { Calificaciones } from './Calificaciones.js';
class Tipo_notas extends EntityClass {
   /** @param {Partial<Tipo_notas>} [props] */
   constructor(props) {
       super(props, 'EntityDbo');
       for (const prop in props) {
           this[prop] = props[prop];
       }
   }
   /**@type {Number}*/ Id;
   /**@type {String}*/ Nombre;
   /**@type {String}*/ Nombre_corto;
   /**@type {Number}*/ Periodo_lectivo_id;
   /**@type {Number}*/ Consolidado_id;
   /**@type {Number}*/ Numero_consolidados;
   /**@type {String}*/ Observaciones;
   /**@type {Date}*/ Created_at;
   /**@type {Date}*/ Updated_at;
   /**@type {Number}*/ Orden;
   /**@type {Array<Calificaciones>} OneToMany*/ Calificaciones;
}
export { Tipo_notas };

