using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using APPCORE;
using APPCORE.SystemConfig;
using CAPA_DATOS;
using CAPA_DATOS.BDCore.Abstracts;
using DataBaseModel;
using Microsoft.Extensions.Configuration;

namespace BusinessLogic.Connection
{
	public class BDConnection
	{
		public BDConnection()
		{
			var configuration = SystemConfig.AppConfiguration();
			SqlCredentials = configuration.GetSection("SQLCredentials");
			
		}
		//  public WDataMapper? DataMapper = SqlADOConexion.BuildDataMapper("localhost", "sa", "zaxscd", "IPS5Db");
		public WDataMapper? DataMapperSeguimiento { get; set; }
		public IConfigurationSection SqlCredentials { get; private set; }
		public bool IniciarMainConecction(bool isDebug = false)
		{
			if (isDebug)
			{
				string machineName = Environment.MachineName;				
				switch (machineName)
				{
					case "WILBER":
						return SqlADOConexion.IniciarConexion("sa", "zaxscd", "localhost", "OLIMPO");
					case "Alder":
						return SqlADOConexion.IniciarConexion("sa", "123", "localhost\\SQLEXPRESS", "SIAC_CCA_BEFORE_DEMO");
					default:
						return SqlADOConexion.IniciarConexion("sa", "**$NIcca24@$PX", "BDSRV\\SQLCCA", "SIAC_CCA_BEFORE_DEMO");
				}
			}
			//CONEXIONES DE PRODUCCION
			return SqlADOConexion.IniciarConexion(
				SqlCredentials["User"],
				SqlCredentials["Password"],
				SqlCredentials["Server"],
				SqlCredentials["Database"]
			);//SIASMOP USAV
		}
	}
}