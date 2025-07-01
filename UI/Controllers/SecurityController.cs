using BusinessLogic.Security;
using APPCORE.Security;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class SecurityController : ControllerBase
    {
        [HttpPost]
        public object Login(UserModel Inst)
        {
            HttpContext.Session.SetString("sessionKey", Guid.NewGuid().ToString());
            return CAPA_NEGOCIO.Security_Users.Login(Inst, HttpContext.Session.GetString("sessionKey"));
        }
        [HttpPost]
        public object LogOut()
        {
            return AuthNetCore.ClearSeason(HttpContext.Session.GetString("sessionKey"));
        }
        [HttpPost]
        public bool Verification()
        {
            return AuthNetCore.Authenticate(HttpContext.Session.GetString("sessionKey"));
        }
        [HttpPost]
        public object RecoveryPassword(UserModel Inst)
        {
            return AuthNetCoreImp.RecoveryPassword(Inst.mail);
        }
        //Statics

        public static bool Auth(string? sessionKey)
        {
            return AuthNetCore.Authenticate(sessionKey);
        }
        public static bool IsAdmin(string? sessionKey)
        {
            return AuthNetCore.HavePermission(Permissions.ADMIN_PANEL_ACCESS.ToString(), sessionKey);
        }       
        public static bool HavePermission(string permission, string? sessionKey)
        {
            return AuthNetCore.HavePermission(permission, sessionKey);
        }
        public static bool HavePermission(string? sessionKey, params Permissions[] permission)
        {            
            return AuthNetCore.HavePermission(sessionKey, permission);
        }      

    }
}
