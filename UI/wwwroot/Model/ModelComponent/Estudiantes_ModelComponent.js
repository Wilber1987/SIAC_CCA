//@ts-check
// @ts-ignore
import { ModelProperty } from '../../WDevCore/WModules/CommonModel.js';
import { EntityClass } from '../../WDevCore/WModules/EntityClass.js';
import { Estudiante_clases_ModelComponent } from './Estudiante_clases_ModelComponent.js'
import { Responsables_ModelComponent } from './Responsables_ModelComponent.js'
class Estudiantes_ModelComponent extends EntityClass {
    /** @param {Partial<Estudiantes_ModelComponent>} [props] */
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
   /**@type {ModelProperty}*/ Fecha_nacimiento = { type: 'date' };
   /**@type {ModelProperty}*/ Lugar_nacimiento = { type: 'text' };
   /**@type {ModelProperty}*/ Sexo = { type: 'text' };
   /**@type {ModelProperty}*/ Codigo = { type: 'text' };
   /**@type {ModelProperty}*/ Religion_id = { type: 'number' };
   /**@type {ModelProperty}*/ Madre_id = { type: 'number' };
   /**@type {ModelProperty}*/ Padre_id = { type: 'number' };
   /**@type {ModelProperty}*/ Created_at = { type: 'date' };
   /**@type {ModelProperty}*/ Updated_at = { type: 'date' };
   /**@type {ModelProperty}*/ Foto = { type: 'img' };
   /**@type {ModelProperty}*/ Peso = { type: 'number' };
   /**@type {ModelProperty}*/ Altura = { type: 'number' };
   /**@type {ModelProperty}*/ Tipo_sangre = { type: 'text' };
   /**@type {ModelProperty}*/ Padecimientos = { type: 'text' };
   /**@type {ModelProperty}*/ Alergias = { type: 'text' };
   /**@type {ModelProperty}*/ Recorrido_id = { type: 'number' };
   /**@type {ModelProperty}*/ Activo = { type: 'checkbox' };
   /**@type {ModelProperty}*/ Direccion = { type: 'textarea' };
   ///**@type {ModelProperty}*/ Estudiante_clases = { type: 'MasterDetail', ModelObject: () => new Estudiante_clases_ModelComponent() };
   /**@type {ModelProperty}*/ Clase_Group = { type: 'MasterDetail', ModelObject: () => new Clase_Group() };
   /**@type {ModelProperty}*/ Responsables = { type: 'MasterDetail', ModelObject: () => new Responsables_ModelComponent() };
   

}
export { Estudiantes_ModelComponent }


class Calificacion_Group {
    constructor(props) {
        for (const prop in props) {
            this[prop] = props[prop];
        }
    }
    /** @type {ModelProperty}*/
    Resultado = { type: 'number' };;

    /** @type {ModelProperty}*/
    Evaluacion = { type: 'text' };

    /** @type {ModelProperty}*/
    Tipo = { type: 'text' };
}
export { Calificacion_Group };

class Asignatura_Group {
    constructor(props) {
        for (const prop in props) {
            this[prop] = props[prop];
        }
    }
    /** @type {ModelProperty}*/
    Descripcion = { type: 'text' };

    /** @type {ModelProperty}*/
    Evaluaciones = { type: 'text' };

    /** @type {ModelProperty}*/
    Calificaciones = { type: 'MasterDetail', ModelObject: () => new Calificacion_Group() };
    get Details() { return this.Calificaciones }
}
export { Asignatura_Group };
class Clase_Group {
    constructor(props) {
        for (const prop in props) {
            this[prop] = props[prop];
        }
    }
    /** @type {ModelProperty}*/
    Descripcion = { type: 'text' };

    /** @type {ModelProperty}*/
    Asignaturas = { type: 'MasterDetail', ModelObject: () => new Asignatura_Group() };
    get Details() { return this.Asignaturas }
}
export { Clase_Group };

