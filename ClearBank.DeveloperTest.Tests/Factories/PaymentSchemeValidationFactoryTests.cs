using ClearBank.DeveloperTest.Factories;
using ClearBank.DeveloperTest.PaymentSchemesValidators.PaymentSchemesValidators;
using Xunit;

namespace ClearBank.DeveloperTest.Tests.Factories
{
    public class PaymentSchemeValidationFactoryTests
    {
        private readonly IPaymentSchemeValidationFactory _paymentSchemeValidationFactory;

        public PaymentSchemeValidationFactoryTests()
        {
            _paymentSchemeValidationFactory = new PaymentSchemeValidationFactory();
        }

        [Fact]
        public void GetPaymentSchemeValidator_WithPaymentSchemeBaps_ReturnsBacsSchemeValidator()
        {
            // Act
            var paymentSchemeValidator = _paymentSchemeValidationFactory.GetPaymentSchemeValidator(Types.PaymentScheme.Bacs);

            // Assert
            Assert.IsType<BacsSchemeValidator>(paymentSchemeValidator);
        }

        [Fact]
        public void GetPaymentSchemeValidator_WithPaymentSchemeBaps_ReturnsChapsSchemeValidator()
        {
            // Act
            var paymentSchemeValidator = _paymentSchemeValidationFactory.GetPaymentSchemeValidator(Types.PaymentScheme.Chaps);

            // Assert
            Assert.IsType<ChapsSchemeValidator>(paymentSchemeValidator);
        }

        [Fact]
        public void GetPaymentSchemeValidator_WithPaymentSchemeBaps_ReturnsFasterPaymentsSchemeValidator()
        {
            // Act
            var paymentSchemeValidator = _paymentSchemeValidationFactory.GetPaymentSchemeValidator(Types.PaymentScheme.FasterPayments);

            // Assert
            Assert.IsType<FasterPaymentsSchemeValidator>(paymentSchemeValidator);
        }
    }
}
