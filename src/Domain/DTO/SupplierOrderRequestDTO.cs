namespace Domain.DTO
{
    /// <summary>
    /// Represents a request for a supplier order.
    /// </summary>
    public class SupplierOrderRequestDTO
    {
        /// <summary>
        /// Internal reference number for the order.
        /// </summary>
        public int InternalReference { get; set; }

        /// <summary>
        /// CNPJ of the dealer.
        /// </summary>
        public string DealerCnpj { get; set; }

        /// <summary>
        /// List of items in the supplier order.
        /// </summary>
        public List<SupplierOrderItensDTO> SupplierOrderItens { get; set; }
    }
}
