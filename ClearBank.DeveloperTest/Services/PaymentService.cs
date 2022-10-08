using ClearBank.DeveloperTest.Factories;
using ClearBank.DeveloperTest.Managers;
using ClearBank.DeveloperTest.Types;

namespace ClearBank.DeveloperTest.Services
{
    public class PaymentService : IPaymentService
    {
        readonly IAccountManager _accountManager;
        readonly IPaymentSchemeValidationFactory _paymentSchemeValidationFactory;
        public PaymentService(IAccountManager accountManager, IPaymentSchemeValidationFactory paymentSchemeValidationFactory)
        {
            _accountManager = accountManager;
            _paymentSchemeValidationFactory = paymentSchemeValidationFactory;
        }
        public MakePaymentResult MakePayment(MakePaymentRequest request)
        {
            MakePaymentResult makePaymentResult = new MakePaymentResult();
            // - Lookup the account the payment is being made from
            Account account = _accountManager.GetByAccountNumber(request.DebtorAccountNumber);
            if (account == null) return makePaymentResult;

            // - Check the account is in a valid state to make the payment
            var paymentSchemeValidator = _paymentSchemeValidationFactory.GetPaymentSchemeValidator(request.PaymentScheme);
            makePaymentResult = paymentSchemeValidator.Validate(account, request);
            if (makePaymentResult == null || !makePaymentResult.Success) return makePaymentResult;

            // Deduct the payment amount from the account's balance and update the account in the database
            _accountManager.DeductPayment(account, request.Amount);
            _accountManager.UpdateAccount(account);

            return makePaymentResult;
        }
    }
}
