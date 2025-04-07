using Domain.Entity;

namespace Domain.Interfaces.Repository
{
    /// <summary>
    /// Interface for product repository operations.
    /// </summary>
    public interface IProductRepository : IBaseRepository<Product, int>
    {
        /// <summary>
        /// Gets a product by its SKU.
        /// </summary>
        /// <param name="sku">The SKU of the product.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the product.</returns>
        Task<Product> GetProductBySKU(string sku);

        /// <summary>
        /// Gets a list of products by their IDs.
        /// </summary>
        /// <param name="ids">The array of product IDs.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the list of products.</returns>
        Task<List<Product>> GetByIds(int[] ids);
    }
}
