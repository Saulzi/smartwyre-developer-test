using Smartwyre.DeveloperTest.Data;
using Smartwyre.DeveloperTest.Types;
using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;

namespace Smartwyre.DeveloperTest.Services
{
    public class PaymentService : IPaymentService
    {
        private IAccountDataStore _accounts;
        
        public static List<PaymentScheme> PaymentSchemes {get; } = new List<PaymentScheme> {
            new AutomatedPaymentSystem(),
            new BankToBankTransfer(),
            new ExpeditedPayments()
        };

        public PaymentService(IAccountDataStore accountDataStore)
        {
            _accounts = accountDataStore ?? throw new ArgumentNullException(nameof(accountDataStore));
        }

        private bool GetIsPaymentAllowed(MakePaymentRequest request, Account account)
        {
            var paymentScheme =  PaymentSchemes.FirstOrDefault(f=>f.Type == request.PaymentScheme);
            return paymentScheme != null && paymentScheme.IsPaymentAllowed(account, request);
        }

        public MakePaymentResult MakePayment(MakePaymentRequest request)
        {
            var wasPaymentSuccessfull = false;
            var account = _accounts.GetAccount(request.DebtorAccountNumber);
            var isPaymentAllowed = GetIsPaymentAllowed(request, account);

            if (isPaymentAllowed)
            {
                account.Balance -= request.Amount;
                _accounts.UpdateAccount(account);
                wasPaymentSuccessfull = true;
            }

            return new MakePaymentResult() {
                Success = wasPaymentSuccessfull
            };
        }
    }
}
