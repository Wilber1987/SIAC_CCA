using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CAPA_DATOS;
using CAPA_DATOS.Services;
using DataBaseModel;

namespace CAPA_NEGOCIO.UpdateModule.Model
{
	public class Parientes_Data_Update : Parientes
	{

		public Parientes_Data_Update()
		{
		}
		public Parientes_Data_Update(Parientes inst)
		{
			AdapterUtil.SetMatchingProperties(inst, this);
		}


		public bool? Correo_enviado { get; set; }
		public bool? Actualizo { get; set; }
		public DateTime? Fecha_actualizacion { get; set; }
		public bool? Acepto_terminos { get; set; }
		public bool? Entro_al_sistema { get; set; }
		public string? Ip_ingreso { get; set; }
		public DateTime? Fecha_ingreso_al_sistema { get; set; }

	}

	public class Estudiantes_Data_Update : Estudiantes
	{
		public Estudiantes_Data_Update()
		{

		}
		public Estudiantes_Data_Update(Estudiantes inst)
		{
			AdapterUtil.SetMatchingProperties(inst, this);
		}
		public bool? Usa_transporte { get; set; }

		[JsonProp]
		public List<Adress>? Puntos_Transportes { get; set; }

		public List<Estudiantes_Data_Update> GetEstudiantesConRecorridos()
		{
			if (filterData?.Count == 0)
			{
				var periodoLectivo = Periodo_lectivos.PeriodoActivo();
				return Where<Estudiantes_Data_Update>(
					FilterData.NotNull("Puntos_Transportes"),
					FilterData.Distinc("Puntos_Transportes", "[]")
				).Where(e =>
					e.Puntos_Transportes?.Count > 0 &&
					e.Estudiante_clases?.Find(ec => ec.Periodo_lectivo_id == periodoLectivo?.Id) != null
				).ToList();
			}
			return Where<Estudiantes_Data_Update>(
				FilterData.NotNull("Puntos_Transportes"),
				FilterData.Distinc("Puntos_Transportes", "[]")
			).Where(e =>
				e.Puntos_Transportes?.Count > 0
			).ToList();
		}
	}

	public class Adress
	{
		public string? Direccion { get; set; }
		public string? Trayecto { get; set; } //IDA y VUELTA
	}


	public class UpdatedData : EntityClass
	{
		[PrimaryKey(Identity = true)]
		public int? Id { get; set; }
		[JsonProp]
		public DataContract? DataContract { get; set; }
		[JsonProp]
		public List<ModelFiles>? Documents_Boletas { get; set; }
		[JsonProp]
		public List<ModelFiles>? Documents_Contracts { get; set; }


	}

	public class DataContract
	{
		public int? Id_Tutor_responsable { get; set; }
		public string? Tutor_responsable { get; set; }
		public List<int>? Estudiantes { get; set; }
		public List<int>? Tutores { get; set; }
		public DateTime? Fecha { get; set; }

	}
}
