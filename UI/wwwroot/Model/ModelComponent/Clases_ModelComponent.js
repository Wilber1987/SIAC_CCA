//@ts-check
// @ts-ignore
import { ModelProperty } from '../../WDevCore/WModules/CommonModel.js';
import { EntityClass } from '../../WDevCore/WModules/EntityClass.js';
import { Niveles_ModelComponent } from './Niveles_ModelComponent.js';
import { Periodo_lectivos_ModelComponent } from './Periodo_lectivos_ModelComponent.js';
const Grados = [
    //preescolar
    { id: 1, Grado: 1, Nivel: 3, Descripcion: "Primero" },
    { id: 2, Grado: 2, Nivel: 3, Descripcion: "Segundo" },
    { id: 3, Grado: 3, Nivel: 3, Descripcion: "Tercero" },
    //primaria
    { id: 1, Grado: 1, Nivel: 2, Descripcion: "Primero" },
    { id: 2, Grado: 2, Nivel: 2, Descripcion: "Segundo" },
    { id: 3, Grado: 3, Nivel: 2, Descripcion: "Tercero" },
    { id: 4, Grado: 4, Nivel: 2, Descripcion: "Cuarto" },
    { id: 5, Grado: 5, Nivel: 2, Descripcion: "Quinto" },
    { id: 6, Grado: 6, Nivel: 2, Descripcion: "Sexto" },
    //secundaria
    { id: 1, Grado: 1, Nivel: 1, Descripcion: "Septimo" },
    { id: 2, Grado: 2, Nivel: 1, Descripcion: "Octavo" },
    { id: 3, Grado: 3, Nivel: 1, Descripcion: "Noveno" },
    { id: 4, Grado: 4, Nivel: 1, Descripcion: "Decimo" },
    { id: 5, Grado: 5, Nivel: 1, Descripcion: "Onceavo" },
]
class Grado_ModelComponent {
    /**@type {ModelProperty}*/ id = { type: 'number', primary: true };
    /**@type {ModelProperty}*/ Descripcion = { type: 'text' };
}
class Clases_ModelComponent extends EntityClass {
    /** @param {Partial<Clases_ModelComponent>} [props] */
    constructor(props) {
        super(props, 'EntityDbo');
        for (const prop in props) {
            this[prop] = props[prop];
        }
    }
   /**@type {ModelProperty}*/ Id = { type: 'number', primary: true, hiddenFilter: true };
   /**@type {ModelProperty}*/ Periodo_lectivos = {
        label: "Periodo Lectivo", type: 'WSELECT',
        ModelObject: () => new Periodo_lectivos_ModelComponent(), ForeignKeyColumn: 'Periodo_lectivo_id'
    };
   /**@type {ModelProperty}*/ Niveles = {
        label: "Nivel", type: 'WSELECT',
        ModelObject: () => new Niveles_ModelComponent(),
        action: (nivelesSeleccionados, control) => {//valido solo para filtros
            if (this.Grado_label.Dataset != undefined) {
               this.Grado_label.Dataset.length = 0; 
            } else {
                this.Grado_label.Dataset = [];
            }
            this.Grado_label.Dataset.push(...Grados.filter(g => nivelesSeleccionados.map(nivel => nivel.Id).includes(g.Nivel)));
        },  
        ForeignKeyColumn: 'Nivel_id'
    };

   /**@type {ModelProperty}*/ Grado = { type: 'number', hiddenInTable: true, hiddenFilter: true };
   /**@type {ModelProperty}*/ Grado_label = { label: "Grado", type: 'WSELECT', ModelObject: new Grado_ModelComponent(), ForeignKeyColumn: "Grado" };
   /**@type {ModelProperty}*/ Nivel_id = { type: 'number', hiddenInTable: true, hiddenFilter: true };
   /**@type {ModelProperty}*/ Periodo_lectivo_id = { type: 'number', hiddenInTable: true, hiddenFilter: true };
   /**@type {ModelProperty}*/ Observaciones = { type: 'text', hiddenFilter: true, hiddenInTable: true };
   /**@type {ModelProperty}*/ Created_at = { type: 'date', label: "Fecha", hiddenInTable: true, hiddenFilter: true };

    //**@type {ModelProperty}*/ Updated_at = { type: 'date', hiddenFilter: true };

}
export { Clases_ModelComponent, Grados, Grado_ModelComponent }
