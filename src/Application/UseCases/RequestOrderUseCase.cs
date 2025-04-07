using Application.Interfaces;
using Domain.DTO;
using Domain.Entity;
using Domain.Interfaces.Services.Supplier;
using Domain.ObjectValue;
using Domain.UoW;
using Microsoft.Extensions.Logging;

namespace Application.UseCases
{
    /// <summary>
    /// Use case for requesting orders from suppliers.
    /// </summary>
    public class RequestOrderUseCase : IRequestOrderUseCase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ISupplierService _supplierService;
        private readonly ILogger<RequestOrderUseCase> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="RequestOrderUseCase"/> class.
        /// </summary>
        /// <param name="unitOfWork">The unit of work.</param>
        /// <param name="supplierService">The supplier service.</param>
        /// <param name="logger">The logger.</param>
        public RequestOrderUseCase(IUnitOfWork unitOfWork, ISupplierService supplierService, ILogger<RequestOrderUseCase> logger)
        {
            _unitOfWork = unitOfWork;
            _supplierService = supplierService;
            _logger = logger;
        }

        /// <summary>
        /// Executes the use case.
        /// </summary>
        public async Task Execute()
        {
            _logger.LogInformation("Starting execution of RequestOrderUseCase.");

            List<Dealer> dealers = _unitOfWork.DealerRepository.GetAll(null).ToList();
            _logger.LogInformation("Retrieved {DealerCount} dealers.", dealers.Count);

            foreach (var dealer in dealers)
            {
                _logger.LogInformation("Processing dealer with ID {DealerId}.", dealer.Id);

                List<ClientOrder> clientOrders = _unitOfWork.ClientOrderRepository.GetAllByDealerIdStatusId(dealer.Id, (int)OrderStatusEnum.Pendente);
                _logger.LogInformation("Retrieved {ClientOrderCount} client orders for dealer ID {DealerId}.", clientOrders.Count, dealer.Id);

                if (clientOrders.Count > 0)
                {
                    var totalAmount = clientOrders.Select(c => c.CLientOrderProducts.Sum(c => c.TotalAmount)).Sum();
                    _logger.LogInformation("Total amount for dealer ID {DealerId} is {TotalAmount}.", dealer.Id, totalAmount);

                    if (totalAmount < 1000)
                    {
                        _logger.LogWarning("Total amount for dealer ID {DealerId} is less than 1000. Skipping supplier order.", dealer.Id);
                        continue;
                    }

                    List<SupplierOrderItensDTO> supplierOrderItens = new List<SupplierOrderItensDTO>();
                    foreach (var order in clientOrders)
                    {
                        List<Product> products = await _unitOfWork.ProductRepository.GetByIds(order.CLientOrderProducts.Select(p => p.ProductId).ToArray());

                        foreach (var product in order.CLientOrderProducts)
                        {
                            supplierOrderItens.Add(new SupplierOrderItensDTO
                            {
                                SKU = products.Select(prd => prd.SKU).FirstOrDefault(),
                                Quantity = product.Quantity,
                                TotalAmount = product.TotalAmount
                            });
                        }
                    }

                    SupplyOrder supplyOrder = new SupplyOrder
                    {
                        DealerId = dealer.Id,
                        CreatedAt = DateTime.Now,
                        StatusId = (int)OrderStatusEnum.Enviado,
                        TotalAmount = totalAmount,
                        SupplyOrderClientOrders = clientOrders.Select(co => new SupplyOrderClientOrder
                        {
                            ClientOrderId = co.Id,
                        }).ToList()
                    };

                    _unitOfWork.SupplyOrderRepository.Save(supplyOrder);
                    _logger.LogInformation("Saved supply order for dealer ID {DealerId}.", dealer.Id);

                    SupplierOrderRequestDTO supplierOrderRequestDTO = new SupplierOrderRequestDTO
                    {
                        DealerCnpj = dealer.CNPJ,
                        InternalReference = supplyOrder.Id,
                        SupplierOrderItens = supplierOrderItens
                    };

                    await _supplierService.SendOrder(supplierOrderRequestDTO);
                    _logger.LogInformation("Sent supplier order for dealer ID {DealerId}.", dealer.Id);
                }
            }

            _logger.LogInformation("Finished execution of RequestOrderUseCase.");
        }
    }
}
