//@ts-check
import { EntityClass } from '../../WDevCore/WModules/EntityClass.js';
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
    /**@type {String}*/ Segundo_apellido;
    /**@type {String}*/ Sexo;
    /**@type {String}*/ Profesion;
    /**@type {String}*/ Direccion;
    /**@type {String}*/ Lugar_trabajo;
    /**@type {String}*/ Telefono;
    /**@type {String}*/ Celular;
    /**@type {String}*/ Telefono_trabajo;
    /**@type {String}*/ Email;
    /**@type {Number}*/ Estado_civil_id;
    /**@type {Number}*/ Religion_id;
    /**@type {Boolean}*/ Resoponsable_pago;
    /**@type {Date}*/ Created_at;
    /**@type {Date}*/ Updated_at;
    /**@type {String}*/ Nombre_completo;
    GetParientesQueLoguearon() {
        return this.GetData("ApiUpdate/GetParientesQueLoguearon");
    }
    GetParientesQueActulizaron() {
        return this.GetData("ApiUpdate/GetParientesQueActulizaron");
    }
    GetParientesInvitados() {
        return this.GetData("ApiUpdate/GetParientesInvitados");
    }
    GetUpdatedData() {
        return this.GetData("ApiUpdate/GetUpdatedData");
    }
}
export { Parientes };

