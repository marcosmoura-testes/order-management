using Domain.Entity;
using Domain.Interfaces.Repository;

namespace Infrastructure.Repository
{
    /// <summary>
    /// Repository for managing Dealer entities.
    /// </summary>
    public class DealerRepository : BaseRepository<Dealer, int>, IDealerRepository
    {
        private readonly DefaultDbContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="DealerRepository"/> class.
        /// </summary>
        /// <param name="context">The database context.</param>
        public DealerRepository(DefaultDbContext context) : base(context)
        {
            _context = context;
        }

        /// <summary>
        /// Checks if a dealer with the specified CNPJ exists.
        /// </summary>
        /// <param name="cnpj">The CNPJ of the dealer.</param>
        /// <returns>True if a dealer with the specified CNPJ exists, otherwise false.</returns>
        public bool CNPJExists(string cnpj)
        {
            return _context.Dealer.Any(d => d.CNPJ == cnpj);
        }

        /// <summary>
        /// Retrieves a dealer by its CNPJ.
        /// </summary>
        /// <param name="cnpj">The CNPJ of the dealer.</param>
        /// <returns>The dealer with the specified CNPJ.</returns>
        public Dealer GetByCNPJ(string cnpj)
        {
            return _context.Dealer.FirstOrDefault(d => d.CNPJ == cnpj);
        }
    }

}
