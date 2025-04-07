using Domain.Entity;
using Domain.Interfaces.Repository;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    /// <summary>
    /// Repository for managing product entities.
    /// </summary>
    public class ProductRepository : BaseRepository<Product, int>, IProductRepository
    {
        private readonly DefaultDbContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductRepository"/> class.
        /// </summary>
        /// <param name="context">The database context.</param>
        public ProductRepository(DefaultDbContext context) : base(context)
        {
            _context = context;
        }

        /// <summary>
        /// Gets a list of products by their IDs.
        /// </summary>
        /// <param name="ids">The array of product IDs.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the list of products.</returns>
        public async Task<List<Product>> GetByIds(int[] ids) => _context.Product
                .Where(p => ids.Contains(p.Id))
                .ToList();

        /// <summary>
        /// Gets a product by its SKU.
        /// </summary>
        /// <param name="sku">The SKU of the product.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the product.</returns>
        public async Task<Product> GetProductBySKU(string sku) => await _context.Product.FirstOrDefaultAsync(p => p.SKU == sku);
    }
}

