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
export const sexos = [
    { id: "m", descripcion: "Femenino" },
    { id: "f", descripcion: "Masculino" }
];
export const viveConJson = [
    { "id": "A", "Descripcion": "Ambos Padres" },
    { "id": "M", "Descripcion": "Madre" }, 
    { "id": "P", "Descripcion": "Padre" },
    { "id": "B", "Descripcion": "Abuelo(a)" }, 
    { "id": "T", "Descripcion": "Tio(a)" },
    { "id": "O", "Descripcion": "Otros" }
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
    /**@type {ModelProperty}*/ Religion = { type: 'WSelect', ModelObject: () => new Religiones_ModelComponent(), hiddenInTable: true, fullDetail: false, require: false };
    /**@type {ModelProperty}*/ Pais = { type: 'WSelect', ModelObject: () => new Paises_ModelComponent(), hiddenInTable: true, hiddenFilter: true, fullDetail: false };
    /**@type {ModelProperty}*/ Region = { label: "Ciudad", type: 'WSelect', ModelObject: () => new Regiones_ModelComponent(), hiddenInTable: true, hiddenFilter: true, fullDetail: false };
    /**@type {ModelProperty}*/ Direccion = { type: 'textarea', label: "Dirección familiar" };
    /**@type {ModelProperty}*/ Vivecon = { type: 'select', label: "Con quién vive", Dataset: viveConJson };
    /**@type {ModelProperty}*/ Colegio_procede = { type: 'text', require: false, label: "Colegio de procedencia" };
    /**@type {ModelProperty}*/ Sacramento = {
        type: 'select', Dataset: sacramentos, require: false
    }
    /**@type {ModelProperty}*/ Aniosacra = { type: 'number', label: "Año de Sacramento", min: 1990, require: false };

}
export { Estudiantes_ModelComponent }

export class Adress_ModelComponent {
    /** @type {ModelProperty}*/
    Direccion = { type: 'textarea' };
    /** @type {ModelProperty} IDA o VUELTA*/
    Trayecto = { type: 'select', Dataset: ["IDA", "VUELTA"], hidden: true };
}