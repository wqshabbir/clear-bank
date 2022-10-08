using System.Collections.Generic;
using ClearBank.DeveloperTest.PaymentSchemesValidators.PaymentSchemesValidators;
using ClearBank.DeveloperTest.Types;
using ClearBank.DeveloperTest.Validators;

namespace ClearBank.DeveloperTest.Factories
{
    public class PaymentSchemeValidationFactory : IPaymentSchemeValidationFactory
    {
        public IDictionary<PaymentScheme, IPaymentSchemeValidator> AvailablePaymentSchemeValidators { get; set; }

        public PaymentSchemeValidationFactory()
        {
            AvailablePaymentSchemeValidators = new Dictionary<PaymentScheme, IPaymentSchemeValidator>
            {
                {PaymentScheme.Bacs, new BacsSchemeValidator()},
                {PaymentScheme.Chaps, new ChapsSchemeValidator()},
                {PaymentScheme.FasterPayments, new FasterPaymentsSchemeValidator()},

            };
        }
        public IPaymentSchemeValidator GetPaymentSchemeValidator(PaymentScheme paymentScheme)
        {
            AvailablePaymentSchemeValidators.TryGetValue(paymentScheme, out IPaymentSchemeValidator paymentSchemeValidator);
            return paymentSchemeValidator;
        }
    }
}
