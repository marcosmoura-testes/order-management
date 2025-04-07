namespace Domain.Entity
{
    /// <summary>
    /// Represents a product entity.
    /// </summary>
    public class Product
    {
        /// <summary>
        /// The unique identifier for the product.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// The name of the product.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// A description of the product.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// The price of the product.
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// The stock keeping unit identifier for the product.
        /// </summary>
        public string SKU { get; set; }

        /// <summary>
        /// The identifier for the category to which the product belongs.
        /// </summary>
        public int CategoryId { get; set; }

        /// <summary>
        /// The quantity of the product in stock.
        /// </summary>
        public int StockQuantity { get; set; }

        /// <summary>
        /// The date and time when the product was created.
        /// </summary>
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// The date and time when the product was last updated.
        /// </summary>
        public DateTime? UpdatedAt { get; set; }
    }
}
