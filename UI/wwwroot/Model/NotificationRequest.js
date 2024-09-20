//@ts-check

//@ts-ignore
import { ModelProperty } from "../../WDevCore/WModules/CommonModel.js";
import { ModelFiles } from "../WDevCore/WModules/CommonModel.js";
import { EntityClass } from "../WDevCore/WModules/EntityClass.js";
class NotificationRequest extends EntityClass {
    /** @param {Partial<NotificationRequest>} [props] */
    constructor(props) {
        super(props, 'Notificaciones');
        for (const prop in props) {
            this[prop] = props[prop];
        }
    }
   /**@type {Number}*/ Id;
   /**@type {String}*/ Mensaje;
   /**@type {Array<ModelFiles>}*/ Files;
   /**@type {String}*/ NotificationType;
   /**@type {Array<Number>}*/ Responsables;
   /**@type {Array<Number>}*/ Niveles;
   /**@type {Array<Number>}*/ Clases;
   /**@type {Array<Number>}*/ Secciones;
   /**@type {Array<Number>}*/ Periodos;
   /**@type {String}*/ NotificationsServicesEnum;

}

export { NotificationRequest }