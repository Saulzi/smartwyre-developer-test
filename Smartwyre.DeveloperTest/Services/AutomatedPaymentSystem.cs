using Smartwyre.DeveloperTest.Types;

namespace Smartwyre.DeveloperTest.Services;

public class AutomatedPaymentSystem : IPaymentScheme
{
    public PaymentScheme Type => PaymentScheme.AutomatedPaymentSystem;

    public bool IsValid(Account account, MakePaymentRequest _) => account != null && 
                                            account.AllowedPaymentSchemes.HasFlag(AllowedPaymentSchemes.AutomatedPaymentSystem) &&
                                            account.Status == AccountStatus.Live;
}