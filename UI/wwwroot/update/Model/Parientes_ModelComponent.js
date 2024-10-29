//@ts-check
// @ts-ignore
import { ModelProperty } from '../../WDevCore/WModules/CommonModel.js';
import { EntityClass } from '../../WDevCore/WModules/EntityClass.js';

class Parientes_ModelComponent extends EntityClass {
   /** @param {Partial<Parientes_ModelComponent>} [props] */
   constructor(props) {
       super(props, 'Update');
       for (const prop in props) {
           this[prop] = props[prop];
       }
   } 
   /**@type {ModelProperty}*/ Identificacion = { type: 'text' };
   /**@type {ModelProperty}*/ Sexo = { type: 'select', Dataset: ["Femenino", "Masculino"], hiddenInTable: true, hiddenFilter: true };
   /**@type {ModelProperty}*/ Fecha_nacimiento = { type: 'text' };
   
   /**@type {ModelProperty}*/ Profesion = { type: 'text' , hiddenInTable: true, hiddenFilter: true };
   
   /**@type {ModelProperty}*/ Direccion = { type: 'text' };
   /**@type {ModelProperty}*/ Lugar_trabajo = { type: 'text', hiddenInTable: true, hiddenFilter: true };
   /**@type {ModelProperty}*/ Telefono = { type: 'text' };
   /**@type {ModelProperty}*/ Celular = { type: 'text' };
   /**@type {ModelProperty}*/ Telefono_trabajo = { type: 'text' };
   /**@type {ModelProperty}*/ Email = { type: 'text' };
   /**@type {ModelProperty}*/ Estado_civil_id = { type: 'number', hiddenInTable: true, hiddenFilter: true };
   /**@type {ModelProperty}*/ Religion_id = { type: 'number', hiddenInTable: true, hiddenFilter: true };
   /**@type {ModelProperty}*/ Created_at = { type: 'date' , label: "Fecha", hiddenInTable: true, hiddenFilter: true};
   /**@type {ModelProperty}*/ Updated_at = { type: 'date',  hiddenInTable: true, hiddenFilter: true };
}
export { Parientes_ModelComponent }
