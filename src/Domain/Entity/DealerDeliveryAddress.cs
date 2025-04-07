namespace Domain.Entity
{
    /// <summary>
    /// Represents the delivery address for a dealer.
    /// </summary>
    public class DealerDeliveryAddress
    {
        /// <summary>
        /// The unique identifier for the delivery address.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// The unique identifier for the dealer.
        /// </summary>
        public int DealerId { get; set; }

        /// <summary>
        /// The delivery address.
        /// </summary>
        public string Address { get; set; }
    }
}
