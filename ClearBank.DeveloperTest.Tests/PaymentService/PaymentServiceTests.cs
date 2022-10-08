using ClearBank.DeveloperTest.Factories;
using ClearBank.DeveloperTest.Managers;
using ClearBank.DeveloperTest.Services;
using ClearBank.DeveloperTest.Types;
using ClearBank.DeveloperTest.Validators;
using FizzWare.NBuilder;
using Moq;
using Xunit;

namespace ClearBank.DeveloperTest.Tests
{
    public class PaymentServiceTests
    {
        readonly Mock<IAccountManager> _mockAccountManager;
        readonly Mock<IPaymentSchemeValidationFactory> _mockPaymentSchemeValidationFactory;

        readonly IPaymentService _paymentService;
        public PaymentServiceTests()
        {
            _mockAccountManager = new Mock<IAccountManager>();
            _mockPaymentSchemeValidationFactory = new Mock<IPaymentSchemeValidationFactory>();
            _paymentService = new PaymentService(_mockAccountManager.Object, _mockPaymentSchemeValidationFactory.Object);
        }

        [Fact]
        public void MakePayment_WhenAccountManagerGetByAccountNumberReturnsNull_ThenMakePaymentResultIsNotSuccess()
        {
            // Arrange
            Account account = null;
            string accountNumber = "123456";
            MakePaymentRequest makePaymentRequest = Builder<MakePaymentRequest>.CreateNew()
               .With(x => x.PaymentScheme = PaymentScheme.Bacs)
               .With(x => x.DebtorAccountNumber = accountNumber)
               .Build();
            _mockAccountManager.Setup(x => x.GetByAccountNumber(accountNumber)).Returns(account);

            //Act
            var result = _paymentService.MakePayment(makePaymentRequest);

            //Assert
            _mockAccountManager.Verify(x => x.GetByAccountNumber(accountNumber), Times.Once);
            Assert.NotNull(result);
            Assert.False(result.Success);
        }

        [Fact]
        public void MakePayment_WhenAnyPaymentSchemeValidateReturnsResultIsNotSuccess_ThenMakePaymentResultIsNotSuccess()
        {
            //Arrange
            string accountNumber = "123456";
            var account = Builder<Account>.CreateNew().With(account => account.AccountNumber = accountNumber).Build();
            MakePaymentRequest makePaymentRequest = Builder<MakePaymentRequest>.CreateNew()
                .With(x => x.PaymentScheme = PaymentScheme.Bacs)
                .With(x => x.DebtorAccountNumber = accountNumber)
                .Build();

            MakePaymentResult makePaymentResult = new MakePaymentResult();
            var paymentchemeValidator = new Mock<IPaymentSchemeValidator>();
            paymentchemeValidator.Setup(x => x.Validate(account, makePaymentRequest)).Returns(makePaymentResult);
            _mockAccountManager.Setup(x => x.GetByAccountNumber(accountNumber)).Returns(account);
            _mockPaymentSchemeValidationFactory.Setup(x => x.GetPaymentSchemeValidator(PaymentScheme.Bacs)).Returns(paymentchemeValidator.Object);
            //Act
            var result = _paymentService.MakePayment(makePaymentRequest);

            //Assert
            _mockAccountManager.Verify(x => x.GetByAccountNumber(accountNumber), Times.Once);
            _mockPaymentSchemeValidationFactory.Verify(x => x.GetPaymentSchemeValidator(makePaymentRequest.PaymentScheme), Times.Once);
            Assert.NotNull(result);
            Assert.False(result.Success);
        }

        [Fact]
        public void MakePayment_WhenAnyPaymentSchemeValidateReturnsSuccessResult_AndAccountIsUpdated_ThenMakePaymentResultIsSuccess()
        {
            //Arrange
            string accountNumber = "123456";
            var account = Builder<Account>.CreateNew().With(account => account.AccountNumber = accountNumber).Build();
            MakePaymentRequest makePaymentRequest = Builder<MakePaymentRequest>.CreateNew()
                .With(x => x.PaymentScheme = PaymentScheme.Bacs)
                .With(x => x.DebtorAccountNumber = accountNumber)
                .Build();

            MakePaymentResult makePaymentResult = new MakePaymentResult() { Success = true };
            var paymentchemeValidator = new Mock<IPaymentSchemeValidator>();
            paymentchemeValidator.Setup(x => x.Validate(account, makePaymentRequest)).Returns(makePaymentResult);
            _mockAccountManager.Setup(x => x.GetByAccountNumber(accountNumber)).Returns(account);
            _mockAccountManager.Setup(x => x.DeductPayment(account, It.IsAny<decimal>()));
            _mockAccountManager.Setup(x => x.UpdateAccount(account));
            _mockPaymentSchemeValidationFactory.Setup(x => x.GetPaymentSchemeValidator(PaymentScheme.Bacs)).Returns(paymentchemeValidator.Object);
            //Act
            var result = _paymentService.MakePayment(makePaymentRequest);

            //Assert
            _mockAccountManager.Verify(x => x.GetByAccountNumber(accountNumber), Times.Once);
            _mockPaymentSchemeValidationFactory.Verify(x => x.GetPaymentSchemeValidator(makePaymentRequest.PaymentScheme), Times.Once);
            Assert.NotNull(result);
            Assert.True(result.Success);
        }
    }
}
