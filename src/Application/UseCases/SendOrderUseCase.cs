using Application.CustomException;
using Application.Interfaces;
using Domain.DTO;
using Domain.Entity;
using Domain.UoW;

namespace Application.UseCases
{
    public class SendOrderUseCase : ISendOrderUseCase
    {
        private readonly IUnitOfWork _unitOfWork;
        public SendOrderUseCase(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ClientOrder> Execute(ClientOrderDTO orderDTO)
        {
            try
            {
                Dealer dealer = _unitOfWork.DealerRepository.GetByCNPJ(orderDTO.DealerCNPJ);

                if (dealer == null)
                    throw new Exception("Revendedor não encontrado");

                if (orderDTO.ClientOrderProducts == null || orderDTO.ClientOrderProducts.Count == 0)
                    throw new BadRequestCustomException("Não foram enviados os produtos");

                ClientOrder clientOrder = new ClientOrder();

                clientOrder.CreatedAt = DateTime.Now;
                clientOrder.ClientCNPJ = orderDTO.ClientCNPJ;
                clientOrder.DealerId = dealer.Id;
                clientOrder.StatusId = 1;

                foreach (var produto in orderDTO.ClientOrderProducts)
                {
                    Product product = await _unitOfWork.ProductRepository.GetProductBySKU(produto.SKU);
                    if (product == null)
                        throw new BadRequestCustomException($"Produto [{produto.SKU}] não encontrado");

                    clientOrder.CLientOrderProducts.Add(new ClientOrderProduct()
                    {
                        ProductId = product.Id,
                        Quantity = produto.Quantity,
                        UnitPrice = product.Price,
                        TotalAmount = (product.Price * produto.Quantity)
                    });
                }

                clientOrder.TotalAmount = clientOrder.CLientOrderProducts.Sum(x => x.TotalAmount);

                var validator = new ClientOrderValidator();
                var validationResult = validator.Validate(clientOrder);
                if (!validationResult.IsValid)
                {
                    throw new ValidationCustomException(string.Join(", ", validationResult.Errors.Select(e => e.ErrorMessage)));
                }

                return _unitOfWork.ClientOrderRepository.Save(clientOrder);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
