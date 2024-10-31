//@ts-check
// @ts-ignore
import { ModelProperty } from '../../WDevCore/WModules/CommonModel.js';
import { EntityClass } from '../../WDevCore/WModules/EntityClass.js';
class Estados_Civiles_ModelComponent extends EntityClass {
    /** @param {Partial<Estados_Civiles_ModelComponent>} [props] */
    constructor(props) {
        super(props, 'EntityDbo');
        for (const prop in props) {
            this[prop] = props[prop];
        }
    }
    /**@type {ModelProperty}*/ Id = { type: 'number', primary: true };
    /**@type {ModelProperty}*/ Idtestadocivil = { type: 'text', hidden: true };
    /**@type {ModelProperty}*/ Texto = { type: 'text' };   
}
export { Estados_Civiles_ModelComponent }
