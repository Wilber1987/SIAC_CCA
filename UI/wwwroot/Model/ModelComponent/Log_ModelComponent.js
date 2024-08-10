//@ts-check
// @ts-ignore
import { ModelProperty } from '../../WDevCore/WModules/CommonModel.js';
import { EntityClass } from '../../WDevCore/WModules/EntityClass.js';
class Log_ModelComponent extends EntityClass {
   /** @param {Partial<Log_ModelComponent>} [props] */
   constructor(props) {
       super(props, 'EntityDbo');
       for (const prop in props) {
           this[prop] = props[prop];
       }
   }
   /**@type {ModelProperty}*/ Id_Log = { type: 'number', primary: true };
   /**@type {ModelProperty}*/ LogType = { type: 'text' };
   /**@type {ModelProperty}*/ Fecha = { type: 'date' };
   /**@type {ModelProperty}*/ Message = { type: 'text' };
   /**@type {ModelProperty}*/ Body = { type: 'text' };
}
export { Log_ModelComponent }
