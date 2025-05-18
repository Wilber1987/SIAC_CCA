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
	 * @type {PowerTranzTpvResponse}
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

class ErrorDetail extends EntityClass {
	/** @param {Partial<ErrorDetail>} [props] */
	constructor(props) {
		super(props, 'ErrorDetail');
		for (const prop in props) {
			this[prop] = props[prop];
		}
	}

	/**
	 * @type {string}
	 * @description Código del error
	 */
	Code;

	/**
	 * @type {string}
	 * @description Mensaje descriptivo del error
	 */
	Message;
}

export { ErrorDetail };

class PowerTranzTpvResponse extends EntityClass {
	/** @param {Partial<PowerTranzTpvResponse>} [props] */
	constructor(props) {
		super(props, 'PowerTranz');
		for (const prop in props) {
			this[prop] = props[prop];
		}
	}

	/**
	 * @type {number}
	 * @description Tipo de transacción
	 */
	TransactionType;

	/**
	 * @type {boolean}
	 * @description Indica si la transacción fue aprobada
	 */
	Approved;

	/**
	 * @type {string}
	 * @description Identificador único de la transacción
	 */
	TransactionIdentifier;

	/**
	 * @type {number}
	 * @description Monto total de la transacción
	 */
	TotalAmount;

	/**
	 * @type {string}
	 * @description Código de la moneda (ej. USD)
	 */
	CurrencyCode;

	/**
	 * @type {string}
	 * @description Código ISO de respuesta (ej. 00 para aprobado)
	 */
	IsoResponseCode;

	/**
	 * @type {string}
	 * @description Mensaje de respuesta del sistema
	 */
	ResponseMessage;

	/**
	 * @type {string}
	 * @description Identificador de la orden original
	 */
	OrderIdentifier;

	/**
	 * @type {string}
	 * @description Información de redirección si aplica
	 */
	RedirectData;

	/**
	 * @type {string}
	 * @description Token SPI para seguimiento de transacción
	 */
	SpiToken;

	/**
	 * @type {ErrorDetail[]}
	 * @description Lista de errores devueltos por el TPV
	 */
	Errors;

	//PROPIEDADES ADAPTADAS
	/**
	 * @type {string}
	 * @description Información MENSAJE
	 */
	Mensaje;
	/**
		 * @type {string}
		 * @description Información ID TRANSACCION
		 */
	Identificador_de_transaccion;
	/**
		 * @type {string}
		 * @description Información de ESTADO
		 */
	Estado;
	/**
		 * @type {string}
		 * @description ERRORES
		 */
	Errores;
}

export { PowerTranzTpvResponse };
