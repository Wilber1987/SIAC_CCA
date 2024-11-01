//@ts-check
import { EntityClass } from "../../WDevCore/WModules/EntityClass.js";
import { html } from "../../WDevCore/WModules/WComponentsTools.js";
import { Estudiantes } from "./Estudiantes.js";
import { Parientes } from "./Parientes.js";

export class UpdateDataRequest extends EntityClass {
    /** @param {Partial<UpdateDataRequest>} [props] */
    constructor(props) {
        super(props, 'Update');
        for (const prop in props) {
            this[prop] = props[prop];
        };
        
    }
    /**@type {Array<Estudiantes>} */
    Estudiantes
    /**@type {Array<Parientes>} */
    Parientes
    /**@type {Boolean} */
    AceptaTerminosYCondiciones
}