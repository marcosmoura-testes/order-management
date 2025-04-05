using Application.Interfaces;
using Domain.Entity;
using Domain.UoW;

namespace Application.UseCases
{
    public class DeleteDealerUseCase : IDeleteDealer
    {
        private readonly IUnitOfWork _unitOfWork;
        public DeleteDealerUseCase(IUnitOfWork unitOfWork  )
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<bool> Execute(int id)
        {
            try
            {
                Dealer dealer = _unitOfWork.DealerRepository.GetById(id);

                if (dealer == null)                
                    throw new Exception("Dealer not found.");
                
                _unitOfWork.DealerRepository.Delete(id);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while deleting the dealer.", ex);
            }
        }
    }
}
