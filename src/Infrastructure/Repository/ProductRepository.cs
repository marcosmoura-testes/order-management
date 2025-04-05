using Domain.Entity;
using Domain.Interfaces.Repository;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    public class ProductRepository : BaseRepository<Product, int>, IProductRepository
    {
        private readonly DefaultDbContext _context;

        public ProductRepository(DefaultDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Product> GetProductBySKU(string sku) => await _context.Product.FirstOrDefaultAsync(p => p.SKU == sku);
    }
}

