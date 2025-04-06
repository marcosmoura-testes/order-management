using Domain.Entity;

namespace OrderManagement.Tests.Entity
{
    public class ClientOrderTests
    {
        private readonly ClientOrderValidator _validator;

        public ClientOrderTests()
        {
            _validator = new ClientOrderValidator();
        }

        [Test]
        public void Should_Not_Have_Error_When_ClientOrder_Data_Is_Valid()
        {
            var clientOrder = new ClientOrder
            {
                ClientCNPJ = "12345678000195",
                DealerId = 1,
                StatusId = 1,
                TotalAmount = 100.50m,
                CLientOrderProducts = new List<ClientOrderProduct>
                    {
                        new ClientOrderProduct
                        {
                            ProductId = 1,
                            Quantity = 10,
                            UnitPrice = 10.05m,
                            TotalAmount = 100.50m
                        }
                    }
            };
            var result = _validator.Validate(clientOrder);
            Assert.IsTrue(result.IsValid);
        }

        [Test]
        public void Should_Have_Error_When_ClientCNPJ_Is_Invalid()
        {
            var clientOrder = new ClientOrder { ClientCNPJ = "12345678901234" };
            var result = _validator.Validate(clientOrder);
            Assert.IsFalse(result.IsValid);
            Assert.That(result.Errors.Any(e => e.PropertyName == "ClientCNPJ"), Is.True);
        }

        [Test]
        public void Should_Have_Error_When_DealerId_Is_Invalid()
        {
            var clientOrder = new ClientOrder { DealerId = 0 };
            var result = _validator.Validate(clientOrder);
            Assert.IsFalse(result.IsValid);
            Assert.That(result.Errors.Any(e => e.PropertyName == "DealerId"), Is.True);
        }

        [Test]
        public void Should_Have_Error_When_CLientOrderProducts_Is_Empty()
        {
            var clientOrder = new ClientOrder { ClientCNPJ = "12345678000195", DealerId = 1, CLientOrderProducts = new List<ClientOrderProduct>() };
            var result = _validator.Validate(clientOrder);
            Assert.IsFalse(result.IsValid);
            Assert.That(result.Errors.Any(e => e.PropertyName == "CLientOrderProducts"), Is.True);
        }
    }
}
