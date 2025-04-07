using Domain.DTO;

namespace Application.Interfaces
{
    /// <summary>
    /// Interface for the request order use case.
    /// </summary>
    public interface IRequestOrderUseCase
    {
        /// <summary>
        /// Executes the request order use case.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation.</returns>
        Task Execute();
    }
}
