//@ts-check
// @ts-ignore
import { ModelProperty } from '../../WDevCore/WModules/CommonModel.js';
import { EntityClass } from '../../WDevCore/WModules/EntityClass.js';
import { Estudiante_clases_ModelComponent }  from './Estudiante_clases_ModelComponent.js'
import { Materias_ModelComponent }  from './Materias_ModelComponent.js'
class Clases_ModelComponent extends EntityClass {   
   /** @param {Partial<Clases_ModelComponent>} [props] */
   constructor(props) {
       super(props, 'EntityDbo');
       for (const prop in props) {
           this[prop] = props[prop];
       }
   }
   /**@type {ModelProperty}*/ Id = { type: 'number', primary: true };
   /**@type {ModelProperty}*/ Grado = { type: 'number' };
   /**@type {ModelProperty}*/ Nivel_id = { type: 'number' };
   /**@type {ModelProperty}*/ Periodo_lectivo_id = { type: 'number' };
   /**@type {ModelProperty}*/ Observaciones = { type: 'text' };
   /**@type {ModelProperty}*/ Created_at = { type: 'date' , label: "Fecha"};
   /**@type {ModelProperty}*/ Updated_at = { type: 'date', hiddenFilter: true };
   /**@type {ModelProperty}*/ Estudiante_clases = { type: 'MasterDetail',  ModelObject: ()=> new Estudiante_clases_ModelComponent()};
   /**@type {ModelProperty}*/ Materias = { type: 'MasterDetail',  ModelObject: ()=> new Materias_ModelComponent()};
}
export { Clases_ModelComponent }
