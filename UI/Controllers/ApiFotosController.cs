using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Controllers;
using APPCORE;
using APPCORE.Security;
using CAPA_NEGOCIO.Oparations;
using DataBaseModel;
using Microsoft.AspNetCore.Mvc;

namespace UI.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class ApiFotosController// : ControllerBase
    {
        [HttpGet]
        
        public bool GetOwFotos()
        {
            return new ImageOperation().MigrateImagenes();
        }
    }
}
