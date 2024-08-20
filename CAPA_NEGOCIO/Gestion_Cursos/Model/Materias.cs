using CAPA_DATOS;
using CAPA_NEGOCIO.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace DataBaseModel
{
    public class Materias : EntityClass
    {
        [PrimaryKey(Identity = true)]
        public int? Id { get; set; }
        public int? Clase_id { get; set; }
        public int? Asignatura_id { get; set; }
        public string? Observaciones { get; set; }
        public string? Config { get; set; }
        public MateriasConfig? MateriasConfig { get; set; }        
        public DateTime? Created_at { get; set; }
        public DateTime? Updated_at { get; set; }
        public int? Lock_version { get; set; }
        [ManyToOne(TableName = "Asignaturas", KeyColumn = "Id", ForeignKeyColumn = "Asignatura_id")]
        public Asignaturas? Asignaturas { get; set; }
        [ManyToOne(TableName = "Clases", KeyColumn = "Id", ForeignKeyColumn = "Clase_id")]
        public Clases? Clases { get; set; }
        public MateriasConfig? ThisConfig
        {
            get
            {
                if (Config != null && MateriasConfig == null)
                {
                    return YmlToJson.ParseToObject<MateriasConfig>(Config);
                }
                return MateriasConfig;
            }
        }
    }
    public class MateriasConfig
    {
        public int? periodo_inicio { get; set; }
        public int? periodo_fin { get; set; }
        public string? nota_minima { get; set; }
        public string? porcentaje_examen { get; set; }
        public string? examenes_reparacion { get; set; }
        public string? escala_literal_id { get; set; }
        public Dictionary<string, int>? periodo_actual_hash { get; set; }
        public string? tipo_materia { get; set; }
    }
}
