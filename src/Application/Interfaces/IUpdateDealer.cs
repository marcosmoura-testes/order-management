using Domain.Entity;

namespace Application.Interfaces
{
    public interface IUpdateDealer
    {
        Task<Dealer> Execute(Dealer dealer, int dealerId);
    }
}
