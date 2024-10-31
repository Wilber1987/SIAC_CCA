//@ts-check
// @ts-ignore
import { ModelProperty } from '../../WDevCore/WModules/CommonModel.js';
import { EntityClass } from '../../WDevCore/WModules/EntityClass.js';
class Religiones_ModelComponent extends EntityClass {
   /** @param {Partial<Religiones_ModelComponent>} [props] */
   constructor(props) {
       super(props, 'EntityDbo');
       for (const prop in props) {
           this[prop] = props[prop];
       }
   }
   /**@type {ModelProperty}*/ Id = { type: 'number', primary: true };
   /**@type {ModelProperty}*/ Idtreligion = { type: 'text' , hidden: true};
   /**@type {ModelProperty}*/ Texto = { type: 'text' };
}
export { Religiones_ModelComponent }
