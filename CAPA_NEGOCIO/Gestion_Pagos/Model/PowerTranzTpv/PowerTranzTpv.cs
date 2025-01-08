using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CAPA_NEGOCIO.Gestion_Pagos.Model.PowerTranzTpv
{
	public class PowerTranzTpvResponse
	{
		public int? TransactionType { get; set; }
		public bool? Approved { get; set; }
		public string? TransactionIdentifier { get; set; }
		public decimal? TotalAmount { get; set; }
		public string? CurrencyCode { get; set; }
		public string? IsoResponseCode { get; set; }
		public string? ResponseMessage { get; set; }
		public string? OrderIdentifier { get; set; }
		public string? RedirectData { get; set; }
		public string? SpiToken { get; set; }
		public List<ErrorDetail>? Errors { get; set; }
	}
	public class ErrorDetail
    {
        public string? Code { get; set; }
        public string? Message { get; set; }
    }
}