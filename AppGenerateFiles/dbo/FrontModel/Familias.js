//@ts-check
import { EntityClass } from "../../../WDevCore/WModules/EntityClass.js";
import { WAjaxTools, BasicStates } from "../../../WDevCore/WModules/WComponentsTools.js";
//@ts-ignore
import { ModelProperty } from "../../../WDevCore/WModules/CommonModel.js";
import { Security_Users }  from './Security_Users.js'
import { Estudiantes_responsables_familias }  from './Estudiantes_responsables_familias.js'
class Familias extends EntityClass {
   /** @param {Partial<Familias>} [props] */
   constructor(props) {
       super(props, 'EntityDbo');
       for (const prop in props) {
           this[prop] = props[prop];
       }
   }
   /**@type {Number}*/ Id;
   /**@type {String}*/ Descripcion;
   /**@type {Boolean}*/ Estado;
   /**@type {Number}*/ Saldo;
   /**@type {Boolean}*/ Actualizado;
   /**@type {Boolean}*/ Aceptacion;
   /**@type {Number}*/ Periodo_aceptacion;
   /**@type {Date}*/ Fecha_actualizacion;
   /**@type {String}*/ Fecha_ultima_notificacion;
   /**@type {Security_Users} ManyToOne*/ Security_Users;
   /**@type {Array<Estudiantes_responsables_familias>} OneToMany*/ Estudiantes_responsables_familias;
}
export { Familias }
