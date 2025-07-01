using API.Controllers;
using APPCORE;
using APPCORE.Security;
using CAPA_NEGOCIO.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace DataBaseModel.SimpleModel
{
	public class Estudiantes : EntityClass
	{
		[PrimaryKey(Identity = true)]
		public int? Id { get; set; }
		public int? Idtestudiante { get; set; }
		public int? Idbellacom { get; set; }
		public string? Primer_nombre { get; set; }
		public string? Segundo_nombre { get; set; }
		public string? Primer_apellido { get; set; }
		public string? Segundo_apellido { get; set; }
		public DateTime? Fecha_nacimiento { get; set; }
		public string? Lugar_nacimiento { get; set; }
		public string? Sexo { get; set; }
		public string? Direccion { get; set; }
		public string? Codigo { get; set; }
		public int? Madre_id { get; set; }
		public int? Padre_id { get; set; }
		public DateTime? Created_at { get; set; }
		public DateTime? Updated_at { get; set; }
		public string? Foto { get; set; }
		public Double? Peso { get; set; }
		public Double? Altura { get; set; }
		public string? Tipo_sangre { get; set; }
		public string? Padecimientos { get; set; }
		public string? Alergias { get; set; }
		public int? Recorrido_id { get; set; }
		public int? Id_familia { get; set; }
		public String? Vivecon { get; set; }
		public int? Id_religion { get; set; }
		public int? Id_pais { get; set; }
		public int? Id_region { get; set; }
		public int? EgresoExAlumno { get; set; }
		public DateTime? Fecha_ingreso { get; set; }
		public int? Id_cliente { get; set; }
		public string? Codigomed { get; set; }
		public Double? Saldoeamd { get; set; }
		public string? Sacramento { get; set; }
		public int? Aniosacra { get; set; }
		public string? Colegio_procede { get; set; }

		public bool? Activo { get; set; }
		public string? Nombre_completo { get { return $"{Primer_nombre} {Segundo_nombre} {Primer_apellido} {Segundo_apellido}"; } }


	}
}
