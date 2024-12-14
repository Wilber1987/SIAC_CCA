using System.Text;
using CAPA_NEGOCIO.Gestion_Pagos.Model;

namespace CAPA_NEGOCIO.Gestion_Pagos.Operations
{
	public class TPVService
	{
		const string GLOBAL_URL = "https://staging.ptranz.com/";
		const string POWERTRANZ_ID = "77700572";
		const string POWERTRANZ_PASSWORD = "G3jXzV3nzXZEnwq9Kz6zHyJksoksQLXTUOtcEFrj533qbmIBcwhN9M0";		
		static string T_IDENTIFY = Guid.NewGuid().ToString(); 

		public static async Task<string> AuthenticateAsync(TPV datosDePago)
		{
			try
			{
				string responseData = await ExecuteRequest("Api/Auth", BuildJsonAuthRequest(datosDePago));
				return responseData;
			}
			catch (Exception ex)
			{
				// Manejo de errores
				throw new ApplicationException($"Error durante la autenticación: {ex.Message}", ex);
			}
		}
		public static async Task<string> SalesAsync(TPV datosDePago)
		{
			try
			{
				string responseData = await ExecuteRequest("Api/Sale", BuildJsonAuthRequest(datosDePago));
				return responseData;
			}
			catch (Exception ex)
			{
				// Manejo de errores
				throw new ApplicationException($"Error durante la venta: {ex.Message}", ex);
			}
		}
		public static async Task<string> RiskMgmtAsync(TPV datosDePago)
		{
			try
			{
				string responseData = await ExecuteRequest("Api/RiskMgmt", BuildJsonRiskMgmtRequest(datosDePago));
				return responseData;
			}
			catch (Exception ex)
			{
				// Manejo de errores
				throw new ApplicationException($"Error durante la venta: {ex.Message}", ex);
			}
		}



		public static async Task<string> PaymentAsync(TPV datosDePago)
		{
			try
			{
				string responseData = await ExecuteRequest("Api/Payment", BuildJsonPaymentRequest(datosDePago));
				return responseData;
			}
			catch (Exception ex)
			{
				// Manejo de errores
				throw new ApplicationException($"Error durante la venta: {ex.Message}", ex);
			}
		}
		public static async Task<string> CaptureAsync(TPV datosDePago)
		{
			try
			{
				string responseData = await ExecuteRequest("Api/Capture", BuildJsonCaptureRequest(datosDePago));
				return responseData;
			}
			catch (Exception ex)
			{
				// Manejo de errores
				throw new ApplicationException($"Error durante la venta: {ex.Message}", ex);
			}
		}

		private static string? BuildJsonCaptureRequest(TPV datosDePago)
		{
			return null;
		}

		private static string? BuildJsonRiskMgmtRequest(TPV datosDePago)
		{
			return null;
		}

		private static string? BuildJsonPaymentRequest(TPV datosDePago)
		{
			return null;
		}



