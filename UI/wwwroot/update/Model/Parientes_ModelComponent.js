//@ts-check
// @ts-ignore
import { Estados_Civiles_ModelComponent } from '../../Model/ModelComponent/Estados_Civiles_ModelComponent.js';
import { Paises_ModelComponent } from '../../Model/ModelComponent/Paises_ModelComponent.js';
import { Profesiones_ModelComponent } from '../../Model/ModelComponent/Profesiones_ModelComponent.js';
import { Regiones_ModelComponent } from '../../Model/ModelComponent/Regiones_ModelComponent.js';
import { Religiones_ModelComponent } from '../../Model/ModelComponent/Religiones_ModelComponent.js';
import { Titulos_ModelComponent } from '../../Model/ModelComponent/Titulos_ModelComponent.js';
// @ts-ignore
import { ModelProperty } from '../../WDevCore/WModules/CommonModel.js';
import { EntityClass } from '../../WDevCore/WModules/EntityClass.js';

class Parientes_ModelComponent extends EntityClass {
    /** @param {Partial<Parientes_ModelComponent>} [props] */
    constructor(props) {
        super(props, 'Update');
        for (const prop in props) {
            this[prop] = props[prop];
        }
    }
    /**@type {ModelProperty}*/ Primer_nombre = { type: 'text' };
    /**@type {ModelProperty}*/ Segundo_nombre = { type: 'text' };
    /**@type {ModelProperty}*/ Primer_apellido = { type: 'text' };
    /**@type {ModelProperty}*/ Segundo_apellido = { type: 'text' };

    /**@type {ModelProperty}*/ Identificacion = { type: 'text', 
        disabled: (pariente) => (pariente?.Identificacion == null || pariente?.Identificacion == undefined) ,
        hiddenFilter: true }; 
    /**@type {ModelProperty}*/ Sexo = { type: 'select', Dataset: ["Femenino", "Masculino"], hiddenInTable: true, hiddenFilter: true };
    /**@type {ModelProperty}*/ Fecha_nacimiento = { type: 'date', hiddenFilter: true };
    //**@type {ModelProperty}*/ Estado_civil_id = { type: 'number', hiddenInTable: true, hiddenFilter: true };
    /**@type {ModelProperty}*/ Estado_civil = { type: 'WSelect', ModelObject: () => new Estados_Civiles_ModelComponent(), hiddenInTable: true, hiddenFilter: true };
    /**@type {ModelProperty}*/ Religion = { type: 'WSelect', ModelObject: () => new Religiones_ModelComponent(), hiddenInTable: true, hiddenFilter: true };
    /**@type {ModelProperty}*/ Pais = { type: 'WSelect', ModelObject: () => new Paises_ModelComponent(), hiddenInTable: true, hiddenFilter: true };
    /**@type {ModelProperty}*/ Region = { type: 'WSelect', ModelObject: () => new Regiones_ModelComponent(), hiddenInTable: true, hiddenFilter: true };
    //pais
    //region
    /**@type {ModelProperty}*/ Telefono = { type: 'text', hiddenFilter: true };
    /**@type {ModelProperty}*/ Celular = { type: 'text', hiddenFilter: true };
    /**@type {ModelProperty}*/ Lugar_trabajo = { type: 'text', hiddenInTable: true, hiddenFilter: true };

    /**@type {ModelProperty}*/ Telefono_trabajo = { type: 'text', hiddenFilter: true , hiddenInTable: true};
    /**@type {ModelProperty}*/ Email = { type: 'text', hiddenFilter: true, hiddenInTable: true };
    //**@type {ModelProperty}*/ Relacion_Familiar = { type: 'select', Dataset: ["MADRE", "PADRE", "HERMANO", "TIO", "TIA", "ABUELA", "ABUELO"], hiddenInTable: true, hiddenFilter: true };

    /**@type {ModelProperty}*/ Direccion = { type: 'textarea', label: "Dirección familiar", hiddenFilter: true , hiddenInTable: true};

    /**@type {ModelProperty}*/ Titulo = { type: 'WSelect', ModelObject: () => new Titulos_ModelComponent(), hiddenInTable: true, hiddenFilter: true };
    /**@type {ModelProperty}*/ Ex_Alumno = { type: 'radio', Dataset: ["SI", "NO"], hiddenFilter: true, hiddenInTable: true };
    /**@type {ModelProperty}*/ EgresoExAlumno = { type: 'number', label: "Año de egreso",  max: 2030, min: 2000, hiddenFilter: true, hiddenInTable: true, require: false };
    //**@type {ModelProperty}*/ Estado_civil_id = { type: 'number', hiddenInTable: true, hiddenFilter: true };
    //**@type {ModelProperty}*/ Religion_id = { type: 'number', hiddenInTable: true, hiddenFilter: true };
    //**@type {ModelProperty}*/ Created_at = { type: 'date' , label: "Fecha", hiddenInTable: true, hiddenFilter: true};
    //**@type {ModelProperty}*/ Updated_at = { type: 'date',  hiddenInTable: true, hiddenFilter: true };
    /**@type {ModelProperty}*/ Ip_ingreso  = {type: "text", hidden: true}
}
export { Parientes_ModelComponent }
