using System.Linq.Expressions;
using Application.Interfaces;
using Domain.Entity;
using Domain.UoW;

namespace Application.UseCases
{
    public class UpdateDealerUseCase : IUpdateDealer
    {
        private readonly IUnitOfWork _unitOfWork;
        public UpdateDealerUseCase(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Dealer> Execute(Dealer dealer, int dealerId)
        {
            Expression<Func<Dealer, object>>[] includes = {
                d => d.ContacstDealer,
                d => d.DealerDeliveryAddress
            };

            var existingDealer = _unitOfWork.DealerRepository.GetById(dealerId, includes);
            if (existingDealer == null)
            {
                throw new Exception("Dealer not found");
            }

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

            return existingDealer;
        }
    }
}
