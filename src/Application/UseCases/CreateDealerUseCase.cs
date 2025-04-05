using Application.CustomException;
using Application.Interfaces;
using Domain.Entity;
using Domain.UoW;

namespace Application.UseCases
{
    public class CreateDealerUseCase : ICreateDealer
    {
        private readonly IUnitOfWork _unitOfWork;
        public CreateDealerUseCase(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Dealer> Execute(Dealer dealer)
        {
            if(_unitOfWork.DealerRepository.CNPJExists(dealer.CNPJ.Replace(".", "").Replace("-", "").Replace("/", "")))
            {
                throw new DuplicateRecordException("CNPJ already registered");
            }

            var validator = new DealerValidator();
            var validationResult = validator.Validate(dealer);
            if (!validationResult.IsValid)
            {
                throw new ValidationCustomException(string.Join(", ", validationResult.Errors.Select(e => e.ErrorMessage)));
            }

            dealer.CNPJ = dealer.CNPJ.Replace(".", "").Replace("-", "").Replace("/", "");
            _unitOfWork.DealerRepository.Save(dealer);

            return dealer;
        }
    }
}
