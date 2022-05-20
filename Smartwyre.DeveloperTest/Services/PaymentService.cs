﻿using Smartwyre.DeveloperTest.Data;
using Smartwyre.DeveloperTest.Types;
using System;
using System.Configuration;
namespace Smartwyre.DeveloperTest.Services
{
    public class PaymentService : IPaymentService
    {
        private IAccountDataStore _accountDataStore;
        public PaymentService(IAccountDataStore accountDataStore)
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
                    if (account == null)
                    {
                        result.Success = false;
                    }
                    else if (!account.AllowedPaymentSchemes.HasFlag(AllowedPaymentSchemes.BankToBankTransfer))
                    {
                        result.Success = false;
                    }
                    break;

                case PaymentScheme.ExpeditedPayments:
                    if (account == null)
                    {
                        result.Success = false;
                    }
                    else if (!account.AllowedPaymentSchemes.HasFlag(AllowedPaymentSchemes.ExpeditedPayments))
                    {
                        result.Success = false;
                    }
                    else if (account.Balance < request.Amount)
                    {
                        result.Success = false;
                    }
                    break;

                case PaymentScheme.AutomatedPaymentSystem:
                    if (account == null)
                    {
                        result.Success = false;
                    }
                    else if (!account.AllowedPaymentSchemes.HasFlag(AllowedPaymentSchemes.AutomatedPaymentSystem))
                    {
                        result.Success = false;
                    }
                    else if (account.Status != AccountStatus.Live)
                    {
                        result.Success = false;
                    }
                    break;
            }

            if (result.Success)
            {
                account.Balance -= request.Amount;

                _accountDataStore.UpdateAccount(account);
            }

            return result;
        }
    }
}
