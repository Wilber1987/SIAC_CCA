//@ts-check
// @ts-ignore
import { ModelProperty } from '../../WDevCore/WModules/CommonModel.js';
import { EntityClass } from '../../WDevCore/WModules/EntityClass.js';
import { Estudiantes_ModelComponent }  from './Estudiantes_ModelComponent.js'
import { Parientes_ModelComponent }  from './Parientes_ModelComponent.js'
class Responsables_ModelComponent extends EntityClass {
   /** @param {Partial<Responsables_ModelComponent>} [props] */
   constructor(props) {
       super(props, 'EntityDbo');
       for (const prop in props) {
           this[prop] = props[prop];
       }
   }
    /**@type {ModelProperty}*/ Id = { type: 'number', primary: true };
    /**@type {ModelProperty}*/ Primer_nombre = { type: 'text' };
    /**@type {ModelProperty}*/ Segundo_nombre = { type: 'text', hiddenFilter: true };
    /**@type {ModelProperty}*/ Primer_apellido = { type: 'text' };
    /**@type {ModelProperty}*/ Segundo_apellido = { type: 'text' };
    /**@type {ModelProperty}*/ Telefono = { type: 'text', hiddenFilter: true  };
    /**@type {ModelProperty}*/ Celular = { type: 'text', hiddenFilter: true  };
    /**@type {ModelProperty}*/ Telefono_trabajo = { type: 'text', hiddenFilter: true  };
    /**@type {ModelProperty}*/ Email = { type: 'text' };
    /**@type {ModelProperty}*/ User_id = { type: 'number', primary: true , hiddenFilter: true };
}
export { Responsables_ModelComponent }
