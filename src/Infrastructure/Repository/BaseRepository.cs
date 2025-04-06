using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    public class BaseRepository<T, TId> where T : class
    {
        private readonly DefaultDbContext _context;

        public BaseRepository(DefaultDbContext context)
        {
            _context = context;
        }

        public T GetById(TId id, params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = _context.Set<T>();

            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            return query.FirstOrDefault(e => EF.Property<TId>(e, "Id").Equals(id));
        }

        public IList<T> GetAll(params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = _context.Set<T>();

            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            return query.ToList();
        }

        public IList<T> GetAllPaginado(int pagina, int tamanho, params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = _context.Set<T>();

            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            return query.Skip((pagina - 1) * tamanho)
                        .Take(tamanho)
                        .ToList();
        }

        public T Save(T t)
        {
            _context.Set<T>().Add(t);
            _context.SaveChanges();
            return t;
        }

        public T Update(T t)
        {
            _context.Entry(t).State = EntityState.Modified;
            _context.SaveChanges();

            return t;
        }

        public T Delete(TId id)
        {
            T t = GetById(id);
            if (t != null)
            {
                _context.Entry(t).State = EntityState.Deleted;
                _context.Remove(t);
                _context.SaveChanges();
            }
            return t;
        }
    }
}
