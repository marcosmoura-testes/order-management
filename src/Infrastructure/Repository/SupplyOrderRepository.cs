using Domain.Entity;
using Domain.Interfaces.Repository;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    /// <summary>
    /// Repository for managing supply orders.
    /// </summary>
    public class SupplyOrderRepository : BaseRepository<SupplyOrder, int>, ISupplyOrderRepository
    {
        private readonly DefaultDbContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="SupplyOrderRepository"/> class.
        /// </summary>
        /// <param name="context">The database context.</param>
        public SupplyOrderRepository(DefaultDbContext context) : base(context)
        {
            _context = context;
        }

        /// <summary>
        /// Gets all supply orders by dealer ID and status ID.
        /// </summary>
        /// <param name="dealerId">The unique identifier for the dealer.</param>
        /// <param name="statusId">The unique identifier for the status.</param>
        /// <returns>A list of supply orders.</returns>
        public List<SupplyOrder> GetAllByDealerIdStatusId(int dealerId, int statusId)
        {
            return _context.SupplyOrder
                 .Include(co => co.SupplyOrderClientOrders)
                 .Where(co => co.DealerId == dealerId && co.StatusId == statusId)
                 .ToList();
        }
    }
}
