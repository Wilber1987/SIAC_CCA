//@ts-check
import { EntityClass } from "../../../WDevCore/WModules/EntityClass.js";
import { WAjaxTools, BasicStates } from "../../../WDevCore/WModules/WComponentsTools.js";
//@ts-ignore
import { ModelProperty } from "../../../WDevCore/WModules/CommonModel.js";
import { Familias_ModelComponent }  from './Familias_ModelComponent.js'
import { Security_Users_Roles_ModelComponent }  from './Security_Users_Roles_ModelComponent.js'
class Security_Users_ModelComponent extends EntityClass {
   /** @param {Partial<Security_Users_ModelComponent>} [props] */
   constructor(props) {
       super(props, 'EntitySecurity');
       for (const prop in props) {
           this[prop] = props[prop];
       }
   }
   /**@type {ModelProperty}*/ Id_User = { type: 'number', primary: true };
   /**@type {ModelProperty}*/ Nombres = { type: 'text' };
   /**@type {ModelProperty}*/ Estado = { type: 'text' };
   /**@type {ModelProperty}*/ Descripcion = { type: 'text' };
   /**@type {ModelProperty}*/ Password = { type: 'text' };
   /**@type {ModelProperty}*/ Mail = { type: 'text' };
   /**@type {ModelProperty}*/ Token = { type: 'text' };
   /**@type {ModelProperty}*/ Token_Date = { type: 'date' };
   /**@type {ModelProperty}*/ Token_Expiration_Date = { type: 'date' };
   /**@type {ModelProperty}*/ Familias = { type: 'MasterDetail',  ModelObject: ()=> new Familias_ModelComponent()};
   /**@type {ModelProperty}*/ Security_Users_Roles = { type: 'MasterDetail',  ModelObject: ()=> new Security_Users_Roles_ModelComponent()};
}
export { Security_Users_ModelComponent }
