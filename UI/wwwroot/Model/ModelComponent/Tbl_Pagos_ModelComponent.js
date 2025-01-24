//@ts-check
// @ts-ignore
import { ModelProperty } from '../../WDevCore/WModules/CommonModel.js';
import { EntityClass } from '../../WDevCore/WModules/EntityClass.js';
import { Estudiantes } from '../Estudiantes.js';
import { Estudiantes_ModelComponent } from './Estudiantes_ModelComponent.js';
class Tbl_Pagos_ModelComponent extends EntityClass {
    /** @param {Partial<Tbl_Pagos_ModelComponent>} [props] */
    constructor(props) {
        super(props, 'Pagos');
        for (const prop in props) {
            this[prop] = props[prop];
        }
    }
    /**
     * @type {ModelProperty}
     * @description Identificador del pago (Primary Key)
     */
    Id_Pago = { type: "number", primary: true , hiddenFilter: true};

    /**
     * @type {ModelProperty}
     * @description Identificador del estudiante
     */
    Estudiante_Id = { type: "number", hiddenFilter: true };

    /**
     * @type {ModelProperty}
     * @description Identificador del responsable del pago
     */
    Responsable_Id = { type: "number" , hiddenFilter: true};

    /**
     * @type {ModelProperty}
     * @description Monto total del pago
     */
    Monto = { type: "number" , hiddenFilter: true};

    /**
     * @type {ModelProperty}
     * @description Monto pagado hasta la fecha
     */
    Monto_Pagado = { type: "number" , hiddenFilter: true};

    /**
     * @type {ModelProperty}
     * @description Monto pendiente por pagar
     */
    Monto_Pendiente = { type: "number", hiddenFilter: true };

    /**
     * @type {ModelProperty}
     * @description Periodo lectivo al que pertenece el pago
     */
    Periodo_lectivo = { type: "number", hiddenFilter: true };

    /**
     * @type {ModelProperty}
     * @description Documento relacionado con el pago
     */
    Documento = { type: "text", hiddenFilter: true };

    /**
     * @type {ModelProperty}
     * @description Concepto del pago
     */
    Concepto = { type: "text",  hiddenFilter: true };

    /**
     * @type {ModelProperty}
     * @description Mes al que pertenece el pago
     */
    Mes = { type: "text" , hiddenFilter: true};

    /**
     * @type {ModelProperty}
     * @description Tipo de moneda (usando MoneyEnum)
     */
    Money = { type: "number", hiddenFilter: true};

    /**
     * @type {ModelProperty}
     * @description Fecha en la que se realizó el pago
     */
    Fecha_Pago = { type: "date" , hiddenFilter: true};

    /**
     * @type {ModelProperty}
     * @description Fecha límite para realizar el pago
     */
    Fecha_Limite = { type: "date" , hiddenFilter: true};

    /**
     * @type {ModelProperty}
     * @description Fecha de creación del registro
     */
    Fecha = { type: "date" };

    /**
     * @type {ModelProperty}
     * @description Estado del pago
     */
    Estado = { type: "text", hiddenFilter: true};

    /**
     * @type {ModelProperty}
     * @description Relación muchos a uno con la entidad Estudiantes
     */
    Estudiante = { type: "wselect", ModelObject: ()=> new Estudiantes_ModelComponent(), hiddenFilter: true };
}

export { Tbl_Pagos_ModelComponent };

