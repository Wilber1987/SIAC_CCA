using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Controllers;
using CAPA_DATOS;
using CAPA_DATOS.Security;
using DataBaseModel;

namespace CAPA_NEGOCIO
{
    public class Security_Users : CAPA_DATOS.Security.Security_Users
    {

        public new Tbl_Profile? Tbl_Profile { get; set; }

        public new Tbl_Profile Get_Profile()
        {
            return Tbl_Profile.Get_Profile(Id_User.GetValueOrDefault(), this);
        }
    }
    public class Tbl_Profile : CAPA_DATOS.Security.Tbl_Profile
    {
        public string? Direccion { get; set; }
        public string? Profesion { get; set; }
        public string? Telefono { get; set; }
        public string? Celular { get; set; }
        public string? Codigo_Familia { get; set; }
        public ProfileType ProfileType { get; set; }
        public List<Parientes?>? FamiliaTutores { get; set; }
        public List<Estudiantes?>? FamiliaEstudiantes { get; set; }

        public static Tbl_Profile Get_Profile(UserModel User)
        {
            return Get_Profile(User.UserId.GetValueOrDefault(), User.UserData);
        }

        public static Tbl_Profile Get_Profile(int UserId, CAPA_DATOS.Security.Security_Users user)
        {
            Docentes? docente = new Docentes { Id_User = UserId }.SimpleFind<Docentes>();
            Parientes? pariente = new Parientes { User_id = UserId }.Find<Parientes>();
            List<Estudiantes_responsables_familia> familia = [];
            if (pariente != null && pariente.Estudiantes_responsables_familia != null)
            {
                List<int> ids = pariente.Estudiantes_responsables_familia
                    .Select(x => x.Familia_id.GetValueOrDefault())
                    .Distinct().ToList();
                ids.ForEach(id => familia.AddRange(new Estudiantes_responsables_familia { Familia_id = id }
                    .Where<Estudiantes_responsables_familia>(
                        FilterData.Distinc("pariente_id", pariente.Id)))
                );
            }
            List<Parientes?> parientes = familia.Select(f => f.Parientes)
                            .DistinctBy(p => p!.Id)
                            .ToList();
            List<Estudiantes?> estudiantes = familia.Select(f => f.Estudiantes)
                            .DistinctBy(p => p!.Id)
                            .ToList();

            return new Tbl_Profile
            {
                ProfileType = docente != null ? ProfileType.DOCENTE : (pariente != null ? ProfileType.PARIENTE : ProfileType.USER),
                Nombres = docente != null ? docente.Nombre_completo : pariente?.Nombre_completo ?? user.Nombres,
                Foto = GetAvatar(docente, pariente),
                Direccion = docente != null ? docente.Direccion : pariente?.Direccion,
                Correo_institucional = docente != null ? docente.Email : pariente?.Email,
                Profesion = docente != null ? docente.Escolaridades?.Nombre : pariente?.Profesion,
                Telefono = docente != null ? docente.Telefono : pariente?.Telefono,
                Celular = docente != null ? docente.Celular : pariente?.Celular,
                Codigo_Familia = familia.FirstOrDefault()?.Familia_id?.ToString("D9") ?? "",
                FamiliaTutores = parientes,
                FamiliaEstudiantes = estudiantes
            };
        }

        private static string GetAvatar(Docentes? docente, Parientes? pariente)
        {
            string sexo = docente?.Sexo?.ToUpper() ?? pariente?.Sexo?.ToUpper() ?? "M";
            var pageConfig = Config.pageConfig();
            return (docente != null && docente.Foto == null) || (pariente != null &&  pariente.Foto == null)
            ? sexo == "M" ? pageConfig.MEDIA_IMG_PATH + "avatar.png" : pageConfig.MEDIA_IMG_PATH + "avatar_fem.png"
            : (docente != null
                ? $"/Media/Images/maestros/{docente.Id}/{docente.Foto}"
                : $"/Media/Images/parientes/{pariente?.Id}/{pariente?.Foto}");
        }
    }


    public enum ProfileType
    {
        DOCENTE, ESTUDIANTE, PARIENTE,
        USER
    }
}