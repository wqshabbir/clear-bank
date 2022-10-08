using ClearBank.DeveloperTest.Types;
using ClearBank.DeveloperTest.Validators;

namespace ClearBank.DeveloperTest.Factories
{
    public interface IPaymentSchemeValidationFactory
    {
        IPaymentSchemeValidator GetPaymentSchemeValidator(PaymentScheme paymentScheme);
    }
}
