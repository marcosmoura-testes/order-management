namespace Domain.Entity
{
    /// <summary>
    /// Represents a category of products.
    /// </summary>
    public class ProductCategory
    {
        /// <summary>
        /// The unique identifier for the product category.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// The name of the product category.
        /// </summary>
        public string Name { get; set; }
    }
}
