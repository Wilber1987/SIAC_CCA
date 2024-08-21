//@ts-check
import { EntityClass } from "../../../WDevCore/WModules/EntityClass.js";
import { WAjaxTools, BasicStates } from "../../../WDevCore/WModules/WComponentsTools.js";
//@ts-ignore
import { ModelProperty } from "../../../WDevCore/WModules/CommonModel.js";
import { Materias }  from './Materias.js'
import { Secciones }  from './Secciones.js'
import { Calificaciones }  from './Calificaciones.js'
class Evaluaciones extends EntityClass {
   /** @param {Partial<Evaluaciones>} [props] */
   constructor(props) {
       super(props, 'EntityDbo');
       for (const prop in props) {
           this[prop] = props[prop];
       }
   }
   /**@type {Number}*/ Id;
   /**@type {Date}*/ Fecha;
   /**@type {}*/ Hora;
   /**@type {String}*/ Tipo;
   /**@type {Number}*/ Porcentaje;
   /**@type {String}*/ Observaciones;
   /**@type {Date}*/ Created_at;
   /**@type {Date}*/ Updated_at;
   /**@type {Number}*/ Periodo;
   /**@type {Number}*/ Nota_maxima;
   /**@type {Materias} ManyToOne*/ Materias;
   /**@type {Secciones} ManyToOne*/ Secciones;
   /**@type {Array<Calificaciones>} OneToMany*/ Calificaciones;
}
export { Evaluaciones }
