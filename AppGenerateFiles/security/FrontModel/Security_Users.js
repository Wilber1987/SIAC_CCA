//@ts-check
import { EntityClass } from "../../../WDevCore/WModules/EntityClass.js";
import { WAjaxTools, BasicStates } from "../../../WDevCore/WModules/WComponentsTools.js";
//@ts-ignore
import { ModelProperty } from "../../../WDevCore/WModules/CommonModel.js";
import { Security_Users_Roles }  from './Security_Users_Roles.js'
class Security_Users extends EntityClass {
   /** @param {Partial<Security_Users>} [props] */
   constructor(props) {
       super(props, 'EntitySecurity');
       for (const prop in props) {
           this[prop] = props[prop];
       }
   }
   /**@type {Number}*/ Id_User;
   /**@type {String}*/ Nombres;
   /**@type {String}*/ Estado;
   /**@type {String}*/ Descripcion;
   /**@type {String}*/ Password;
   /**@type {String}*/ Mail;
   /**@type {String}*/ Token;
   /**@type {Date}*/ Token_Date;
   /**@type {Date}*/ Token_Expiration_Date;
   /**@type {Array<Security_Users_Roles>} OneToMany*/ Security_Users_Roles;
}
export { Security_Users }
