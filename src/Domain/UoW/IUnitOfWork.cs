using Domain.Interfaces.Repository;

namespace Domain.UoW
{
    /// <summary>
    /// Interface for Unit of Work pattern, providing repositories for various entities.
    /// </summary>
    public interface IUnitOfWork
    {
        /// <summary>
        /// Gets the dealer repository.
        /// </summary>
        IDealerRepository DealerRepository { get; }

        /// <summary>
        /// Gets the client order repository.
        /// </summary>
        IClientOrderRepository ClientOrderRepository { get; }

        /// <summary>
        /// Gets the product repository.
        /// </summary>
        IProductRepository ProductRepository { get; }

        /// <summary>
        /// Gets the supply order repository.
        /// </summary>
        ISupplyOrderRepository SupplyOrderRepository { get; }
    }
}
