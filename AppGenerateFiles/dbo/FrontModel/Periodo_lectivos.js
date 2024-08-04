//@ts-check
import { EntityClass } from "../../../WDevCore/WModules/EntityClass.js";
import { WAjaxTools, BasicStates } from "../../../WDevCore/WModules/WComponentsTools.js";
//@ts-ignore
import { ModelProperty } from "../../../WDevCore/WModules/CommonModel.js";
import { Estudiante_clases }  from './Estudiante_clases.js'
class Periodo_lectivos extends EntityClass {
   /** @param {Partial<Periodo_lectivos>} [props] */
   constructor(props) {
       super(props, 'EntityDbo');
       for (const prop in props) {
           this[prop] = props[prop];
       }
   }
   /**@type {Number}*/ Id;
   /**@type {String}*/ Nombre;
   /**@type {String}*/ Nombre_corto;
   /**@type {Date}*/ Inicio;
   /**@type {Date}*/ Fin;
   /**@type {String}*/ Observaciones;
   /**@type {String}*/ Config;
   /**@type {Date}*/ Created_at;
   /**@type {Date}*/ Updated_at;
   /**@type {Boolean}*/ Abierto;
   /**@type {Boolean}*/ Oculto;
   /**@type {Array<Estudiante_clases>} OneToMany*/ Estudiante_clases;
}
export { Periodo_lectivos }
