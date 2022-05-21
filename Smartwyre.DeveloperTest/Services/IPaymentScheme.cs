using Smartwyre.DeveloperTest.Types;

public interface IPaymentScheme
{
    PaymentScheme Type { get; }
    bool IsValid(Account account, MakePaymentRequest request);
}