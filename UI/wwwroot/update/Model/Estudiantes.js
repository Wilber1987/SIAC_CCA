//@ts-check
import { EntityClass } from "../../WDevCore/WModules/EntityClass.js";


class Estudiantes extends EntityClass {

    /** @param {Partial<Estudiantes>} [props] */
    constructor(props) {
        super(props, 'Update');
        this.IdaVueltaForm = {};
        for (const prop in props) {
            this[prop] = props[prop];
        };
    }
    /**@type {Number}*/ Id;
    /**@type {String}*/ Nombre_completo;
    /**@type {String}*/ Primer_nombre;
    /**@type {String}*/ Segundo_nombre;
    /**@type {String}*/ Primer_apellido;
    /**@type {String}*/ Segundo_apellido;
    /**@type {Date}*/ Fecha_nacimiento;
    /**@type {String}*/ Lugar_nacimiento;
    /**@type {String}*/ Sexo;
    /**@type {String}*/ Direccion;
    /**@type {String}*/ Vivecon;
    /**@type {String}*/ Colegio_procede;
    /**@type {String}*/ Sacramento;
    /**@type {Number}*/ SacramentoA;

    /**@type {String}*/ Codigo;
    /**@type {Number}*/ Id_religion;
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
    /**@type {Boolean}*/  Activo;
    /**@type {Array<Adress>}*/ Puntos_Transportes;    
    /**@type {Religiones}*/ Religion
    /**@type {Paises}*/ Pais
    /**@type {Regiones}*/ Region

}
export { Estudiantes };

export class Adress {
    /** @param {Partial<Adress>} [props] */
    constructor(props) {
        for (const prop in props) {
            this[prop] = props[prop];
        }
    }
    /** @type {String}*/
    Direccion
    /** @type {String} IDA o VUELTA*/
    Trayecto
}

export class Religiones extends EntityClass {
    /**@type {String} */Texto;
}
export class Paises extends EntityClass {
    /**@type {String} */Texto;
}
export class Regiones extends EntityClass {
    /**@type {String} */Texto;
}
export class Estados_Civiles extends EntityClass {
    /**@type {String} */Texto;
}
export class Titulos extends EntityClass {
    /**@type {String} */Texto;
}