		private static string BuildJsonAuthRequest(TPV datosDePago)
		{
			return @"{""TransactionIdentifier"": """ + datosDePago.pagosRequest?.Monto.ToString() + @""",
				""TotalAmount"": " + T_IDENTIFY + @",			
				""CurrencyCode"": ""NIO"",	
				""TerminalCode"":""""			
				""Source"": {
					""CardPresent"": true,
					""CardEmvFallback"": true,
					""ManualEntry"": true,
					""Debit"": true,					
					""Contactless"": true,					
					""MaskedPan"": ""string""	
					""CardPan"": """ + datosDePago.CardNumber?.ToString() + @""", 
					""CardCvv"": """ + datosDePago.Cvv?.ToString() + @""", 
					""CardExpiration"": """ + datosDePago.Cvv?.ToString() + @""", 
					""CardholderName"": """ + datosDePago.CardholderName + @"""
				}
			}";
		}
		private static string BuildJsonSalesRequest(TPV datosDePago)
		{
			return @"{""TransactionIdentifier"": """ + datosDePago.pagosRequest?.Monto.ToString() + @""",
				""TotalAmount"": " + T_IDENTIFY + @",			
				""CurrencyCode"": ""NIO"",	
				""TerminalCode"":""""			
				""Source"": {
					""CardPresent"": true,
					""CardEmvFallback"": true,
					""ManualEntry"": true,
					""Debit"": true,					
					""Contactless"": true,					
					""MaskedPan"": ""string""	
					""CardPan"": """ + datosDePago.CardNumber?.ToString() + @""", 
					""CardCvv"": """ + datosDePago.Cvv?.ToString() + @""", 
					""CardExpiration"": """ + datosDePago.Cvv?.ToString() + @""", 
					""CardholderName"": """ + datosDePago.CardholderName + @"""
				}
			}";
		}
		private static async Task<string> ExecuteRequest(string url, string jsonContent)
		{
			using HttpClient _httpClient = new HttpClient();

			// Agregar encabezados requeridos
			_httpClient.DefaultRequestHeaders.Add("PowerTranz-PowerTranzId", POWERTRANZ_ID);
			_httpClient.DefaultRequestHeaders.Add("PowerTranz-PowerTranzPassword", POWERTRANZ_PASSWORD);
			_httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
			StringContent? content;
			HttpResponseMessage? response;
			if (jsonContent == null)
			{
				response = await _httpClient.PostAsync(GLOBAL_URL + url, null);
			}
			else
			{
				content = new StringContent(jsonContent, Encoding.UTF8, "application/json-patch+json");
				response = await _httpClient.PostAsync(GLOBAL_URL + url, content);
			}

			response.EnsureSuccessStatusCode(); // Lanza una excepción si ocurre un error.
			return await response.Content.ReadAsStringAsync();


		}
		private static string BuildJsonRequest2(TPV datosDePago)
		{
			return @"{""TransactionIdentifier"": ""string"",
				""TotalAmount"": {TotalAmount},
				""TipAmount"": 0, 
				""TaxAmount"": 0,
				""OtherAmount"": 0,
				""CurrencyCode"": ""NIO"",
				""LocalTime"": ""{LocalTime}"",
				""LocalDate"": ""{LocalDate}"",
				""AddressVerification"": true,
				""ThreeDSecure"": true,
				""BinCheck"": true,
				""FraudCheck"": true,
				""RecurringInitial"": true,
				""Recurring"": true,
				""CardOnFile"": true,
				""AccountVerification"": true,
				""Source"": {
					""CardPresent"": true,
					""CardEmvFallback"": true,
					""ManualEntry"": true,
					""Debit"": true,
					""AccountType"": ""string"",
					""Contactless"": true,
					""CardPan"": ""string"",
					""EncryptedCardPan"": ""string"",
					""MaskedPan"": ""string"",
					""CardCvv"": ""string"",
					""EncryptedCardCvv"": ""string"",
					""EncryptedCardExpiration"": ""string"",
					""CardExpiration"": ""string"",
					""Token"": ""string"",
					""TokenType"": ""string"",
					""CardTrack1Data"": ""string"",
					""CardTrack2Data"": ""string"",
					""CardTrack3Data"": ""string"",
					""CardTrackData"": ""string"",
					""EncryptedCardTrack1Data"": ""string"",
					""EncryptedCardTrack2Data"": ""string"",
					""EncryptedCardTrack3Data"": ""string"",
					""EncryptedCardTrackData"": ""string"",
					""Ksn"": ""string"",
					""EncryptedPinBlock"": ""string"",
					""PinBlockKsn"": ""string"",
					""CardEmvData"": ""string"",
					""CardholderName"": ""string"",
					""Wallet"": ""string""
				},
				""TerminalId"": ""string"",
				""TerminalCode"": ""string"",
				""TerminalSerialNumber"": ""string"",
				""ExternalIdentifier"": ""string"",
				""ExternalBatchIdentifier"": ""string"",
				""ExternalGroupIdentifier"": ""string"",
				""OrderIdentifier"": ""string"",
				""BillingAddress"": {
					""FirstName"": ""string"",
					""LastName"": ""string"",
					""Line1"": ""string"",
					""Line2"": ""string"",
					""City"": ""string"",
					""County"": ""string"",
					""State"": ""string"",
					""PostalCode"": ""string"",
					""CountryCode"": ""string"",
					""EmailAddress"": ""string"",
					""PhoneNumber"": ""string"",
					""PhoneNumber2"": ""string"",
					""PhoneNumber3"": ""string""
				},
				""ShippingAddress"": {
					""FirstName"": ""string"",
					""LastName"": ""string"",
					""Line1"": ""string"",
					""Line2"": ""string"",
					""City"": ""string"",
					""County"": ""string"",
					""State"": ""string"",
					""PostalCode"": ""string"",
					""CountryCode"": ""string"",
					""EmailAddress"": ""string"",
					""PhoneNumber"": ""string"",
					""PhoneNumber2"": ""string"",
					""PhoneNumber3"": ""string""
				},
				""AddressMatch"": false,
				""ExtendedData"": {
					""SecondaryAddress"": {
						""FirstName"": ""string"",
						""LastName"": ""string"",
						""Line1"": ""string"",
						""Line2"": ""string"",
						""City"": ""string"",
						""County"": ""string"",
						""State"": ""string"",
						""PostalCode"": ""string"",
						""CountryCode"": ""string"",
						""EmailAddress"": ""string"",
						""PhoneNumber"": ""string"",
						""PhoneNumber2"": ""string"",
						""PhoneNumber3"": ""string""
					},
					""CustomData"": ""string"",
					""Level2CustomData"": ""string"",
					""Level3CustomData"": ""string"",
					""ThreeDSecure"": {
						""Eci"": ""string"",
						""Cavv"": ""string"",
						""Xid"": ""string"",
						""AuthenticationStatus"": ""string"",
						""ProtocolVersion"": ""string"",
						""DSTransId"": ""string"",
						""ChallengeWindowSize"": 0,
						""ChannelIndicator"": ""string"",
						""RiIndicator"": ""string"",
						""ChallengeIndicator"": ""string"",
						""AuthenticationIndicator"": ""string"",
						""MessageCategory"": ""string"",
						""TransactionType"": ""string"",
						""AccountInfo"": {
							""AccountAgeIndicator"": ""string"",
							""AccountChangeDate"": ""string"",
							""AccountChangeIndicator"": ""string"",
							""AccountDate"": ""string"",
							""AccountPasswordChangeDate"": ""string"",
							""AccountPasswordChangeIndicator"": ""string"",
							""AccountPurchaseCount"": ""string"",
							""AccountProvisioningAttempts"": ""string"",
							""AccountDayTransactions"": ""string"",
							""AccountYearTransactions"": ""string"",
							""PaymentAccountAge"": ""string"",
							""PaymentAccountAgeIndicator"": ""string"",
							""ShipAddressUsageDate"": ""string"",
							""ShipAddressUsageIndicator"": ""string"",
							""ShipNameIndicator"": ""string"",
							""SuspiciousAccountActivity"": ""string""
						},
						""MessageExtensions"": [
							{
								""name"": ""string"",
								""id"": ""string"",
								""criticalityIndicator"": true,
								""version"": ""string"",
								""data"": ""string""
							}
						]
					},
					""Recurring"": {
						""Managed"": true,
						""StartDate"": ""string"",
						""Frequency"": ""string"",
						""ExpiryDate"": ""string""
					},
					""BrowserInfo"": {
						""AcceptHeader"": ""string"",
						""Language"": ""string"",
						""ScreenHeight"": ""string"",
						""ScreenWidth"": ""string"",
						""TimeZone"": ""string"",
						""UserAgent"": ""string"",
						""IP"": ""string"",
						""JavaEnabled"": true,
						""JavascriptEnabled"": true,
						""ColorDepth"": ""string""
					},
					""MerchantResponseUrl"": ""string"",
					""HostedPage"": {
						""PageSet"": ""string"",
						""PageName"": ""string""
					},
					""FraudCheck"": ""string""
				}
			}";
		}

	}
}
