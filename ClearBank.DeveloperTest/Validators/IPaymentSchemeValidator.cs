using ClearBank.DeveloperTest.Types;

namespace ClearBank.DeveloperTest.Validators
{
    public interface IPaymentSchemeValidator
    {
        MakePaymentResult Validate(Account account, MakePaymentRequest paymentRequest);
    }
}
