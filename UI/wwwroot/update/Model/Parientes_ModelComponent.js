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
export const sexos = [
    { id: "M", descripcion: "Masculino" },
    { id: "F", descripcion: "Femenino" }    
];
class Parientes_ModelComponent extends EntityClass {
    /** @param {Partial<Parientes_ModelComponent>} [props] */
    constructor(props) {
        super(props, 'Update');
        for (const prop in props) {
            this[prop] = props[prop];
        }
    }
    /**@type {ModelProperty}*/ Primer_nombre = { type: 'text' };
    /**@type {ModelProperty}*/ Segundo_nombre = { type: 'text', require: false };
    /**@type {ModelProperty}*/ Primer_apellido = { type: 'text' };
    /**@type {ModelProperty}*/ Segundo_apellido = { type: 'text', require: false };

    /**@type {ModelProperty}*/ Identificacion = { type: 'text', 
        //disabled: (pariente) => (pariente?.Identificacion == null || pariente?.Identificacion == undefined) ,
        }; 
    /**@type {ModelProperty}*/ Sexo = { type: 'select', Dataset: sexos, hiddenInTable: true, hiddenFilter: true };
    /**@type {ModelProperty}*/ Fecha_Nacimiento = { type: 'date', hiddenFilter: true , hiddenInTable: true };
    
    //**@type {ModelProperty}*/ Estado_civil_id = { type: 'number', hiddenInTable: true, hiddenFilter: true };
    /**@type {ModelProperty}*/ Estado_civil = { type: 'WSelect', ModelObject: () => new Estados_Civiles_ModelComponent(), hiddenInTable: true, hiddenFilter: true , fullDetail: false};
    /**@type {ModelProperty}*/ Religion = { type: 'WSelect', ModelObject: () => new Religiones_ModelComponent(), hiddenInTable: true, hiddenFilter: true , fullDetail: false, require: false};
    /**@type {ModelProperty}*/ Pais = { type: 'WSelect', ModelObject: () => new Paises_ModelComponent(), hiddenInTable: true, hiddenFilter: true , fullDetail: false};
    /**@type {ModelProperty}*/ Region = { type: 'WSelect', ModelObject: () => new Regiones_ModelComponent(),label: "Departamento", hiddenInTable: true, hiddenFilter: true , fullDetail: false};
    //pais
    //region
    /**@type {ModelProperty}*/ Telefono = { type: 'text', hiddenFilter: true };
    /**@type {ModelProperty}*/ Celular = { type: 'text', hiddenFilter: true };
    /**@type {ModelProperty}*/ Lugar_trabajo = { type: 'text', hiddenInTable: true, hiddenFilter: true, require: false };

    /**@type {ModelProperty}*/ Telefono_trabajo = { type: 'text', hiddenFilter: true , hiddenInTable: true, require: false};
    /**@type {ModelProperty}*/ Email = { type: 'text', hiddenFilter: true };
    //**@type {ModelProperty}*/ Relacion_Familiar = { type: 'select', Dataset: ["MADRE", "PADRE", "HERMANO", "TIO", "TIA", "ABUELA", "ABUELO"], hiddenInTable: true, hiddenFilter: true };
    /**@type {ModelProperty}*/ Direccion = { type: 'textarea', label: "Dirección familiar", hiddenFilter: true , hiddenInTable: true};    
    /**@type {ModelProperty}*/ Ex_Alumno = { type: 'radio', Dataset: ["SI", "NO"], hiddenFilter: true, hiddenInTable: true, require: false };

    /**@type {ModelProperty}*/ EgresoExAlumno = { type: 'number', label: "Año de egreso", min: 1980, max: new Date().getFullYear(), hiddenFilter: true, hiddenInTable: true, require: false, action: (object, form, control)=> {
        
        object.EgresoExAlumno = Math.round(object.EgresoExAlumno);       
        if(object.EgresoExAlumno < parseFloat(control.min) ){
            object.EgresoExAlumno = Math.round(object.min);
        } else  if(object.EgresoExAlumno > parseFloat(control.max) ){
            object.EgresoExAlumno = Math.round(object.max);
        }
        control.value = object.EgresoExAlumno;
    } };
    //**@type {ModelProperty}*/ Estado_civil_id = { type: 'number', hiddenInTable: true, hiddenFilter: true };
    //**@type {ModelProperty}*/ Id_religion = { type: 'number', hiddenInTable: true, hiddenFilter: true };
    //**@type {ModelProperty}*/ Created_at = { type: 'date' , label: "Fecha", hiddenInTable: true, hiddenFilter: true};
    //**@type {ModelProperty}*/ Updated_at = { type: 'date',  hiddenInTable: true, hiddenFilter: true };
    /**@type {ModelProperty}*/ Ip_ingreso  = {type: "text", hidden: true, require: false}
    ///**@type {ModelProperty}*/ Titulo = { type: 'WSelect', ModelObject: () => new Titulos_ModelComponent(), hiddenInTable: true, hiddenFilter: true, fullDetail: false , require: false, label: "Profesión u oficio"};
    /**@type {ModelProperty}*/ Profesion = { type: 'text',  hiddenInTable: true, hiddenFilter: true,  require: false, label: "Profesión u oficio"};
}
export { Parientes_ModelComponent }
