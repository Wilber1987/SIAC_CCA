using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CAPA_NEGOCIO.Gestion_Pagos.Model.PowerTranzTpv
{   
    public class PT3DSResponse
    {
        public int? TransactionType { get; set; }
        public bool? Approved { get; set; }
        public string? TransactionIdentifier { get; set; }
        public decimal TotalAmount { get; set; }
        public string? CurrencyCode { get; set; }
        public string? CardBrand { get; set; }
        public string? IsoResponseCode { get; set; }
        public string? ResponseMessage { get; set; }
        public RiskManagement? RiskManagement { get; set; }
        public string? PanToken { get; set; }
        public string? OrderIdentifier { get; set; }
        public string? SpiToken { get; set; }
        public BillingAddress? BillingAddress { get; set; }
    }

    public class RiskManagement
    {
        public ThreeDSecure? ThreeDSecure { get; set; }
    }

    public class ThreeDSecure
    {
        public string? Eci { get; set; }
        public string? Cavv { get; set; }
        public string? Xid { get; set; }
        public string? AuthenticationStatus { get; set; }
        public string? CardEnrolled { get; set; }
        public string? ProtocolVersion { get; set; }
        public string? ResponseCode { get; set; }
    }

    public class BillingAddress
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Line1 { get; set; }
        public string? Line2 { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? PostalCode { get; set; }
        public string? CountryCode { get; set; }
        public string? EmailAddress { get; set; }
        public string? PhoneNumber { get; set; }
    }

}