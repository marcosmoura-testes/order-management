using Application.Interfaces;
using Domain.DTO;
using Domain.Entity;
using Domain.Interfaces.Services.Supplier;
using Domain.ObjectValue;
using Domain.UoW;

namespace Application.UseCases
{
    public class RequestOrderUseCase : IRequestOrderUseCase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ISupplierService _supplierService;
        public RequestOrderUseCase(IUnitOfWork unitOfWork, ISupplierService supplierService)
        {
            _unitOfWork = unitOfWork;
            _supplierService = supplierService;
        }

        public async Task<SupplierOrderResponseDTO> Execute(RequestOrderDTO requestOrderDTO)
        {
            Dealer dealer = _unitOfWork.DealerRepository.GetById(requestOrderDTO.DealerId);
            if (dealer == null)
            {
                throw new Exception("Dealer not found");
            }

            List<ClientOrder> clientOrders = _unitOfWork.ClientOrderRepository.GetAllByDealerIdStatusId(dealer.Id, (int)OrderStatusEnum.Pendente);


            if (clientOrders.Count > 0)
            {
                var totalAmount = clientOrders.Select(c => c.CLientOrderProducts.Sum(c => c.TotalAmount)).Sum();

                if (totalAmount < 1000)
                {
                    throw new Exception("Dealer not found");
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

                

                SupplierOrderRequestDTO supplierOrderRequestDTO = new SupplierOrderRequestDTO
                {
                    DealerCnpj = dealer.CNPJ,
                    SupplierOrderItens = supplierOrderItens
                };

                return await _supplierService.SendOrder(supplierOrderRequestDTO);
            }
            return null;
        }
    }
}
