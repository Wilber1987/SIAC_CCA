//@ts-check
// @ts-ignore
import { EntityClass } from '../WDevCore/WModules/EntityClass.js';
import { Docentes } from './Docentes.js';
import { Materias } from './Materias.js';
import { Secciones } from './Secciones.js';
class Docente_materias extends EntityClass {
    /** @param {Partial<Docente_materias>} [props] */
    constructor(props) {
        super(props, 'GestionCursos');
        for (const prop in props) {
            this[prop] = props[prop];
        };
    }
    /**@type {Number}*/ Id;
    /**@type {Date}*/ Created_at;
    /**@type {Date}*/ Updated_at;
    /**@type {Object}*/ ThisConfig;
    /**@type {Number}*/ Seccion_id;
    /**@type {Docentes} ManyToOne*/ Docentes;
    /**@type {Materias} ManyToOne*/ Materias;
    /**@type {Secciones} ManyToOne*/ Secciones;
}
export { Docente_materias };

