using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataBaseModel;

namespace CAPA_NEGOCIO.UpdateModule.Model
{
	public class UpdateData
	{
		public List<Estudiantes_Data_Update>? Estudiantes { get; set; }
		public List<Parientes_Data_Update>? Parientes { get; set; }
		public bool? SendAll { get; set; }
		public string? Contrato { get; set; }
	}
}