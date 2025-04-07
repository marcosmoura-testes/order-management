using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    /// <summary>
    /// Base repository class providing common data access methods for entities.
    /// </summary>
    /// <typeparam name="T">The type of the entity.</typeparam>
    /// <typeparam name="TId">The type of the entity's identifier.</typeparam>
    public class BaseRepository<T, TId> where T : class
    {
        private readonly DefaultDbContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseRepository{T, TId}"/> class.
        /// </summary>
        /// <param name="context">The database context.</param>
        public BaseRepository(DefaultDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Gets an entity by its identifier.
        /// </summary>
        /// <param name="id">The identifier of the entity.</param>
        /// <param name="includes">Related entities to include in the query.</param>
        /// <returns>The entity if found; otherwise, null.</returns>
        public T GetById(TId id, params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = _context.Set<T>();

            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            return query.FirstOrDefault(e => EF.Property<TId>(e, "Id").Equals(id));
        }

        /// <summary>
        /// Gets all entities.
        /// </summary>
        /// <param name="includes">Related entities to include in the query.</param>
        /// <returns>A list of all entities.</returns>
        public IList<T> GetAll(params Expression<Func<T, object>>[]? includes)
        {
            IQueryable<T> query = _context.Set<T>();

            if (includes != null)
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            }

            return query.ToList();
        }

        /// <summary>
        /// Gets a paginated list of entities.
        /// </summary>
        /// <param name="pagina">The page number.</param>
        /// <param name="tamanho">The page size.</param>
        /// <param name="includes">Related entities to include in the query.</param>
        /// <returns>A paginated list of entities.</returns>
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

        /// <summary>
        /// Saves a new entity.
        /// </summary>
        /// <param name="t">The entity to save.</param>
        /// <returns>The saved entity.</returns>
        public T Save(T t)
        {
            _context.Set<T>().Add(t);
            _context.SaveChanges();
            return t;
        }

        /// <summary>
        /// Updates an existing entity.
        /// </summary>
        /// <param name="t">The entity to update.</param>
        /// <returns>The updated entity.</returns>
        public T Update(T t)
        {
            _context.Entry(t).State = EntityState.Modified;
            _context.SaveChanges();

            return t;
        }

        /// <summary>
        /// Deletes an entity by its identifier.
        /// </summary>
        /// <param name="id">The identifier of the entity to delete.</param>
        /// <returns>The deleted entity if found; otherwise, null.</returns>
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
