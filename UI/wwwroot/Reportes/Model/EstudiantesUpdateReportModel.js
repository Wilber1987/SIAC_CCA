//@ts-check
import { Periodo_lectivos } from '../../Model/Periodo_lectivos.js';
// @ts-ignore
import { ModelProperty } from '../../WDevCore/WModules/CommonModel.js';
import { EntityClass } from '../../WDevCore/WModules/EntityClass.js';
import { SecurityOption, SecurityOption_ModelComponent, securityOptions } from '../../update/Model/Estudiantes_ModelComponent.js'; // Importar SecurityOption de la clase de referencia


/**
 * @class EstudiantesUpdateReportModel
 * @extends EntityClass
 * @description Modelo de datos para la actualización de estudiantes, utilizado para consultas de solo lectura.
 */
class EstudiantesUpdateReportModel extends EntityClass {

    /**
     * @param {Partial<EstudiantesUpdateReportModel>} [props]
     */
    constructor(props) {
        super(props, "Reportes");
        /** @type {number | null} */ this.Id = null;
        /** @type {string | null} */ this.Id_familia = null;
        /** @type {string | null} */ this.Idtfamilia = null;
        /** @type {string | null} */ this.Codigo = null;
        /** @type {string | null} */ this.Nombre_completo = null;
        /** @type {string | null} */ this.Primer_nombre = null;
        /** @type {string | null} */ this.Segundo_nombre = null;
        /** @type {string | null} */ this.Primer_apellido = null;
        /** @type {string | null} */ this.Segundo_apellido = null;
        /** @type {Date | null} */ this.Fecha_nacimiento = null; // DateTime? en C# se mapea a Date | null en JS
        /** @type {string | null} */ this.Periodo_Lectivo_Update = null;
        /** @type {string | null} */ this.Grado = null;
        /** @type {string | null} */ this.NombreGrado = null;
        /** @type {string | null} */ this.Seccion = null;
        /** @type {string | null} */ this.Nivel = null;
        /** @type {number | null} */ this.Periodo_lectivo_id = null;
        /** @type {SecurityOption | null} */ this.SecurityOption = new SecurityOption(); // Asumiendo que SecurityOption es una clase o un objeto
       
        // Aplicar propiedades pasadas en el constructor
        if (props) {
            /** @type {keyof EstudiantesUpdateReportModel} */
            let prop;
            for (prop in props) {
                if (Object.prototype.hasOwnProperty.call(props, prop)) {
                    // @ts-ignore: TypeScript no puede inferir completamente que prop es una clave válida aquí
                    this[prop] = props[prop];
                }
            }
        }
        /** @type {string | null} */ this.SecurityOptionLabel = this.SecurityOption?.Descripcion ?? "";
    }
    /**
    * @returns {Promise<any>}
    */
    async ReenviarBoleta() {
        return await this.GetData("ApiReportes/GetEstudiantesConRecorridos");
    }
}

export { EstudiantesUpdateReportModel };

class EstudiantesUpdateReportModel_Modelcomponent {
    /** @param {Partial<EstudiantesUpdateReportModel_Modelcomponent>} [props] */
    constructor(props) {
        //super(props, 'Reportes'); // 'EstudiantesUpdate' como nombre de la entidad para la API
        //@ts-ignore   
        for (const prop in props) { this[prop] = props[prop]; };
        //this.Model = new EstudiantesUpdateReportModel(); // Mantener una instancia del modelo de datos
    }

    /**@type {ModelProperty}*/ Id = { type: "number", primary: true, hidden: true, hiddenFilter: true };
    //**@type {ModelProperty}*/ Id_familia = { type: "text", label: "ID Familia", hiddenFilter: true };
    /**@type {ModelProperty}*/ Idtfamilia = { type: "text", label: "ID Familia", hiddenFilter: true };
    /**@type {ModelProperty}*/ Codigo = { type: "text", label: "Código", hiddenFilter: true };
    /**@type {ModelProperty}*/ Nombre_completo = { type: "text", hiddenFilter: true };
    //**@type {ModelProperty}*/ Primer_nombre = { type: "text", label: "Primer Nombre" , hiddenFilter: true};
    //**@type {ModelProperty}*/ Segundo_nombre = { type: "text", label: "Segundo Nombre", require: false, hiddenFilter: true };
    //**@type {ModelProperty}*/ Primer_apellido = { type: "text", label: "Primer Apellido" , hiddenFilter: true};
    //**@type {ModelProperty}*/ Segundo_apellido = { type: "text", label: "Segundo Apellido", require: false, hiddenFilter: true };
    /**@type {ModelProperty}*/ Fecha_nacimiento = { type: "date", label: "Fecha de Nacimiento", hiddenFilter: true };
    /**@type {ModelProperty}*/ Periodo_Lectivo_Update = { type: "select", hiddenInTable: true, label: "Periodo de Actualización", Dataset: [] };
    /**@type {ModelProperty}*/ NombreGrado = { type: "text", label: "Grado", hiddenFilter: true };
    /**@type {ModelProperty}*/ Seccion = { type: "text", label: "Sección", hiddenFilter: true };
    /**@type {ModelProperty}*/ Nivel = { type: "text", label: "Nivel", hiddenFilter: true };
    /**@type {ModelProperty}*/ Periodo_lectivo_id = { type: "number", label: "ID Periodo Lectivo", hidden: true, hiddenFilter: true };

    /**@type {ModelProperty}*/ SecurityOptionLabel = {
        type: 'WRADIO', label: "Seguro estudiantil",
        Dataset: securityOptions,
        ModelObject: new SecurityOption_ModelComponent(), // Usar SecurityOption_ModelComponent para la UI
        require: false
    };

    // El método GetQuery ya no es necesario aquí, ya que EntityClass maneja la interacción con la API
    // a través de las propiedades y el nombre de la entidad.
}

export { EstudiantesUpdateReportModel_Modelcomponent };

