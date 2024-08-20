using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Controllers;
using CAPA_NEGOCIO.Templates;
using Microsoft.AspNetCore.Mvc;

namespace UI.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class ApiDocumentsDataController : ControllerBase
    {
        [HttpPost]
        [AuthController]
        public object GetBoletinDataFragments(DocumentsData Inst)
        {           
            return Inst.GetBoletinDataFragments();
        }   
    }
}