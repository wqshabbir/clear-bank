using ClearBank.DeveloperTest.PaymentSchemesValidators.PaymentSchemesValidators;
using ClearBank.DeveloperTest.Types;
using ClearBank.DeveloperTest.Validators;
using Xunit;

namespace ClearBank.DeveloperTest.Tests.Validators.PaymentSchemesValidators
{
    public class ChapsSchemeValidatorTests
    {
        private readonly Account _account;
        private readonly MakePaymentRequest _makePaymentRequest;
        private readonly IPaymentSchemeValidator _chapsSchemeValidator;

        public ChapsSchemeValidatorTests()
        {
            _account = new Account();
            _makePaymentRequest = new MakePaymentRequest();
            _chapsSchemeValidator = new ChapsSchemeValidator();
        }

        [Fact]
        public void Validate_AllowedPaymentSchemeWhenBacs_ReturnsFalse()
        {
            //Arrange
            _account.AllowedPaymentSchemes = AllowedPaymentSchemes.Bacs;

            var result = _chapsSchemeValidator.Validate(_account, _makePaymentRequest);

            Assert.False(result.Success);
        }

        [Fact]
        public void Validate_AllowedPaymentSchemeWhenFasterPayments_ReturnsFalse()
        {
            //Arrange
            _account.AllowedPaymentSchemes = AllowedPaymentSchemes.FasterPayments;

            var result = _chapsSchemeValidator.Validate(_account, _makePaymentRequest);

            Assert.False(result.Success);
        }

        [Fact]
        public void Validate_AllowedPaymentSchemeWhenChapsAndAccountStatusLive_ReturnsTrue()
        {
            //Arrange
            _account.AllowedPaymentSchemes = AllowedPaymentSchemes.Chaps;
            _account.Status = AccountStatus.Live;

            var result = _chapsSchemeValidator.Validate(_account, _makePaymentRequest);

            Assert.True(result.Success);
        }

        [Fact]
        public void Validate_AllowedPaymentSchemeWhenChapsAndAccountStatusNotLive_ReturnsFalse()
        {
            //Arrange
            _account.AllowedPaymentSchemes = AllowedPaymentSchemes.Chaps;
            _account.Status = AccountStatus.Disabled;

            var result = _chapsSchemeValidator.Validate(_account, _makePaymentRequest);

            Assert.False(result.Success);
        }
    }
}
