//@ts-check
// @ts-ignore
import { EntityClass } from '../WDevCore/WModules/EntityClass.js';
import { Estudiantes } from './Estudiantes.js';
import { Parientes } from './Parientes.js';
class Responsables extends EntityClass {
   /** @param {Partial<Responsables>} [props] */
   constructor(props) {
       super(props, 'EntityDbo');
       for (const prop in props) {
           this[prop] = props[prop];
       }
   }
   /**@type {Number}*/ Id;
   /**@type {String}*/ Primer_nombre;
   /**@type {String}*/ Segundo_nombre;
   /**@type {String}*/ Primer_apellido;
   /**@type {String}*/ Segundo_apellido;
   /**@type {String}*/ Sexo;
   /**@type {String}*/ Telefono;
   /**@type {String}*/ Celular;
   /**@type {String}*/ Telefono_trabajo;
   /**@type {String}*/ Email;
   /**@type {Number}*/ User_id;

}
export { Responsables };

