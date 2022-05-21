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

        public MakePaymentResult MakePayment(MakePaymentRequest request)
        {
            Account account = _accountDataStore.GetAccount(request.DebtorAccountNumber);

            var paymentScheme =  _paymentSchemes.FirstOrDefault(f=>f.Type == request.PaymentScheme);
            var isPaymentAllowed = paymentScheme != null && paymentScheme.IsValid(account, request);
            
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
