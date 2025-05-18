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
    Id_Pago_Request = { type: "number", primary: true, hiddenFilter: true }

    /**
     * @type {ModelProperty}
     * @description Referencia del pago
     */
    Referencia = { type: "text", hiddenFilter: true }

    /**
     * @type {ModelProperty}
     * @description Descripción del pago
     */
    Descripcion = { type: "textarea", hiddenInTable: true, hiddenFilter: true }

    /**
     * @type {ModelProperty}
     * @description ID del responsable
     */
    Responsable_Id = { type: "number", hiddenInTable: true, hiddenFilter: true }

    /**
     * @type {ModelProperty}
     * @description ID del usuario
     */
    Id_User = { type: "number", hiddenInTable: true, hiddenFilter: true }

    /**
     * @type {ModelProperty}
     * @description Fecha de creación
     */
    Fecha = { type: "Date" }

    /**
     * @type {ModelProperty}
     * @description Estado actual del pago
     */
    Estado = { type: "text", hiddenFilter: true }

    /**
     * @type {ModelProperty}
     * @description Monto total del pago
     */
    Monto = { type: "MONEY", hiddenFilter: true }

    /**
     * @type {ModelProperty}
     * @description Moneda en la que se realiza el pago
     */
    Moneda = { type: "text", hiddenInTable: true, hiddenFilter: true }


    /**
     * @type {ModelProperty}
     * @description Ultimos 4 digitos de la tarjeta
     */
    CardNumber = { type: "text", hiddenFilter: true }

    /**
     * @type {ModelProperty}
     * @description Lista de detalles del pago (relación uno a muchos con Detalle_Pago)
     */
    //Detalle_Pago = { type : "masterdetail", ModelObject : ()=> new Detalle_Pago_ModelComponent() }
}
export { PagosRequest_ModelComponent }

class PowerTranzTpvResponse_ModelComponent extends EntityClass {
    /** @param {Partial<PowerTranzTpvResponse_ModelComponent>} [props] */
    constructor(props) {
        super(props, 'PowerTranz');
        for (const prop in props) {
            this[prop] = props[prop];
        }
    }

    /**
     * @type {ModelProperty}
     * @description Tipo de transacción
     */
    TransactionType = { type: "number" };

    /**
     * @type {ModelProperty}
     * @description Indica si fue aprobada
     */
    Approved = { type: "boolean" };

    /**
     * @type {ModelProperty}
     * @description Identificador de la transacción
     */
    TransactionIdentifier = { type: "text" };

    /**
     * @type {ModelProperty}
     * @description Monto total
     */
    TotalAmount = { type: "MONEY" , label: "Monto"};

    /**
     * @type {ModelProperty}
     * @description Código de moneda (ej. USD)
     */
    CurrencyCode = { type: "text", hiddenFilter: true, label: "Moneda" };

    /**
     * @type {ModelProperty}
     * @description Código de respuesta ISO
     */
    IsoResponseCode = { type: "text", hiddenFilter: true };

    /**
     * @type {ModelProperty}
     * @description Mensaje de respuesta
     */
    ResponseMessage = { type: "text", hiddenFilter: true };

    /**
     * @type {ModelProperty}
     * @description ID de orden de compra
     */
    OrderIdentifier = { type: "text" };

    /**
     * @type {ModelProperty}
     * @description Datos para redirección
     */
    RedirectData = { type: "textarea", hiddenInTable: true, hiddenFilter: true };

    /**
     * @type {ModelProperty}
     * @description Token SPI de la transacción
     */
    SpiToken = { type: "text", hiddenFilter: true };

    /**
     * @type {ModelProperty}
     * @description Lista de errores (si los hay)
     */
    Errors = { type: "masterdetail", ModelObject: () => new ErrorDetail_ModelComponent() };

    /**
         * @type {ModelProperty}
         * @description Token SPI de la transacción
         */
    Mensaje = { type: "text", hiddenFilter: true };
    /**
         * @type {ModelProperty}
         * @description Token SPI de la transacción
         */
    Identificador_de_transaccion = { type: "text", hiddenFilter: true };
    /**
         * @type {ModelProperty}
         * @description Token SPI de la transacción
         */
    Estado = { type: "text", hiddenFilter: true };
    /**
         * @type {ModelProperty}
         * @description Token SPI de la transacción
         */
    Errores = { type: "text", hiddenFilter: true };
}
export { PowerTranzTpvResponse_ModelComponent };

class ErrorDetail_ModelComponent extends EntityClass {
    /** @param {Partial<ErrorDetail_ModelComponent>} [props] */
    constructor(props) {
        super(props, 'ErrorDetail');
        for (const prop in props) {
            this[prop] = props[prop];
        }
    }

    /**
     * @type {ModelProperty}
     * @description Código del error
     */
    Code = { type: "text", hiddenFilter: true };

    /**
     * @type {ModelProperty}
     * @description Mensaje descriptivo del error
     */
    Message = { type: "text", hiddenFilter: true };
}
export { ErrorDetail_ModelComponent };