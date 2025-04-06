using Domain.Entity;

namespace OrderManagement.Tests.Entity
{
    public class DealerTests
    {
        private readonly DealerValidator _validator;

        public DealerTests()
        {
            _validator = new DealerValidator();
        }

        [Test]
        public void Should_Not_Have_Error_When_Dealer_Data_Is_Valid()
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
            var result = _validator.Validate(dealer);
            Assert.IsTrue(result.IsValid);
        }

        [Test]
        public void Should_Have_Error_When_CNPJ_Is_Invalid()
        {
            var dealer = new Dealer { CNPJ = "12345678901234" };
            var result = _validator.Validate(dealer);
            Assert.IsFalse(result.IsValid);
            Assert.That(result.Errors.Any(e => e.PropertyName == "CNPJ"), Is.True);
        }

        [Test]
        public void Should_Have_Error_When_RazaoSocial_Is_Invalid()
        {
            var dealer = new Dealer
            {
                CNPJ = "12345678000195",
                RazaoSocial = "123 ",
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
            var result = _validator.Validate(dealer);
            Assert.IsFalse(result.IsValid);
            Assert.That(result.Errors.Any(e => e.PropertyName == "RazaoSocial"), Is.True);
        }

        [Test]
        public void Should_Have_Error_When_NomeFantasia_Is_Invalid()
        {
            var dealer = new Dealer { NomeFantasia = "123" };
            var result = _validator.Validate(dealer);
            Assert.IsFalse(result.IsValid);
            Assert.That(result.Errors.Any(e => e.PropertyName == "NomeFantasia"), Is.True);
        }

        [Test]
        public void Should_Have_Error_When_Email_Is_Invalid()
        {
            var dealer = new Dealer { Email = "invalid-email" };
            var result = _validator.Validate(dealer);
            Assert.IsFalse(result.IsValid);
            Assert.That(result.Errors.Any(e => e.PropertyName == "Email"), Is.True);
        }

        [Test]
        public void Should_Have_Error_When_ContacstDealer_Is_Invalid()
        {
            var dealer = new Dealer { ContacstDealer = new List<DealerContact>() };
            var result = _validator.Validate(dealer);
            Assert.IsFalse(result.IsValid);
            Assert.That(result.Errors.Any(e => e.PropertyName == "ContacstDealer"), Is.True);
        }

        [Test]
        public void Should_Have_Error_When_DealerDeliveryAddress_Is_Invalid()
        {
            var dealer = new Dealer { DealerDeliveryAddress = new List<DealerDeliveryAddress>() };
            var result = _validator.Validate(dealer);
            Assert.IsFalse(result.IsValid);
            Assert.That(result.Errors.Any(e => e.PropertyName == "DealerDeliveryAddress"), Is.True);
        }
    }
}
