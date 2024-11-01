using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataBaseModel;

namespace CAPA_NEGOCIO.UpdateModule.Model
{
	public class UpdateDataRequest
	{
		public List<Estudiantes_Data_Update>? Estudiantes { get; set; }
		public List<Parientes_Data_Update>? Parientes { get; set; }
		public bool? AceptaTerminosYCondiciones { get; set; }
	}
}