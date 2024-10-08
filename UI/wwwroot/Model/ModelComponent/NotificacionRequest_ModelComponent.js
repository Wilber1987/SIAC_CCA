//@ts-check

//@ts-ignore
import { ModelProperty } from "../../WDevCore/WModules/CommonModel.js";
import { EntityClass } from "../../WDevCore/WModules/EntityClass.js";
class NotificationRequest_ModelComponent extends EntityClass {
    /** @param {Partial<NotificationRequest_ModelComponent>} [props] */
    constructor(props) {
        super(props, 'Notificaciones');
        for (const prop in props) {
            this[prop] = props[prop];
        }
    }
   /**@type {ModelProperty}*/ Id = { type: 'number', primary: true };
   /**@type {ModelProperty}*/ Titulo = { type: 'text' };
   /**@type {ModelProperty}*/ Mensaje = { type: 'richtext' };
   /**@type {ModelProperty}*/ Files = { type: 'file' , require: false};
   /**@type {ModelProperty}*/ NotificationType = { type: 'text', hidden: true };
   /**@type {ModelProperty}*/ Responsables = { type: 'multiselect', hidden: true  };
   /**@type {ModelProperty}*/ Niveles = { type: 'multiselect', hidden: true };
   /**@type {ModelProperty}*/ Clases = { type: 'multiselect', hidden: true };
   /**@type {ModelProperty}*/ Secciones = { type: 'multiselect', hidden: true };
   /**@type {ModelProperty}*/ Periodos = { type: 'multiselect', hidden: true };
   /**@type {ModelProperty}*/ NotificationsServicesEnum = { type: 'multiselect', hidden: true };

}
export { NotificationRequest_ModelComponent }

const NotificationsServicesEnum = {
    MAIL: "MAIL"
}
export { NotificationsServicesEnum }

const NotificationTypeEnum =
{
    SECCION: "SECCION", CLASE: "CLASE", PERIODO: "PERIODO", NIVEL: "NIVEL", RESPONSABLE: "RESPONSABLE"
}
export { NotificationTypeEnum }