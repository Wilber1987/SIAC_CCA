//@ts-check
// @ts-ignore
import { ModelProperty } from '../../WDevCore/WModules/CommonModel.js';
import { EntityClass } from '../../WDevCore/WModules/EntityClass.js';
import { Clases_ModelComponent }  from './Clases_ModelComponent.js'
import { Estudiantes_ModelComponent }  from './Estudiantes_ModelComponent.js'
import { Periodo_lectivos_ModelComponent }  from './Periodo_lectivos_ModelComponent.js'
import { Secciones_ModelComponent }  from './Secciones_ModelComponent.js'
import { Calificaciones_ModelComponent }  from './Calificaciones_ModelComponent.js'
class Estudiante_clases_ModelComponent extends EntityClass {
   /** @param {Partial<Estudiante_clases_ModelComponent>} [props] */
   constructor(props) {
       super(props, 'EntityDbo');
       for (const prop in props) {
           this[prop] = props[prop];
       }
   }
   /**@type {ModelProperty}*/ Id = { type: 'number', primary: true };
   /**@type {ModelProperty}*/ Transferido = { type: 'date' };
   /**@type {ModelProperty}*/ Retirado = { type: 'date' };
   /**@type {ModelProperty}*/ Observaciones = { type: 'text' };
   /**@type {ModelProperty}*/ Created_at = { type: 'date' };
   /**@type {ModelProperty}*/ Updated_at = { type: 'date' };
   /**@type {ModelProperty}*/ Promedio = { type: 'number' };
   /**@type {ModelProperty}*/ Repitente = { type: 'checkbox' };
   /**@type {ModelProperty}*/ Reprobadas = { type: 'number' };
   /**@type {ModelProperty}*/ Clases = { type: 'WSELECT',  ModelObject: ()=> new Clases_ModelComponent(), ForeignKeyColumn: 'Clase_id'};
   /**@type {ModelProperty}*/ Estudiantes = { type: 'WSELECT',  ModelObject: ()=> new Estudiantes_ModelComponent(), ForeignKeyColumn: 'Estudiante_id'};
   /**@type {ModelProperty}*/ Periodo_lectivos = { type: 'WSELECT',  ModelObject: ()=> new Periodo_lectivos_ModelComponent(), ForeignKeyColumn: 'Periodo_lectivo_id'};
   /**@type {ModelProperty}*/ Secciones = { type: 'WSELECT',  ModelObject: ()=> new Secciones_ModelComponent(), ForeignKeyColumn: 'Seccion_id'};
   /**@type {ModelProperty}*/ Calificaciones = { type: 'MasterDetail',  ModelObject: ()=> new Calificaciones_ModelComponent()};
}
export { Estudiante_clases_ModelComponent }
