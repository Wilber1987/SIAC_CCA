using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using CAPA_DATOS;
using CAPA_DATOS.BDCore.Abstracts;
using CAPA_DATOS.Services;
using CAPA_NEGOCIO.UpdateModule.Model;

namespace CAPA_NEGOCIO.Services
{
	public class MigrateService
	{
		public static DateTime GetLastUpdate(string tipo)
		{
			var ActualizacionesCron = new ActualizacionesCron();			
			var resultado = ActualizacionesCron.Where<ActualizacionesCron>(FilterData.Equal("descripcion", tipo)).FirstOrDefault();			

			return (DateTime)(resultado != null ? resultado.Fecha_Actualizacion : DateTime.Now);
		}

		public static void UpdateLastUpdate(string tipo)
		{			
			var existing = new ActualizacionesCron()
						{
							Descripcion = tipo
						}.Find<ActualizacionesCron>();


			existing.Fecha_Actualizacion = DateTime.Now.AddMinutes(-120);
			existing.Update();
		}
	}
}