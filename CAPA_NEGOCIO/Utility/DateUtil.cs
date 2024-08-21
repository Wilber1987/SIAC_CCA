using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CAPA_NEGOCIO.Util
{
	public class DateUtil
	{
		public static DateTime? ValidSqlDateTime(DateTime? date)
		{
			if (date == null)
			{
				return null;
			}

			DateTime minSqlDate = new DateTime(1900, 1, 1);
			DateTime maxSqlDate = new DateTime(9999, 12, 31);

			if (date < minSqlDate)
			{
				return minSqlDate;
			}
			else if (date > maxSqlDate)
			{
				return maxSqlDate;
			}
			else
			{
				return date;
			}
		}

	}
}