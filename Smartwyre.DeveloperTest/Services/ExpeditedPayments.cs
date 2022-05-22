using Smartwyre.DeveloperTest.Types;

namespace Smartwyre.DeveloperTest.Services;

public class ExpeditedPayments : PaymentScheme
{
    public override PaymentSchemeType Type => PaymentSchemeType.ExpeditedPayments;

    public override bool IsPaymentAllowed(Account account, MakePaymentRequest request)
    {
        return account != null &&
                          account.AllowedPaymentSchemes.HasFlag(AllowedPaymentSchemes.ExpeditedPayments) &&
                          account.Balance >= request.Amount;
    }
}
