using Domain.DTO;

namespace Domain.Interfaces.Services.Supplier
{
    public interface ISupplierService
    {
        Task<SupplierOrderResponseDTO> SendOrder(SupplierOrderRequestDTO supplierOrderRequestDTO);
    }
}
