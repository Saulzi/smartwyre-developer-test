using Smartwyre.DeveloperTest.Types;

public abstract class PaymentScheme
{
    public abstract PaymentSchemeType Type { get; }
    public abstract bool IsPaymentAllowed(Account account, MakePaymentRequest request);
}