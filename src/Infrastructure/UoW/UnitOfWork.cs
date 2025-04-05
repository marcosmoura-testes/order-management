using Domain.Interfaces.Repository;
using Domain.UoW;
using Infrastructure.Repository;

namespace Infrastructure.UoW
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DefaultDbContext _context;

        public UnitOfWork(DefaultDbContext context)
        {
            _context = context;
        }

        private IDealerRepository _dealerRepository;
        private IClientOrderRepository _clientOrderRepository;
        private IProductRepository _productRepository;

        public IDealerRepository DealerRepository
        {
            get
            {
                if (_dealerRepository == null)
                {
                    _dealerRepository = new DealerRepository(_context);
                }

                return _dealerRepository;
            }
        }
        public IClientOrderRepository ClientOrderRepository
        {
            get
            {
                if (_clientOrderRepository == null)
                {
                    _clientOrderRepository = new ClientOrderRepository(_context);
                }
                return _clientOrderRepository;
            }
        }
        public IProductRepository ProductRepository
        {
            get
            {
                if (_productRepository == null)
                {
                    _productRepository = new ProductRepository(_context);
                }
                return _productRepository;
            }
        }
    }
}
