using Domain.DTO;
using Domain.Entity;

namespace Application.Interfaces
{
    public interface ISendOrderUseCase
    {
        Task<ClientOrder> Execute(ClientOrderDTO orderDTO);
    }
}
