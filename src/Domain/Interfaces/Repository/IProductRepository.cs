using Domain.Entity;

namespace Domain.Interfaces.Repository
{
    public interface IProductRepository : IBaseRepository<Product, int>
    {
        Task<Product> GetProductBySKU(string sku);
    }
}
