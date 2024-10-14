import { EntityClass } from "../../WDevCore/WModules/EntityClass.js";

// @ts-check
class Detalle_Pago_ModelComponent extends EntityClass {
    constructor(props) {
        super(props, 'Pagos');
        for (const prop in props) {
            this[prop] = props[prop];
        }
        
    }
}
export { Detalle_Pago_ModelComponent }