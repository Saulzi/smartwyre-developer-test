using Smartwyre.DeveloperTest.Types;

namespace Smartwyre.DeveloperTest.Services;

public class BankToBankTransfer : IPaymentScheme
{
    public PaymentScheme Type => PaymentScheme.BankToBankTransfer;

    public bool IsValid(Account account, MakePaymentRequest _) => account != null 
                                                               && account.AllowedPaymentSchemes.HasFlag(AllowedPaymentSchemes.BankToBankTransfer);
}