//@ts-check
// @ts-ignore
import { ModelProperty } from '../../WDevCore/WModules/CommonModel.js';
import { EntityClass } from '../../WDevCore/WModules/EntityClass.js';
import { Niveles_ModelComponent } from './Niveles_ModelComponent.js';
import { Periodo_lectivos_ModelComponent } from './Periodo_lectivos_ModelComponent.js';
const Grados = [
    { id: 1, Descripcion: "Primero"},
    { id: 2, Descripcion: "Segundo"},
    { id: 3, Descripcion: "Tercero"},
    { id: 4, Descripcion: "Cuarto"},
    { id: 5, Descripcion: "Quinto"},
    { id: 6, Descripcion: "Sexto"},
    { id: 7, Descripcion: "Septimo"},
    { id: 8, Descripcion: "Octavo"},
    { id: 9, Descripcion: "Noveno"},
    { id: 10, Descripcion: "Decimo"},
    { id: 11, Descripcion: "Onceavo"}
    
]
class Grado_ModelComponent {
    /**@type {ModelProperty}*/ id = { type: 'number' };
    /**@type {ModelProperty}*/ Descripcion = { type: 'text' };
}
/*,
const Grados = [
    { id: 1, Descripcion: "Primero", Nivel: 2},
    { id: 2, Descripcion: "Segundo", Nivel: 3},
    { id: 3, Descripcion: "Tercero", Nivel: 4},
    { id: 4, Descripcion: "Cuarto", Nivel: 5},
    { id: 5, Descripcion: "Quinto", Nivel: 6},
    { id: 6, Descripcion: "Sexto", Nivel: 1},
    { id: 7, Descripcion: "Septimo", Nivel: 1},
    { id: 8, Descripcion: "Octavo", Nivel: 1},
    { id: 9, Descripcion: "Noveno", Nivel: 1},
    { id: 10, Descripcion: "Decimo", Nivel: 1},
    { id: 11, Descripcion: "Onceavo", Nivel: 1}    
]*/
class Clases_ModelComponent extends EntityClass {   
   /** @param {Partial<Clases_ModelComponent>} [props] */
   constructor(props) {
       super(props, 'EntityDbo');
       for (const prop in props) {
           this[prop] = props[prop];
       }
   }
   /**@type {ModelProperty}*/ Id = { type: 'number', primary: true , hiddenFilter: true};
   /**@type {ModelProperty}*/ Grado = { type: 'number', hiddenInTable: true , hiddenFilter: true};
   /**@type {ModelProperty}*/ Grado_label = { type: 'WSELECT', Dataset: Grados, ModelObject: new Grado_ModelComponent()};
   /**@type {ModelProperty}*/ Nivel_id = { type: 'number' , hiddenInTable: true , hiddenFilter: true};
   /**@type {ModelProperty}*/ Periodo_lectivo_id = { type: 'number', hiddenInTable: true, hiddenFilter: true };   
   /**@type {ModelProperty}*/ Periodo_lectivos = { type: 'WSELECT', ModelObject: () => new Periodo_lectivos_ModelComponent(), ForeignKeyColumn: 'Periodo_lectivo_id' };
   /**@type {ModelProperty}*/ Niveles = { type: 'WSELECT', ModelObject: () => new Niveles_ModelComponent(), ForeignKeyColumn: 'Nivel_id' };
   /**@type {ModelProperty}*/ Observaciones = { type: 'text', hiddenFilter: true };
   /**@type {ModelProperty}*/ Created_at = { type: 'date' , label: "Fecha"};

   //**@type {ModelProperty}*/ Updated_at = { type: 'date', hiddenFilter: true };
   
}
export { Clases_ModelComponent, Grados }
