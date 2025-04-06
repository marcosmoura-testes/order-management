namespace Domain.DTO
{
    public class SupplierOrderItensDTO
    {
        public string SKU { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalAmount { get; set; }
    }
}
