using API.Controllers;
using APPCORE;
using APPCORE.Security;
using CAPA_NEGOCIO.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace DataBaseModel
{
    public class Estados_Civiles : EntityClass
    {
        [PrimaryKey(Identity = true)]
        public int? Id { get; set; }
        public string? Idtestadocivil { get; set; }
        public string? Texto { get; set; }
    }
}