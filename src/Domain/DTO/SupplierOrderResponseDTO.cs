namespace Domain.DTO
{
    /// <summary>
    /// Represents the response for a supplier order.
    /// </summary>
    public class SupplierOrderResponseDTO
    {
        /// <summary>
        /// Order ID.
        /// </summary>
        public int OrderId { get; set; }

        /// <summary>
        ///Total amount of the order.
        /// </summary>
        public decimal TotalAmount { get; set; }

        /// <summary>
        /// List of supplier order items.
        /// </summary>
        public List<SupplierOrderItensDTO> SupplierOrderItens { get; set; }
    }
}
