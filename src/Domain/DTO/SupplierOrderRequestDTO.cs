namespace Domain.DTO
{
    public class SupplierOrderRequestDTO
    {
        public int InternalReference { get; set; }
        public string DealerCnpj { get; set; }
        public List<SupplierOrderItensDTO> SupplierOrderItens { get; set; }
    }
}
