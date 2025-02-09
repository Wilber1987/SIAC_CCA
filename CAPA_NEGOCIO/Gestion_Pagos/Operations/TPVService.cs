using System.Text;
using CAPA_NEGOCIO.Gestion_Pagos.Model;
using CAPA_NEGOCIO.Gestion_Pagos.Model.PowerTranzTpv;

namespace CAPA_NEGOCIO.Gestion_Pagos.Operations
{
	public class TPVService
	{
		const string GLOBAL_URL = "https://staging.ptranz.com/";
		const string POWERTRANZ_ID = "77700572";
		const string POWERTRANZ_PASSWORD = "G3jXzV3nzXZEnwq9Kz6zHyJksoksQLXTUOtcEFrj533qbmIBcwhN9M0";
		public string T_IDENTIFY = "";
		public string O_IDENTIFY = Guid.NewGuid().ToString();
		public string? SPI;
		public async Task<PowerTranzTpvResponse> AuthenticateAsync(TPV datosDePago)
		{
			try
			{
				PowerTranzTpvResponse responseData = await ExecuteRequest("Api/spi/Auth", BuildJsonAuthRequest(datosDePago));
				return responseData;
			}
			catch (Exception ex)
			{
				// Manejo de errores
				throw new ApplicationException($"Error durante la autenticación: {ex.Message}", ex);
			}
		}
		public async Task<PowerTranzTpvResponse> SalesAsync(TPV datosDePago)
		{
			try
			{
				PowerTranzTpvResponse responseData = await ExecuteRequest("Api/spi/Sale", BuildJsonAuthRequest(datosDePago));
				SPI = responseData.SpiToken;
				return responseData;
			}
			catch (Exception ex)
			{
				// Manejo de errores
				throw new ApplicationException($"Error durante la venta: {ex.Message}", ex);
			}
		}

		public async Task<PowerTranzTpvResponse> PaymentAsync(string? spiToken)
		{
			try
			{
				PowerTranzTpvResponse responseData = await Execute("Api/spi/Payment", spiToken);
				return responseData;
			}
			catch (Exception ex)
			{
				// Manejo de errores
				throw new ApplicationException($"Error durante la venta: {ex.Message}", ex);
			}
		}

		public async Task<PowerTranzTpvResponse> RiskMgmtAsync(TPV datosDePago)
		{
			try
			{
				PowerTranzTpvResponse responseData = await ExecuteRequest("Api/spi/RiskMgmt", BuildJsonRiskMgmtRequest(datosDePago));
				return responseData;
			}
			catch (Exception ex)
			{
				// Manejo de errores
				throw new ApplicationException($"Error durante la venta: {ex.Message}", ex);
			}
		}
		public async Task<PowerTranzTpvResponse> CaptureAsync(TPV datosDePago)
		{
			try
			{
				PowerTranzTpvResponse responseData = await ExecuteRequest("Api/spi/Capture", BuildJsonCaptureRequest(datosDePago));
				return responseData;
			}
			catch (Exception ex)
			{
				// Manejo de errores
				throw new ApplicationException($"Error durante la venta: {ex.Message}", ex);
			}
		}

		private string? BuildJsonCaptureRequest(TPV datosDePago)
		{
			return null;
		}

		private string? BuildJsonRiskMgmtRequest(TPV datosDePago)
		{
			return null;
		}

		private string BuildJsonAuthRequest(TPV datosDePago)
		{
			return @"{
				""TransactionIdentifier"": """ + T_IDENTIFY + @""",
				""TotalAmount"": " + datosDePago.pagosRequest?.Monto.ToString() + @",			
				""CurrencyCode"": ""558"",	
				""ThreeDSecure"": true,		
				""Source"": {					
					""CardPan"": """ + datosDePago.CardNumber?.ToString() + @""", 
					""CardCvv"": """ + datosDePago.Cvv?.ToString() + @""", 
					""CardExpiration"": """ + datosDePago.ExpYear + datosDePago.ExpMonth + @""", 
					""CardholderName"": """ + datosDePago.CardholderName + @"""
				},
				""OrderIdentifier"": """ + O_IDENTIFY + @""",
				""AddressMatch"": false,
				""ExtendedData"": {
					""ThreeDSecure"": {
						""ChallengeWindowSize"": 4,
						""ChallengeIndicator"": ""01""
					},
					""MerchantResponseUrl"":  ""https://28c3-186-1-23-18.ngrok-free.app/api/ApiPagos/MerchantResponseURL""
				}
			}";
		}
		private string BuildJsonSalesRequest(TPV datosDePago)
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
		private async Task<PowerTranzTpvResponse> Execute(string url, string jsonContent)
		{
			using HttpClient _httpClient = new HttpClient();

			// Agregar encabezados requeridos
			_httpClient.DefaultRequestHeaders.Add("Host", "staging.ptranz.com");
			_httpClient.DefaultRequestHeaders.Add("Accept", "text/plain");

			HttpResponseMessage response;
			try
			{
				// Configurar el contenido como un string simple (no JSON objeto)
				var content = new StringContent($"\"{jsonContent}\"" , Encoding.UTF8, "application/json");

				// Enviar la solicitud POST
				response = await _httpClient.PostAsync(GLOBAL_URL + url, content);

				// Verificar el código de estado HTTP
				response.EnsureSuccessStatusCode();

				return await response.Content.ReadAsAsync<PowerTranzTpvResponse>();
			}
			catch (HttpRequestException ex)
			{
				// Leer contenido de error si está disponible
				//var errorContent = response != null ? await response.Content.ReadAsStringAsync() : "No response content";
				throw new Exception($"Request failed: {ex.Message}");
			}
		}
		private async Task<PowerTranzTpvResponse> ExecuteRequest(string url, string jsonContent)
		{
			using HttpClient _httpClient = new HttpClient();

			// Agregar encabezados requeridos
			_httpClient.DefaultRequestHeaders.Add("PowerTranz-PowerTranzId", POWERTRANZ_ID);
			_httpClient.DefaultRequestHeaders.Add("PowerTranz-PowerTranzPassword", POWERTRANZ_PASSWORD);
			//_httpClient.DefaultRequestHeaders.Add("PowerTranz-PowerTranzPassword", POWERTRANZ_PASSWORD);
			//_httpClient.DefaultRequestHeaders.Add("Content-Type", "application/json");

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
			return await response.Content.ReadAsAsync<PowerTranzTpvResponse>();
		}
		

	}
}
