using Domain.Entity;

namespace Domain.Interfaces.Repository
{
    /// <summary>
    /// Interface for client order repository.
    /// </summary>
    public interface IClientOrderRepository : IBaseRepository<ClientOrder, int>
    {
        /// <summary>
        /// Gets all client orders by dealer ID and status ID.
        /// </summary>
        /// <param name="dealerId">The ID of the dealer.</param>
        /// <param name="statusId">The ID of the status.</param>
        /// <returns>A list of client orders.</returns>
        List<ClientOrder> GetAllByDealerIdStatusId(int dealerId, int statusId);
    }
}
