//@ts-check
import { EntityClass } from "../../../WDevCore/WModules/EntityClass.js";
import { WAjaxTools, BasicStates } from "../../../WDevCore/WModules/WComponentsTools.js";
//@ts-ignore
import { ModelProperty } from "../../../WDevCore/WModules/CommonModel.js";
import { Asignaturas }  from './Asignaturas.js'
import { Clases }  from './Clases.js'
class Niveles extends EntityClass {
   /** @param {Partial<Niveles>} [props] */
   constructor(props) {
       super(props, 'EntityDbo');
       for (const prop in props) {
           this[prop] = props[prop];
       }
   }
   /**@type {Number}*/ Id;
   /**@type {String}*/ Nombre;
   /**@type {String}*/ Nombre_corto;
   /**@type {String}*/ Nombre_grado;
   /**@type {Number}*/ Numero_grados;
   /**@type {String}*/ Observaciones;
   /**@type {Boolean}*/ Habilitado;
   /**@type {Date}*/ Created_at;
   /**@type {Date}*/ Updated_at;
   /**@type {Number}*/ Orden;
   /**@type {Number}*/ Inicio_grado;
   /**@type {Array<Asignaturas>} OneToMany*/ Asignaturas;
   /**@type {Array<Clases>} OneToMany*/ Clases;
}
export { Niveles }
