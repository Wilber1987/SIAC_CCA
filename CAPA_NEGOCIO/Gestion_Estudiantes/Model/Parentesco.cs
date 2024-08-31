using API.Controllers;
using CAPA_DATOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace DataBaseModel
{
    public class Parentesco : EntityClass
    {
        [PrimaryKey(Identity = true)]
		public int? Id { get; set; }
        public string? Sigla { get; set; }
        public string? Descripcion { get; set; }
    }
}