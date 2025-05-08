using CAPA_DATOS;
using CAPA_DATOS.Services;
using CAPA_NEGOCIO.Utility;
namespace DataBaseModel
{
	public class Estudiante_View : EntityClass
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
		public string? Idtfamilia { get; set; } 
		public string? Codigo_familia { get; set; }
		public string? Observaciones { get;  set; }
		
		public string? Nombre_completo { get { return $"{Nombre_Estudiantes}"; } }

		public string? Descripcion
		{
			get { return $"{NumberUtility.ObtenerEnumeracion((Nombre_nivel?.ToUpper() == "SECUNDARIA" ?  this.Grado + 6: this.Grado )?? 0)} {Nombre_nivel} - {Nombre_corto_periodo}"; }
		}

		public string? NombreGradoCompleto
		{
			get { return $"{NumberUtility.ObtenerEnumeracion((Nombre_nivel?.ToUpper() == "SECUNDARIA" ?  this.Grado + 6: this.Grado )?? 0)} {GetGradeName()}"; }
		}
		
		private string? GetGradeName()
        {
            if (Nombre_nivel?.ToUpper() == NivelesEnum.PREESCOLAR.ToString())
            {
                return "NIVEL";
            }
            return "GRADO";
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


	}

}
