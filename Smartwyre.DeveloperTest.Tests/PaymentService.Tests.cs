using System;
using Smartwyre.DeveloperTest.Services;
using Xunit;

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
            Assert.Throws<ArgumentNullException>(act);
        }

        
    }
}
