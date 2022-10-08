using ClearBank.DeveloperTest.Types;
using ClearBank.DeveloperTest.Validators;
using FizzWare.NBuilder;
using Moq;
using Xunit;

namespace ClearBank.DeveloperTest.Tests.Validators
{
    public class PaymentSchemeValidatorBaseTests
    {
        [Fact]
        public void Validate_PaymentSchemeWhereAccountIsNull_ReturnsPaymentSchemeNotValidForAccount()
        {
            //Arrange
            Account account = null;
            MakePaymentRequest makePaymentRequest = Builder<MakePaymentRequest>.CreateNew()
                .With(x => x.PaymentScheme = PaymentScheme.Bacs)
                .Build();
            MakePaymentResult makePaymentResult = new MakePaymentResult();
            var paymentSchemeValidator = new Mock<PaymentSchemeValidatorBase>()
            {
                CallBase = true
            };
            //paymentSchemeValidator.Setup(x => x.IsPaymentSchemeValid(account, makePaymentRequest)).Returns(true);

            //Act
            var result = paymentSchemeValidator.Object.Validate(account, makePaymentRequest);

            //Assert
            Assert.NotNull(result);
            Assert.False(result.Success);
        }

        [Fact]
        public void Validate_PaymentScheme_ReturnsFalse()
        {
            //Arrange
            Account account = null;
            MakePaymentRequest makePaymentRequest = Builder<MakePaymentRequest>.CreateNew()
                .With(x => x.PaymentScheme = PaymentScheme.Bacs)
                .Build();
            MakePaymentResult makePaymentResult = new MakePaymentResult();
            var paymentSchemeValidator = new Mock<PaymentSchemeValidatorBase>()
            {
                CallBase = true
            };
            paymentSchemeValidator.Setup(x => x.IsPaymentSchemeValid(account, makePaymentRequest)).Returns(false);

            //Act
            var result = paymentSchemeValidator.Object.Validate(account, makePaymentRequest);

            //Assert
            Assert.NotNull(result);
            Assert.False(result.Success);
        }

        [Fact]
        public void Validate_PaymentScheme_ReturnsTrue()
        {
            //Arrange
            var account = Builder<Account>.CreateNew().Build();
            MakePaymentRequest makePaymentRequest = Builder<MakePaymentRequest>.CreateNew()
                .With(x => x.PaymentScheme = PaymentScheme.Bacs)
                .Build();
            MakePaymentResult makePaymentResult = new MakePaymentResult();
            var paymentSchemeValidator = new Mock<PaymentSchemeValidatorBase>()
            {
                CallBase = true
            };
            paymentSchemeValidator.Setup(x => x.IsPaymentSchemeValid(account, makePaymentRequest)).Returns(true);

            //Act
            var result = paymentSchemeValidator.Object.Validate(account, makePaymentRequest);

            //Assert
            Assert.NotNull(result);
            Assert.True(result.Success);
        }
    }
}
