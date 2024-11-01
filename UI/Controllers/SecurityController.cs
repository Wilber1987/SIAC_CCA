using CAPA_DATOS.Security;
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
            HttpContext.Session.SetString("seassonKey", Guid.NewGuid().ToString());
            return CAPA_NEGOCIO.Security_Users.Login(Inst, HttpContext.Session.GetString("seassonKey"));
        }
        [HttpPost]
        public object LogOut()
        {
            return AuthNetCore.ClearSeason();
        }
        [HttpPost]
        public bool Verification()
        {
            return AuthNetCore.Authenticate(HttpContext.Session.GetString("seassonKey"));
        }
        [HttpPost]
        public object RecoveryPassword(UserModel Inst)
        {
            return AuthNetCore.RecoveryPassword(Inst.mail);
        }
        //Statics

        public static bool Auth(string? seassonKey)
        {
            return AuthNetCore.Authenticate(seassonKey);
        }
        public static bool IsAdmin(string? seassonKey)
        {
            return AuthNetCore.HavePermission(Permissions.ADMIN_ACCESS.ToString(), seassonKey);
        }       
        public static bool HavePermission(string permission, string? seassonKey)
        {
            return AuthNetCore.HavePermission(permission, seassonKey);
        }
        public static bool HavePermission(string? seassonKey, params Permissions[] permission)
        {            
            return AuthNetCore.HavePermission(seassonKey, permission);
        }      

    }
}
