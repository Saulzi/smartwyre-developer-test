using System;

namespace Smartwyre.DeveloperTest.Types
{
    public class MakePaymentRequest
    {
        public string CreditorAccountNumber { get; set; }

        public string DebtorAccountNumber { get; set; }

        public decimal Amount { get; set; }

        public DateTimeOffset PaymentDate { get; set; }

        public PaymentSchemeType PaymentScheme { get; set; }
    }
}
