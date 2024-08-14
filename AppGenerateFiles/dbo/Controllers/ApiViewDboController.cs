using DataBaseModel;
using Security;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
namespace API.Controllers {
   [Route("api/[controller]/[action]")]
   [ApiController]
   public class  ApiViewDboController : ControllerBase {
       //Estudiante_Clases_View
       [HttpPost]
       [AuthController]
       public List<Estudiante_Clases_View> getEstudiante_Clases_View(Estudiante_Clases_View Inst) {
           return Inst.Get<Estudiante_Clases_View>();
       }
       [HttpPost]
       [AuthController]
       public Estudiante_Clases_View findEstudiante_Clases_View(Estudiante_Clases_View Inst) {
           return Inst.Find<Estudiante_Clases_View>();
       }
   }
}
