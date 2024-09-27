//@ts-check
// @ts-ignore
import { Responsables } from './Responsables.js';
import { EntityClass } from '../WDevCore/WModules/EntityClass.js';
import { Estudiantes } from './Estudiantes.js';
import { Pagos } from './Pagos.js';
class PagosRequest extends EntityClass {
    /** @param {Partial<PagosRequest>} [props] */
    constructor(props) {
        super(props, 'Pagos');
        for (const prop in props) {
            this[prop] = props[prop];
        }
    }
    /** @type {Array<Pagos>}*/ Pagos
}
export { PagosRequest };

