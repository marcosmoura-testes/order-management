using Domain.Entity;

namespace Application.Interfaces
{
    /// <summary>
    /// Interface for reading dealer information.
    /// </summary>
    public interface IReadDealer
    {
        /// <summary>
        /// Retrieves a paginated list of dealers.
        /// </summary>
        /// <param name="page">The page number to retrieve.</param>
        /// <param name="limit">The number of dealers per page.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a list of dealers.</returns>
        Task<List<Dealer>> Execute(int page, int limit);

        /// <summary>
        /// Retrieves a dealer by its ID.
        /// </summary>
        /// <param name="id">The ID of the dealer to retrieve.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the dealer.</returns>
        Task<Dealer> ExecuteById(int id);
    }
}
