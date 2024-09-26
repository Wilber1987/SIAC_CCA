using CAPA_DATOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace DataBaseModel
{
    public class AsignacionUsuarios : EntityClass
    {
        [PrimaryKey(Identity = true)]
        public int? Id { get; set; }
        public string? Primer_nombre { get; set; }
        public string? Segundo_nombre { get; set; }      
        public string? Foto { get; set; }
        public DateTime? Created_at { get; set; }
        public DateTime? Updated_at { get; set; }

    }
}