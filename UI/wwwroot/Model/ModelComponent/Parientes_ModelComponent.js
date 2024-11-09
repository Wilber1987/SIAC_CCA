//@ts-check
// @ts-ignore
import { ModelProperty } from '../../WDevCore/WModules/CommonModel.js';
import { EntityClass } from '../../WDevCore/WModules/EntityClass.js';
import { Responsables_ModelComponent }  from './Responsables_ModelComponent.js'
class Parientes_ModelComponent extends EntityClass {
   /** @param {Partial<Parientes_ModelComponent>} [props] */
   constructor(props) {
       super(props, 'EntityDbo');
       for (const prop in props) {
           this[prop] = props[prop];
       }
   }
   /**@type {ModelProperty}*/ Id = { type: 'number', primary: true };
   /**@type {ModelProperty}*/ Primer_nombre = { type: 'text' };
   /**@type {ModelProperty}*/ Segundo_nombre = { type: 'text' };
   /**@type {ModelProperty}*/ Primer_apellido = { type: 'text' };
   /**@type {ModelProperty}*/ Segundo_apellido = { type: 'text' };
   /**@type {ModelProperty}*/ Sexo = { type: 'text', hiddenInTable: true, hiddenFilter: true };
   /**@type {ModelProperty}*/ Profesion = { type: 'text' , hiddenInTable: true, hiddenFilter: true };
   /**@type {ModelProperty}*/ Direccion = { type: 'text' };
   /**@type {ModelProperty}*/ Lugar_trabajo = { type: 'text', hiddenInTable: true, hiddenFilter: true };
   /**@type {ModelProperty}*/ Telefono = { type: 'text' };
   /**@type {ModelProperty}*/ Celular = { type: 'text' };
   /**@type {ModelProperty}*/ Telefono_trabajo = { type: 'text' };
   /**@type {ModelProperty}*/ Email = { type: 'text' };
   /**@type {ModelProperty}*/ Estado_civil_id = { type: 'number', hiddenInTable: true, hiddenFilter: true };
   /**@type {ModelProperty}*/ Id_religion = { type: 'number', hiddenInTable: true, hiddenFilter: true };
   /**@type {ModelProperty}*/ Created_at = { type: 'date' , label: "Fecha", hiddenInTable: true, hiddenFilter: true};
   /**@type {ModelProperty}*/ Updated_at = { type: 'date',  hiddenInTable: true, hiddenFilter: true };
   /**@type {ModelProperty}*/ Responsables = { type: 'MasterDetail',  ModelObject: ()=> new Responsables_ModelComponent()};
}
export { Parientes_ModelComponent }
