using Domain.Entity;

namespace Application.Interfaces
{
    /// <summary>
    /// Interface for updating a dealer.
    /// </summary>
    public interface IUpdateDealer
    {
        /// <summary>
        /// Executes the update operation for a dealer.
        /// </summary>
        /// <param name="dealer">The dealer entity with updated information.</param>
        /// <param name="dealerId">The ID of the dealer to be updated.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the updated dealer entity.</returns>
        Task<Dealer> Execute(Dealer dealer, int dealerId);
    }
}
