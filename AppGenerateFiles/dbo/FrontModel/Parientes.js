//@ts-check
import { EntityClass } from "../../../WDevCore/WModules/EntityClass.js";
import { WAjaxTools, BasicStates } from "../../../WDevCore/WModules/WComponentsTools.js";
//@ts-ignore
import { ModelProperty } from "../../../WDevCore/WModules/CommonModel.js";
import { Estudiantes_responsables_familias }  from './Estudiantes_responsables_familias.js'
class Parientes extends EntityClass {
   /** @param {Partial<Parientes>} [props] */
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
   /**@type {String}*/ Profesion;
   /**@type {String}*/ Direccion;
   /**@type {String}*/ Lugar_trabajo;
   /**@type {String}*/ Telefono;
   /**@type {String}*/ Celular;
   /**@type {String}*/ Telefono_trabajo;
   /**@type {String}*/ Email;
   /**@type {Number}*/ Estado_civil_id;
   /**@type {Number}*/ Religion_id;
   /**@type {Date}*/ Created_at;
   /**@type {Date}*/ Updated_at;
   /**@type {Number}*/ Pais_id;
   /**@type {Boolean}*/ Resoponsable_pago;
   /**@type {String}*/ Noidentificacion;
   /**@type {Number}*/ Id_titulo;
   /**@type {Number}*/ Id_region;
   /**@type {Number}*/ Id_estado_civil;
   /**@type {Number}*/ Id_responsable_pago;
   /**@type {String}*/ Ex_alumno;
   /**@type {Date}*/ Fecha_nacimiento;
   /**@type {Date}*/ Fecha_modificacion;
   /**@type {String}*/ Usuario_grabacion;
   /**@type {String}*/ Usuario_edicion;
   /**@type {Number}*/ Ejercicio;
   /**@type {Boolean}*/ Actualizado;
   /**@type {Number}*/ No_responsable;
   /**@type {Array<Estudiantes_responsables_familias>} OneToMany*/ Estudiantes_responsables_familias;
}
export { Parientes }
