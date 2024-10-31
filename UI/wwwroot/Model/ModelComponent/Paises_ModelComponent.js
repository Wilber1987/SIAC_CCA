//@ts-check
// @ts-ignore
import { ModelProperty } from '../../WDevCore/WModules/CommonModel.js';
import { EntityClass } from '../../WDevCore/WModules/EntityClass.js';
class Paises_ModelComponent extends EntityClass {
    /** @param {Partial<Paises_ModelComponent>} [props] */
    constructor(props) {
        super(props, 'EntityDbo');
        for (const prop in props) {
            this[prop] = props[prop];
        }
    }
    /**@type {ModelProperty}*/ Id_pais = { type: 'number', primary: true };
    /**@type {ModelProperty}*/ Idtpais = { type: 'number', primary: true };
    /**@type {ModelProperty}*/ Texto = { type: 'text' };   
}
export { Paises_ModelComponent }
