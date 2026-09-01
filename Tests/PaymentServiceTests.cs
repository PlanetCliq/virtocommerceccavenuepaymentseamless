using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Moq;
using VirtoCommerce.Payment.CCAvenue.Services;
using Xunit;

namespace VirtoCommerce.Payment.CCAvenue.Tests
{
    public class PaymentServiceTests
    {
        [Fact]
        public void BuildEncryptedRequest_ShouldThrow_WhenZipNotServiceable()
        {
            var config = new Mock<IConfiguration>();
            config.Setup(c => c["CCAvenue:WorkingKey"]).Returns("12345678901234567890123456789012");

            var pincodeValidator = new Mock<PincodeValidator>();
            pincodeValidator.Setup(p => p.IsServiceable(It.IsAny<string>())).Returns(false);

            var service = new CCAvenuePaymentService(config.Object, pincodeValidator.Object,
                new CCAvenueChecksumValidator(), new CCAvenueTimestampValidator());

            var data = new Dictionary<string, string> { { "delivery_zip", "400001" } };

            Assert.Throws<InvalidOperationException>(() => service.BuildEncryptedRequest(data));
        }

        [Fact]
        public void BuildEncryptedRequest_ShouldReturnEncryptedString_WhenValid()
        {
            var config = new Mock<IConfiguration>();
            config.Setup(c => c["CCAvenue:WorkingKey"]).Returns("12345678901234567890123456789012");

            var pincodeValidator = new Mock<PincodeValidator>();
            pincodeValidator.Setup(p => p.IsServiceable(It.IsAny<string>())).Returns(true);

            var service = new CCAvenuePaymentService(config.Object, pincodeValidator.Object,
                new CCAvenueChecksumValidator(), new CCAvenueTimestampValidator());

            var data = new Dictionary<string, string>
            {
                { "delivery_zip", "400001" },
                { "order_id", "12345" }
            };

            var result = service.BuildEncryptedRequest(data);
            Assert.False(string.IsNullOrEmpty(result));
        }
    }
}
