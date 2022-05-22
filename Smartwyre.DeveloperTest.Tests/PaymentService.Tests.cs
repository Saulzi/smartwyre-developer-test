using System;
using FakeItEasy;
using Smartwyre.DeveloperTest.Data;
using Smartwyre.DeveloperTest.Services;
using Xunit;
using FluentAssertions;
using Smartwyre.DeveloperTest.Types;

namespace Smartwyre.DeveloperTest.Tests
{
    public class PaymentServiceTests
    {
        [Fact]
        public void PaymentService_ConstructorPassedNoDataStore_ThrowsArgumentNull()
        {
            // Arrange
    
            // Act
            Action act = () => new PaymentService(null);

            // Assert
            act.Should().Throw<ArgumentNullException>().WithParameterName("accountDataStore");
        }

        [Fact]
        public void PaymentService_ConstructorPassedDataSource_DoesntThrow()
        {
            // Arrange
            var fake = A.Fake<IAccountDataStore>();

            // Act
            Action act = () => new PaymentService(fake);

            // Assert
            act.Should().NotThrow();
        }

        [Theory()]
        [InlineData(PaymentSchemeType.AutomatedPaymentSystem)]
        [InlineData(PaymentSchemeType.BankToBankTransfer)]
        [InlineData(PaymentSchemeType.ExpeditedPayments)]
        public void PaymentService_AccontInvalid_ReturnsFalse(PaymentSchemeType paymentScheme)
        {
            // Arrange
            var accounts = A.Fake<IAccountDataStore>();
            A.CallTo(() => accounts.GetAccount("AAA"))
             .Returns(null);

            var itemUnderTest = new PaymentService(accounts);
            
            var request = new MakePaymentRequest() {
                Amount = 1,
                CreditorAccountNumber = "BBB",
                DebtorAccountNumber = "AAA",
                PaymentScheme = paymentScheme
            };

            // Act
            var result = itemUnderTest.MakePayment(request);

            // Assert
            result.Success.Should().BeFalse(); 
        }

        [Fact]
        public void PaymentService_ValidBankToBank_ReturnsTrueAndReducesValue()
        {
            // Arrange
            var account = new Account()
            {
                AccountNumber = "AAA",
                Balance = 100,
                AllowedPaymentSchemes =AllowedPaymentSchemes.BankToBankTransfer | AllowedPaymentSchemes.ExpeditedPayments
            };

            var accounts = A.Fake<IAccountDataStore>();
            A.CallTo(() => accounts.GetAccount("AAA"))
             .Returns(account);

            var itemUnderTest = new PaymentService(accounts);
            
            var request = new MakePaymentRequest() {
                Amount = 1,
                CreditorAccountNumber = "BBB",
                DebtorAccountNumber = "AAA",
                PaymentScheme = PaymentSchemeType.BankToBankTransfer
            };

            // Act
            var result = itemUnderTest.MakePayment(request);

            // Assert
            result.Success.Should().BeTrue();
            account.Balance.Should().Be(99);
            A.CallTo(() => accounts.UpdateAccount(account)).MustHaveHappened(); 
        }      
    }
}
