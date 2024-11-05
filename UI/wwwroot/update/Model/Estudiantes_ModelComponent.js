//@ts-check
// @ts-ignore
import { Paises_ModelComponent } from '../../Model/ModelComponent/Paises_ModelComponent.js';
import { Regiones_ModelComponent } from '../../Model/ModelComponent/Regiones_ModelComponent.js';
import { Religiones_ModelComponent } from '../../Model/ModelComponent/Religiones_ModelComponent.js';
// @ts-ignore
import { ModelProperty } from '../../WDevCore/WModules/CommonModel.js';
import { EntityClass } from '../../WDevCore/WModules/EntityClass.js';
export const sacramentos = [
    { id: "-", descripcion: "Seleccione" },
    { id: "B", descripcion: "Bautizo" },
    { id: "P", descripcion: "Primera Comunión" },
    { id: "C", descripcion: "Confirmación" }
];
class Estudiantes_ModelComponent extends EntityClass {
    /** @param {Partial<Estudiantes_ModelComponent>} [props] */
    constructor(props) {
        super(props, 'Update');
        for (const prop in props) {
            this[prop] = props[prop];
        }
    }
  
    /**@type {ModelProperty}*/ Fecha_nacimiento = { type: 'date', disabled: true };
    /**@type {ModelProperty}*/ Religion = { type: 'WSelect', ModelObject: () => new Religiones_ModelComponent(), hiddenInTable: true };
    /**@type {ModelProperty}*/ Pais = { type: 'WSelect', ModelObject: () => new Paises_ModelComponent(), hiddenInTable: true, hiddenFilter: true };
    /**@type {ModelProperty}*/ Region = { type: 'WSelect', ModelObject: () => new Regiones_ModelComponent(), hiddenInTable: true, hiddenFilter: true };
    /**@type {ModelProperty}*/ Direccion = { type: 'textarea', label: "Dirección familiar" };
    /**@type {ModelProperty}*/ Vive_con = { type: 'text', label: "Con quién vive" };
    /**@type {ModelProperty}*/ Colegio_procede = { type: 'text',  label: "Colegio de procedencia" };
    /**@type {ModelProperty}*/ Sacramento = {
        type: 'select', Dataset: sacramentos , require: false}
    /**@type {ModelProperty}*/ SacramentoA = { type: 'number', label: "Año de Saceamento",
         max: 2030, min: 2000, require: false };

    //**@type {ModelProperty}*/ Lugar_nacimiento = { type: 'text', hiddenInTable: true, hidden: true };
    //**@type {ModelProperty}*/ Sexo = { type: 'text', hiddenInTable: true, hidden: true };
    //**@type {ModelProperty}*/ Peso = { type: 'number', hiddenInTable: true };
    //**@type {ModelProperty}*/ Altura = { type: 'number', hiddenInTable: true };
    //**@type {ModelProperty}*/ Tipo_sangre = { type: 'text', hiddenInTable: true };
    //**@type {ModelProperty}*/ Padecimientos = { type: 'text', hiddenInTable: true };
    //**@type {ModelProperty}*/ Alergias = { type: 'text', hiddenInTable: true };
    //**@type {ModelProperty}*/ Activo = { type: 'checkbox', hiddenInTable: true };
    
    

    ///**@type {ModelProperty}*/ Recorrido_id = { type: 'number' };  
    ///**@type {ModelProperty}*/ Religion_id = { type: 'number' };
    ///**@type {ModelProperty}*/ Madre_id = { type: 'number' };
    ///**@type {ModelProperty}*/ Padre_id = { type: 'number' };
    ///**@type {ModelProperty}*/ Created_at = { type: 'date' , label: "Fecha"};
    ///**@type {ModelProperty}*/ Updated_at = { type: 'date', hiddenFilter: true };
    ///**@type {ModelProperty}*/ Estudiante_clases = { type: 'MasterDetail', ModelObject: () => new Estudiante_clases_ModelComponent() };
    ///**@type {ModelProperty}*/ Clase_Group = { type: 'MasterDetail', ModelObject: () => new Clase_Group() };
    ///**@type {ModelProperty}*/ Responsables = { type: 'MasterDetail', ModelObject: () => new Responsables_ModelComponent() };


}
export { Estudiantes_ModelComponent }

export class Adress_ModelComponent {    
    /** @type {ModelProperty}*/
    Direccion = { type: 'textarea' };
    /** @type {ModelProperty} IDA o VUELTA*/
    Trayecto = { type: 'select', Dataset: ["IDA", "VUELTA"], hidden: true };
}