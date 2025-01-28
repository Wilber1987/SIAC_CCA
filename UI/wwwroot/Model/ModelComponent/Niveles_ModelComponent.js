//@ts-check
// @ts-ignore
import { ModelProperty } from '../../WDevCore/WModules/CommonModel.js';
import { EntityClass } from '../../WDevCore/WModules/EntityClass.js';
import { Asignaturas_ModelComponent }  from './Asignaturas_ModelComponent.js'
class Niveles_ModelComponent extends EntityClass {
   /** @param {Partial<Niveles_ModelComponent>} [props] */
   constructor(props) {
       super(props, 'EntityDbo');
       for (const prop in props) {
           this[prop] = props[prop];
       }
   }
   /**@type {ModelProperty}*/ Id = { type: 'number', primary: true };
   /**@type {ModelProperty}*/ Nombre = { type: 'text' };
   /**@type {ModelProperty}*/ Nombre_corto = { type: 'text' , hiddenInTable: true};
   /**@type {ModelProperty}*/ Nombre_grado = { type: 'text'};
   ///**@type {ModelProperty}*/ Numero_grados = { type: 'number' };
   /**@type {ModelProperty}*/ Observaciones = { type: 'text' };
   /**@type {ModelProperty}*/ Habilitado = { type: 'checkbox', hiddenInTable: true };
   ///**@type {ModelProperty}*/ Created_at = { type: 'date' , label: "Fecha"};
   ///**@type {ModelProperty}*/ Updated_at = { type: 'date', hiddenFilter: true };
   ///**@type {ModelProperty}*/ Orden = { type: 'number' };
   ///**@type {ModelProperty}*/ Inicio_grado = { type: 'number' };
   ///**@type {ModelProperty}*/ Asignaturas = { type: 'MasterDetail',  ModelObject: ()=> new Asignaturas_ModelComponent()};
}
export { Niveles_ModelComponent }
