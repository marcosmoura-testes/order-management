using Domain.Entity;

namespace Domain.Interfaces.Repository
{
    /// <summary>
    /// Interface for the SupplyOrder repository.
    /// </summary>
    public interface ISupplyOrderRepository : IBaseRepository<SupplyOrder, int>
    {
        /// <summary>
        /// Gets all supply orders by dealer ID and status ID.
        /// </summary>
        /// <param name="dealerId">The unique identifier for the dealer.</param>
        /// <param name="statusId">The unique identifier for the status.</param>
        /// <returns>A list of supply orders.</returns>
        List<SupplyOrder> GetAllByDealerIdStatusId(int dealerId, int statusId);
    }
}
