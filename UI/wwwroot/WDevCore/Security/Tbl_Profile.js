//@ts-check
//import { Cat_Dependencias, Tbl_Servicios } from "../../ModelProyect/Tbl_CaseModule.js";
import { WForm } from "../WComponents/WForm.js";
//import { FilterData } from "../WModules/CommonModel.js";
import { EntityClass } from "../WModules/EntityClass.js";
//import { type } from "../WModules/WComponentsTools.js";

//@ts-check
class Tbl_Profile extends EntityClass {
    constructor(props) {
        super(props, 'EntityHelpdesk');
        for (const prop in props) {
            this[prop] = props[prop];
        }
    }
    Id_Perfil = { type: 'number', primary: true };
    Nombres = { type: 'text' };
    Apellidos = { type: 'text' };
    FechaNac = { type: 'date' };
    Sexo = { type: "Select", Dataset: ["Masculino", "Femenino"] };
    Foto = { type: 'img' };
    DNI = { type: 'text' };
    Correo_institucional = { type: 'text', label: "correo", disabled: true, hidden: true };
    Estado = { type: "Select", Dataset: ["ACTIVO", "INACTIVO"] };    
}
export { Tbl_Profile }