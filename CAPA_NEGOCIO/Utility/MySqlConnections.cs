
using CAPA_DATOS;
using CAPA_DATOS.BDCore.Abstracts;

namespace CAPA_NEGOCIO.Util
{
    public class MySqlConnections
    {
        public static WDataMapper? Siac
        {
            get
            {
                return MySQLConnection.BuildDataMapper("localhost","root", "",  "siac_migracion", 3306);
            }
        }
        public static WDataMapper? Bellacom
        {
            get
            {
                return MySQLConnection.BuildDataMapper("localhost", "root", "", "bellacom_migracion", 3306);
            }
        }

        public static WDataMapper? ConnectToMysql
        {
            get
            {
                return MySQLConnection.BuildDataMapper("200.12.42.38", "siac_cca", "HytttGHjasd66LittleBarco", "siac_cca_production", 3606, "200.12.42.38", "root", "Dsj.local2024$");
            }
        }
    }
}
