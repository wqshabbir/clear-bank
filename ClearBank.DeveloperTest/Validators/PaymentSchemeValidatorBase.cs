using ClearBank.DeveloperTest.Types;

namespace ClearBank.DeveloperTest.Validators
{
    public abstract class PaymentSchemeValidatorBase : IPaymentSchemeValidator
    {
        public MakePaymentResult Validate(Account account, MakePaymentRequest paymentRequest)
        {
            var makePaymentResult = new MakePaymentResult();
            if (account != null)
            {
                makePaymentResult.Success = IsPaymentSchemeValid(account, paymentRequest);
            }
            return makePaymentResult;
        }
        public abstract bool IsPaymentSchemeValid(Account account, MakePaymentRequest paymentRequest);
    }
}
