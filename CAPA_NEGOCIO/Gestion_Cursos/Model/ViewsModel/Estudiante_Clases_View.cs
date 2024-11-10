using CAPA_DATOS;
using CAPA_DATOS.Services;
using CAPA_NEGOCIO.Utility;
namespace DataBaseModel
{
	public class Estudiante_Clases_View : EntityClass
	{
		public DateTime? Transferido { get; set; }
		public int? Estudiante_id { get; set; }
		public int? Materia_id { get; set; }
		public int? Clase_id { get; set; }
		public int? Id { get; set; }
		public DateTime? Retirado { get; set; }
		public Double? Promedio { get; set; }
		public bool? Repitente { get; set; }
		public int? Reprobadas { get; set; }
		public string? Nombre_periodo { get; set; }
		public string? Nombre_corto_periodo { get; set; }
		public DateTime? Inicio_periodo { get; set; }
		public DateTime? Fin_periodo { get; set; }
		public bool? Abierto { get; set; }
		public bool? Oculto { get; set; }
		public string? Nombre_nota { get; set; }
		public string? Nombre_corto_nota { get; set; }
		public int? Numero_consolidados { get; set; }
		public int? Consolidado_id { get; set; }
		public int? Orden { get; set; }
		public Double? Resultado { get; set; }
		public string? Tipo { get; set; }
		public DateTime? Fecha { get; set; }
		public Double? Porcentaje { get; set; }
		public string? Nombre_asignatura { get; set; }
		public string? Nombre_corto_asignatura { get; set; }
		public string? Nombre_grado { get; set; }
		public string? Nombre_corto_nivel { get; set; }
		public string? Nombre_nivel { get; set; }
		public int? Numero_grados { get; set; }
		public int? Inicio_grado { get; set; }
		public string? Config { get; set; }
		public MateriasConfig? MateriasConfig { get; set; }
		public int? Grado { get; set; }
		public int? Seccion_id { get; set; }
		public string? nombre_seccion { get; set; }
		public string? Codigo { get; set; }
		public string? Nombre_Estudiantes { get; set; }
		public string? Sexo { get; set; }
		public string? Estado { get; set; } 
		public string? Id_familia { get; set; } 
		public string? Observaciones { get;  set; }

		public string? Descripcion
		{
			get { return $"{NumberUtility.ObtenerEnumeracion(this.Grado ?? 0)} {Nombre_nivel} - {Nombre_corto_periodo}"; }
		}
		public string? Evaluacion { get { return $"{Nombre_corto_nota}"; } }
		public string? EvaluacionCompleta { get { return $"{Nombre_nota} {Tipo}"; } }
		//public string? Nombre_Docente { get { return $"{Primer_nombre} {Segundo_nombre} {Primer_apellido} {Segundo_apellido}"; } }
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


		public Clase_Group? GetClaseEstudianteConsolidado()
		{
			if (Estudiante_id == null || Clase_id == null)
			{
				throw new Exception("El Estudiante_id y Clase_id requerido no puede ser nulo o vacío.");
			}
			if (filterData == null)
			{
				filterData = [];
			}
			filterData?.Add(FilterData.NotNull("Nombre_nota"));
			return GetConsolidado();
		}
		public Clase_Group? GetClaseEstudianteCompleta()
		{
			if (Estudiante_id == null || Clase_id == null)
			{
				throw new Exception("El Estudiante_id y Clase_id requerido no puede ser nulo o vacío.");
			}
			return GetConsolidado();
		}
		private Clase_Group? GetConsolidado()
		{
			var ClasesF = Get<Estudiante_Clases_View>();
			if (ClasesF.Count == 0) throw  new Exception("Sin calificaciones para mostrar.");
			var clase_Group = InformeClasesBuilder.BuildClaseGroupList(ClasesF);
			return clase_Group?.First();
		}


		public Clase_Group? GetClaseMateriaConsolidado()
		{
			if (Materia_id == null || Seccion_id == null)
			{
				throw new Exception("El Materia_id y Seccion_id requerido no puede ser nulo o vacío.");
			}
			if (filterData == null)
			{
				filterData = [];
			}
			filterData?.Add(FilterData.NotNull("Nombre_nota"));
			return GetConsolidadoMaterias();
		}

		public Clase_Group? GetClaseMateriaCompleta()
		{
			if (Materia_id == null || Seccion_id == null)
			{
				throw new Exception("El Materia_id y Seccion_id requerido no puede ser nulo o vacío.");
			}
			return GetConsolidadoMaterias();
		}

		private Clase_Group? GetConsolidadoMaterias()
		{
			var ClasesF = Get<Estudiante_Clases_View>();
			if (ClasesF.Count == 0) throw new Exception("Sin calificaciones para mostrar.");
			var clase_Group = InformeClasesBuilder.BuildClaseGroupMateriaList(ClasesF);
			return clase_Group?.First();
		}

		public List<Clase_Group>? GetClaseCompleta()
		{
			if (Nombre_corto_periodo == null || Grado == null)
			{
				throw new Exception("El Nombre_corto_periodo y Grado requerido no puede ser nulo o vacío.");
			}
			if (filterData == null)
			{
				filterData = [];
			}
			filterData?.Add(FilterData.NotNull("Nombre_nota"));
			var ClasesF = Get<Estudiante_Clases_View>();
			 if (ClasesF.Count == 0) throw  new Exception("Sin calificaciones para mostrar.");
			var clase_Group = InformeClasesBuilder.BuildClaseList(ClasesF);
			return clase_Group;
		}
	}

}
