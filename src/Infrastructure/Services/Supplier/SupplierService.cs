using System.Net.Http.Json;
using System.Text.Json;
using Azure.Messaging.ServiceBus;
using Domain.DTO;
using Domain.Interfaces.Services.Supplier;
using Microsoft.Extensions.Logging;
using Polly;
using Polly.Retry;

namespace Infrastructure.Services.Supplier
{
    public class SupplierService : ISupplierService
    {
        private readonly ServiceBusClient _serviceBusClient;
        private readonly ILogger<SupplierService> _logger;
        private readonly AsyncRetryPolicy _retryPolicy;
        private readonly string _queueName = "supply-orders";

        public SupplierService(ServiceBusClient serviceBusClient, ILogger<SupplierService> logger)
        {
            _serviceBusClient = serviceBusClient;
            _logger = logger;

            _retryPolicy = Policy
                .Handle<Exception>()
                .WaitAndRetryAsync(5, attempt => TimeSpan.FromSeconds(Math.Pow(2, attempt)),
                    (exception, timespan, retryCount, context) =>
                    {
                        _logger.LogWarning($"Tentativa {retryCount} falhou. Retentando em {timespan.TotalSeconds}s...");
                    });
        }

        public async Task SendOrder(SupplierOrderRequestDTO supplierOrderRequestDTO)
        {
            var messageBody = JsonSerializer.Serialize(supplierOrderRequestDTO);
            var message = new ServiceBusMessage(messageBody);

            await _retryPolicy.ExecuteAsync(async () =>
            {
                ServiceBusSender sender = _serviceBusClient.CreateSender(_queueName);
                await sender.SendMessageAsync(message);
            });
        }
    }
}
