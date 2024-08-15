using API.Controllers;
using CAPA_DATOS;
using CAPA_DATOS.Security;
using DataBaseModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace UI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ExporterController : ControllerBase
    {
        [HttpPost]
        [AuthController(Permissions.GESTION_ESTUDIANTES, Permissions.GESTION_ESTUDIANTES_PROPIOS)]
        public ResponseService ExportClaseBoletin(Estudiante_clases Inst)
        {           
            return Inst.ExportClaseBoletin();
        }
    }
}
