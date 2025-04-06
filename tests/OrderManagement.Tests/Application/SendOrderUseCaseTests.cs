using Application.CustomException;
using Application.UseCases;
using Domain.DTO;
using Domain.Entity;
using Domain.UoW;
using Moq;

namespace OrderManagement.Tests.Application
{
    public class SendOrderUseCaseTests
    {
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly SendOrderUseCase _sendOrderUseCase;

        public SendOrderUseCaseTests()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _sendOrderUseCase = new SendOrderUseCase(_unitOfWorkMock.Object);
        }

        [Test]
        public async Task Execute_ShouldSendOrder_WhenValidOrder()
        {
            var orderDTO = new ClientOrderDTO
            {
                ClientCNPJ = "12345678000195",
                DealerCNPJ = "98765432000195",
                ClientOrderProducts = new List<ClientOrderProductDTO>
                    {
                        new ClientOrderProductDTO { SKU = "PROD1", Quantity = 2 }
                    }
            };

            var dealer = new Dealer { Id = 1, CNPJ = "98765432000195" };
            var product = new Product { Id = 1, SKU = "PROD1", Price = 10.0m };
            var clientOrder = new ClientOrder
            {
                Id = 1,
                ClientCNPJ = "12345678000195",
                DealerId = 1,
                StatusId = 1,
                TotalAmount = 20.0m,
                CLientOrderProducts = new List<ClientOrderProduct>
                    {
                        new ClientOrderProduct { ProductId = 1, Quantity = 2, UnitPrice = 10.0m, TotalAmount = 20.0m }
                    }
            };

            _unitOfWorkMock.Setup(uow => uow.DealerRepository.GetByCNPJ(orderDTO.DealerCNPJ)).Returns(dealer);
            _unitOfWorkMock.Setup(uow => uow.ProductRepository.GetProductBySKU("PROD1")).ReturnsAsync(product);
            _unitOfWorkMock.Setup(uow => uow.ClientOrderRepository.Save(It.IsAny<ClientOrder>())).Returns(clientOrder);

            var result = await _sendOrderUseCase.Execute(orderDTO);

            Assert.NotNull(result);
            Assert.That(result.ClientCNPJ, Is.EqualTo("12345678000195"));
            Assert.That(result.TotalAmount, Is.EqualTo(20.0m));
            _unitOfWorkMock.Verify(uow => uow.ClientOrderRepository.Save(It.IsAny<ClientOrder>()), Times.Once);
        }

        [Test]
        public void Execute_ShouldThrowException_WhenDealerNotFound()
        {
            var orderDTO = new ClientOrderDTO
            {
                ClientCNPJ = "12345678000195",
                DealerCNPJ = "98765432000195",
                ClientOrderProducts = new List<ClientOrderProductDTO>
                    {
                        new ClientOrderProductDTO { SKU = "PROD1", Quantity = 2 }
                    }
            };

            _unitOfWorkMock.Setup(uow => uow.DealerRepository.GetByCNPJ(orderDTO.DealerCNPJ)).Returns((Dealer)null);

            Func<Task> act = async () => await _sendOrderUseCase.Execute(orderDTO);

            var ex = Assert.ThrowsAsync<Exception>(async () => await act());

            Assert.That(ex.Message, Is.EqualTo("Revendedor não encontrado"));
        }

        [Test]
        public void Execute_ShouldThrowBadRequestCustomException_WhenNoProducts()
        {
            var orderDTO = new ClientOrderDTO
            {
                ClientCNPJ = "12345678000195",
                DealerCNPJ = "98765432000195",
                ClientOrderProducts = new List<ClientOrderProductDTO>()
            };

            var dealer = new Dealer { Id = 1, CNPJ = "98765432000195" };

            _unitOfWorkMock.Setup(uow => uow.DealerRepository.GetByCNPJ(orderDTO.DealerCNPJ)).Returns(dealer);

            Func<Task> act = async () => await _sendOrderUseCase.Execute(orderDTO);

            var ex = Assert.ThrowsAsync<BadRequestCustomException>(async () => await act());

            Assert.That(ex.Message, Is.EqualTo("Não foram enviados os produtos"));
        }

        [Test]
        public void Execute_ShouldThrowBadRequestCustomException_WhenProductNotFound()
        {
            var orderDTO = new ClientOrderDTO
            {
                ClientCNPJ = "12345678000195",
                DealerCNPJ = "98765432000195",
                ClientOrderProducts = new List<ClientOrderProductDTO>
                    {
                        new ClientOrderProductDTO { SKU = "PROD1", Quantity = 2 }
                    }
            };

            var dealer = new Dealer { Id = 1, CNPJ = "98765432000195" };

            _unitOfWorkMock.Setup(uow => uow.DealerRepository.GetByCNPJ(orderDTO.DealerCNPJ)).Returns(dealer);
            _unitOfWorkMock.Setup(uow => uow.ProductRepository.GetProductBySKU("PROD1")).ReturnsAsync((Product)null);

            Func<Task> act = async () => await _sendOrderUseCase.Execute(orderDTO);

            var ex = Assert.ThrowsAsync<BadRequestCustomException>(async () => await act());

            Assert.That(ex.Message, Is.EqualTo("Produto [PROD1] não encontrado"));
        }
    }
}
