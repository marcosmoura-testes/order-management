using Application.UseCases;
using Domain.Entity;
using Domain.UoW;
using Moq;

namespace OrderManagement.Tests.Application
{
    public class DeleteDealerUseCaseTests
    {
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly DeleteDealerUseCase _deleteDealerUseCase;

        public DeleteDealerUseCaseTests()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _deleteDealerUseCase = new DeleteDealerUseCase(_unitOfWorkMock.Object);
        }

        [Test]
        public async Task Execute_ShouldDeleteDealer_WhenDealerExists()
        {
            var dealer = new Dealer { Id = 1, Name = "Test Dealer" };

            _unitOfWorkMock.Setup(uow => uow.DealerRepository.GetById(It.IsAny<int>())).Returns(dealer);
            _unitOfWorkMock.Setup(uow => uow.DealerRepository.Delete(It.IsAny<int>()));

            var result = await _deleteDealerUseCase.Execute(dealer.Id);

            Assert.IsTrue(result);
            _unitOfWorkMock.Verify(uow => uow.DealerRepository.Delete(It.IsAny<int>()), Times.Once);
        }

        [Test]
        public void Execute_ShouldThrowException_WhenDealerNotFound()
        {
            _unitOfWorkMock.Setup(uow => uow.DealerRepository.GetById(It.IsAny<int>())).Returns((Dealer)null);

            Func<Task> act = async () => await _deleteDealerUseCase.Execute(1);

            var ex = Assert.ThrowsAsync<Exception>(async () => await act());

            Assert.That(ex.Message, Is.EqualTo("Dealer not found."));
        }

    }
}
