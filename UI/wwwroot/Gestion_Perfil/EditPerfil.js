
//@ts-check
import { WSecurity } from "../WDevCore/Security/WSecurity.js";
import { WModalForm } from "../WDevCore/WComponents/WModalForm.js";
import { ProfileRequest, ProfileRequest_ModelComponent } from "./ProfilesRequest.js";

const model = new ProfileRequest_ModelComponent({
    Nombre: undefined,
    Telefono_Anterior: undefined,
    Celular_Anterior: undefined,
    Correo_Anterior: undefined,
    Observacion: undefined,
    Estado: undefined
})

window.onload = () => {
    // @ts-ignore
    editBtn.onclick = () => {
        document.body.appendChild(new WModalForm({
            ModelObject: model,
            AutoSave: true,
            EditObject: new ProfileRequest({
                Correo: WSecurity.UserData.Correo,
                Telefono: WSecurity.UserData.Telefono,
                Celular: WSecurity.UserData.Celular,
                Direccion: WSecurity.UserData.Direccion,
                Foto: WSecurity.UserData.Foto
            }), ObjectOptions: {
                SaveFunction: async () => {
                    location.reload();
                }
            }
        }));
    }
    document.querySelectorAll(".btn-pariente").forEach(btn => {
        //**@type {ProfileRequest} */
        // @ts-ignore
        var pariente = JSON.parse(btn.name);
        // @ts-ignore
        btn.onclick = () => {

            document.body.appendChild(new WModalForm({
                ModelObject: model,
                AutoSave: true,
                EditObject: new ProfileRequest({
                    Correo: pariente.Email,
                    Telefono: pariente.Telefono,
                    Celular: pariente.Celular,
                    Direccion: pariente.Direccion,
                    Foto: pariente.Foto,
                    ParienteId: pariente.Id
                }), ObjectOptions: {
                    SaveFunction: async () => {
                        location.reload();
                    }
                }
            }));
        }
    })
}