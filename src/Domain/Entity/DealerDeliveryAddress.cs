namespace Domain.Entity
{
    public class DealerDeliveryAddress
    {
        public int Id { get; set; }
        public int DealerId { get; set; }
        public string Address { get; set; }
    }
}
