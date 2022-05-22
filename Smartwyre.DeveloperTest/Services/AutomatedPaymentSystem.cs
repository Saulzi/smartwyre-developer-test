using Smartwyre.DeveloperTest.Types;

namespace Smartwyre.DeveloperTest.Services;

public class AutomatedPaymentSystem : PaymentScheme
{
    public override PaymentSchemeType Type => PaymentSchemeType.AutomatedPaymentSystem;

    public override bool IsPaymentAllowed(Account account, MakePaymentRequest _) => account != null && 
                                                                           account.AllowedPaymentSchemes.HasFlag(AllowedPaymentSchemes.AutomatedPaymentSystem) &&
                                                                           account.Status == AccountStatus.Live;
}