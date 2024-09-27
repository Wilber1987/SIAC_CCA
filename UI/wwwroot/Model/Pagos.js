//@ts-check
// @ts-ignore
import { Responsables } from './Responsables.js';
import { EntityClass } from '../WDevCore/WModules/EntityClass.js';
import { Estudiantes } from './Estudiantes.js';
class Pagos extends EntityClass {
    /** @param {Partial<Pagos>} [props] */
    constructor(props) {
        super(props, 'Pagos');
        for (const prop in props) {
            this[prop] = props[prop];
        }
    }

    /** @type {Number}*/ Id_Pago
    /** @type {Number}*/ Estudiante_Id
    /** @type {Number}*/ Responsable_Id
    /** @type {Number}*/ Monto
    /** @type {Number}*/ Periodo_lectivo
    /** @type {String}*/ Mes
    /** @type {String}*/ Money
    /** @type {Date}*/ Fecha
    /** @type {Date}*/ Fecha_Pago
    /** @type {Date}*/ Fecha_Limite
    /** @type {String}*/ Estado
    /** @type {Estudiantes}*/ Estudiante
}
export { Pagos };

