using Domain.DTO;

namespace Domain.Interfaces.Services.Supplier
{
    public interface ISupplierService
    {
        Task SendOrder(SupplierOrderRequestDTO supplierOrderRequestDTO);
    }
}
