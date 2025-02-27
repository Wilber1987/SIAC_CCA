//@ts-check
// @ts-ignore
import { ModelProperty } from '../../WDevCore/WModules/CommonModel.js';
import { EntityClass } from '../../WDevCore/WModules/EntityClass.js';
import { Estudiante_clases_ModelComponent }  from './Estudiante_clases_ModelComponent.js'
class Periodo_lectivos_ModelComponent extends EntityClass {
   /** @param {Partial<Periodo_lectivos_ModelComponent>} [props] */
   constructor(props) {
       super(props, 'EntityDbo');
       for (const prop in props) {
           this[prop] = props[prop];
       }
   }
   /**@type {ModelProperty}*/ Id = { type: 'number', primary: true };
   /**@type {ModelProperty}*/ Nombre = { type: 'text' };
   /**@type {ModelProperty}*/ Nombre_corto = {hiddenInTable: true, type: 'text' };
   /**@type {ModelProperty}*/ Inicio = { type: 'date', hiddenInTable:true };
   /**@type {ModelProperty}*/ Fin = { type: 'date', hiddenInTable:true };
   ///**@type {ModelProperty}*/ Observaciones = { type: 'text' };
   ///**@type {ModelProperty}*/ Config = { type: 'text' };
   ///**@type {ModelProperty}*/ Created_at = { type: 'date' , label: "Fecha"};
   ///**@type {ModelProperty}*/ Updated_at = { type: 'date', hiddenFilter: true };
   ///**@type {ModelProperty}*/ Abierto = { type: 'checkbox' };
   ///**@type {ModelProperty}*/ Oculto = { type: 'checkbox' };
   ///**@type {ModelProperty}*/ Estudiante_clases = { type: 'MasterDetail',  ModelObject: ()=> new Estudiante_clases_ModelComponent()};
}
export { Periodo_lectivos_ModelComponent }
