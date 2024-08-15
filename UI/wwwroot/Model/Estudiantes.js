//@ts-check
// @ts-ignore
import { EntityClass } from '../WDevCore/WModules/EntityClass.js';
import { Estudiante_clases } from './Estudiante_clases.js';
import { Responsables } from './Responsables.js';
class Estudiantes extends EntityClass {
    
    /** @param {Partial<Estudiantes>} [props] */
    constructor(props) {
        super(props, 'GestionEstudiantes');
        for (const prop in props) {
            this[prop] = props[prop];
        }
    }
    /**@type {Number}*/ Id;
    /**@type {String}*/ Primer_nombre;
    /**@type {String}*/ Segundo_nombre;
    /**@type {String}*/ Primer_apellido;
    /**@type {String}*/ Segundo_apellido;
    /**@type {Date}*/ Fecha_nacimiento;
    /**@type {String}*/ Lugar_nacimiento;
    /**@type {String}*/ Sexo;
    /**@type {String}*/ Direccion;
    /**@type {String}*/ Codigo;
    /**@type {Number}*/ Religion_id;
    /**@type {Number}*/ Madre_id;
    /**@type {Number}*/ Padre_id;
    /**@type {Date}*/ Created_at;
    /**@type {Date}*/ Updated_at;
    /**@type {String}*/ Foto;
    /**@type {Number}*/ Peso;
    /**@type {Number}*/ Altura;
    /**@type {String}*/ Tipo_sangre;
    /**@type {String}*/ Padecimientos;
    /**@type {String}*/ Alergias;
    /**@type {Number}*/ Recorrido_id;
    /**@type {Boolean}*/ Activo;
    /**@type {Array<Estudiante_clases>} OneToMany*/ Estudiante_clases;
    /**@type {Array<Responsables>} OneToMany*/ Responsables;
    /**@type {Array<Clase_Group>} OneToMany*/ Clase_Group;
    /**
    * @returns {Promise<Array<Estudiantes>>}
    */
    async GetOwEstudiantes() {
        return await this.GetData("ApiGestionEstudiantes/GetOwEstudiantes");
    }
    /**
    * @returns {String}
    */
    GetNombreCompleto() {
        return `${this.Primer_nombre} ${this.Segundo_nombre ?? ''} ${this.Primer_apellido} ${this.Segundo_apellido ?? ''}`;
    }
}
export { Estudiantes };

class Calificacion_Group {
    constructor(props) {
        for (const prop in props) {
            this[prop] = props[prop];
        }
    }
    /** @type {Number}*/
    Resultado;

    /** @type {String}*/
    Evaluacion;

    /** @type {String}*/
    Tipo;
}
export { Calificacion_Group };

class Asignatura_Group {
    constructor(props) {
        for (const prop in props) {
            this[prop] = props[prop];
        }
    }
    /** @type {String}*/
    Descripcion;

    /** @type {String[]}*/
    Evaluaciones;

    /** @type {Calificacion_Group[]}*/
    Calificaciones;
    get Details() { return this.Calificaciones }
}
export { Asignatura_Group };
class Clase_Group {
    constructor(props) {
        for (const prop in props) {
            this[prop] = props[prop];
        }
    }
    /** @type {Number}*/
    Id_Clase;

    /** @type {String}*/
    Descripcion;

    /** @type {Asignatura_Group[]}*/
    Asignaturas;
    get Details() { return this.Asignaturas }
}
export { Clase_Group };
