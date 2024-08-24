using CAPA_DATOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace DataBaseModel {
	/*
	Esto es el catalogo de asignaturas que estan habilitadas para cada docente,
	osea cuales son las asignaturas que el docente puede impartir
	para luego ser asignadas las materias anuales
	*/
   public class Asignaturas : EntityClass {
	   [PrimaryKey(Identity = true)]
	   public int? Id { get; set; }
	   public string? Nombre { get; set; }
	   public string? Nombre_corto { get; set; }
	   public string? Observaciones { get; set; }
	   public int? Nivel_id { get; set; }
	   public bool? Habilitado { get; set; }
	   public DateTime? Created_at { get; set; }
	   public DateTime? Updated_at { get; set; }
	   public int? Orden { get; set; }
	   
	   [ManyToOne(TableName = "Niveles", KeyColumn = "Id", ForeignKeyColumn = "Nivel_id")]
	   public Niveles? Niveles { get; set; }     
   }
}
