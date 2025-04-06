using System.Linq.Expressions;

namespace Domain.Interfaces.Repository
{
    public interface IBaseRepository<T, TId> where T : class
    {
        T GetById(TId id, params Expression<Func<T, object>>[] includes);
        IList<T> GetAll(params Expression<Func<T, object>>[] includes);
        IList<T> GetAllPaginado(int pagina, int tamanho, params Expression<Func<T, object>>[] includes);
        T Save(T t);
        T Update(T t);
        T Delete(TId id);
    }
}
