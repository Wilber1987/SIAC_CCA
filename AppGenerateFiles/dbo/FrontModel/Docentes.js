//@ts-check
import { EntityClass } from "../../../WDevCore/WModules/EntityClass.js";
import { WAjaxTools, BasicStates } from "../../../WDevCore/WModules/WComponentsTools.js";
//@ts-ignore
import { ModelProperty } from "../../../WDevCore/WModules/CommonModel.js";
import { Escolaridades }  from './Escolaridades.js'
import { Docente_asignaturas }  from './Docente_asignaturas.js'
import { Docente_materias }  from './Docente_materias.js'
class Docentes extends EntityClass {
   /** @param {Partial<Docentes>} [props] */
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
   /**@type {Date}*/ Fecha_nacimiento;
   /**@type {String}*/ Lugar_nacimiento;
   /**@type {String}*/ Direccion;
   /**@type {String}*/ Telefono;
   /**@type {String}*/ Celular;
   /**@type {String}*/ Email;
   /**@type {Number}*/ Estado_civil_id;
   /**@type {Number}*/ Religion_id;
   /**@type {String}*/ Foto;
   /**@type {Date}*/ Created_at;
   /**@type {Date}*/ Updated_at;
   /**@type {Boolean}*/ Habilitado;
   /**@type {String}*/ Cargo;
   /**@type {Escolaridades} ManyToOne*/ Escolaridades;
   /**@type {Array<Docente_asignaturas>} OneToMany*/ Docente_asignaturas;
   /**@type {Array<Docente_materias>} OneToMany*/ Docente_materias;
}
export { Docentes }
