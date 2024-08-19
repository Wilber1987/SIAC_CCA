
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
                return MySQLConnection.BuildDataMapper("root", "", "localhost", "siac_cca_production", 3306);
            }
        }
        public static WDataMapper? Bellacom
        {
            get
            {
                return MySQLConnection.BuildDataMapper("root", "", "localhost", "bellacom_dbcca", 3306);
            }
        }
    }
}
