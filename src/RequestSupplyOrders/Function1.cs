using Application.Interfaces;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace RequestSupplyOrders
{
    public class Function1
    {
        private readonly ILogger _logger;
        private readonly IRequestOrderUseCase _requestOrderUseCase;

        public Function1(ILoggerFactory loggerFactory, IRequestOrderUseCase requestOrderUseCase)
        {
            _logger = loggerFactory.CreateLogger<Function1>();
            _requestOrderUseCase = requestOrderUseCase;
        }

        /// <summary>
        /// Timer trigger function to process dealer orders.
        /// </summary>
        /// <param name="myTimer">Timer information</param>
        [Function("RequestOrderFunction")]
        public async Task Run([TimerTrigger("0 */5 * * * *")] TimerInfo myTimer)
        {
            _logger.LogInformation($"C# Timer trigger function started at: {DateTime.Now}");

            try
            {
                await _requestOrderUseCase.Execute();
                _logger.LogInformation("Dealer orders processed successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error processing dealer orders: {ex.Message}");
            }

            _logger.LogInformation($"C# Timer trigger function ended at: {DateTime.Now}");
        }
    }
}
