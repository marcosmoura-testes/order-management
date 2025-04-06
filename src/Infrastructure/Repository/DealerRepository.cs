using Domain.Entity;
using Domain.Interfaces.Repository;

namespace Infrastructure.Repository
{
    public class DealerRepository : BaseRepository<Dealer, int>, IDealerRepository
    {
        private readonly DefaultDbContext _context;

        public DealerRepository(DefaultDbContext context) : base(context)
        {
            _context = context;
        }

        public bool CNPJExists(string cnpj)
        {
            return _context.Dealer.Any(d => d.CNPJ == cnpj);
        }

        public Dealer GetByCNPJ(string cnpj)
        {
            return _context.Dealer.FirstOrDefault(d => d.CNPJ == cnpj);
        }
    }

}
