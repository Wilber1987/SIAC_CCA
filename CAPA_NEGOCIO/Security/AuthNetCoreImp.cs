using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Controllers;
using CAPA_NEGOCIO.SystemConfig;
using CAPA_NEGOCIO.Util;
using DataBaseModel;

namespace BusinessLogic.Security
{
    public class AuthNetCoreImp: AuthNetCore
    {
        public static UserModel RecoveryPassword(string? mail) 
        {
            string password = StringUtil.GenerateRandomPassword(6);
        	return RecoveryPassword(mail, SystemConfigImpl.GetSMTPDefaultConfig(),password);
        }
    }
}