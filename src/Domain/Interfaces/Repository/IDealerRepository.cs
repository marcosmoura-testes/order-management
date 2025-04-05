using Domain.Entity;

namespace Domain.Interfaces.Repository
{
    public interface IDealerRepository : IBaseRepository<Dealer, int>
    {
        bool CNPJExists(string cnpj);
        Dealer GetByCNPJ(string cnpj);
    }
}
