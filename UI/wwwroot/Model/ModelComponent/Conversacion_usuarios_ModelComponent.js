//@ts-check
// @ts-ignore
import { ModelProperty } from '../../WDevCore/WModules/CommonModel.js';
import { EntityClass } from '../../WDevCore/WModules/EntityClass.js';
class Conversacion_usuarios_ModelComponent extends EntityClass {
   /** @param {Partial<Conversacion_usuarios_ModelComponent>} [props] */
   constructor(props) {
       super(props, 'EntityDbo');
       for (const prop in props) {
           this[prop] = props[prop];
       }
   }
   /**@type {ModelProperty}*/ Id_conversacion = { type: 'number' };
   /**@type {ModelProperty}*/ Id_usuario = { type: 'number' };
}
export { Conversacion_usuarios_ModelComponent }
