//@ts-check
// @ts-ignore
import { ModelProperty } from '../../WDevCore/WModules/CommonModel.js';
import { EntityClass } from '../../WDevCore/WModules/EntityClass.js';
class Estudiantes_ModelComponent extends EntityClass {
    /** @param {Partial<Estudiantes_ModelComponent>} [props] */
    constructor(props) {
        super(props, 'Update');
        for (const prop in props) {
            this[prop] = props[prop];
        }
    }

    ///**@type {ModelProperty}*/ Foto = { type: 'img', hidden: true };
    /**@type {ModelProperty}*/ Fotografia = { type: 'img' , hidden: true};
    /**@type {ModelProperty}*/ Codigo = { type: 'text' };
    /**@type {ModelProperty}*/ Id = { type: 'number', primary: true };
    /**@type {ModelProperty}*/ Nombre_completo = { type: 'text'};
    /**@type {ModelProperty}*/ Primer_nombre = { type: 'text', hiddenInTable: true, hidden: true };
    /**@type {ModelProperty}*/ Segundo_nombre = { type: 'text', hiddenInTable: true, hidden: true };
    /**@type {ModelProperty}*/ Primer_apellido = { type: 'text', hiddenInTable: true, hidden: true };
    /**@type {ModelProperty}*/ Segundo_apellido = { type: 'text', hiddenInTable: true , hidden: true};
    /**@type {ModelProperty}*/ Fecha_nacimiento = { type: 'date' , hidden: true};
    /**@type {ModelProperty}*/ Lugar_nacimiento = { type: 'text', hiddenInTable: true , hidden: true};
    /**@type {ModelProperty}*/ Sexo = { type: 'text', hiddenInTable: true, hidden: true };
    /**@type {ModelProperty}*/ Peso = { type: 'number', hiddenInTable: true };
    /**@type {ModelProperty}*/ Altura = { type: 'number', hiddenInTable: true };
    /**@type {ModelProperty}*/ Tipo_sangre = { type: 'text', hiddenInTable: true };
    /**@type {ModelProperty}*/ Padecimientos = { type: 'text', hiddenInTable: true };
    /**@type {ModelProperty}*/ Alergias = { type: 'text', hiddenInTable: true };
    /**@type {ModelProperty}*/ Activo = { type: 'checkbox', hiddenInTable: true };
    /**@type {ModelProperty}*/ Direccion = { type: 'textarea', hiddenInTable: true , hidden: true};

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

