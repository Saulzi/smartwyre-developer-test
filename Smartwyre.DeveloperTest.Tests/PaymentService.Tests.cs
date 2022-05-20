using System;
using FakeItEasy;
using Smartwyre.DeveloperTest.Data;
using Smartwyre.DeveloperTest.Services;
using Xunit;
using FluentAssertions;

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
    }
}
