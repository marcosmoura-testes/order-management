using Domain.Entity;
using Domain.Interfaces.Repository;

namespace Infrastructure.Repository
{
    public class ClientOrderRepository : BaseRepository<ClientOrder, int>, IClientOrderRepository
    {
        private readonly DefaultDbContext _context;

        public ClientOrderRepository(DefaultDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
