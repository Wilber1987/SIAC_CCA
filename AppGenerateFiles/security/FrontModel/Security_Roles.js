//@ts-check
import { EntityClass } from "../../../WDevCore/WModules/EntityClass.js";
import { WAjaxTools, BasicStates } from "../../../WDevCore/WModules/WComponentsTools.js";
//@ts-ignore
import { ModelProperty } from "../../../WDevCore/WModules/CommonModel.js";
import { Security_Permissions_Roles }  from './Security_Permissions_Roles.js'
import { Security_Users_Roles }  from './Security_Users_Roles.js'
class Security_Roles extends EntityClass {
   /** @param {Partial<Security_Roles>} [props] */
   constructor(props) {
       super(props, 'EntitySecurity');
       for (const prop in props) {
           this[prop] = props[prop];
       }
   }
   /**@type {Number}*/ Id_Role;
   /**@type {String}*/ Descripcion;
   /**@type {String}*/ Estado;
   /**@type {Array<Security_Permissions_Roles>} OneToMany*/ Security_Permissions_Roles;
   /**@type {Array<Security_Users_Roles>} OneToMany*/ Security_Users_Roles;
}
export { Security_Roles }
