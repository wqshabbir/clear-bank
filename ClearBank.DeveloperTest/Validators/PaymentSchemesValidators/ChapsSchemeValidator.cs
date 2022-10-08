using ClearBank.DeveloperTest.Types;
using ClearBank.DeveloperTest.Validators;

namespace ClearBank.DeveloperTest.PaymentSchemesValidators.PaymentSchemesValidators
{
    public class ChapsSchemeValidator : PaymentSchemeValidatorBase
    {
        public override bool IsPaymentSchemeValid(Account account, MakePaymentRequest paymentRequest)
        {
            return account.AllowedPaymentSchemes.HasFlag(AllowedPaymentSchemes.Chaps) && account.Status == AccountStatus.Live;
        }
    }
}
