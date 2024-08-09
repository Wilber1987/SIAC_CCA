//@ts-check
// @ts-ignore
import { ModelProperty } from '../../WDevCore/WModules/CommonModel.js';
import { EntityClass } from '../../WDevCore/WModules/EntityClass.js';
import { Escolaridades_ModelComponent }  from './Escolaridades_ModelComponent.js'
import { Docente_asignaturas_ModelComponent }  from './Docente_asignaturas_ModelComponent.js'
import { Docente_materias_ModelComponent }  from './Docente_materias_ModelComponent.js'
class Docentes_ModelComponent extends EntityClass {
   /** @param {Partial<Docentes_ModelComponent>} [props] */
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
   /**@type {ModelProperty}*/ Sexo = { type: 'text' };
   /**@type {ModelProperty}*/ Fecha_nacimiento = { type: 'date' };
   /**@type {ModelProperty}*/ Lugar_nacimiento = { type: 'text' };
   /**@type {ModelProperty}*/ Direccion = { type: 'text' };
   /**@type {ModelProperty}*/ Telefono = { type: 'text' };
   /**@type {ModelProperty}*/ Celular = { type: 'text' };
   /**@type {ModelProperty}*/ Email = { type: 'text' };
   /**@type {ModelProperty}*/ Estado_civil_id = { type: 'number' };
   /**@type {ModelProperty}*/ Religion_id = { type: 'number' };
   /**@type {ModelProperty}*/ Foto = { type: 'text' };
   /**@type {ModelProperty}*/ Created_at = { type: 'date' };
   /**@type {ModelProperty}*/ Updated_at = { type: 'date' };
   /**@type {ModelProperty}*/ Habilitado = { type: 'checkbox' };
   /**@type {ModelProperty}*/ Cargo = { type: 'text' };
   /**@type {ModelProperty}*/ Escolaridades = { type: 'WSELECT',  ModelObject: ()=> new Escolaridades_ModelComponent(), ForeignKeyColumn: 'Escolaridad_id'};
   /**@type {ModelProperty}*/ Docente_asignaturas = { type: 'MasterDetail',  ModelObject: ()=> new Docente_asignaturas_ModelComponent()};
   /**@type {ModelProperty}*/ Docente_materias = { type: 'MasterDetail',  ModelObject: ()=> new Docente_materias_ModelComponent()};
}
export { Docentes_ModelComponent }
