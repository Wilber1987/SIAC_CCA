using CAPA_DATOS;
using CAPA_NEGOCIO.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace DataBaseModel {
   public class Calificaciones : EntityClass {
       [PrimaryKey(Identity = true)]
       public int? Id { get; set; }
       public Double? Resultado { get; set; }
       public int? Tipo_nota_id { get; set; }
       public int? Evaluacion_id { get; set; }
       public string? Observaciones { get; set; }
       public DateTime? Created_at { get; set; }
       public DateTime? Updated_at { get; set; }
       public int? Consolidado_id { get; set; }
       public int? Estudiante_clase_id { get; set; }
       public int? Materia_id { get; set; }
       public int? Periodo { get; set; }
       [ManyToOne(TableName = "Estudiante_clases", KeyColumn = "Id", ForeignKeyColumn = "Estudiante_clase_id")]
       public Estudiante_clases? Estudiante_clases { get; set; }
       [ManyToOne(TableName = "Evaluaciones", KeyColumn = "Id", ForeignKeyColumn = "Evaluacion_id")]
       public Evaluaciones? Evaluaciones { get; set; }
       [ManyToOne(TableName = "Tipo_notas", KeyColumn = "Id", ForeignKeyColumn = "Tipo_nota_id")]
       public Tipo_notas? Tipo_notas { get; set; }
       
       [ManyToOne(TableName = "Materias", KeyColumn = "Id", ForeignKeyColumn = "Materia_id")]
       public Materias? Materia { get; set; }
   }

   public class ViewCalificacionesActivasSiac : Calificaciones
	{
        public int? Periodo_lectivo_id { get; set; }

		public object? CreateViewEstudiantesActivos()
		{
			int currentYear = MigrationDates.GetCurrentYear();

			string query = $"DROP VIEW IF EXISTS viewcalificacionesactivassiac; " +
						   $"CREATE VIEW viewcalificacionesactivassiac AS " +
						   $"select calif.*, ec.periodo_lectivo_id from calificaciones calif " +
						   $"inner join estudiante_clases ec on ec.id = calif.estudiante_clase_id"	+					   
						   $"WHERE ec.periodo_lectivo_id = (select id from periodo_lectivos pl where pl.nombre_corto = '{currentYear}');";
						   

			return ExecuteSqlQuery(query);
		}

		public object? DestroyView(String view)
		{
			string query = $"DROP VIEW IF EXISTS {view};";
			return ExecuteSqlQuery(query);
		}
	}
}
