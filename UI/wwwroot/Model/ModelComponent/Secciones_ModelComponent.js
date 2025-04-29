//@ts-check
// @ts-ignore
import { ModelProperty } from '../../WDevCore/WModules/CommonModel.js';
import { EntityClass } from '../../WDevCore/WModules/EntityClass.js';
import { Secciones } from '../Secciones.js';
import { Docente_materias_ModelComponent } from './Docente_materias_ModelComponent.js'
import { Estudiante_clases_ModelComponent } from './Estudiante_clases_ModelComponent.js'
import { Evaluaciones_ModelComponent } from './Evaluaciones_ModelComponent.js'
class Secciones_ModelComponent extends EntityClass {
    /** @param {Partial<Secciones_ModelComponent>} [props] */
    constructor(props) {
        super(props, 'EntityDbo');
        for (const prop in props) {
            this[prop] = props[prop];
        }
    }
    /**@type {ModelProperty}*/ Clase = {
        type: 'Operation', action: (/**@type {Secciones} */ seccion) => {
            return seccion?.Clases?.Descripcion ?? ""
        }
    };
    /**@type {ModelProperty}*/ Id = { type: 'number', primary: true };
    /**@type {ModelProperty}*/ Nombre = { type: 'text', label: "Sección" };
    /**@type {ModelProperty}*/ Clase_id = { type: 'number', hiddenFilter: true, hiddenInTable: true };
    /**@type {ModelProperty}*/ Docente_id = { type: 'number', hiddenFilter: true, hiddenInTable: true };
    /**@type {ModelProperty}*/ Observaciones = { type: 'text', hiddenFilter: true, hiddenInTable: true };
    /**@type {ModelProperty}*/ Created_at = { type: 'date', label: "Fecha", hiddenFilter: true, hiddenInTable: true };
    /**@type {ModelProperty}*/ Updated_at = { type: 'date', hiddenFilter: true, hiddenInTable: true };
    /**@type {ModelProperty}*/ Docente_materias = { type: 'MasterDetail', ModelObject: () => new Docente_materias_ModelComponent() };
    /**@type {ModelProperty}*/ Estudiante_clases = { type: 'MasterDetail', ModelObject: () => new Estudiante_clases_ModelComponent() };
    /**@type {ModelProperty}*/ Evaluaciones = { type: 'MasterDetail', ModelObject: () => new Evaluaciones_ModelComponent() };
    /**@type {ModelProperty}*/ Guia = {
            type: 'Operation', action: (/**@type {Secciones} */ seccion) => {
            return seccion?.Guia?.Nombre_completo ?? ""
        }
    };
    
}
export { Secciones_ModelComponent }
