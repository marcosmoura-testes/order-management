using Domain.Interfaces.Repository;

namespace Domain.UoW
{
    public interface IUnitOfWork
    {
        IDealerRepository DealerRepository { get; }
        IClientOrderRepository ClientOrderRepository { get; }
        IProductRepository ProductRepository { get; }
    }
}
