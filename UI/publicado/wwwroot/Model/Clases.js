//@ts-check
import { EntityClass } from '../WDevCore/WModules/EntityClass.js';
import { Estudiante_clases } from './Estudiante_clases.js'
import { Materias } from './Materias.js'
import { Grados } from './ModelComponent/Clases_ModelComponent.js';
import { Niveles } from './Niveles.js';
import { Periodo_lectivos } from './Periodo_lectivos.js';
class Clases extends EntityClass {

    /** @param {Partial<Clases>} [props] */
    constructor(props) {
        super(props, 'EntityDbo');
        for (const prop in props) {
            this[prop] = props[prop];
        }
    }
    /**@type {Number}*/ Id;
    /**@type {Number}*/ Grado;
    /**@type {Number}*/ Nivel_id;
    /**@type {Number}*/ Periodo_lectivo_id;
    /**@type {String}*/ Observaciones;
    /**@type {String}*/ Nombre_Grado;
    /**@type {String}*/ Descripcion;
    /**@type {Niveles} ManyToOne*/ Niveles;
    /**@type {Date}*/ Created_at;
    /**@type {Date}*/ Updated_at;
    /**@type {Object}*/ get Grado_label() { return Grados.find(g => g.id == this.Grado) };
    /**@type {Periodo_lectivos}*/ Periodo_lectivos;   
    /**@type {Array<Estudiante_clases>} OneToMany*/ Estudiante_clases;
    /**@type {Array<Materias>} OneToMany*/ Materias;
    /**
    * @returns {Promise<Array<Clases>>}
    */
    GetOwEstudiantes() {
        throw new Error("Method not implemented.");
    }
}
export { Clases }
