//@ts-check
import { EntityClass } from "../../../WDevCore/WModules/EntityClass.js";
import { WAjaxTools, BasicStates } from "../../../WDevCore/WModules/WComponentsTools.js";
//@ts-ignore
import { ModelProperty } from "../../../WDevCore/WModules/CommonModel.js";
import { Estudiantes_responsables_familias_ModelComponent }  from './Estudiantes_responsables_familias_ModelComponent.js'
class Parientes_ModelComponent extends EntityClass {
   /** @param {Partial<Parientes_ModelComponent>} [props] */
   constructor(props) {
       super(props, 'EntityDbo');
       for (const prop in props) {
           this[prop] = props[prop];
       }
   }
   /**@type {ModelProperty}*/ Id = { type: 'number', primary: true };
   /**@type {ModelProperty}*/ Primer_nombre = { type: 'text' };
   /**@type {ModelProperty}*/ Segundo_nombre = { type: 'text' };
   /**@type {ModelProperty}*/ Primer_apellido = { type: 'text' };
   /**@type {ModelProperty}*/ Segundo_apellido = { type: 'text' };
   /**@type {ModelProperty}*/ Sexo = { type: 'text' };
   /**@type {ModelProperty}*/ Profesion = { type: 'text' };
   /**@type {ModelProperty}*/ Direccion = { type: 'text' };
   /**@type {ModelProperty}*/ Lugar_trabajo = { type: 'text' };
   /**@type {ModelProperty}*/ Telefono = { type: 'text' };
   /**@type {ModelProperty}*/ Celular = { type: 'text' };
   /**@type {ModelProperty}*/ Telefono_trabajo = { type: 'text' };
   /**@type {ModelProperty}*/ Email = { type: 'text' };
   /**@type {ModelProperty}*/ Estado_civil_id = { type: 'number' };
   /**@type {ModelProperty}*/ Religion_id = { type: 'number' };
   /**@type {ModelProperty}*/ Created_at = { type: 'date' };
   /**@type {ModelProperty}*/ Updated_at = { type: 'date' };
   /**@type {ModelProperty}*/ Pais_id = { type: 'number' };
   /**@type {ModelProperty}*/ Resoponsable_pago = { type: 'checkbox' };
   /**@type {ModelProperty}*/ Noidentificacion = { type: 'text' };
   /**@type {ModelProperty}*/ Id_titulo = { type: 'number' };
   /**@type {ModelProperty}*/ Id_region = { type: 'number' };
   /**@type {ModelProperty}*/ Id_estado_civil = { type: 'number' };
   /**@type {ModelProperty}*/ Id_responsable_pago = { type: 'number' };
   /**@type {ModelProperty}*/ Ex_alumno = { type: 'text' };
   /**@type {ModelProperty}*/ Fecha_nacimiento = { type: 'date' };
   /**@type {ModelProperty}*/ Fecha_modificacion = { type: 'date' };
   /**@type {ModelProperty}*/ Usuario_grabacion = { type: 'text' };
   /**@type {ModelProperty}*/ Usuario_edicion = { type: 'text' };
   /**@type {ModelProperty}*/ Ejercicio = { type: 'number' };
   /**@type {ModelProperty}*/ Actualizado = { type: 'checkbox' };
   /**@type {ModelProperty}*/ No_responsable = { type: 'number' };
   /**@type {ModelProperty}*/ Estudiantes_responsables_familias = { type: 'MasterDetail',  ModelObject: ()=> new Estudiantes_responsables_familias_ModelComponent()};
}
export { parientes_ModelComponent }
