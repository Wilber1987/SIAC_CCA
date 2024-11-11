//@ts-check
import { EntityClass } from '../../WDevCore/WModules/EntityClass.js';
import { Estados_Civiles, Paises, Regiones, Religiones, Titulos } from './Estudiantes.js';
class Parientes extends EntityClass {
   
    /** @param {Partial<Parientes>} [props] */
    constructor(props) {
        super(props, 'Update');
        for (const prop in props) {
            this[prop] = props[prop];
        };
    }
    /**@type {Number}*/ Id;
    /**@type {String}*/ Primer_nombre;
    /**@type {String}*/ Segundo_nombre;
    /**@type {String}*/ Primer_apellido;
    /**@type {String}*/ Identificacion;
    /**@type {String}*/ Segundo_apellido;
    /**@type {String}*/ Sexo;
    /**@type {String}*/ Profesion;
    /**@type {String}*/ Direccion;
    /**@type {String}*/ Lugar_trabajo;
    /**@type {String}*/ Telefono;
    /**@type {String}*/ Celular;
    /**@type {String}*/ Telefono_trabajo;
    /**@type {String}*/ Email;
    /**@type {String}*/ Ex_Alumno;
    /**@type {Number}*/ EgresoExAlumno
    /**@type {Number}*/ Estado_civil_id;
    /**@type {Number}*/ Id_religion;
    /**@type {Boolean}*/ Resoponsable_pago;
    /**@type {Date}*/ Created_at;
    /**@type {Date}*/ Updated_at;
    /**@type {String}*/ Nombre_completo;
    /**@type {Religiones}*/ Religion
    /**@type {Paises}*/ Pais
    /**@type {Regiones}*/ Region
    /**@type {Estados_Civiles}*/ Estado_civil
    /**@type {Titulos}*/ Titulo
    async GetParientesQueLoguearon() {
        return await this.GetData("ApiUpdate/GetParientesQueLoguearon");
    }
    async GetParientesQueActulizaron() {
        return await this.GetData("ApiUpdate/GetParientesQueActulizaron");
    }
    async GetParientesInvitados() {
        return await this.GetData("ApiUpdate/GetParientesInvitados");
    }
    async GetUpdatedData() {
        return await this.GetData("ApiUpdate/GetUpdatedData");
    }
    async GetParientesQueNoLoguearon() {
        return await this.GetData("ApiUpdate/GetParientesQueNoLoguearon");
    }
}
export { Parientes };

