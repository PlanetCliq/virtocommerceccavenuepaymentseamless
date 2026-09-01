using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using VirtoCommerce.Payment.CCAvenue.Controllers;
using VirtoCommerce.Payment.CCAvenue.Services;
using Xunit;

namespace VirtoCommerce.Payment.CCAvenue.Tests
{
    public class ControllersTests
    {
        [Fact]
        public void CCAvenueController_Create_ReturnsEncryptedRequest()
        {
            // Arrange
            var config = new ConfigurationBuilder()
                .AddInMemoryCollection(new Dictionary<string, string>
                {
                    { "CCAvenue:WorkingKey", "12345678901234567890123456789012" }
                })
                .Build();

            var service = new CCAvenuePaymentService(
                config,
                new PincodeValidator(),
                new CCAvenueChecksumValidator(),
                new CCAvenueTimestampValidator()
            );

            var controller = new CCAvenueController(service);
            var payload = new Dictionary<string, string>
            {
                { "delivery_zip", "400001" },
                { "order_id", "ORD123" }
            };

            // Act
            var result = controller.Create(payload) as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Contains("encRequest", result.Value?.ToString());
        }

        [Fact]
        public void WebhookController_Notify_Success()
        {
            var handler = new CCAvenueResponseHandler();
            var controller = new WebhookController(handler);

            var response = new Dictionary<string, string> { { "order_status", "Success" } };

            var result = controller.Notify(response) as OkObjectResult;

            Assert.NotNull(result);
            Assert.Equal("Payment success", result.Value);
        }

        [Fact]
        public void WebhookController_Notify_Failure()
        {
            var handler = new CCAvenueResponseHandler();
            var controller = new WebhookController(handler);

            var response = new Dictionary<string, string> { { "order_status", "Failure" } };

            var result = controller.Notify(response) as BadRequestObjectResult;

            Assert.NotNull(result);
            Assert.Equal("Payment failed", result.Value);
        }

        [Fact]
        public void SuccessController_ReturnsView()
        {
            var controller = new SuccessController();

            var result = controller.Index() as ViewResult;

            Assert.NotNull(result);
            Assert.Equal("Success", result.ViewName);
        }

        [Fact]
        public void FailureController_ReturnsView()
        {
            var controller = new FailureController();

            var result = controller.Index() as ViewResult;

            Assert.NotNull(result);
            Assert.Equal("Failure", result.ViewName);
        }
    }
}
