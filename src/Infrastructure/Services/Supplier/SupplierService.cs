using System.Text.Json;
using Azure.Messaging.ServiceBus;
using Domain.DTO;
using Domain.Interfaces.Services.Supplier;
using Microsoft.Extensions.Logging;
using Polly;
using Polly.Retry;

namespace Infrastructure.Services.Supplier
{
    /// <summary>
    /// Service for handling supplier operations.
    /// </summary>
    public class SupplierService : ISupplierService
    {
        private readonly ServiceBusClient _serviceBusClient;
        private readonly ILogger<SupplierService> _logger;
        private readonly AsyncRetryPolicy _retryPolicy;
        private readonly string _queueName = "supply-orders";

        /// <summary>
        /// Initializes a new instance of the <see cref="SupplierService"/> class.
        /// </summary>
        /// <param name="serviceBusClient">The Service Bus client.</param>
        /// <param name="logger">The logger instance.</param>
        public SupplierService(ServiceBusClient serviceBusClient, ILogger<SupplierService> logger)
        {
            _serviceBusClient = serviceBusClient;
            _logger = logger;

            _retryPolicy = Policy
                .Handle<Exception>()
                .WaitAndRetryAsync(5, attempt => TimeSpan.FromSeconds(Math.Pow(2, attempt)),
                    (exception, timespan, retryCount, context) =>
                    {
                        _logger.LogWarning($"falure {retryCount} to try. retry in {timespan.TotalSeconds}s...");
                    });
        }

        /// <summary>
        /// Sends an order to the supplier.
        /// </summary>
        /// <param name="supplierOrderRequestDTO">The supplier order request data transfer object.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
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
