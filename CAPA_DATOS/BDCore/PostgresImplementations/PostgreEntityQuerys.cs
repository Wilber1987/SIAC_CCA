using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CAPA_DATOS.BDCore.PostgresImplementations
{
    public class PostgreEntityQuerys
    {
        public static string DescribeEntityQuery = @"
            SELECT COLUMN_NAME AS ""COLUMN_NAME"", IS_NULLABLE as ""IS_NULLABLE"", 
            DATA_TYPE as ""DATA_TYPE"", TABLE_SCHEMA as ""TABLE_SCHEMA""
            FROM INFORMATION_SCHEMA.COLUMNS 
            WHERE TABLE_NAME = 'entityName' 
            ORDER BY ORDINAL_POSITION;
        ";
    }
}