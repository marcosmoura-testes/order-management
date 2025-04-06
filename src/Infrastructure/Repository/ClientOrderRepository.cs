using Domain.Entity;
using Domain.Interfaces.Repository;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    public class ClientOrderRepository : BaseRepository<ClientOrder, int>, IClientOrderRepository
    {
        private readonly DefaultDbContext _context;

        public ClientOrderRepository(DefaultDbContext context) : base(context)
        {
            _context = context;
        }

        public List<ClientOrder> GetAllByDealerIdStatusId(int dealerId, int statusId)
        {
            return _context.ClientOrder
                 .Include(co => co.CLientOrderProducts)
                 .Where(co => co.DealerId == dealerId && co.StatusId == statusId)
                 .ToList();
        }
    }
}
