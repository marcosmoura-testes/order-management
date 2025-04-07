using System.Linq.Expressions;
using Application.Interfaces;
using Domain.Entity;
using Domain.UoW;
using Microsoft.Extensions.Logging;

namespace Application.UseCases
{
    /// <summary>
    /// Use case for updating a dealer.
    /// </summary>
    public class UpdateDealerUseCase : IUpdateDealer
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<UpdateDealerUseCase> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateDealerUseCase"/> class.
        /// </summary>
        /// <param name="unitOfWork">The unit of work.</param>
        /// <param name="logger">The logger.</param>
        public UpdateDealerUseCase(IUnitOfWork unitOfWork, ILogger<UpdateDealerUseCase> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        /// <summary>
        /// Executes the update dealer use case.
        /// </summary>
        /// <param name="dealer">The dealer entity with updated information.</param>
        /// <param name="dealerId">The ID of the dealer to update.</param>
        /// <returns>The updated dealer entity.</returns>
        public async Task<Dealer> Execute(Dealer dealer, int dealerId)
        {
            _logger.LogInformation("Starting update for dealer with ID {DealerId}", dealerId);

            Expression<Func<Dealer, object>>[] includes = {
                    d => d.ContacstDealer,
                    d => d.DealerDeliveryAddress
                };

            var existingDealer = _unitOfWork.DealerRepository.GetById(dealerId, includes);
            if (existingDealer == null)
            {
                _logger.LogWarning("Dealer with ID {DealerId} not found", dealerId);
                throw new Exception("Dealer not found");
            }

            _logger.LogInformation("Updating dealer information for dealer with ID {DealerId}", dealerId);

            existingDealer.Name = dealer.Name;
            existingDealer.RazaoSocial = dealer.RazaoSocial;
            existingDealer.NomeFantasia = dealer.NomeFantasia;
            existingDealer.Email = dealer.Email;

            foreach (var contact in dealer.ContacstDealer)
            {
                var existingContact = existingDealer.ContacstDealer.FirstOrDefault(c => c.Id == contact.Id);
                if (existingContact != null)
                {
                    existingContact.Name = contact.Name;
                }
                else
                {
                    existingDealer.ContacstDealer.Add(contact);
                }
            }

            foreach (var phone in dealer.PhonesDealer)
            {
                var existingPone = existingDealer.PhonesDealer.FirstOrDefault(c => c.Id == phone.Id);
                if (existingPone != null)
                {
                    existingPone.PhoneNumber = phone.PhoneNumber;
                }
                else
                {
                    existingDealer.PhonesDealer.Add(phone);
                }
            }

            existingDealer.ContacstDealer.RemoveAll(c => !dealer.ContacstDealer.Any(dc => dc.Id == c.Id));

            foreach (var address in dealer.DealerDeliveryAddress)
            {
                var existingAddress = existingDealer.DealerDeliveryAddress.FirstOrDefault(a => a.Id == address.Id);
                if (existingAddress != null)
                {
                    existingAddress.Address = address.Address;
                }
                else
                {
                    existingDealer.DealerDeliveryAddress.Add(address);
                }
            }

            existingDealer.DealerDeliveryAddress.RemoveAll(a => !dealer.DealerDeliveryAddress.Any(da => da.Id == a.Id));

            _unitOfWork.DealerRepository.Update(existingDealer);

            _logger.LogInformation("Successfully updated dealer with ID {DealerId}", dealerId);

            return existingDealer;
        }
    }
}
