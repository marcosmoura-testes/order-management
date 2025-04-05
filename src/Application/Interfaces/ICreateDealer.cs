using Domain.Entity;

namespace Application.Interfaces
{
    public interface ICreateDealer
    {
        Task<Dealer> Execute(Dealer dealer);
    }
}
