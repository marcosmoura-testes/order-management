namespace Domain.DTO
{
    /// <summary>
    /// Represents a client's order.
    /// </summary>
    public class ClientOrderDTO
    {
        /// <summary>
        /// Order ID.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Date the order was created.
        /// </summary>
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// CNPJ of the client.
        /// </summary>
        public string ClientCNPJ { get; set; }

        /// <summary>
        /// CNPJ of the dealer.
        /// </summary>
        public string DealerCNPJ { get; set; }

        /// <summary>
        /// Products in the client's order.
        /// </summary>
        public List<ClientOrderProductDTO> ClientOrderProducts { get; set; } = new List<ClientOrderProductDTO>();
    }
}
