namespace Application.Interfaces
{
    /// <summary>
    /// Interface for deleting a dealer.
    /// </summary>
    public interface IDeleteDealer
    {
        /// <summary>
        /// Executes the deletion of a dealer by their ID.
        /// </summary>
        /// <param name="id">The ID of the dealer to delete.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a boolean indicating whether the deletion was successful.</returns>
        Task<bool> Execute(int id);
    }
}
