using System.Linq.Expressions;
using System.Net.Http.Json;
using System.Text.Json;
using Azure.Messaging.ServiceBus;
using Domain.DTO;
using Domain.Entity;
using Domain.ObjectValue;
using Domain.UoW;
using Infrastructure.UoW;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Polly;
using Polly.Retry;

namespace ProcessSupplyOrders
{
    public class Function1
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<Function1> _logger;
        private readonly AsyncRetryPolicy<HttpResponseMessage> _retryPolicy;
        private readonly IUnitOfWork _unitOfWork;

        public Function1(IHttpClientFactory httpClientFactory, ILogger<Function1> logger, IUnitOfWork unitOfWork)
        {
            _httpClient = httpClientFactory.CreateClient("SupplierApi");
            _logger = logger;
            _unitOfWork = unitOfWork;

            _retryPolicy = Policy
                .HandleResult<HttpResponseMessage>(r => !r.IsSuccessStatusCode)
                .WaitAndRetryAsync(5, attempt => TimeSpan.FromSeconds(Math.Pow(2, attempt)),
                    (response, timespan, retryCount, context) =>
                    {
                        _logger.LogWarning($"Tentativa {retryCount} falhou. Retentando em {timespan.TotalSeconds}s...");
                    });
        }

        [Function(nameof(Function1))]
        public async Task Run(
            [ServiceBusTrigger("supply-orders", Connection = "ServiceBusConnection")]
            ServiceBusReceivedMessage message,
            ServiceBusMessageActions messageActions)
        {
            _logger.LogInformation("Message ID: {id}", message.MessageId);
            _logger.LogInformation("Message Body: {body}", message.Body);
            _logger.LogInformation("Message Content-Type: {contentType}", message.ContentType);

            SupplierOrderRequestDTO supplierOrderRequestDTO = JsonSerializer.Deserialize<SupplierOrderRequestDTO>(message.Body);
            var requestUrl = "/order";
            var response = await _retryPolicy.ExecuteAsync(() => _httpClient.PostAsJsonAsync(requestUrl, supplierOrderRequestDTO));
            if (response.IsSuccessStatusCode)
            {
                var responseOrder = await response.Content.ReadAsStringAsync();
                SupplierOrderResponseDTO supplierOrderResponseDTO = JsonSerializer.Deserialize<SupplierOrderResponseDTO>(responseOrder);

                SupplyOrder supplyOrder = _unitOfWork.SupplyOrderRepository.GetById(supplierOrderRequestDTO.InternalReference);

                if (supplyOrder != null)
                {
                    supplyOrder.StatusId = (int)OrderStatusEnum.EmSeparacao;
                    supplyOrder.TotalAmount = supplierOrderResponseDTO.TotalAmount;
                    supplyOrder.SupplyOrderId = supplierOrderResponseDTO.OrderId;

                    _unitOfWork.SupplyOrderRepository.Update(supplyOrder);                    
                }
                await messageActions.CompleteMessageAsync(message);
            }
        }
    }
}
