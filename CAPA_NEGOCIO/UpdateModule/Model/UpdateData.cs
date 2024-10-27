using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataBaseModel;

namespace CAPA_NEGOCIO.UpdateModule.Model
{
	public class UpdateData
	{
		public List<Estudiantes>? Estudiantes { get; set; }
		public List<Parientes>? Parientes { get; set; }
		public bool? SendAll { get; set; }
	}
}