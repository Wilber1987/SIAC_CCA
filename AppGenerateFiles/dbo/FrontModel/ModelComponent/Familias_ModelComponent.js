//@ts-check
import { EntityClass } from "../../../WDevCore/WModules/EntityClass.js";
import { WAjaxTools, BasicStates } from "../../../WDevCore/WModules/WComponentsTools.js";
//@ts-ignore
import { ModelProperty } from "../../../WDevCore/WModules/CommonModel.js";
import { Security_Users_ModelComponent }  from './Security_Users_ModelComponent.js'
import { Estudiantes_responsables_familias_ModelComponent }  from './Estudiantes_responsables_familias_ModelComponent.js'
class Familias_ModelComponent extends EntityClass {
   /** @param {Partial<Familias_ModelComponent>} [props] */
   constructor(props) {
       super(props, 'EntityDbo');
       for (const prop in props) {
           this[prop] = props[prop];
       }
   }
   /**@type {ModelProperty}*/ Id = { type: 'number', primary: true };
   /**@type {ModelProperty}*/ Descripcion = { type: 'text' };
   /**@type {ModelProperty}*/ Estado = { type: 'checkbox' };
   /**@type {ModelProperty}*/ Saldo = { type: 'number' };
   /**@type {ModelProperty}*/ Actualizado = { type: 'checkbox' };
   /**@type {ModelProperty}*/ Aceptacion = { type: 'checkbox' };
   /**@type {ModelProperty}*/ Periodo_aceptacion = { type: 'number' };
   /**@type {ModelProperty}*/ Fecha_actualizacion = { type: 'date' };
   /**@type {ModelProperty}*/ Fecha_ultima_notificacion = { type: 'text' };
   /**@type {ModelProperty}*/ Security_Users = { type: 'WSELECT',  ModelObject: ()=> new Security_Users_ModelComponent(), ForeignKeyColumn: 'Id_usuario'};
   /**@type {ModelProperty}*/ Estudiantes_responsables_familias = { type: 'MasterDetail',  ModelObject: ()=> new Estudiantes_responsables_familias_ModelComponent()};
}
export { familias_ModelComponent }
