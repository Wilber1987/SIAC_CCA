using System;
using System.Collections.Generic;
using System.Globalization;
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

		public static string? GetMonthName(DateTime? fecha)
		{
			if (fecha == null) return null;
			return  fecha.Value.ToString("dddd, d 'del' MMMM 'del' yyyy", new CultureInfo("es-ES"));

		}

	}

	public static class MigrationDates
	{
		// Retorna el inicio del año actual
		public static DateTime GetStartOfCurrentYear()
		{
			return new DateTime(DateTime.Now.Year, 1, 1);
		}

		// Retorna el fin del año actual
		public static DateTime GetEndOfCurrentYear()
		{
			return new DateTime(DateTime.Now.Year, 12, 31, 23, 59, 59);
		}

		// Retorna la fecha de inicio de un mes específico restando n meses desde la fecha actual
		public static DateTime GetStartOfMonthSubtractingMonths(int monthsToSubtract)
		{
			DateTime currentDate = DateTime.Now;
			DateTime targetDate = currentDate.AddMonths(-monthsToSubtract);
			return new DateTime(targetDate.Year, targetDate.Month, 1);
		}

		// Retorna el inicio de un año específico
		public static DateTime GetStartOfYear(int year)
		{
			return new DateTime(year, 1, 1);
		}
		// Retornar le inicio del año pasado
		public static DateTime GetStartOfLastYear()
		{
			int lastYear = DateTime.Now.Year - 1;
			return new DateTime(lastYear, 1, 1);
		}


		// Retorna el fin de un año específico
		public static DateTime GetEndOfYear(int year)
		{
			return new DateTime(year, 12, 31, 23, 59, 59);
		}

		public static int GetCurrentYear()
		{
			return DateTime.Now.Year;
		}		

	}

}