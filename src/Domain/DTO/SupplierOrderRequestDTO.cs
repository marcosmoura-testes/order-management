namespace Domain.DTO
{
    public class SupplierOrderRequestDTO
    {
        public string DealerCnpj { get; set; }
        public List<SupplierOrderItensDTO> SupplierOrderItens { get; set; }
    }
}
