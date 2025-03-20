//@ts-check
// @ts-ignore
import { EntityClass } from '../WDevCore/WModules/EntityClass.js';
import { Calificaciones } from './Calificaciones.js';
import { Clases } from './Clases.js';
import { Estudiantes } from './Estudiantes.js';
import { Periodo_lectivos } from './Periodo_lectivos.js';
import { Secciones } from './Secciones.js';
class Estudiante_clases extends EntityClass {
    /** @param {Partial<Estudiante_clases>} [props] */
    constructor(props) {
        super(props, 'EntityDbo');
        for (const prop in props) {
            this[prop] = props[prop];
        };
    }
    /**@type {Number}*/ Id;
    /**@type {Date}*/ Transferido;
    /**@type {Date}*/ Retirado;
    /**@type {String}*/ Observaciones;
    /**@type {String}*/ ObservacionesPuntaje;
    /**@type {String}*/ Descripcion;
    /**@type {Date}*/ Created_at;
    /**@type {Date}*/ Updated_at;
    /**@type {Number}*/ Promedio;
    /**@type {Boolean}*/ Repitente;
    /**@type {Number}*/ Reprobadas;
    /**@type {Number}*/ Seccion_id;
    /**@type {Number}*/ Clase_id;
    /**@type {Number}*/ Estudiante_id;
    /**@type {Clases} ManyToOne*/ Clases;
    /**@type {Estudiantes} ManyToOne*/ Estudiantes;
    /**@type {Periodo_lectivos} ManyToOne*/ Periodo_lectivos;
    /**@type {Secciones} ManyToOne*/ Secciones;
    /**@type {Array<Calificaciones>} OneToMany*/ Calificaciones;

    /**
    * @returns {Promise< import('../WDevCore/WModules/CommonModel.js').ResponseServices>}
    */
    async ExportClaseBoletin() {
        return await this.Post("Exporter/ExportClaseBoletin", this);
    }
}
export { Estudiante_clases };

