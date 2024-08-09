//@ts-check
// @ts-ignore
import { ModelProperty } from '../../WDevCore/WModules/CommonModel.js';
import { EntityClass } from '../../WDevCore/WModules/EntityClass.js';
import { Materias_ModelComponent }  from './Materias_ModelComponent.js'
import { Secciones_ModelComponent }  from './Secciones_ModelComponent.js'
import { Calificaciones_ModelComponent }  from './Calificaciones_ModelComponent.js'
class Evaluaciones_ModelComponent extends EntityClass {
   /** @param {Partial<Evaluaciones_ModelComponent>} [props] */
   constructor(props) {
       super(props, 'EntityDbo');
       for (const prop in props) {
           this[prop] = props[prop];
       }
   }
   /**@type {ModelProperty}*/ Id = { type: 'number', primary: true };
   /**@type {ModelProperty}*/ Fecha = { type: 'date' };
   /**@type {ModelProperty}*/ Hora = { type: '' };
   /**@type {ModelProperty}*/ Tipo = { type: 'text' };
   /**@type {ModelProperty}*/ Porcentaje = { type: 'number' };
   /**@type {ModelProperty}*/ Observaciones = { type: 'text' };
   /**@type {ModelProperty}*/ Created_at = { type: 'date' };
   /**@type {ModelProperty}*/ Updated_at = { type: 'date' };
   /**@type {ModelProperty}*/ Periodo = { type: 'number' };
   /**@type {ModelProperty}*/ Nota_maxima = { type: 'number' };
   /**@type {ModelProperty}*/ Materias = { type: 'WSELECT',  ModelObject: ()=> new Materias_ModelComponent(), ForeignKeyColumn: 'Materia_id'};
   /**@type {ModelProperty}*/ Secciones = { type: 'WSELECT',  ModelObject: ()=> new Secciones_ModelComponent(), ForeignKeyColumn: 'Seccion_id'};
   /**@type {ModelProperty}*/ Calificaciones = { type: 'MasterDetail',  ModelObject: ()=> new Calificaciones_ModelComponent()};
}
export { Evaluaciones_ModelComponent }
