//@ts-check
// @ts-ignore
import { ModelProperty } from '../../WDevCore/WModules/CommonModel.js';
import { EntityClass } from '../../WDevCore/WModules/EntityClass.js';
class Regiones_ModelComponent extends EntityClass {
    /** @param {Partial<Regiones_ModelComponent>} [props] */
    constructor(props) {
        super(props, 'EntityDbo');
        for (const prop in props) {
            this[prop] = props[prop];
        }
    }
    /**@type {ModelProperty}*/ Id_region = { type: 'number', primary: true };
    ///**@type {ModelProperty}*/ Id_pais = { type: 'number' };
    /**@type {ModelProperty}*/ Idtregion = { type: 'number', primary: true };
    /**@type {ModelProperty}*/ Texto = { type: 'text' };   
}
export { Regiones_ModelComponent }
