//@ts-check
// @ts-ignore
import { EntityClass } from '../WDevCore/WModules/EntityClass.js';
import { Conversacion_usuarios } from './Conversacion_usuarios.js';
import { Mensajes } from './Mensajes.js';
class Conversacion extends EntityClass {
    /** @param {Partial<Conversacion>} [props] */
    constructor(props) {
        super(props, 'EntityDbo');
        for (const prop in props) {
            this[prop] = props[prop];
        }
    }
    /**@type {Number}*/ Id_conversacion;
    /**@type {String}*/ Descripcion;
    /**@type {Array<Conversacion_usuarios>} OneToMany*/ Conversacion_usuarios;
    /**@type {Array<Mensajes>} OneToMany*/ Mensajes;
 }
 export { Conversacion }
