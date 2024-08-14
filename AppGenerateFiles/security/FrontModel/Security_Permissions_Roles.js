//@ts-check
import { EntityClass } from "../../../WDevCore/WModules/EntityClass.js";
import { WAjaxTools, BasicStates } from "../../../WDevCore/WModules/WComponentsTools.js";
//@ts-ignore
import { ModelProperty } from "../../../WDevCore/WModules/CommonModel.js";
import { Security_Roles }  from './Security_Roles.js'
import { Security_Permissions }  from './Security_Permissions.js'
class Security_Permissions_Roles extends EntityClass {
   /** @param {Partial<Security_Permissions_Roles>} [props] */
   constructor(props) {
       super(props, 'EntitySecurity');
       for (const prop in props) {
           this[prop] = props[prop];
       }
   }
   /**@type {String}*/ Estado;
   /**@type {Security_Roles} ManyToOne*/ Security_Roles;
   /**@type {Security_Permissions} ManyToOne*/ Security_Permissions;
}
export { Security_Permissions_Roles }
