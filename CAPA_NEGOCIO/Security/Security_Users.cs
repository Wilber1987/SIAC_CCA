using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CAPA_DATOS.Security;

namespace CAPA_NEGOCIO
{
    public class Security_Users : CAPA_DATOS.Security.Security_Users
    {

        public new Tbl_Profile? Tbl_Profile { get; set; }

        public new Tbl_Profile? Get_Profile()
        {
            if (Tbl_Profile == null)
            {
                Tbl_Profile = new Tbl_Profile { IdUser = Id_User }.Find<Tbl_Profile>();
            }
            return Tbl_Profile;
        }
    }
}