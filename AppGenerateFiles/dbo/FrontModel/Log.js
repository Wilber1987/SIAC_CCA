//@ts-check
import { EntityClass } from "../../../WDevCore/WModules/EntityClass.js";
import { WAjaxTools, BasicStates } from "../../../WDevCore/WModules/WComponentsTools.js";
//@ts-ignore
import { ModelProperty } from "../../../WDevCore/WModules/CommonModel.js";
class Log extends EntityClass {
   /** @param {Partial<Log>} [props] */
   constructor(props) {
       super(props, 'EntityDbo');
       for (const prop in props) {
           this[prop] = props[prop];
       }
   }
   /**@type {Number}*/ Id_Log;
   /**@type {String}*/ LogType;
   /**@type {Date}*/ Fecha;
   /**@type {String}*/ Message;
   /**@type {String}*/ Body;
}
export { Log }
