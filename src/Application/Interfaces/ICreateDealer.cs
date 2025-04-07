using Domain.Entity;

namespace Application.Interfaces
{
    /// <summary>
    /// Interface for creating a dealer.
    /// </summary>
    public interface ICreateDealer
    {
        /// <summary>
        /// Executes the creation of a dealer.
        /// </summary>
        /// <param name="dealer">The dealer entity to be created.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the created dealer entity.</returns>
        Task<Dealer> Execute(Dealer dealer);
    }
}
