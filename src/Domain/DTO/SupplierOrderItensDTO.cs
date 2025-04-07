namespace Domain.DTO
{
    /// <summary>
    /// Data Transfer Object for Supplier Order Items.
    /// </summary>
    public class SupplierOrderItensDTO
    {
        /// <summary>
        /// Stock Keeping Unit (SKU) of the item.
        /// </summary>
        public string SKU { get; set; }

        /// <summary>
        /// Quantity of the item.
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        /// Unit price of the item.
        /// </summary>
        public decimal UnitPrice { get; set; }

        /// <summary>
        /// Total amount for the item.
        /// </summary>
        public decimal TotalAmount { get; set; }
    }
}
