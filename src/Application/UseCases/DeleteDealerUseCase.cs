using Application.Interfaces;
using Domain.Entity;
using Domain.UoW;
using Microsoft.Extensions.Logging;

namespace Application.UseCases
{
    /// <summary>
    /// Use case for deleting a dealer.
    /// </summary>
    public class DeleteDealerUseCase : IDeleteDealer
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<DeleteDealerUseCase> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteDealerUseCase"/> class.
        /// </summary>
        /// <param name="unitOfWork">The unit of work.</param>
        /// <param name="logger">The logger.</param>
        public DeleteDealerUseCase(IUnitOfWork unitOfWork, ILogger<DeleteDealerUseCase> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        /// <summary>
        /// Executes the deletion of a dealer by ID.
        /// </summary>
        /// <param name="id">The dealer ID.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a boolean indicating success.</returns>
        public async Task<bool> Execute(int id)
        {
            _logger.LogInformation("Starting deletion process for dealer with ID: {DealerId}", id);

            Dealer dealer = _unitOfWork.DealerRepository.GetById(id);

            if (dealer == null)
            {
                _logger.LogWarning("Dealer with ID: {DealerId} not found.", id);
                throw new Exception("Dealer not found.");
            }

            _unitOfWork.DealerRepository.Delete(id);
            _logger.LogInformation("Successfully deleted dealer with ID: {DealerId}", id);

            return true;
        }
    }
}
