using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Controllers;
using CAPA_DATOS.Security;
using DataBaseModel;

namespace CAPA_NEGOCIO
{
    public class Security_Users : CAPA_DATOS.Security.Security_Users
    {

        public new Tbl_Profile? Tbl_Profile { get; set; }

        public new Tbl_Profile Get_Profile()
        {
            return Tbl_Profile.Get_Profile(Id_User.GetValueOrDefault());
        }
    }
    public class Tbl_Profile : CAPA_DATOS.Security.Tbl_Profile
    {
        public ProfileType ProfileType { get; set; }
        public static Tbl_Profile Get_Profile(UserModel User)
        {
            return Get_Profile(User.UserId.GetValueOrDefault());
        }

        public static Tbl_Profile Get_Profile(int UserId)
        {
            Docentes? docente = new Docentes { Id_User = UserId }.Find<Docentes>();
            Parientes? pariente = new Parientes { User_id = UserId }.Find<Parientes>();

            return new Tbl_Profile
            {
                ProfileType = docente != null ? ProfileType.DOCENTE : ProfileType.PARIENTE,
                Nombres = docente != null ? docente.Nombre_completo : (pariente?.Nombre_completo),
                Foto = docente == null && docente == null ? null
                    : (docente != null ? $"/Media/Images/maestros/{docente.Id}/{docente.Foto}"
                        : $"/Media/Images/parientes/{pariente?.Id}/{pariente?.Foto}")
            };
        }
    }


    public enum ProfileType
    {
        DOCENTE, ESTUDIANTE, PARIENTE
    }
}