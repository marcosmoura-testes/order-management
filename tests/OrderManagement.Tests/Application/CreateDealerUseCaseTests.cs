using Application.CustomException;
using Application.UseCases;
using Domain.Entity;
using Domain.UoW;
using Moq;

namespace OrderManagement.Tests.Application
{
    public class CreateDealerUseCaseTests
    {
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly CreateDealerUseCase _createDealerUseCase;

        public CreateDealerUseCaseTests()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _createDealerUseCase = new CreateDealerUseCase(_unitOfWorkMock.Object);
        }

        [Test]
        public async Task Execute_ShouldCreateDealer_WhenValidDealer()
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

            _unitOfWorkMock.Setup(uow => uow.DealerRepository.CNPJExists(It.IsAny<string>())).Returns(false);
            _unitOfWorkMock.Setup(uow => uow.DealerRepository.Save(It.IsAny<Dealer>())).Returns(dealer);

            var result = await _createDealerUseCase.Execute(dealer);

            Assert.NotNull(result);
            Assert.That(result.CNPJ,Is.EqualTo("12345678000195"));
            Assert.That(result.RazaoSocial, Is.EqualTo("Distribuidora São João Ltda"));
            _unitOfWorkMock.Verify(uow => uow.DealerRepository.Save(It.IsAny<Dealer>()), Times.Once);
        }

        [Test]
        public async Task Execute_ShouldThrowDuplicateRecordException_WhenCNPJExists()
        {
            var dealer = new Dealer
            {
                CNPJ = "12.345.678/0001-90",
                Name = "Test Dealer",
                ContacstDealer = new List<DealerContact>
                    {
                        new DealerContact { Name = "Contact 1", ContactDefault = true }
                    }
            };

            _unitOfWorkMock.Setup(uow => uow.DealerRepository.CNPJExists(It.IsAny<string>())).Returns(true);

            Func<Task> act = async () => await _createDealerUseCase.Execute(dealer);

            var ex = Assert.ThrowsAsync<DuplicateRecordException>(async () => await act());

            Assert.That(ex.Message, Is.EqualTo("CNPJ already registered"));
            _unitOfWorkMock.Verify(uow => uow.DealerRepository.Save(It.IsAny<Dealer>()), Times.Never);
        }

        [Test]
        public async Task Execute_ShouldThrowValidationCustomException_WhenInvalidDealer()
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
                        new DealerContact { Name = "Contact 1", ContactDefault = true },
                        new DealerContact { Name = "Contact 2", ContactDefault = true }
                    },
                DealerDeliveryAddress = new List<DealerDeliveryAddress>
                        {
                            new DealerDeliveryAddress
                            {
                                Address = "Rua das Laranjeiras, 456 - São Paulo/SP"
                            }
                        }
            };

            _unitOfWorkMock.Setup(uow => uow.DealerRepository.CNPJExists(It.IsAny<string>())).Returns(false);

            Func<Task> act = async () => await _createDealerUseCase.Execute(dealer);

            var ex = Assert.ThrowsAsync<ValidationCustomException>(async () => await act());

            Assert.That(ex.Message, Is.EqualTo("There cannot be more than one default contact."));
            _unitOfWorkMock.Verify(uow => uow.DealerRepository.Save(It.IsAny<Dealer>()), Times.Never);
        }
    }
}
