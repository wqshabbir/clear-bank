using ClearBank.DeveloperTest.PaymentSchemesValidators.PaymentSchemesValidators;
using ClearBank.DeveloperTest.Types;
using ClearBank.DeveloperTest.Validators;
using Xunit;
namespace ClearBank.DeveloperTest.Tests.Validators.PaymentSchemesValidators
{
    public class FasterPaymentsSchemeValidatorTests
    {
        private readonly Account _account;
        private readonly MakePaymentRequest _makePaymentRequest;
        private readonly IPaymentSchemeValidator _fasterPaymentsSchemeValidator;

        public FasterPaymentsSchemeValidatorTests()
        {
            _account = new Account();
            _makePaymentRequest = new MakePaymentRequest();
            _fasterPaymentsSchemeValidator = new FasterPaymentsSchemeValidator();
        }
        [Fact]
        public void Validate_AllowedPaymentSchemeWhenBacs_ReturnsFalse()
        {
            //Arrange
            _account.AllowedPaymentSchemes = AllowedPaymentSchemes.Bacs;

            var result = _fasterPaymentsSchemeValidator.Validate(_account, _makePaymentRequest);

            Assert.False(result.Success);
        }

        [Fact]
        public void Validate_AllowedPaymentSchemeWhenChaps_ReturnsFalse()
        {
            //Arrange
            _account.AllowedPaymentSchemes = AllowedPaymentSchemes.Chaps;

            var result = _fasterPaymentsSchemeValidator.Validate(_account, _makePaymentRequest);

            Assert.False(result.Success);
        }

        [Fact]
        public void Validate_AllowedPaymentSchemeWhenFasterPaymentsAndAccountBalanceGreaterThanPaymentRequestAmount_ReturnsTrue()
        {
            //Arrange
            _account.AllowedPaymentSchemes = AllowedPaymentSchemes.FasterPayments;
            _account.Balance = 30m;

            _makePaymentRequest.Amount = 20.1m;

            var result = _fasterPaymentsSchemeValidator.Validate(_account, _makePaymentRequest);

            Assert.True(result.Success);
        }

        [Fact]
        public void Validate_AllowedPaymentSchemeWhenFasterPaymentsAndAccountBalanceSameAsPaymentRequestAmount_ReturnsTrue()
        {
            //Arrange
            _account.AllowedPaymentSchemes = AllowedPaymentSchemes.FasterPayments;
            _account.Balance = 30m;

            _makePaymentRequest.Amount = 30m;

            var result = _fasterPaymentsSchemeValidator.Validate(_account, _makePaymentRequest);

            Assert.True(result.Success);
        }

        [Fact]
        public void Validate_AllowedPaymentSchemeWhenFasterPaymentsAndAccountBalanceLesserThanPaymentRequest_ReturnsFalse()
        {
            //Arrange
            _account.AllowedPaymentSchemes = AllowedPaymentSchemes.FasterPayments;
            _account.Balance = 30m;

            _makePaymentRequest.Amount = 30.1m;

            var result = _fasterPaymentsSchemeValidator.Validate(_account, _makePaymentRequest);

            Assert.False(result.Success);
        }
    }
}
