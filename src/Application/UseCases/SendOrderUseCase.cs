using Application.CustomException;
using Application.Interfaces;
using Domain.DTO;
using Domain.Entity;
using Domain.UoW;
using Microsoft.Extensions.Logging;

namespace Application.UseCases
{
    /// <summary>
    /// Use case for sending an order.
    /// </summary>
    public class SendOrderUseCase : ISendOrderUseCase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<SendOrderUseCase> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="SendOrderUseCase"/> class.
        /// </summary>
        /// <param name="unitOfWork">The unit of work.</param>
        /// <param name="logger">The logger.</param>
        public SendOrderUseCase(IUnitOfWork unitOfWork, ILogger<SendOrderUseCase> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        /// <summary>
        /// Executes the use case to send an order.
        /// </summary>
        /// <param name="orderDTO">The order DTO.</param>
        /// <returns>The created client order.</returns>
        public async Task<ClientOrder> Execute(ClientOrderDTO orderDTO)
        {
            _logger.LogInformation("Starting execution of SendOrderUseCase.");

            _logger.LogInformation("Fetching dealer by CNPJ: {CNPJ}", orderDTO.DealerCNPJ);
            Dealer dealer = _unitOfWork.DealerRepository.GetByCNPJ(orderDTO.DealerCNPJ);

            if (dealer == null)
            {
                _logger.LogWarning("Dealer not found for CNPJ: {CNPJ}", orderDTO.DealerCNPJ);
                throw new BadRequestCustomException("Revendedor não encontrado");
            }

            if (orderDTO.ClientOrderProducts == null || orderDTO.ClientOrderProducts.Count == 0)
            {
                _logger.LogWarning("No products sent in the order.");
                throw new BadRequestCustomException("Não foram enviados os produtos");
            }

            ClientOrder clientOrder = new ClientOrder
            {
                CreatedAt = DateTime.Now,
                ClientCNPJ = orderDTO.ClientCNPJ,
                DealerId = dealer.Id,
                StatusId = 1
            };

            foreach (var produto in orderDTO.ClientOrderProducts)
            {
                _logger.LogInformation("Fetching product by SKU: {SKU}", produto.SKU);
                Product product = await _unitOfWork.ProductRepository.GetProductBySKU(produto.SKU);
                if (product == null)
                {
                    _logger.LogWarning("Product not found for SKU: {SKU}", produto.SKU);
                    throw new BadRequestCustomException($"Produto [{produto.SKU}] não encontrado");
                }

                clientOrder.CLientOrderProducts.Add(new ClientOrderProduct
                {
                    ProductId = product.Id,
                    Quantity = produto.Quantity,
                    UnitPrice = product.Price,
                    TotalAmount = product.Price * produto.Quantity
                });
            }

            clientOrder.TotalAmount = clientOrder.CLientOrderProducts.Sum(x => x.TotalAmount);

            var validator = new ClientOrderValidator();
            var validationResult = validator.Validate(clientOrder);
            if (!validationResult.IsValid)
            {
                _logger.LogWarning("Validation failed for client order: {Errors}", string.Join(", ", validationResult.Errors.Select(e => e.ErrorMessage)));
                throw new ValidationCustomException(string.Join(", ", validationResult.Errors.Select(e => e.ErrorMessage)));
            }

            _logger.LogInformation("Saving client order.");
            return _unitOfWork.ClientOrderRepository.Save(clientOrder);
        }
    }
}
