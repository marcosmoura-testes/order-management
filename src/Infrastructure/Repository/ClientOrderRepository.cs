using Domain.Entity;
using Domain.Interfaces.Repository;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    /// <summary>
    /// Repository for managing client orders.
    /// </summary>
    public class ClientOrderRepository : BaseRepository<ClientOrder, int>, IClientOrderRepository
    {
        private readonly DefaultDbContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="ClientOrderRepository"/> class.
        /// </summary>
        /// <param name="context">The database context.</param>
        public ClientOrderRepository(DefaultDbContext context) : base(context)
        {
            _context = context;
        }

        /// <summary>
        /// Gets all client orders by dealer ID and status ID.
        /// </summary>
        /// <param name="dealerId">The ID of the dealer.</param>
        /// <param name="statusId">The ID of the status.</param>
        /// <returns>A list of client orders.</returns>
        public List<ClientOrder> GetAllByDealerIdStatusId(int dealerId, int statusId)
        {
            return _context.ClientOrder
                 .Include(co => co.CLientOrderProducts)
                 .Where(co => co.DealerId == dealerId && co.StatusId == statusId)
                 .ToList();
        }
    }
}
