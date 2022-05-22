using Smartwyre.DeveloperTest.Types;

namespace Smartwyre.DeveloperTest.Services;

public class BankToBankTransfer : PaymentScheme
{
    public override PaymentSchemeType Type => PaymentSchemeType.BankToBankTransfer;

    public override bool IsPaymentAllowed(Account account, MakePaymentRequest _) => account != null 
                                                                        && account.AllowedPaymentSchemes.HasFlag(AllowedPaymentSchemes.BankToBankTransfer);
}