//@ts-check
// @ts-ignore
import { EntityClass } from '../WDevCore/WModules/EntityClass.js';
import { Docente_asignaturas } from './Docente_asignaturas.js';
import { Docente_materias } from './Docente_materias.js';
import { Escolaridades } from './Escolaridades.js';
class Docentes extends EntityClass {
   /** @param {Partial<Docentes>} [props] */
   constructor(props) {
       super(props, 'EntityDbo');
       for (const prop in props) {
           this[prop] = props[prop];
       };       
   }
   /**@type {Number}*/ Id;
   /**@type {String}*/ Primer_nombre;
   /**@type {String}*/ Segundo_nombre;
   /**@type {String}*/ Primer_apellido;
   /**@type {String}*/ Segundo_apellido;
   /**@type {String}*/ Nombre_completo;
   /**@type {String}*/ Sexo;
   /**@type {Date}*/ Fecha_nacimiento;
   /**@type {String}*/ Lugar_nacimiento;
   /**@type {String}*/ Direccion;
   /**@type {String}*/ Telefono;
   /**@type {String}*/ Celular;
   /**@type {String}*/ Email;
   /**@type {Number}*/ Estado_civil_id;
   /**@type {Number}*/ Id_religion;
   /**@type {String}*/ Foto;
   /**@type {Date}*/ Created_at;
   /**@type {Date}*/ Updated_at;
   /**@type {Boolean}*/ Habilitado;
   /**@type {String}*/ Cargo;
   /**@type {Escolaridades} ManyToOne*/ Escolaridades;
   /**@type {Array<Docente_asignaturas>} OneToMany*/ Docente_asignaturas;
   /**@type {Array<Docente_materias>} OneToMany*/ Docente_materias;
}
export { Docentes };

