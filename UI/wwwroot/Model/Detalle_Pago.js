//@ts-check

import { EntityClass } from "../WDevCore/WModules/EntityClass.js";
import { PagosRequest } from "./PagosRequest.js";
import { Tbl_Pago } from "./Tbl_Pago.js";

/**
 * @class Detalle_Pago
 * @extends EntityClass
 */
class Detalle_Pago extends EntityClass {

    /** @param {Partial<Detalle_Pago>} [props] */
    constructor(props) {
        super(props, 'Pagos');
        for (const prop in props) {
            this[prop] = props[prop];
        }
    }
    /**
     * @type {?number}
     * @description Identificador del detalle de pago (Primary Key)
     */
    Id_Detalle = null;

    /**
     * @type {?number}
     * @description Identificador del pago
     */
    Id_Pago = null;

    /**
     * @type {?number}
     * @description Total del detalle de pago
     */
    Total = null;

    /**
     * @type {?number}
     * @description Cantidad relacionada con el detalle del pago
     */
    Cantidad = null;

    /**
     * @type {?number}
     * @description Monto correspondiente a este detalle
     */
    Monto = null;

    /**
     * @type {?number}
     * @description Impuesto aplicado al detalle del pago
     */
    Impuesto = null;

    /**
     * @type {?string}
     * @description Concepto relacionado con el pago
     */
    Concepto = null;

    /**
     * @type {?number}
     * @description Identificador del pago request (Foreign Key a PagosRequest)
     */
    Id_Pago_Request = null;

    /**
     * @type {?Tbl_Pago}
     * @description Estado anterior del pago (almacenado como JSON)
     */
    Estado_Anterior_Pago = null;

    /**
     * @type {?PagosRequest}
     * @description Relación muchos a uno con PagosRequest
     */
    PagosRequest = null;

    /**
     * @type {?Tbl_Pago}
     * @description Relación muchos a uno con Tbl_Pago
     */
    Pago = null;

    /**
     * Actualiza el monto del detalle del pago, 
     * teniendo en cuenta la cantidad y el valor del pago
     * @param {Detalle_Pago} detail - Detalle del pago a actualizar
     * @param {number} valor - Valor del pago
     */
    static ActualizarDetalle(detail, valor) {
        detail.Monto = valor;
        detail.Cantidad = 1;
        detail.Total = detail.Cantidad * valor;
    }
    /**
     * Crea un nuevo Detalle_Pago a partir de un Tbl_Pago.
     * El monto del nuevo Detalle_Pago es igual al monto del Tbl_Pago
     * y la cantidad es 1. El impuesto es 0.
     * @param {Tbl_Pago} pago - Tbl_Pago a partir del cual se crea el nuevo Detalle_Pago
     * @returns {Detalle_Pago} - El nuevo Detalle_Pago creado
     */
    static CrearPago(pago) {
        return new Detalle_Pago({
            Pago: pago,
            Monto: pago.Monto_Pendiente,
            Cantidad: 1,
            Total: pago.Monto_Pendiente,
            Impuesto: 0
        });
    }
}
export { Detalle_Pago };