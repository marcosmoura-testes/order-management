using Domain.Entity;

namespace Domain.Interfaces.Repository
{
    public interface IClientOrderRepository : IBaseRepository<ClientOrder, int>
    {
        List<ClientOrder> GetAllByDealerIdStatusId(int dealerId, int statusId);
    }
}
