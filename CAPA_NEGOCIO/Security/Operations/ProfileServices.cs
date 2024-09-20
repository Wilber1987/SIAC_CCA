using API.Controllers;
using CAPA_DATOS;
using CAPA_DATOS.Services;
using DataBaseModel;

namespace CAPA_NEGOCIO.Security.Operations
{
    public class ProfileServices : TransactionalClass
    {
        public static ResponseService SaveProfileRequest(ProfileRequest Inst, string? identity)
        {
            try
            {
                UserModel user = AuthNetCore.User(identity);
                Tbl_Profile profile = Tbl_Profile.Get_Profile(user);
                Inst.Id = Guid.NewGuid().ToString();
                if (profile.ProfileType.Equals(ProfileType.PARIENTE))
                {
                    Parientes? pariente = new Parientes { Id = profile.Pariente_id }.SimpleFind<Parientes>();
                    if (pariente?.ProfileRequest == null)
                    {
                        pariente!.ProfileRequest = [];
                    }
                    Inst.Estado = ProfileRequestsStatus.PENDIENTE.ToString();
                    Inst.Fecha_solicitud = DateTime.Now;
                    Inst.Telefono_Anterior = pariente!.Telefono;
                    Inst.Celular_Anterior = pariente!.Celular;
                    Inst.Correo_Anterior = pariente!.Email;
                    pariente!.Fecha_Modificacion = DateTime.Now;

                    pariente!.Foto = ((ModelFiles?)FileService.upload($"/Images/parientes/{pariente.Id}",
                     new ModelFiles
                     {
                        Value = Inst.Foto,
                        Type = "png"
                     }).body)?.Name;

                    pariente?.ProfileRequest?.Add(Inst);
                    LoggerServices.AddMessageInfo($"Se realizo una solicitud de modificacion del perfil del pariente id={pariente?.Id}, nombre={profile.GetNombreCompleto()}");
                    pariente?.Update();
                }
                else if (profile.ProfileType.Equals(ProfileType.DOCENTE))
                {
                    Docentes? docente = new Docentes { Id_User = profile.IdUser }.SimpleFind<Docentes>();
                    docente!.Celular = Inst.Celular;
                    docente!.Direccion = Inst.Direccion;
                    docente!.Telefono = Inst.Telefono;
                    docente!.Email = Inst.Correo;

                    docente!.Foto = ((ModelFiles?)FileService.upload($"/Images/maestros/{docente.Id}",
                    new ModelFiles
                    {
                        Value = Inst.Foto,
                        Type = "png"
                    }).body)?.Name;

                    docente.Updated_at = DateTime.Now;
                    docente?.Update();
                    LoggerServices.AddMessageInfo($"Se actualizo el perfil del docente id={docente?.Id}, nombre={profile.GetNombreCompleto()}");
                }
                else
                {
                    var pageConfig = Config.pageConfig();
                    var InstFoto = ((ModelFiles?)FileService.upload(pageConfig.MEDIA_IMG_PATH,
                    new ModelFiles
                    {
                        Value = Inst.Foto,
                        Type = "png"
                    }).body)?.Value?.Replace("wwwroot", "");;

                    var ProfileInst = new Tbl_Profile
                    {
                        IdUser = profile.IdUser,
                        Direccion = Inst.Direccion,
                        Telefono = Inst.Telefono,
                        Celular = Inst.Celular,
                        Correo_institucional = Inst.Correo,
                        Id_Perfil = profile.Id_Perfil,
                        Foto = InstFoto
                    };
                    if (profile.Id_Perfil == null)
                    {
                        ProfileInst.Save();
                    } else {
                        ProfileInst.Update();
                    }
                    LoggerServices.AddMessageInfo($"Se actualizo el perfil del usuario id={profile.IdUser}, nombre={profile.GetNombreCompleto()}");
                }
                return new ResponseService() { status = 200, message = "perfil registrado correctamente" };
            }
            catch (System.Exception ex)
            {
                //RollBackGlobalTransaction();
                LoggerServices.AddMessageError("Error al registrar el perfil", ex);
                throw new Exception("Error al registrar el perfil");
            }
        }
        public static List<ProfileRequest> GetProfileRequestParientes()
        {
            var parientesPendientes = new Parientes().Where<Parientes>(
                FilterData.NotNull("ProfileRequest"),
                FilterData.Like("ProfileRequest", "PENDIENTE")
            );
            return parientesPendientes
            .SelectMany(x => x.ProfileRequest ?? [])
            .Where(x => x.Estado == ProfileRequestsStatus.PENDIENTE.ToString()).ToList();
        }

        public static ResponseService UpdateProfileRequestParientes(ProfileRequest inst, string? identity)
        {
            if (inst.Id == null)
            {
                return SaveProfileRequest(inst, identity);
            }
            if (!AuthNetCore.HavePermission(identity, CAPA_DATOS.Security.Permissions.ADMINISTRAR_USUARIOS))
            {
                throw new Exception("No tiene permisos para realizar esta accion");
            }
            UserModel user = AuthNetCore.User(identity);
            var pariente = new Parientes().Find<Parientes>(
                FilterData.Like("ProfileRequest", $"{inst.Id}")
            );

            if (pariente != null)
            {
                ProfileRequest? solicitud = pariente.ProfileRequest?.Find(x => x.Id == inst.Id);
                if (inst.Estado == ProfileRequestsStatus.APROBADO.ToString())
                {
                    pariente.Email = inst.Correo;
                    pariente.Direccion = inst.Direccion;
                    pariente.Telefono = inst.Telefono;
                    pariente.Celular = inst.Celular;
                    pariente.Fecha_Modificacion = DateTime.Now;
                }
                solicitud!.Estado = inst.Estado;
                pariente.Update();
                LoggerServices.AddMessageInfo($"Se actualizo el estado de la solicitud por el usuario con id={user.UserId}");
                return new ResponseService { status = 200, message = "solicitud actualizada" };
            }
            throw new Exception("Solicitud no existe");
        }
    }
}