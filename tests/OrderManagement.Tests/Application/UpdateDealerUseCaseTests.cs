using System.Linq.Expressions;
using Application.UseCases;
using Domain.Entity;
using Domain.UoW;
using Moq;


namespace OrderManagement.Tests.Application
{
    public class UpdateDealerUseCaseTests
    {
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly UpdateDealerUseCase _updateDealerUseCase;

        public UpdateDealerUseCaseTests()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _updateDealerUseCase = new UpdateDealerUseCase(_unitOfWorkMock.Object);
        }

        [Test]
        public async Task Execute_ShouldUpdateDealer_WhenValidDealer()
        {
            var dealer = new Dealer
            {
                Id = 1,
                Name = "Updated Dealer",
                RazaoSocial = "Updated Razao Social",
                NomeFantasia = "Updated Nome Fantasia",
                Email = "updated@example.com",
                PhonesDealer = new List<DealerPhone>
                    {
                        new DealerPhone { Id = 1, PhoneNumber = "(11) 99999-1234" }
                    },
                ContacstDealer = new List<DealerContact>
                    {
                        new DealerContact { Id = 1, Name = "Updated Contact" }
                    },
                DealerDeliveryAddress = new List<DealerDeliveryAddress>
                    {
                        new DealerDeliveryAddress { Id = 1, Address = "Updated Address" }
                    }
            };

            var existingDealer = new Dealer
            {
                Id = 1,
                Name = "Existing Dealer",
                RazaoSocial = "Existing Razao Social",
                NomeFantasia = "Existing Nome Fantasia",
                Email = "existing@example.com",
                PhonesDealer = new List<DealerPhone>
                    {
                        new DealerPhone { Id = 1, PhoneNumber = "(11) 88888-1234" }
                    },
                ContacstDealer = new List<DealerContact>
                    {
                        new DealerContact { Id = 1, Name = "Existing Contact" }
                    },
                DealerDeliveryAddress = new List<DealerDeliveryAddress>
                    {
                        new DealerDeliveryAddress { Id = 1, Address = "Existing Address" }
                    }
            };

            _unitOfWorkMock.Setup(uow => uow.DealerRepository.GetById(It.IsAny<int>(), It.IsAny<Expression<Func<Dealer, object>>[]>())).Returns(existingDealer);

            var result = await _updateDealerUseCase.Execute(dealer, dealer.Id);

            Assert.NotNull(result);
            Assert.That(result.Name, Is.EqualTo("Updated Dealer"));
            Assert.That(result.RazaoSocial, Is.EqualTo("Updated Razao Social"));
            Assert.That(result.NomeFantasia, Is.EqualTo("Updated Nome Fantasia"));
            Assert.That(result.Email, Is.EqualTo("updated@example.com"));
            _unitOfWorkMock.Verify(uow => uow.DealerRepository.Update(It.IsAny<Dealer>()), Times.Once);
        }

        [Test]
        public void Execute_ShouldThrowException_WhenDealerNotFound()
        {
            var dealer = new Dealer
            {
                Id = 1,
                Name = "Updated Dealer"
            };

            _unitOfWorkMock.Setup(uow => uow.DealerRepository.GetById(It.IsAny<int>(), It.IsAny<Expression<Func<Dealer, object>>[]>())).Returns((Dealer)null);

            Func<Task> act = async () => await _updateDealerUseCase.Execute(dealer, dealer.Id);

            var ex = Assert.ThrowsAsync<Exception>(async () => await act());

            Assert.That(ex.Message, Is.EqualTo("Dealer not found"));
            _unitOfWorkMock.Verify(uow => uow.DealerRepository.Update(It.IsAny<Dealer>()), Times.Never);
        }
    }
}
