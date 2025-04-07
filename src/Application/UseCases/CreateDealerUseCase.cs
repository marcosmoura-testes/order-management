using Application.CustomException;
using Application.Interfaces;
using Domain.Entity;
using Domain.UoW;
using Microsoft.Extensions.Logging;

namespace Application.UseCases
{
    /// <summary>
    /// Use case for creating a new dealer.
    /// </summary>
    public class CreateDealerUseCase : ICreateDealer
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<CreateDealerUseCase> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateDealerUseCase"/> class.
        /// </summary>
        /// <param name="unitOfWork">The unit of work.</param>
        /// <param name="logger">The logger.</param>
        public CreateDealerUseCase(IUnitOfWork unitOfWork, ILogger<CreateDealerUseCase> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        /// <summary>
        /// Executes the use case to create a new dealer.
        /// </summary>
        /// <param name="dealer">The dealer entity to be created.</param>
        /// <returns>The created dealer entity.</returns>
        /// <exception cref="DuplicateRecordException">Thrown when the CNPJ is already registered.</exception>
        /// <exception cref="ValidationCustomException">Thrown when validation fails.</exception>
        public async Task<Dealer> Execute(Dealer dealer)
        {
            _logger.LogInformation("Starting execution of CreateDealerUseCase for dealer with CNPJ: {CNPJ}", dealer.CNPJ);

            if (_unitOfWork.DealerRepository.CNPJExists(dealer.CNPJ.Replace(".", "").Replace("-", "").Replace("/", "")))
            {
                _logger.LogWarning("CNPJ already registered: {CNPJ}", dealer.CNPJ);
                throw new DuplicateRecordException("CNPJ already registered");
            }

            var validator = new DealerValidator();
            var validationResult = validator.Validate(dealer);
            if (!validationResult.IsValid)
            {
                _logger.LogWarning("Validation failed for dealer: {Errors}", string.Join(", ", validationResult.Errors.Select(e => e.ErrorMessage)));
                throw new ValidationCustomException(string.Join(", ", validationResult.Errors.Select(e => e.ErrorMessage)));
            }

            int defaultContactCount = dealer.ContacstDealer.Count(c => c.ContactDefault);
            if (defaultContactCount > 1)
            {
                _logger.LogWarning("More than one default contact found for dealer with CNPJ: {CNPJ}", dealer.CNPJ);
                throw new ValidationCustomException("There cannot be more than one default contact.");
            }

            foreach (var contact in dealer.ContacstDealer)
            {
                var contactValidator = new DealerContactValidator();
                var contactValidationResult = contactValidator.Validate(contact);
                if (!contactValidationResult.IsValid)
                {
                    _logger.LogWarning("Validation failed for contact: {Errors}", string.Join(", ", contactValidationResult.Errors.Select(e => e.ErrorMessage)));
                    throw new ValidationCustomException(string.Join(", ", contactValidationResult.Errors.Select(e => e.ErrorMessage)));
                }
            }

            dealer.CNPJ = dealer.CNPJ.Replace(".", "").Replace("-", "").Replace("/", "");
            _unitOfWork.DealerRepository.Save(dealer);

            _logger.LogInformation("Dealer created successfully with CNPJ: {CNPJ}", dealer.CNPJ);

            return dealer;
        }
    }
}
