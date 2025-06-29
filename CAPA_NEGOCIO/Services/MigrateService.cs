using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using APPCORE;
using APPCORE.BDCore.Abstracts;
using APPCORE.Services;
using CAPA_NEGOCIO.UpdateModule.Model;

namespace CAPA_NEGOCIO.Services
{
	public class MigrateService
	{
		public static DateTime GetLastUpdate(string tipo)
		{
			try
			{
				var ActualizacionesCron = new ActualizacionesCron();
				var resultado = ActualizacionesCron.Where<ActualizacionesCron>(FilterData.Equal("descripcion", tipo)).FirstOrDefault();
				if (resultado != null)
				{
					return DateTime.Now.AddDays(-5);
				}

				return (DateTime)(resultado?.Fecha_Actualizacion);
			}
			catch (Exception)
			{
				return DateTime.Now.AddDays(-5);
			}
		}

		public static void UpdateLastUpdate(string tipo)
		{
			var existing = new ActualizacionesCron()
			{
				Descripcion = tipo
			}.Find<ActualizacionesCron>();


			existing.Fecha_Actualizacion = DateTime.Now.AddDays(-2);
			existing?.Update();
		}
	}
}