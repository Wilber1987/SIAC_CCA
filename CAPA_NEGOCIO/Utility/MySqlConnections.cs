
using System.Configuration;
using APPCORE;
using APPCORE.BDCore.Abstracts;
using Microsoft.Extensions.Configuration;

namespace CAPA_NEGOCIO.Util
{
    public class MySqlConnections
    {
        public static WDataMapper? SiacTest
        {
            get
            {
                return MySQLConnection.BuildDataMapper("127.0.0.1", "root", "HytttGHjasd66LittleBarco", "siac_cca_production", 3307);
            }
        }
        private static WDataMapper? BellacomTestConection;
        public static WDataMapper? BellacomTest
        {
            get
            {
                if (BellacomTestConection == null)
                {
                    BellacomTestConection = MySQLConnection.BuildDataMapper("127.0.0.1", "root", "LY2016$root", "bellacom_dbcca", 3308);
                }
                return BellacomTestConection;
            }
        }

        public static WDataMapper? Siac
        {
            get
            {
                var configuration = LoadConfiguration();

                var mysqlSettings = configuration.GetSection("ConnectionStrings:MySQLConnectionSiac");
                var sshSettings = configuration.GetSection("ConnectionStrings:SSHConnectionSiac");

                return MySQLConnection.BuildDataMapper(
                            mysqlSettings["Server"],
                            mysqlSettings["User"],
                            mysqlSettings["Password"],
                            mysqlSettings["Database"],
                            int.Parse(mysqlSettings["Port"]),
                            sshSettings["HostName"],
                            sshSettings["UserName"],
                            sshSettings["Password"],
                            int.Parse(sshSettings["Port"])
                        );
            }
        }
        public static WDataMapper? Bellacom
        {
            get
            {
                var configuration = LoadConfiguration();

                var mysqlSettingsSiac = configuration.GetSection("ConnectionStrings:MySQLConnectionBellacom");
                var sshSettings = configuration.GetSection("ConnectionStrings:SSHConnectionBellacom");

                return MySQLConnection.BuildDataMapper(
                            mysqlSettingsSiac["Server"],
                            mysqlSettingsSiac["User"],
                            mysqlSettingsSiac["Password"],
                            mysqlSettingsSiac["Database"],
                            int.Parse(mysqlSettingsSiac["Port"]),
                            sshSettings["HostName"],
                            sshSettings["UserName"],
                            sshSettings["Password"],
                            int.Parse(sshSettings["Port"])
                        );
            }
        }

        private static IConfigurationRoot LoadConfiguration()
        {
            return new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();
        }

    }

}
