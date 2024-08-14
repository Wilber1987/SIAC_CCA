using CAPA_DATOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace DataBaseModel
{
    public class Clases : EntityClass
    {
        [PrimaryKey(Identity = true)]
        public int? Id { get; set; }
        public int? Grado { get; set; }
        public int? Nivel_id { get; set; }
        public int? Periodo_lectivo_id { get; set; }
        public string? Observaciones { get; set; }
        public DateTime? Created_at { get; set; }
        public DateTime? Updated_at { get; set; }

        [ManyToOne(TableName = "Niveles", KeyColumn = "Id", ForeignKeyColumn = "Nivel_id")]
        public Niveles? Niveles { get; set; }

        //[OneToMany(TableName = "Estudiante_clases", KeyColumn = "Id", ForeignKeyColumn = "Clase_id")]
        public List<Estudiante_clases>? Estudiante_clases { get; set; }
        //[OneToMany(TableName = "Materias", KeyColumn = "Id", ForeignKeyColumn = "Clase_id")]
        public List<Materias>? Materias { get; set; }       

    }
}
