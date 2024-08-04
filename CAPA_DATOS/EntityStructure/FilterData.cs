namespace CAPA_DATOS
{
	public class FilterData
	{
		public string? PropName { get; set; }
		public string? FilterType { get; set; }
		public List<FilterData>? Filters { get; set; }
		public List<String?>? Values { get; set; }
		public static FilterData In(string? propName, params object?[] values)
		{
			return new FilterData { PropName = propName, FilterType = "in", Values = values.Select(v => v?.ToString()).ToList() };
		}
		public static FilterData In(string? propName, params int?[] values)
		{
			return new FilterData { PropName = propName, FilterType = "in", Values = values.Select(v => v.GetValueOrDefault().ToString()).ToList() };
		}
		public static FilterData NotIn(string? propName, params object?[] values)
		{
			return new FilterData { PropName = propName, FilterType = "not in", Values = values.Select(v => v?.ToString()).ToList() };
		}
		/*EQUALS*/
		public static FilterData Equal(string? propName, String? value)
		{
			return new FilterData { PropName = propName, FilterType = "=", Values = new List<string?> { value } };
		}
		public static FilterData Equal(string? propName, int? value)
		{
			return new FilterData { PropName = propName, FilterType = "=", Values = new List<string?> { value.ToString() } };
		}
		public static FilterData Equal(string? propName, object? value)
		{
			return new FilterData { PropName = propName, FilterType = "=", Values = new List<string?> { value?.ToString() } };
		}
		/*GREATER*/
		public static FilterData Greater(string? propName, DateTime? value)
		{
			return new FilterData { PropName = propName, FilterType = ">", Values = new List<string?> { value.ToString() } };
		}
		public static FilterData Greater(string? propName, int? value)
		{
			return new FilterData { PropName = propName, FilterType = ">", Values = new List<string?> { value.ToString() } };
		}
		public static FilterData Greater(string? propName, object? value)
		{
			return new FilterData { PropName = propName, FilterType = ">", Values = new List<string?> { value?.ToString() } };
		}
		/*GREATER EQUAL*/
		public static FilterData GreaterEqual(string? propName, DateTime? value)
		{
			return new FilterData { PropName = propName, FilterType = ">=", Values = new List<string?> { value.ToString() } };
		}
		public static FilterData GreaterEqual(string? propName, int? value)
		{
			return new FilterData { PropName = propName, FilterType = ">=", Values = new List<string?> { value.ToString() } };
		}
		public static FilterData GreaterEqual(string? propName, object? value)
		{
			return new FilterData { PropName = propName, FilterType = ">=", Values = new List<string?> { value?.ToString() } };
		}
		/*LESS*/
		public static FilterData Less(string? propName, DateTime? value)
		{
			return new FilterData { PropName = propName, FilterType = "<", Values = new List<string?> { value.ToString() } };
		}
		public static FilterData Less(string? propName, int? value)
		{
			return new FilterData { PropName = propName, FilterType = "<", Values = new List<string?> { value.ToString() } };
		}
		public static FilterData Less(string? propName, object? value)
		{
			return new FilterData { PropName = propName, FilterType = "<", Values = new List<string?> { value?.ToString() } };
		}
		/*LESS EQUAL*/
		public static FilterData LessEqual(string? propName, DateTime? value)
		{
			return new FilterData { PropName = propName, FilterType = "<=", Values = new List<string?> { value.ToString() } };
		}
		public static FilterData LessEqual(string? propName, int? value)
		{
			return new FilterData { PropName = propName, FilterType = "<=", Values = new List<string?> { value.ToString() } };
		}
		public static FilterData LessEqual(string? propName, object? value)
		{
			return new FilterData { PropName = propName, FilterType = "<=", Values = new List<string?> { value?.ToString() } };
		}
		/*DISTINCS*/
		public static FilterData Distinc(string? propName, String? value)
		{
			return new FilterData { PropName = propName, FilterType = "!=", Values = new List<string?> { value } };
		}
		public static FilterData Distinc(string? propName, int? value)
		{
			return new FilterData { PropName = propName, FilterType = "!=", Values = new List<string?> { value.ToString() } };
		}
		public static FilterData Distinc(string? propName, object? value)
		{
			return new FilterData { PropName = propName, FilterType = "!=", Values = new List<string?> { value?.ToString() } };
		}
		/*LIKE*/
		public static FilterData Like(string? propName, String? value)
		{
			return new FilterData { PropName = propName, FilterType = "like", Values = new List<string?> { value } };
		}

		/*Between*/
		public static FilterData Between(string? propName, DateTime value, DateTime value2)
		{
			return new FilterData { PropName = propName, FilterType = "BETWEEN", Values = new List<string?> { value.ToString(), value2.ToString() } };
		}
		public static FilterData Between(string? propName, int value, int value2)
		{
			return new FilterData { PropName = propName, FilterType = "BETWEEN", Values = new List<string?> { value.ToString(), value2.ToString() } };
		}
		public static FilterData Between(string? propName, double value, double value2)
		{
			return new FilterData { PropName = propName, FilterType = "BETWEEN", Values = new List<string?> { value.ToString(), value2.ToString() } };
		}
		/*Concatenaciones*/
		public static FilterData Or(params FilterData[] where_condition)
		{
			return new FilterData { FilterType = "or", Filters = where_condition.ToList() };
		}
		public static FilterData And(params FilterData[] where_condition)
		{
			return new FilterData { FilterType = "and", Filters = where_condition.ToList() };
		}
		/*ORDERS*/
		public static FilterData OrderByAsc(string? propName)
		{
			return new FilterData { PropName = propName, FilterType = "asc" };
		}
		public static FilterData OrderByDesc(string? propName)
		{
			return new FilterData { PropName = propName, FilterType = "desc" };
		}
		/*ORDERS*/
		public static FilterData Paginate(int value, int value2)
		{
			return new FilterData { FilterType = "paginate", Values = new List<string?> { value.ToString(), value2.ToString() } };
		}
		public static FilterData Limit(int value)
		{
			return new FilterData { FilterType = "limit", Values = new List<string?> { value.ToString() } };
		}
	}
	public class OrdeData
	{
		public string? PropName { get; set; }
		public string? OrderType { get; set; }
		public OrdeData() { }
		public static OrdeData Asc(string? propName)
		{
			return new OrdeData { PropName = propName, OrderType = "ASC" };
		}
		public static OrdeData Desc(string? propName)
		{
			return new OrdeData { PropName = propName, OrderType = "DESC" };
		}

	}
}