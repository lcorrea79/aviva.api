using AVIVA.Application.DTOs.Orders;
using AVIVA.Application.Interfaces;
using AVIVA.Application.Models;
using System;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;


namespace AVIVA.Infrastructure.Shared.Services
{
    public class PaymentProviderService : IPaymentProviderService
    {
        private readonly HttpClient _httpClient;
        //private readonly PaymentProviderConfig _paymentProviderConfig;

        // Inyectamos la configuración y el HttpClient
        public PaymentProviderService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ApiResponse<OrderProvider>> AddOrder(OrderRequestDto orderRequestDto, PaymentProviderConfig paymentProviderConfig)
        {
            // Crear el cuerpo de la solicitud para la API externa (puedes mapear el OrderDto a lo que espera la API).
            var paymentRequest = new
            {
                method = paymentProviderConfig.Method[orderRequestDto.Method],
                products = orderRequestDto.Products.Select(p => new { p.Name, p.UnitPrice }).ToList(),

            };

            // Serializar el objeto a JSON
            var jsonContent = new StringContent(JsonSerializer.Serialize(paymentRequest), Encoding.UTF8, "application/json");

            // Configurar el encabezado para la autorización (API Key)
            _httpClient.DefaultRequestHeaders.Add("x-api-key", paymentProviderConfig.ApiKey);

            try
            {
                // Hacer la solicitud HTTP a la API del proveedor
                var response = await _httpClient.PostAsync($"{paymentProviderConfig.ApiUrl}/order", jsonContent);


                // Procesar la respuesta de la API
                var responseContent = await response.Content.ReadAsStringAsync();
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                var paymentResult = JsonSerializer.Deserialize<OrderProvider>(responseContent, options);

                return new ApiResponse<OrderProvider>(true, "Success", paymentResult); ;
            }
            catch (Exception ex)
            {
                return new ApiResponse<OrderProvider>(false, $"Error de conexión: {ex.Message}", null);
            }
        }

        public async Task<ApiResponse<OrderProvider>> GetOrder(string orderId, PaymentProviderConfig paymentProviderConfig)
        {

            // Configurar el encabezado para la autorización (API Key)
            _httpClient.DefaultRequestHeaders.Add("x-api-key", paymentProviderConfig.ApiKey);

            try
            {
                // Hacer la solicitud HTTP a la API del proveedor
                var response = await _httpClient.GetAsync($"{paymentProviderConfig.ApiUrl}/order/{orderId}");

                if (response.IsSuccessStatusCode)
                {
                    // Procesar la respuesta de la API
                    var responseContent = await response.Content.ReadAsStringAsync();
                    var options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };
                    var paymentResult = JsonSerializer.Deserialize<OrderProvider>(responseContent, options);

                    return new ApiResponse<OrderProvider>(true, "Success", paymentResult); ;
                }
                else return new ApiResponse<OrderProvider>(false, "Failed", null);

            }
            catch (Exception ex)
            {
                return new ApiResponse<OrderProvider>(false, $"Error de conexión: {ex.Message}", null);
            }
        }
        public async Task<ApiResponse<string>> CancelOrder(string orderId, PaymentProviderConfig paymentProviderConfig)
        {

            // Configurar el encabezado para la autorización (API Key)
            _httpClient.DefaultRequestHeaders.Add("x-api-key", paymentProviderConfig.ApiKey);

            try
            {
                // Construir el URI con parámetros de consulta
                var uriBuilder = new UriBuilder($"{paymentProviderConfig.ApiUrl}/{paymentProviderConfig.EndpointCancel}")
                {
                    Query = $"id={orderId}"
                };
                // Hacer la solicitud HTTP a la API del proveedor
                var response = await _httpClient.PutAsync(uriBuilder.Uri, null);

                if (response.IsSuccessStatusCode)
                {
                    return new ApiResponse<string>(true, "Success", "OK"); ;
                }
                else return new ApiResponse<string>(false, "Failed", response.ReasonPhrase);

            }
            catch (Exception ex)
            {
                return new ApiResponse<string>(false, $"Error de conexión: {ex.Message}", "");
            }
        }

        public async Task<ApiResponse<string>> PayOrder(string orderId, PaymentProviderConfig paymentProviderConfig)
        {

            // Configurar el encabezado para la autorización (API Key)
            _httpClient.DefaultRequestHeaders.Add("x-api-key", paymentProviderConfig.ApiKey);

            try
            {
                // Construir el URI con parámetros de consulta
                var uriBuilder = new UriBuilder($"{paymentProviderConfig.ApiUrl}/{paymentProviderConfig.EndpointPay}")
                {
                    Query = $"id={orderId}"
                };
                // Hacer la solicitud HTTP a la API del proveedor
                var response = await _httpClient.PutAsync(uriBuilder.Uri, null);

                if (response.IsSuccessStatusCode)
                {
                    return new ApiResponse<string>(true, "Success", "OK"); ;
                }
                else return new ApiResponse<string>(false, "Failed", response.ReasonPhrase);

            }
            catch (Exception ex)
            {
                return new ApiResponse<string>(false, $"Error de conexión: {ex.Message}", "");
            }
        }
    }
}