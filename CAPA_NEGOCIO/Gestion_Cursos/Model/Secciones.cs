using APPCORE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace DataBaseModel
{
    public class Secciones : EntityClass
    {
        [PrimaryKey(Identity = true)]
        public int? Id { get; set; }
        public string? Nombre { get; set; }
        public int? Clase_id { get; set; }
        public int? Docente_id { get; set; }
        public string? Observaciones { get; set; }
        public DateTime? Created_at { get; set; }
        public DateTime? Updated_at { get; set; }
        [ManyToOne(TableName = "Docentes", KeyColumn = "Id", ForeignKeyColumn = "Docente_id")]
        public Docentes? Guia { get; set; }
        
        [ManyToOne(TableName = "Clases", KeyColumn = "Id", ForeignKeyColumn = "Clase_id")]
        public Clases? Clases { get; set; }
    }
}
