//@ts-check
import { EntityClass } from "../../../WDevCore/WModules/EntityClass.js";
import { WAjaxTools, BasicStates } from "../../../WDevCore/WModules/WComponentsTools.js";
//@ts-ignore
import { ModelProperty } from "../../../WDevCore/WModules/CommonModel.js";
class Estudiante_Clases_View_ModelComponent extends EntityClass {
   /** @param {Partial<Estudiante_Clases_View_ModelComponent>} [props] */
   constructor(props) {
       super(props, 'ViewDbo');
       for (const prop in props) {
           this[prop] = props[prop];
       }
   }
   /**@type {ModelProperty}*/ Transferido = { type: 'date' };
   /**@type {ModelProperty}*/ Estudiante_id = { type: 'number' };
   /**@type {ModelProperty}*/ Id = { type: 'number' };
   /**@type {ModelProperty}*/ Retirado = { type: 'date' };
   /**@type {ModelProperty}*/ Promedio = { type: 'number' };
   /**@type {ModelProperty}*/ Repitente = { type: 'checkbox' };
   /**@type {ModelProperty}*/ Reprobadas = { type: 'number' };
   /**@type {ModelProperty}*/ Nombre_periodo = { type: 'text' };
   /**@type {ModelProperty}*/ Nombre_corto_periodo = { type: 'text' };
   /**@type {ModelProperty}*/ Inicio_periodo = { type: 'date' };
   /**@type {ModelProperty}*/ Fin_periodo = { type: 'date' };
   /**@type {ModelProperty}*/ Abierto = { type: 'checkbox' };
   /**@type {ModelProperty}*/ Oculto = { type: 'checkbox' };
   /**@type {ModelProperty}*/ Nombre_nota = { type: 'text' };
   /**@type {ModelProperty}*/ Nombre_corto_nota = { type: 'text' };
   /**@type {ModelProperty}*/ Numero_consolidados = { type: 'number' };
   /**@type {ModelProperty}*/ Consolidado_id = { type: 'number' };
   /**@type {ModelProperty}*/ Orden = { type: 'number' };
   /**@type {ModelProperty}*/ Resultado = { type: 'number' };
   /**@type {ModelProperty}*/ Tipo = { type: 'text' };
   /**@type {ModelProperty}*/ Hora = { type: '' };
   /**@type {ModelProperty}*/ Fecha = { type: 'date' };
   /**@type {ModelProperty}*/ Porcentaje = { type: 'number' };
   /**@type {ModelProperty}*/ Nombre_asifnatura = { type: 'text' };
   /**@type {ModelProperty}*/ Nombre_corto_asignatura = { type: 'text' };
   /**@type {ModelProperty}*/ Nombre_grado = { type: 'text' };
   /**@type {ModelProperty}*/ Nombre_corto_nivel = { type: 'text' };
   /**@type {ModelProperty}*/ Nombre_nivel = { type: 'text' };
   /**@type {ModelProperty}*/ Numero_grados = { type: 'number' };
   /**@type {ModelProperty}*/ Inicio_grado = { type: 'number' };
   /**@type {ModelProperty}*/ Grado = { type: 'number' };
}
export { Estudiante_Clases_View_ModelComponent }
