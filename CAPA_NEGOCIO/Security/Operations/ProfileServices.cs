using API.Controllers;
using CAPA_DATOS;
using CAPA_DATOS.Services;
using CAPA_NEGOCIO.Util;
using DataBaseModel;
using Microsoft.Extensions.Configuration;

namespace CAPA_NEGOCIO.Security.Operations
{
    public class ProfileServices : TransactionalClass
    {
        private readonly SshTunnelService _sshTunnelService;

        public ProfileServices()
        {
            _sshTunnelService = new SshTunnelService(LoadConfiguration());
        }
        private IConfigurationRoot LoadConfiguration()
        {
            return new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();
        }

        public ResponseService SaveProfileRequest(ProfileRequest Inst, string? identity)
        {
            try
            {
                UserModel user = AuthNetCore.User(identity);
                Tbl_Profile profile = Tbl_Profile.Get_Profile(user);
                Inst.Id = Guid.NewGuid().ToString();
                if (profile.ProfileType.Equals(ProfileType.PARIENTE))
                {
                    Parientes? pariente = new Parientes { Id = Inst.ParienteId != null ? Inst.ParienteId : profile.Pariente_id }.SimpleFind<Parientes>();
                    if (pariente?.ProfileRequest == null)
                    {
                        pariente!.ProfileRequest = [];
                    }
                    string foto = ((ModelFiles?)FileService.upload($"/Images/parientes/{pariente.Id}",
                     new ModelFiles
                     {
                         Value = Inst.Foto,
                         Type = "png"
                     }).body)?.Value?.Replace("wwwroot", "")?.Replace("\\", "/") ?? "/media/img/avatar.png";
                    Inst.Estado = ProfileRequestsStatus.PENDIENTE.ToString();
                    Inst.Fecha_solicitud = DateTime.Now;
                    Inst.Telefono_Anterior = pariente!.Telefono;
                    Inst.Celular_Anterior = pariente!.Celular;
                    Inst.Correo_Anterior = pariente!.Email;
                    Inst.Foto = foto;
                    pariente!.Fecha_Modificacion = DateTime.Now;

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
                    }).body)?.Value?.Replace("wwwroot", "")?.Replace("\\", "/") ?? docente!.Foto;

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
                    }).body)?.Value?.Replace("wwwroot", "")?.Replace("\\", "/");

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
                    }
                    else
                    {
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
                return new ResponseService() { status = 500, message = "Error al registrar el perfil" };// throw new Exception("Error al registrar el perfil");
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

        public ResponseService UpdateProfileRequestParientes(ProfileRequest inst, string? identity)
        {
            try
            {
                if (inst.Id == null)
                {
                    SaveProfileRequest(inst, identity);

                }
                UserModel user = AuthNetCore.User(identity);
                var pariente = new Parientes().Find<Parientes>(
                    FilterData.Like("ProfileRequest", $"{inst.Id}")
                );
                var dbUser = new Security_Users { Id_User = user.UserId}.Find<Security_Users>();
                if (pariente != null)
                {
                    ProfileRequest? solicitud = pariente.ProfileRequest?.Find(x => x.Id == inst.Id);

                    pariente.Email = inst.Correo;
                    pariente.Direccion = inst.Direccion;
                    pariente.Telefono = inst.Telefono;
                    pariente.Celular = inst.Celular;
                    pariente.Fecha_Modificacion = DateTime.Now;
                    pariente.Foto = inst.Foto;

                    solicitud!.Estado = inst.Estado;
                    pariente.Update();
                    LoggerServices.AddMessageInfo($"Se actualizo el estado de la solicitud por el usuario con id={user.UserId}");

                    if(dbUser != null && dbUser.Id_User.Equals(pariente.User_id)){//si el usuario responsable cambia el correo lo actualizamos en el user
                        dbUser.Mail = inst.Correo;
                        dbUser.Update();
                    }
                    if (!SendToBellacom(inst, pariente))
                    {
                        return new ResponseService { status = 400, message = "Error, intentelo nuevamente" };
                    }

                    return new ResponseService { status = 200, message = "Datos actualizados" };
                }
                return new ResponseService { status = 400, message = "Datos no existen, intentelo nuevamente" };
            }
            catch (Exception e)
            {
                LoggerServices.AddMessageError("ERROR: UpdateProfileRequestParientes", e);
                return new ResponseService { status = 400, message = "Error, intentelo nuevamente" };
            }
        }

        private bool SendToBellacom(ProfileRequest inst, Parientes pariente)
        {
            Console.Write("--> datos de actualizacion SendToBellacom");
            // Establecer conexión SSH y puerto de MySQL
            using (var siacSshClient = _sshTunnelService.GetSshClient("Bellacom"))
            {
                siacSshClient.Connect(); // Conectar al cliente SSH
                var siacTunnel = _sshTunnelService.GetForwardedPort("Bellacom", siacSshClient, 3308);
                siacTunnel.Start(); // Iniciar el túnel

                var tutor = new Tbl_aca_tutor();
                tutor.SetConnection(MySqlConnections.BellacomTest);
                var dataMsql = tutor.Where<Tbl_aca_tutor>(FilterData.Equal("idtutor", pariente.Id)).FirstOrDefault();
                dataMsql?.SetConnection(MySqlConnections.BellacomTest);
                try
                {
                    if (dataMsql != null)
                    {
                        BeginGlobalTransaction(); // Inicia la transacción global
                        dataMsql.Direccion = inst.Direccion;
                        dataMsql.Telefono = inst.Telefono;
                        dataMsql.Celular = inst.Celular;
                        dataMsql.Email = inst.Correo;
                        var success = dataMsql.Update(); // Si Update retorna un booleano
                        Console.WriteLine($"Actualización {(success.status == 200 ? "exitosa" : "fallida")}");


                        CommitGlobalTransaction(); // Confirmar la transacción global
                    }
                }
                catch (Exception ex)
                {
                    RollBackGlobalTransaction();
                    LoggerServices.AddMessageError("ERROR: SendToBellacom.", ex);
                    return false;
                }
                finally
                {
                    siacTunnel.Stop(); // Detener el túnel SSH
                    siacSshClient.Disconnect(); // Desconectar el cliente SSH
                }
            }

            return true;
        }
    }
}