//@ts-check
// @ts-ignore
import { Responsables } from './Responsables.js';
import { EntityClass } from '../WDevCore/WModules/EntityClass.js';
import { Estudiantes } from './Estudiantes.js';
import { Tbl_Pago } from './Tbl_Pago.js';
import { Detalle_Pago } from './Detalle_Pago.js';
class PagosRequest extends EntityClass {
	/** @param {Partial<PagosRequest>} [props] */
	constructor(props) {
		super(props, 'Pagos');
		for (const prop in props) {
			this[prop] = props[prop];
		};
	}
	/**
	 * @type {number}
	 * @description Identificador del pago request (Primary Key)
	 */
	Id_Pago_Request;

	/**
	 * @type {string}
	 * @description Referencia del pago
	 */
	Referencia;

	/**
	 * @type {string}
	 * @description Descripción del pago
	 */
	Descripcion;

	/**
	 * @type {number}
	 * @description ID del responsable
	 */
	Responsable_Id;

	/**
	 * @type {number}
	 * @description ID del usuario
	 */
	Id_User;

	/**
	 * @type {Date}
	 * @description Fecha de creación
	 */
	Fecha;

	/**
	 * @type {string}
	 * @description Usuario creador del registro
	 */
	Creador;

	/**
	 * @type {string}
	 * @description Estado actual del pago
	 */
	Estado;

	/**
	 * @type {number}
	 * @description Monto total del pago
	 */
	Monto;

	/**
	 * @type {string}
	 * @description Moneda en la que se realiza el pago
	 */
	Moneda;
	/**
	 * @type {string}
	 * @description Moneda en la que se realiza el pago
	 */
	Year;

	/**
	 * @type {Detalle_Pago[]}
	 * @description Lista de detalles del pago (relación uno a muchos con Detalle_Pago)
	 */
	Detalle_Pago;
	/**
	 * @type {Object}
	 * @description datos de la referencia de la tpv
	 */
	TpvInfo
	/**
	 * @type {string}
	 * @description ultimos 4 digitos de la tarjeta
	 */
	CardNumber;
}

export { PagosRequest };

