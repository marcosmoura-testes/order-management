using Domain.DTO;

namespace Application.Interfaces
{
    public interface IRequestOrderUseCase
    {
        Task<SupplierOrderResponseDTO> Execute(RequestOrderDTO requestOrderDTO);
    }
}
