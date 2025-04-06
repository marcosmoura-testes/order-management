namespace Domain.DTO
{
    public class SupplierOrderResponseDTO
    {
        public int OrderId { get; set; }
        public decimal TotalAmount { get; set; }
        public List<SupplierOrderItensDTO> SupplierOrderItens { get; set; }
    }
}
