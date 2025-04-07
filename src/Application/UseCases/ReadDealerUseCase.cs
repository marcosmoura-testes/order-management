using System.Linq.Expressions;
using Application.Interfaces;
using Domain.Entity;
using Domain.UoW;
using Microsoft.Extensions.Logging;

namespace Application.UseCases
{
    /// <summary>
    /// Use case for reading dealer information.
    /// </summary>
    public class ReadDealerUseCase : IReadDealer
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<ReadDealerUseCase> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="ReadDealerUseCase"/> class.
        /// </summary>
        /// <param name="unitOfWork">The unit of work.</param>
        /// <param name="logger">The logger instance.</param>
        public ReadDealerUseCase(IUnitOfWork unitOfWork, ILogger<ReadDealerUseCase> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        /// <summary>
        /// Executes the use case to get a paginated list of dealers.
        /// </summary>
        /// <param name="page">The page number.</param>
        /// <param name="limit">The number of items per page.</param>
        /// <returns>A list of dealers.</returns>
        public async Task<List<Dealer>> Execute(int page, int limit)
        {
            _logger.LogInformation("Executing ReadDealerUseCase.Execute with page: {Page}, limit: {Limit}", page, limit);

            Expression<Func<Dealer, object>>[] includes = {
                    d => d.ContacstDealer,
                    d => d.DealerDeliveryAddress
                };

            var dealers = _unitOfWork.DealerRepository.GetAllPaginado(page, limit, includes).ToList();
            _logger.LogInformation("Retrieved {Count} dealers", dealers.Count);

            return dealers;
        }

        /// <summary>
        /// Executes the use case to get a dealer by ID.
        /// </summary>
        /// <param name="id">The dealer ID.</param>
        /// <returns>The dealer entity.</returns>
        public async Task<Dealer> ExecuteById(int id)
        {
            _logger.LogInformation("Executing ReadDealerUseCase.ExecuteById with ID: {Id}", id);

            Expression<Func<Dealer, object>>[] includes = {
                    d => d.ContacstDealer,
                    d => d.DealerDeliveryAddress
                };

            var dealer = _unitOfWork.DealerRepository.GetById(id, includes);
            if (dealer == null)
            {
                _logger.LogWarning("Dealer with ID: {Id} not found", id);
            }
            else
            {
                _logger.LogInformation("Retrieved dealer with ID: {Id}", id);
            }

            return dealer;
        }
    }
}
