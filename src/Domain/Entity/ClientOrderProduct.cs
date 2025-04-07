namespace Domain.Entity
{
    /// <summary>
    /// Represents a product in a client's order.
    /// </summary>
    public class ClientOrderProduct
    {
        /// <summary>
        /// Unique identifier for the client order product.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Identifier for the associated client order.
        /// </summary>
        public int ClientOrderId { get; set; }

        /// <summary>
        /// Identifier for the associated product.
        /// </summary>
        public int ProductId { get; set; }

        /// <summary>
        /// Quantity of the product ordered.
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        /// Unit price of the product.
        /// </summary>
        public decimal UnitPrice { get; set; }

        /// <summary>
        /// Total amount for the product (Quantity * UnitPrice).
        /// </summary>
        public decimal TotalAmount { get; set; }
    }
}
