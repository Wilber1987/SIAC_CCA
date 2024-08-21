//@ts-check
import { EntityClass } from "../../../WDevCore/WModules/EntityClass.js";
import { WAjaxTools, BasicStates } from "../../../WDevCore/WModules/WComponentsTools.js";
//@ts-ignore
import { ModelProperty } from "../../../WDevCore/WModules/CommonModel.js";
import { Security_Permissions_Roles_ModelComponent }  from './Security_Permissions_Roles_ModelComponent.js'
import { Security_Users_Roles_ModelComponent }  from './Security_Users_Roles_ModelComponent.js'
class Security_Roles_ModelComponent extends EntityClass {
   /** @param {Partial<Security_Roles_ModelComponent>} [props] */
   constructor(props) {
       super(props, 'EntitySecurity');
       for (const prop in props) {
           this[prop] = props[prop];
       }
   }
   /**@type {ModelProperty}*/ Id_Role = { type: 'number', primary: true };
   /**@type {ModelProperty}*/ Descripcion = { type: 'text' };
   /**@type {ModelProperty}*/ Estado = { type: 'text' };
   /**@type {ModelProperty}*/ Security_Permissions_Roles = { type: 'MasterDetail',  ModelObject: ()=> new Security_Permissions_Roles_ModelComponent()};
   /**@type {ModelProperty}*/ Security_Users_Roles = { type: 'MasterDetail',  ModelObject: ()=> new Security_Users_Roles_ModelComponent()};
}
export { Security_Roles_ModelComponent }
