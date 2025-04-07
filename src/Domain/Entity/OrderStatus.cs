namespace Domain.Entity
{
    /// <summary>
    /// Represents the status of an order.
    /// </summary>
    public class OrderStatus
    {
        /// <summary>
        /// The unique identifier for the order status.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// The name of the order status.
        /// </summary>
        public string Name { get; set; }
    }
}
