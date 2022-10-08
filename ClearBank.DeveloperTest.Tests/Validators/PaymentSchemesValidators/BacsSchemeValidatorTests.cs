using ClearBank.DeveloperTest.PaymentSchemesValidators.PaymentSchemesValidators;
using ClearBank.DeveloperTest.Types;
using ClearBank.DeveloperTest.Validators;
using Xunit;

namespace ClearBank.DeveloperTest.Tests.Validators.PaymentSchemesValidators
{
    public class BacsSchemeValidatorTests
    {
        private readonly Account _account;
        private readonly MakePaymentRequest _makePaymentRequest;
        private readonly IPaymentSchemeValidator _bacsValidator;

        public BacsSchemeValidatorTests()
        {
            _account = new Account();
            _makePaymentRequest = new MakePaymentRequest();
            _bacsValidator = new BacsSchemeValidator();
        }

        [Fact]
        public void Validate_AllowedPaymentSchemeWhenBacs_ReturnsTrue()
        {
            //Arrange
            _account.AllowedPaymentSchemes = AllowedPaymentSchemes.Bacs;

            var result = _bacsValidator.Validate(_account, _makePaymentRequest);

            Assert.True(result.Success);
        }

        [Fact]
        public void Validate_AllowedPaymentSchemeWhenChaps_ReturnsFalse()
        {
            //Arrange
            _account.AllowedPaymentSchemes = AllowedPaymentSchemes.Chaps;

            var result = _bacsValidator.Validate(_account, _makePaymentRequest);

            Assert.False(result.Success);
        }

        [Fact]
        public void Validate_AllowedPaymentSchemeWhenFasterPayments_ReturnsFalse()
        {
            //Arrange
            _account.AllowedPaymentSchemes = AllowedPaymentSchemes.FasterPayments;

            var result = _bacsValidator.Validate(_account, _makePaymentRequest);

            Assert.False(result.Success);
        }
    }
}
