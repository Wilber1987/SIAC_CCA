//@ts-check
import { EntityClass } from "../../../WDevCore/WModules/EntityClass.js";
import { WAjaxTools, BasicStates } from "../../../WDevCore/WModules/WComponentsTools.js";
//@ts-ignore
import { ModelProperty } from "../../../WDevCore/WModules/CommonModel.js";
class Estudiante_Clases_View extends EntityClass {
   /** @param {Partial<Estudiante_Clases_View>} [props] */
   constructor(props) {
       super(props, 'ViewDbo');
       for (const prop in props) {
           this[prop] = props[prop];
       }
   }
   /**@type {Date}*/ Transferido;
   /**@type {Number}*/ Estudiante_id;
   /**@type {Number}*/ Id;
   /**@type {Date}*/ Retirado;
   /**@type {Number}*/ Promedio;
   /**@type {Boolean}*/ Repitente;
   /**@type {Number}*/ Reprobadas;
   /**@type {String}*/ Nombre_periodo;
   /**@type {String}*/ Nombre_corto_periodo;
   /**@type {Date}*/ Inicio_periodo;
   /**@type {Date}*/ Fin_periodo;
   /**@type {Boolean}*/ Abierto;
   /**@type {Boolean}*/ Oculto;
   /**@type {String}*/ Nombre_nota;
   /**@type {String}*/ Nombre_corto_nota;
   /**@type {Number}*/ Numero_consolidados;
   /**@type {Number}*/ Consolidado_id;
   /**@type {Number}*/ Orden;
   /**@type {Number}*/ Resultado;
   /**@type {String}*/ Tipo;
   /**@type {}*/ Hora;
   /**@type {Date}*/ Fecha;
   /**@type {Number}*/ Porcentaje;
   /**@type {String}*/ Nombre_asignatura;
   /**@type {String}*/ Nombre_corto_asignatura;
   /**@type {String}*/ Nombre_grado;
   /**@type {String}*/ Nombre_corto_nivel;
   /**@type {String}*/ Nombre_nivel;
   /**@type {Number}*/ Numero_grados;
   /**@type {Number}*/ Inicio_grado;
   /**@type {Number}*/ Grado;
   /**@type {Number}*/ Clase_id;
}
export { Estudiante_Clases_View }
