namespace Domain.Entity
{
    public class ClientOrderProduct
    {
        public int Id { get; set; }
        public int ClientOrderId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalAmount { get; set; }
    }
}
