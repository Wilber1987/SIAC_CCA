//@ts-check
// @ts-ignore
import { ModelProperty } from '../../WDevCore/WModules/CommonModel.js';
import { EntityClass } from '../../WDevCore/WModules/EntityClass.js';
import { Asignaturas_ModelComponent }  from './Asignaturas_ModelComponent.js'
import { Docentes_ModelComponent }  from './Docentes_ModelComponent.js'
class Docente_asignaturas_ModelComponent extends EntityClass {
   /** @param {Partial<Docente_asignaturas_ModelComponent>} [props] */
   constructor(props) {
       super(props, 'EntityDbo');
       for (const prop in props) {
           this[prop] = props[prop];
       }
   }
   /**@type {ModelProperty}*/ Id = { type: 'number', primary: true };
   /**@type {ModelProperty}*/ Jefe = { type: 'checkbox' };
   /**@type {ModelProperty}*/ Observaciones = { type: 'text' };
   /**@type {ModelProperty}*/ Created_at = { type: 'date' };
   /**@type {ModelProperty}*/ Updated_at = { type: 'date' };
   /**@type {ModelProperty}*/ Asignaturas = { type: 'WSELECT',  ModelObject: ()=> new Asignaturas_ModelComponent(), ForeignKeyColumn: 'Asignatura_id'};
   /**@type {ModelProperty}*/ Docentes = { type: 'WSELECT',  ModelObject: ()=> new Docentes_ModelComponent(), ForeignKeyColumn: 'Docente_id'};
}
export { Docente_asignaturas_ModelComponent }
