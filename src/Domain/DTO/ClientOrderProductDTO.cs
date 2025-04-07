namespace Domain.DTO
{
    /// <summary>
    /// Represents a product in a client's order.
    /// </summary>
    public class ClientOrderProductDTO
    {
        /// <summary>
        /// Stock Keeping Unit (SKU) of the product.
        /// </summary>
        public string SKU { get; set; }

        /// <summary>
        /// Quantity of the product ordered.
        /// </summary>
        public int Quantity { get; set; }
    }
}
