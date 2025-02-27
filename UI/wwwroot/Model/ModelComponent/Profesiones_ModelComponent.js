//@ts-check
// @ts-ignore
import { ModelProperty } from '../../WDevCore/WModules/CommonModel.js';
import { EntityClass } from '../../WDevCore/WModules/EntityClass.js';
class Profesiones_ModelComponent extends EntityClass {
    /** @param {Partial<Profesiones_ModelComponent>} [props] */
    constructor(props) {
        super(props, 'EntityDbo');
        for (const prop in props) {
            this[prop] = props[prop];
        }
    }
    /**@type {ModelProperty}*/ Id_profesion = { type: 'number', primary: true };
    /**@type {ModelProperty}*/ Texto = { type: 'text' };   
}
export { Profesiones_ModelComponent }
