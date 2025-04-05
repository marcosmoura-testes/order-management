using Domain.Entity;

namespace Application.Interfaces
{
    public interface IReadDealer
    {
        Task<List<Dealer>> Execute(int page, int limit);
        Task<Dealer> ExecuteById(int id);
    }
}
