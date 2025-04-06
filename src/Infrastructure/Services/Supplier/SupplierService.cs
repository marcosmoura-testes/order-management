using System.Net.Http.Json;
using System.Text.Json;
using Domain.DTO;
using Domain.Interfaces.Services.Supplier;
using Microsoft.Extensions.Logging;
using Polly;
using Polly.Retry;

namespace Infrastructure.Services.Supplier
{
    public class SupplierService : ISupplierService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<SupplierService> _logger;
        private readonly AsyncRetryPolicy<HttpResponseMessage> _retryPolicy;

        public SupplierService(IHttpClientFactory httpClientFactory, ILogger<SupplierService> logger)
        {
            _httpClient = httpClientFactory.CreateClient("SupplierApi");
            _logger = logger;

            _retryPolicy = Policy
                .HandleResult<HttpResponseMessage>(r => !r.IsSuccessStatusCode)
                .WaitAndRetryAsync(5, attempt => TimeSpan.FromSeconds(Math.Pow(2, attempt)),
                    (response, timespan, retryCount, context) =>
                    {
                        _logger.LogWarning($"Tentativa {retryCount} falhou. Retentando em {timespan.TotalSeconds}s...");
                    });
        }

        public async Task<SupplierOrderResponseDTO?> SendOrder(SupplierOrderRequestDTO supplierOrderRequestDTO)
        {
            var requestUrl = "/orders";
            var response = await _retryPolicy.ExecuteAsync(() => _httpClient.PostAsJsonAsync(requestUrl, supplierOrderRequestDTO));
            if (response.IsSuccessStatusCode)
            {
                var responseOrder = await response.Content.ReadAsStringAsync();
                var dto = JsonSerializer.Deserialize<SupplierOrderResponseDTO>(responseOrder, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
                return dto;
            }

            throw new Exception("request api failed, try again");
        }
    }
}
