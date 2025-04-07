using System.Linq.Expressions;

namespace Domain.Interfaces.Repository
{
    /// <summary>
    /// Interface for a base repository providing common data access methods.
    /// </summary>
    /// <typeparam name="T">The type of the entity.</typeparam>
    /// <typeparam name="TId">The type of the entity's identifier.</typeparam>
    public interface IBaseRepository<T, TId> where T : class
    {
        /// <summary>
        /// Gets an entity by its identifier.
        /// </summary>
        /// <param name="id">The identifier of the entity.</param>
        /// <param name="includes">Optional related entities to include in the query.</param>
        /// <returns>The entity with the specified identifier.</returns>
        T GetById(TId id, params Expression<Func<T, object>>[]? includes);

        /// <summary>
        /// Gets all entities.
        /// </summary>
        /// <param name="includes">Optional related entities to include in the query.</param>
        /// <returns>A list of all entities.</returns>
        IList<T> GetAll(params Expression<Func<T, object>>[] includes);

        /// <summary>
        /// Gets a paginated list of entities.
        /// </summary>
        /// <param name="pagina">The page number.</param>
        /// <param name="tamanho">The page size.</param>
        /// <param name="includes">Optional related entities to include in the query.</param>
        /// <returns>A paginated list of entities.</returns>
        IList<T> GetAllPaginado(int pagina, int tamanho, params Expression<Func<T, object>>[] includes);

        /// <summary>
        /// Saves a new entity.
        /// </summary>
        /// <param name="t">The entity to save.</param>
        /// <returns>The saved entity.</returns>
        T Save(T t);

        /// <summary>
        /// Updates an existing entity.
        /// </summary>
        /// <param name="t">The entity to update.</param>
        /// <returns>The updated entity.</returns>
        T Update(T t);

        /// <summary>
        /// Deletes an entity by its identifier.
        /// </summary>
        /// <param name="id">The identifier of the entity to delete.</param>
        /// <returns>The deleted entity.</returns>
        T Delete(TId id);
    }
}
