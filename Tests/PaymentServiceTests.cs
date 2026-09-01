using System;
using System.Collections.Generic;
using Microsoft.Extensions.Options;
using Moq;
using VirtoCommerce.Payment.CCAvenue;
using VirtoCommerce.Payment.CCAvenue.Services;
using Xunit;

namespace VirtoCommerce.Payment.CCAvenue.Tests
{
    public class PaymentServiceTests
    {
        private CCAvenuePaymentService CreateService(string workingKey, bool isServiceable = true, string merchantId = "MERCHANT123")
        {
            var options = Options.Create(new CCAvenueOptions
            {
                WorkingKey = workingKey,
                MerchantId = merchantId
            });

            var pincodeValidator = new Mock<PincodeValidator>();
            pincodeValidator.Setup(p => p.IsServiceable(It.IsAny<string>())).Returns(isServiceable);

            return new CCAvenuePaymentService(
                options,
                pincodeValidator.Object,
                new CCAvenueChecksumValidator(),
                new CCAvenueTimestampValidator()
            );
        }

        [Fact]
        public void BuildEncryptedRequest_ShouldThrow_WhenZipNotServiceable()
        {
            var service = CreateService("12345678901234567890123456789012", isServiceable: false);
            var data = new Dictionary<string, string> { { "delivery_zip", "400001" } };

            Assert.Throws<InvalidOperationException>(() => service.BuildEncryptedRequest(data));
        }

        [Fact]
        public void BuildEncryptedRequest_ShouldThrow_WhenKeyLengthInvalid()
        {
            var service = CreateService("shortkey123");
            var data = new Dictionary<string, string> { { "delivery_zip", "400001" } };

            Assert.Throws<ArgumentException>(() => service.BuildEncryptedRequest(data));
        }

        [Fact]
        public void BuildEncryptedRequest_ShouldAddMerchantId_WhenMissing()
        {
            var service = CreateService("12345678901234567890123456789012");
            var data = new Dictionary<string, string> { { "delivery_zip", "400001" } };

            var result = service.BuildEncryptedRequest(data);

            Assert.Contains("merchant_id=MERCHANT123", result);
        }

        [Fact]
        public void BuildEncryptedRequest_ShouldReturnEncryptedString_WhenValid()
        {
            var service = CreateService("12345678901234567890123456789012");
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
