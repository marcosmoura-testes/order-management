using System.Linq.Expressions;
using Application.Interfaces;
using Domain.Entity;
using Domain.UoW;

namespace Application.UseCases
{
    public class ReadDealerUseCase : IReadDealer
    {
        private readonly IUnitOfWork _unitOfWork;
        public ReadDealerUseCase(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<List<Dealer>> Execute(int page, int limit)
        {
            Expression<Func<Dealer, object>>[] includes = {
                d => d.ContacstDealer,
                d => d.DealerDeliveryAddress
            };
            return _unitOfWork.DealerRepository.GetAllPaginado(page, limit, includes).ToList();
        }

        public async Task<Dealer> ExecuteById(int id)
        {
            Expression<Func<Dealer, object>>[] includes = {
                d => d.ContacstDealer,
                d => d.DealerDeliveryAddress
            };

            return _unitOfWork.DealerRepository.GetById(id, includes);
        }
    }
}
