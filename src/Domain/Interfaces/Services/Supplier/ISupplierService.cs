using Domain.DTO;

namespace Domain.Interfaces.Services.Supplier
{
    /// <summary>
    /// Interface for supplier service operations.
    /// </summary>
    public interface ISupplierService
    {
        /// <summary>
        /// Sends an order to the supplier.
        /// </summary>
        /// <param name="supplierOrderRequestDTO">The supplier order request data transfer object.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        Task SendOrder(SupplierOrderRequestDTO supplierOrderRequestDTO);
    }
}
