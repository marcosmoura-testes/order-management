using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

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
