using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

		}

		public bool? Actualizo { get; set; }
		public bool? Entro_al_sistema { get; set; }
		public DateTime? Fecha_ingreso_al_sistema { get; set; }
		public bool? Usa_transporte { get; set; }
	}

	public class Estudiantes_Data_Update : Estudiantes
	{
		public Estudiantes_Data_Update()
		{

		}
		public Estudiantes_Data_Update(Estudiantes inst)
		{

		}
	}
}
