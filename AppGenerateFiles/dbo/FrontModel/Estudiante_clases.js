//@ts-check
import { EntityClass } from "../../../WDevCore/WModules/EntityClass.js";
import { WAjaxTools, BasicStates } from "../../../WDevCore/WModules/WComponentsTools.js";
//@ts-ignore
import { ModelProperty } from "../../../WDevCore/WModules/CommonModel.js";
import { Estudiantes }  from './Estudiantes.js'
import { Periodo_lectivos }  from './Periodo_lectivos.js'
import { Clases }  from './Clases.js'
import { Secciones }  from './Secciones.js'
import { Calificaciones }  from './Calificaciones.js'
class Estudiante_clases extends EntityClass {
   /** @param {Partial<Estudiante_clases>} [props] */
   constructor(props) {
       super(props, 'EntityDbo');
       for (const prop in props) {
           this[prop] = props[prop];
       }
   }
   /**@type {Number}*/ Id;
   /**@type {Date}*/ Transferido;
   /**@type {Date}*/ Retirado;
   /**@type {String}*/ Observaciones;
   /**@type {Date}*/ Created_at;
   /**@type {Date}*/ Updated_at;
   /**@type {Number}*/ Promedio;
   /**@type {Boolean}*/ Repitente;
   /**@type {Number}*/ Reprobadas;
   /**@type {Estudiantes} ManyToOne*/ Estudiantes;
   /**@type {Periodo_lectivos} ManyToOne*/ Periodo_lectivos;
   /**@type {Clases} ManyToOne*/ Clases;
   /**@type {Secciones} ManyToOne*/ Secciones;
   /**@type {Array<Calificaciones>} OneToMany*/ Calificaciones;
}
export { Estudiante_clases }