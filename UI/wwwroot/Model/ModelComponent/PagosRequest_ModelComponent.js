import { ModelProperty } from "../../WDevCore/WModules/CommonModel.js";
import { EntityClass } from "../../WDevCore/WModules/EntityClass.js";

class PagosRequest_ModelComponent extends EntityClass {
    /** @param {Partial<PagosRequest_ModelComponent>} [props] */
    constructor(props) {
        super(props, 'Notificaciones');
        for (const prop in props) {
            this[prop] = props[prop];
        }
    }  
    /**
     * @type {ModelProperty}
     * @description Identificador del pago request (Primary Key)
     */
    Id_Pago_Request = { type : "number", primary : true, hiddenFilter: true }

    /**
     * @type {ModelProperty}
     * @description Referencia del pago
     */
    Referencia = { type : "text", hiddenFilter: true}

    /**
     * @type {ModelProperty}
     * @description Descripción del pago
     */
    Descripcion = { type : "textarea", hiddenInTable: true , hiddenFilter: true}

    /**
     * @type {ModelProperty}
     * @description ID del responsable
     */
    Responsable_Id = { type : "number", hiddenInTable: true , hiddenFilter: true}

    /**
     * @type {ModelProperty}
     * @description ID del usuario
     */
    Id_User = { type : "number" , hiddenInTable: true , hiddenFilter: true}

    /**
     * @type {ModelProperty}
     * @description Fecha de creación
     */
    Fecha = { type : "Date"}

    /**
     * @type {ModelProperty}
     * @description Estado actual del pago
     */
    Estado = { type : "text", hiddenFilter: true}

    /**
     * @type {ModelProperty}
     * @description Monto total del pago
     */
    Monto = { type : "MONEY",  hiddenFilter: true}

    /**
     * @type {ModelProperty}
     * @description Moneda en la que se realiza el pago
     */
    Moneda = { type : "text", hiddenInTable: true , hiddenFilter: true}

    /**
     * @type {ModelProperty}
     * @description Lista de detalles del pago (relación uno a muchos con Detalle_Pago)
     */
    //Detalle_Pago = { type : "masterdetail", ModelObject : ()=> new Detalle_Pago_ModelComponent() }
}
export { PagosRequest_ModelComponent }