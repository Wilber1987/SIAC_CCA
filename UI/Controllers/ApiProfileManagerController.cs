using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Controllers;
using APPCORE;
using APPCORE.Security;
using CAPA_NEGOCIO.Security;
using CAPA_NEGOCIO.Security.Operations;
using Microsoft.AspNetCore.Mvc;

namespace UI.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class ApiProfileManager : ControllerBase
    {
        [HttpPost]
        [AuthController(Permissions.PERFIL_MANAGER)]
        public ResponseService SaveProfileRequest(ProfileRequest Inst)
        {
            return  new ProfileServices().SaveProfileRequest(Inst, HttpContext.Session.GetString("sessionKey"));
        }
        [HttpPost]
        [AuthController(Permissions.ADMINISTRAR_USUARIOS)]
        public List<ProfileRequest> GetProfileRequest(ProfileRequest Inst)
        {
            return  ProfileServices.GetProfileRequestParientes();
        }

        [HttpPost]
        [AuthController(Permissions.ADMINISTRAR_USUARIOS, Permissions.PERFIL_MANAGER)]
        public ResponseService UpdateProfileRequest(ProfileRequest Inst)
        {
            return  new ProfileServices().UpdateProfileRequestParientes(Inst, HttpContext.Session.GetString("sessionKey"));
        }
    }   
}