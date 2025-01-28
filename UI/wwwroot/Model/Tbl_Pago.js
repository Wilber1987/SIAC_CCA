//@ts-check
// @ts-ignore
import { Responsables } from './Responsables.js';
import { EntityClass } from '../WDevCore/WModules/EntityClass.js';
import { Estudiantes } from './Estudiantes.js';
import { Estudiante_Clases_View } from './Estudiante_Clases_View.js';
class Tbl_Pago extends EntityClass {
    /** @param {Partial<Tbl_Pago>} [props] */
    constructor(props) {
        super(props, 'Pagos');
        for (const prop in props) {
            this[prop] = props[prop];
        }
    }
    /**
     * @type {Number}
     * @description Identificador del pago (Primary Key)
     */
    Id_Pago;

    /**
     * @type {Number}
     * @description Identificador del estudiante
     */
    Estudiante_Id;

    /**
     * @type {Number}
     * @description Identificador del responsable del pago
     */
    Responsable_Id;

    /**
     * @type {Number}
     * @description Monto total del pago
     */
    Monto;

    /**
     * @type {Number}
     * @description Monto pagado hasta la fecha
     */
    Monto_Pagado;

    /**
     * @type {Number}
     * @description Monto pendiente por pagar
     */
    Monto_Pendiente;

    /**
     * @type {String}
     * @description Periodo lectivo al que pertenece el pago
     */
    Periodo_lectivo;

    /**
     * @type {String}
     * @description Documento relacionado con el pago
     */
    Documento;

    /**
     * @type {String}
     * @description Concepto del pago
     */
    Concepto;

    /**
     * @type {String}
     * @description Mes al que pertenece el pago
     */
    Mes;

    /**
     * @type {String}
     * @description Tipo de moneda (usando MoneyEnum)
     */
    Money;

    /**
     * @type {Date}
     * @description Fecha en la que se realizó el pago
     */
    Fecha_Pago;

    /**
     * @type {Date}
     * @description Fecha límite para realizar el pago
     */
    Fecha_Limite;

    /**
     * @type {Date}
     * @description Fecha de creación del registro
     */
    Fecha;

    /**
     * @type {String}
     * @description Estado del pago
     */
    Estado;

    /**
     * @type {?Estudiante_Clases_View}
     * @description Relación muchos a uno con la entidad Estudiantes
     */
    Estudiante;
}

export { Tbl_Pago };

