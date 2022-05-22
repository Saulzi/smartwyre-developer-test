using System;
using Smartwyre.DeveloperTest.Data;
using Smartwyre.DeveloperTest.Services;
using Smartwyre.DeveloperTest.Types;

var accounts = new AccountDataStore();
var payments = new PaymentService(accounts);
var request = new MakePaymentRequest()
{
    Amount = 100,
    PaymentScheme=PaymentSchemeType.AutomatedPaymentSystem,
    CreditorAccountNumber="AAA",
    DebtorAccountNumber="BBB",
    PaymentDate = DateTimeOffset.UtcNow
};

var result = payments.MakePayment(request);    
Console.WriteLine("there is no account etc so this program will do nothing");