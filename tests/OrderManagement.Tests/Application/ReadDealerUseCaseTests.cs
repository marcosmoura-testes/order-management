using System.Linq.Expressions;
using Application.UseCases;
using Domain.Entity;
using Domain.UoW;
using Moq;

namespace OrderManagement.Tests.Application
{
    public class ReadDealerUseCaseTests
    {
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly ReadDealerUseCase _readDealerUseCase;

        public ReadDealerUseCaseTests()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _readDealerUseCase = new ReadDealerUseCase(_unitOfWorkMock.Object);
        }

        [Test]
        public async Task Execute_ShouldReturnDealer_WhenDealerExists()
        {
            var dealer = new Dealer
            {
                CNPJ = "12345678000195",
                RazaoSocial = "Distribuidora São João Ltda",
                NomeFantasia = "São João Bebidas",
                Email = "contato@saojoaobebidas.com",
                PhonesDealer = new List<DealerPhone>
                        {
                            new DealerPhone
                            {
                                PhoneNumber = "(11) 99999-1234"
                            }
                        },
                ContacstDealer = new List<DealerContact>
                        {
                            new DealerContact
                            {
                                Name = "Carlos Silva",
                                ContactDefault = true
                            }
                        },
                DealerDeliveryAddress = new List<DealerDeliveryAddress>
                        {
                            new DealerDeliveryAddress
                            {
                                Address = "Rua das Laranjeiras, 456 - São Paulo/SP"
                            }
                        }
            };

            List<Dealer> dealers = new List<Dealer>
                {
                    dealer
                };

            _unitOfWorkMock.Setup(uow => uow.DealerRepository.GetAllPaginado(
                It.IsAny<int>(),
                It.IsAny<int>(),
                It.IsAny<Expression<Func<Dealer, object>>[]>()
            )).Returns(dealers);

            var result = await _readDealerUseCase.Execute(1, 100);

            Assert.NotNull(result);
            _unitOfWorkMock.Verify(uow => uow.DealerRepository.GetAllPaginado(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<Expression<Func<Dealer, object>>[]>()), Times.Once);
        }
    }
}
