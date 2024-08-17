//@ts-check
import { EntityClass } from "../../../WDevCore/WModules/EntityClass.js";
import { WAjaxTools, BasicStates } from "../../../WDevCore/WModules/WComponentsTools.js";
//@ts-ignore
import { ModelProperty } from "../../../WDevCore/WModules/CommonModel.js";
import { Estudiante_clases_ModelComponent }  from './Estudiante_clases_ModelComponent.js'
import { Estudiantes_responsables_familias_ModelComponent }  from './Estudiantes_responsables_familias_ModelComponent.js'
class Estudiantes_ModelComponent extends EntityClass {
   /** @param {Partial<Estudiantes_ModelComponent>} [props] */
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
   /**@type {ModelProperty}*/ Fecha_nacimiento = { type: 'date' };
   /**@type {ModelProperty}*/ Lugar_nacimiento = { type: 'text' };
   /**@type {ModelProperty}*/ Sexo = { type: 'text' };
   /**@type {ModelProperty}*/ Direccion = { type: 'text' };
   /**@type {ModelProperty}*/ Codigo = { type: 'text' };
   /**@type {ModelProperty}*/ Religion_id = { type: 'number' };
   /**@type {ModelProperty}*/ Madre_id = { type: 'number' };
   /**@type {ModelProperty}*/ Padre_id = { type: 'number' };
   /**@type {ModelProperty}*/ Created_at = { type: 'date' };
   /**@type {ModelProperty}*/ Updated_at = { type: 'date' };
   /**@type {ModelProperty}*/ Foto = { type: 'text' };
   /**@type {ModelProperty}*/ Peso = { type: 'number' };
   /**@type {ModelProperty}*/ Altura = { type: 'number' };
   /**@type {ModelProperty}*/ Tipo_sangre = { type: 'text' };
   /**@type {ModelProperty}*/ Padecimientos = { type: 'text' };
   /**@type {ModelProperty}*/ Alergias = { type: 'text' };
   /**@type {ModelProperty}*/ Recorrido_id = { type: 'number' };
   /**@type {ModelProperty}*/ Activo = { type: 'checkbox' };
   /**@type {ModelProperty}*/ Estudiante_clases = { type: 'MasterDetail',  ModelObject: ()=> new Estudiante_clases_ModelComponent()};
   /**@type {ModelProperty}*/ Estudiantes_responsables_familias = { type: 'MasterDetail',  ModelObject: ()=> new Estudiantes_responsables_familias_ModelComponent()};
}
export { estudiantes_ModelComponent }
