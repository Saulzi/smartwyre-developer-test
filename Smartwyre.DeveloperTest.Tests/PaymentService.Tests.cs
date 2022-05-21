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
        [InlineData(PaymentScheme.AutomatedPaymentSystem)]
        [InlineData(PaymentScheme.BankToBankTransfer)]
        [InlineData(PaymentScheme.ExpeditedPayments)]
        [InlineData((IPaymentScheme)null)]  // For now this one raises warning
        public void PaymentService_AccontInvalid_ReturnsFalse(PaymentScheme paymentScheme)
        {
            // Arrange
            var accounts = A.Fake<IAccountDataStore>();
            A.CallTo(() => accounts.GetAccount("1234"))
             .Returns(null);

            var itemUnderTest = new PaymentService(accounts);
            
            var request = new MakePaymentRequest() {
                Amount = 1,
                CreditorAccountNumber = "AAA",
                DebtorAccountNumber = "BBB",
                PaymentScheme = paymentScheme
            };

            // Act
            var result = itemUnderTest.MakePayment(request);

            // Assert
            result.Success.Should().BeFalse(); 
        }

        

      
    }
}
