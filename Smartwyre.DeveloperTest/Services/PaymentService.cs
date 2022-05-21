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
        private IAccountDataStore _accountDataStore;
        
        private static IReadOnlyList<IPaymentScheme> _paymentSchemes = new IPaymentScheme[] {
            new AutomatedPaymentSystem(),
            new BankToBankTransfer(),
            new ExpeditedPayments()
        };

        public PaymentService(IAccountDataStore accountDataStore)
        {
            _accountDataStore = accountDataStore ?? throw new ArgumentNullException(nameof(accountDataStore));
        }

        private bool GetIsPaymentAllowed(MakePaymentRequest request, Account account)
        {
            var paymentScheme =  _paymentSchemes.FirstOrDefault(f=>f.Type == request.PaymentScheme);
            return paymentScheme != null && paymentScheme.IsValid(account, request);
        }

        public MakePaymentResult MakePayment(MakePaymentRequest request)
        {
            var account = _accountDataStore.GetAccount(request.DebtorAccountNumber);

            var isPaymentAllowed = GetIsPaymentAllowed(request, account);
            
            if (isPaymentAllowed)
            {
                account.Balance -= request.Amount;
                _accountDataStore.UpdateAccount(account);
            }

            return new MakePaymentResult() {
                Success = isPaymentAllowed
            };
        }
    }
}
