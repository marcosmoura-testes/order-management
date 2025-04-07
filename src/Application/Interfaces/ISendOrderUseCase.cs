using Domain.DTO;
using Domain.Entity;

namespace Application.Interfaces
{
    public interface ISendOrderUseCase
    {
        /// <summary>
        /// Executes the process of sending a client order.
        /// </summary>
        /// <param name="orderDTO">The data transfer object containing the client order details.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the client order.</returns>
        Task<ClientOrder> Execute(ClientOrderDTO orderDTO);
    }
}
