//@ts-check
import { EntityClass } from "../../../WDevCore/WModules/EntityClass.js";
import { WAjaxTools, BasicStates } from "../../../WDevCore/WModules/WComponentsTools.js";
//@ts-ignore
import { ModelProperty } from "../../../WDevCore/WModules/CommonModel.js";
import { Calificaciones_ModelComponent }  from './Calificaciones_ModelComponent.js'
class Tipo_notas_ModelComponent extends EntityClass {
   /** @param {Partial<Tipo_notas_ModelComponent>} [props] */
   constructor(props) {
       super(props, 'EntityDbo');
       for (const prop in props) {
           this[prop] = props[prop];
       }
   }
   /**@type {ModelProperty}*/ Id = { type: 'number', primary: true };
   /**@type {ModelProperty}*/ Nombre = { type: 'text' };
   /**@type {ModelProperty}*/ Nombre_corto = { type: 'text' };
   /**@type {ModelProperty}*/ Periodo_lectivo_id = { type: 'number' };
   /**@type {ModelProperty}*/ Consolidado_id = { type: 'number' };
   /**@type {ModelProperty}*/ Numero_consolidados = { type: 'number' };
   /**@type {ModelProperty}*/ Observaciones = { type: 'text' };
   /**@type {ModelProperty}*/ Created_at = { type: 'date' };
   /**@type {ModelProperty}*/ Updated_at = { type: 'date' };
   /**@type {ModelProperty}*/ Orden = { type: 'number' };
   /**@type {ModelProperty}*/ Calificaciones = { type: 'MasterDetail',  ModelObject: ()=> new Calificaciones_ModelComponent()};
}
export { tipo_notas_ModelComponent }
