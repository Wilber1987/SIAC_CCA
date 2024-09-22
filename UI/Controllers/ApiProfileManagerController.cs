using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Controllers;
using CAPA_DATOS;
using CAPA_DATOS.Security;
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
            return  ProfileServices.SaveProfileRequest(Inst, HttpContext.Session.GetString("seassonKey"));
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
            return  ProfileServices.UpdateProfileRequestParientes(Inst, HttpContext.Session.GetString("seassonKey"));
        }
    }   
}