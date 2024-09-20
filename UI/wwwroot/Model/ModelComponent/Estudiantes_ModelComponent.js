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

    ///**@type {ModelProperty}*/ Foto = { type: 'img', hidden: true };
    /**@type {ModelProperty}*/ Fotografia = { type: 'img' };
    /**@type {ModelProperty}*/ Codigo = { type: 'text' };
    /**@type {ModelProperty}*/ Id = { type: 'number', primary: true };
    /**@type {ModelProperty}*/ Nombre_completo = { type: 'text', hidden: true };
    /**@type {ModelProperty}*/ Primer_nombre = { type: 'text', hiddenInTable: true };
    /**@type {ModelProperty}*/ Segundo_nombre = { type: 'text', hiddenInTable: true };
    /**@type {ModelProperty}*/ Primer_apellido = { type: 'text', hiddenInTable: true };
    /**@type {ModelProperty}*/ Segundo_apellido = { type: 'text', hiddenInTable: true };
    /**@type {ModelProperty}*/ Fecha_nacimiento = { type: 'date' };
    /**@type {ModelProperty}*/ Lugar_nacimiento = { type: 'text', hiddenInTable: true };
    /**@type {ModelProperty}*/ Sexo = { type: 'text', hiddenInTable: true };
    /**@type {ModelProperty}*/ Peso = { type: 'number', hiddenInTable: true };
    /**@type {ModelProperty}*/ Altura = { type: 'number', hiddenInTable: true };
    /**@type {ModelProperty}*/ Tipo_sangre = { type: 'text', hiddenInTable: true };
    /**@type {ModelProperty}*/ Padecimientos = { type: 'text', hiddenInTable: true };
    /**@type {ModelProperty}*/ Alergias = { type: 'text', hiddenInTable: true };
    /**@type {ModelProperty}*/ Activo = { type: 'checkbox', hiddenInTable: true };
    /**@type {ModelProperty}*/ Direccion = { type: 'textarea', hiddenInTable: true };

    ///**@type {ModelProperty}*/ Recorrido_id = { type: 'number' };  
    ///**@type {ModelProperty}*/ Religion_id = { type: 'number' };
    ///**@type {ModelProperty}*/ Madre_id = { type: 'number' };
    ///**@type {ModelProperty}*/ Padre_id = { type: 'number' };
    ///**@type {ModelProperty}*/ Created_at = { type: 'date' , label: "Fecha"};
    ///**@type {ModelProperty}*/ Updated_at = { type: 'date', hiddenFilter: true };
    ///**@type {ModelProperty}*/ Estudiante_clases = { type: 'MasterDetail', ModelObject: () => new Estudiante_clases_ModelComponent() };
    ///**@type {ModelProperty}*/ Clase_Group = { type: 'MasterDetail', ModelObject: () => new Clase_Group() };
    ///**@type {ModelProperty}*/ Responsables = { type: 'MasterDetail', ModelObject: () => new Responsables_ModelComponent() };


}
export { Estudiantes_ModelComponent }


class Calificacion_Group_ModelComponent {
    constructor(props) {
        for (const prop in props) {
            this[prop] = props[prop];
        }
    }
    /** @type {ModelProperty}*/
    Evaluacion = { type: 'text' };
    /** @type {ModelProperty}*/
    EvaluacionCompleta = { type: 'text' , label: "Nombre de la evaluación"};
    /** @type {ModelProperty}*/
    Tipo = { type: 'text' };
    /** @type {ModelProperty}*/
    Resultado = { type: 'number' };
    /** @type {ModelProperty}*/
    Fecha = { type: 'Date' };
    /** @type {ModelProperty}*/
    Observaciones = { type: 'textarea' };
   
}
export { Calificacion_Group_ModelComponent };

class Asignatura_Group_ModelComponent {
    constructor(props) {
        for (const prop in props) {
            this[prop] = props[prop];
        }
    }
    /** @type {ModelProperty}*/
    Descripcion = { type: 'text' };

    /** @type {ModelProperty}*/
    Descripcion_corta = { type: 'text' };

    /** @type {ModelProperty}*/
    Docente = { type: 'text' };

    /** @type {ModelProperty}*/
    Evaluaciones = { type: 'text' };

    /** @type {ModelProperty}*/
    Calificaciones = { type: 'MasterDetail', ModelObject: () => new Calificacion_Group_ModelComponent() };
    get Details() { return this.Calificaciones }
}
export { Asignatura_Group_ModelComponent };

class Estudiante_Group_ModelComponent {
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
    Calificaciones = { type: 'MasterDetail', ModelObject: () => new Calificacion_Group_ModelComponent() };

    /** @type {ModelProperty}*/
    Asignaturas = { type: 'MasterDetail', ModelObject: () => new Asignatura_Group_ModelComponent() };


    get Details() { return this.Calificaciones }
}
export { Estudiante_Group_ModelComponent };
class Clase_Group_ModelComponent {
    constructor(props) {
        for (const prop in props) {
            this[prop] = props[prop];
        }
    }
    /** @type {ModelProperty}*/
    Descripcion = { type: 'text' };
    /** @type {ModelProperty}*/
    Clase = { type: 'text' };
    /** @type {ModelProperty}*/
    Repite = { type: 'text' };
    /** @type {ModelProperty}*/
    Nivel = { type: 'text' };
    /** @type {ModelProperty}*/
    Seccion = { type: 'text' };
    /** @type {ModelProperty}*/
    Guia = { type: 'text' };

    /** @type {ModelProperty}*/
    Asignaturas = { type: 'MasterDetail', ModelObject: () => new Asignatura_Group_ModelComponent() };
    /** @type {ModelProperty}*/
    Estudiantes = { type: 'MasterDetail', ModelObject: () => new Estudiante_Group_ModelComponent() };
    get Details() { return this.Asignaturas }
}
export { Clase_Group_ModelComponent };

