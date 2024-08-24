//@ts-check
// @ts-ignore
import { ModelProperty } from '../../WDevCore/WModules/CommonModel.js';
import { EntityClass } from '../../WDevCore/WModules/EntityClass.js';
import { Estudiante_clases_ModelComponent } from './Estudiante_clases_ModelComponent.js'
import { Evaluaciones_ModelComponent } from './Evaluaciones_ModelComponent.js'
import { Tipo_notas_ModelComponent } from './Tipo_notas_ModelComponent.js'
class Calificaciones_ModelComponent extends EntityClass {
    /** @param {Partial<Calificaciones_ModelComponent>} [props] */
    constructor(props) {
        super(props, 'EntityDbo');
        for (const prop in props) {
            this[prop] = props[prop];
        }
    }
   /**@type {ModelProperty}*/ Id = { type: 'number', primary: true };
   /**@type {ModelProperty}*/ Resultado = { type: 'number' };
   /**@type {ModelProperty}*/ Observaciones = { type: 'text' };
   /**@type {ModelProperty}*/ Created_at = { type: 'date' , label: "Fecha"};
   /**@type {ModelProperty}*/ Updated_at = { type: 'date', hiddenFilter: true };
   /**@type {ModelProperty}*/ Consolidado_id = { type: 'number' };
   /**@type {ModelProperty}*/ Materia_id = { type: 'number' };
   /**@type {ModelProperty}*/ Periodo = { type: 'number' };
   /**@type {ModelProperty}*/ Estudiante_clases = { type: 'WSELECT', ModelObject: () => new Estudiante_clases_ModelComponent(), ForeignKeyColumn: 'Estudiante_clase_id' };
   /**@type {ModelProperty}*/ Evaluaciones = { type: 'WSELECT', ModelObject: () => new Evaluaciones_ModelComponent(), ForeignKeyColumn: 'Evaluacion_id' };
   /**@type {ModelProperty}*/ Tipo_notas = { type: 'WSELECT', ModelObject: () => new Tipo_notas_ModelComponent(), ForeignKeyColumn: 'Tipo_nota_id' };
}
export { Calificaciones_ModelComponent }
