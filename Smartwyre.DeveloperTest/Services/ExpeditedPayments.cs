using Smartwyre.DeveloperTest.Types;

namespace Smartwyre.DeveloperTest.Services;

public class ExpeditedPayments : IPaymentScheme
{
    public PaymentScheme Type => PaymentScheme.ExpeditedPayments;

    public bool IsValid(Account account, MakePaymentRequest request)
    {
        return account != null &&
                          account.AllowedPaymentSchemes.HasFlag(AllowedPaymentSchemes.ExpeditedPayments) &&
                          account.Balance >= request.Amount;
    }
}
