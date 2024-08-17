//@ts-check
import { EntityClass } from "../../../WDevCore/WModules/EntityClass.js";
import { WAjaxTools, BasicStates } from "../../../WDevCore/WModules/WComponentsTools.js";
//@ts-ignore
import { ModelProperty } from "../../../WDevCore/WModules/CommonModel.js";
import { Security_Permissions_ModelComponent }  from './Security_Permissions_ModelComponent.js'
import { Security_Roles_ModelComponent }  from './Security_Roles_ModelComponent.js'
class Security_Permissions_Roles_ModelComponent extends EntityClass {
   /** @param {Partial<Security_Permissions_Roles_ModelComponent>} [props] */
   constructor(props) {
       super(props, 'EntitySecurity');
       for (const prop in props) {
           this[prop] = props[prop];
       }
   }
   /**@type {ModelProperty}*/ Estado = { type: 'text' };
   /**@type {ModelProperty}*/ Security_Permissions = { type: 'WSELECT',  ModelObject: ()=> new Security_Permissions_ModelComponent(), ForeignKeyColumn: 'Id_Permission'};
   /**@type {ModelProperty}*/ Security_Roles = { type: 'WSELECT',  ModelObject: ()=> new Security_Roles_ModelComponent(), ForeignKeyColumn: 'Id_Role'};
}
export { Security_Permissions_Roles_ModelComponent }
