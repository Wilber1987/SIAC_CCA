﻿using Microsoft.AspNetCore.Mvc;
using APPCORE.Security;
using APPCORE;

namespace API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ApiLogController : ControllerBase
    {
        [HttpPost]
        [AuthController]
        public object getLog(Log Inst)
        {           
            return Inst.Where<Log>(FilterData.Limit(100));
        }

    }
}
