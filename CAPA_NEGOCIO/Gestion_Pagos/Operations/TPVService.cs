using System.Text;
using CAPA_NEGOCIO.Gestion_Pagos.Model;
using CAPA_NEGOCIO.Gestion_Pagos.Model.PowerTranzTpv;
using Newtonsoft.Json;

namespace CAPA_NEGOCIO.Gestion_Pagos.Operations
{
	public class TPVService
	{
		//const string GLOBAL_URL = "https://staging.ptranz.com/";
		const string GLOBAL_URL = "https://gateway.ptranz.com/";
		const string POWERTRANZ_ID = "33103870";
		const string POWERTRANZ_PASSWORD = "hBMTL1byyeLtoeWWIfDbX9TU1dKkwdjpC2faL7ya1JpuK6nlmmAaG2";
		public string T_IDENTIFY = "";
		public string O_IDENTIFY = Guid.NewGuid().ToString();
		public string? SPI;
		public async Task<PowerTranzTpvResponse> AuthenticateAsync(TPV datosDePago)
		{
			try
			{
				PowerTranzTpvResponse responseData = await ExecuteRequest("api/spi/Auth", BuildJsonAuthRequest(datosDePago));
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
				PowerTranzTpvResponse responseData = await ExecuteRequest("api/spi/Sale", BuildJsonAuthRequest(datosDePago));
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
				PowerTranzTpvResponse responseData = await Execute("api/spi/Payment", spiToken);
				return responseData;
			}
			catch (Exception ex)
			{
				// Manejo de errores
				throw new ApplicationException($"Error durante la venta: {ex.Message}", ex);
			}
		}

		public async Task<PowerTranzTpvResponse> PaymentAsync(string spiToken, string transactionIdentifier)
		{
			try
			{
				var request = new
				{
					TransactionIdentifier = transactionIdentifier
				};
				string requestJson = JsonConvert.SerializeObject(new { TransactionIdentifier = request.TransactionIdentifier });

				PowerTranzTpvResponse responseData = await Execute("api/spi/Payment", spiToken, requestJson);
				return responseData;
			}
			catch (Exception ex)
			{
				throw new ApplicationException($"Error durante la venta: {ex.Message}", ex);
			}
		}

		public async Task<PowerTranzTpvResponse> RiskMgmtAsync(TPV datosDePago)
		{
			try
			{
				PowerTranzTpvResponse responseData = await ExecuteRequest("api/spi/RiskMgmt", BuildJsonRiskMgmtRequest(datosDePago));
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
				PowerTranzTpvResponse responseData = await ExecuteRequest("api/spi/Capture", BuildJsonCaptureRequest(datosDePago));
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
					""CardExpiration"": """ + datosDePago.ExpYear + datosDePago.ExpMonth.ToString() + @""", 
					""CardholderName"": """ + datosDePago.CardholderName + @"""
				},
				""OrderIdentifier"": """ + O_IDENTIFY + @""",
				""AddressMatch"": false,
				""ExtendedData"": {
					""ThreeDSecure"": {
						""ChallengeWindowSize"": 4,
						""ChallengeIndicator"": ""01""
					},
					""MerchantResponseUrl"":  ""https://portal.cca.edu.ni/api/ApiPagos/MerchantResponseURL""
				},
				 ""BillingAddress"": { 					
					""EmailAddress"": """ + datosDePago.CardholderMail + @""", 
					""PhoneNumber"": """ + datosDePago.CardholderPhone + @"""
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
				},
				 ""BillingAddress"": { 					
					""EmailAddress"": """ + datosDePago.CardholderMail + @""", 
					""PhoneNumber"": """ + datosDePago.CardholderPhone + @""", 
				}				
			}";
			/*""BillingAddress"": { 
					""FirstName"": ""John"", 
					""LastName"": ""Smith"", 
					""Line1"": ""1200 Whitewall Blvd."", 
					""Line2"": "Unit 15", 
					""City"": "Boston", 
					""State"": "NY", 
					""PostalCode"": "200341", 
					""CountryCode"": "840", 
					"EmailAddress": "john.smith@gmail.com", 
					"PhoneNumber": "211-345-6790" */
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
				var content = new StringContent($"\"{jsonContent}\"", Encoding.UTF8, "application/json");

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

		private async Task<PowerTranzTpvResponse> Execute(string url, string jsonContent, string? body = null)
		{
			using HttpClient _httpClient = new HttpClient();

			// Agregar encabezados requeridos
			_httpClient.DefaultRequestHeaders.Add("Host", "staging.ptranz.com");
			_httpClient.DefaultRequestHeaders.Add("Accept", "text/plain");

			// Log de la solicitud
			Console.WriteLine("---- Request ----");
			Console.WriteLine($"URL: {GLOBAL_URL + url}");
			Console.WriteLine($"Body: {body ?? "No body provided"}");
			Console.WriteLine($"Headers: Host=staging.ptranz.com, Accept=text/plain");
			Console.WriteLine($"Request Body: {jsonContent}");

			HttpResponseMessage response;
			try
			{
				// Configurar el contenido como un string simple (no JSON objeto)
				var content = new StringContent($"\"{jsonContent}\"", Encoding.UTF8, "application/json");

				// Enviar la solicitud POST
				response = await _httpClient.PostAsync(GLOBAL_URL + url, content);

				// Verificar el código de estado HTTP
				response.EnsureSuccessStatusCode();

				// Leer la respuesta
				var responseContent = await response.Content.ReadAsStringAsync();

				// Log de la respuesta
				Console.WriteLine("---- Response ----");
				Console.WriteLine($"Status Code: {response.StatusCode}");
				Console.WriteLine($"Response Body: {responseContent}");

				// Convertir la respuesta a PowerTranzTpvResponse y retornarla
				return JsonConvert.DeserializeObject<PowerTranzTpvResponse>(responseContent);
			}
			catch (HttpRequestException ex)
			{
				// Log de error
				Console.WriteLine("---- Error ----");
				Console.WriteLine($"Error Message: {ex.Message}");
				if (ex.InnerException != null)
				{
					Console.WriteLine($"Inner Exception: {ex.InnerException.Message}");
				}

				// Lanzar la excepción nuevamente para que sea manejada en el llamador
				throw new Exception($"Request failed: {ex.Message}", ex);
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
