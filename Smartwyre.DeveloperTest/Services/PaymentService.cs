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
        private IReadOnlyList<IPaymentScheme> _paymentSchemes;

        public PaymentService(IAccountDataStore accountDataStore, IReadOnlyList<IPaymentScheme> paymentSchemes)
        {
            _accountDataStore = accountDataStore ?? throw new ArgumentNullException(nameof(accountDataStore));
        }

        public MakePaymentResult MakePayment(MakePaymentRequest request)
        {
            Account account = _accountDataStore.GetAccount(request.DebtorAccountNumber);
            
            var result = new MakePaymentResult();

            switch (request.PaymentScheme)
            {
                case PaymentScheme.BankToBankTransfer:
                    break;
            }

            var paymentScheme =  _paymentSchemes.FirstOrDefault(f=>f.Type == request.PaymentScheme);
            result.Success = paymentScheme != null && paymentScheme.IsValid(account, request);
            
            if (result.Success)
            {
                account.Balance -= request.Amount;

                _accountDataStore.UpdateAccount(account);
            }

            return result;
        }
    }
}
