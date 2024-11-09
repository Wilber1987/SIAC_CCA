using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CAPA_DATOS;
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
		public List<Adress>? Puntos_Transportes { get ; set; }
	}

	public class Adress
	{		
		public string? Direccion { get; set; }
		public string? Trayecto { get; set; } //IDA y VUELTA
	}
}
