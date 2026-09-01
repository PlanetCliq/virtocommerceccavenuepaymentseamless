using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Moq;
using Moq.Protected;
using VirtoCommerce.Payment.CCAvenue.Services;
using Xunit;

namespace VirtoCommerce.Payment.CCAvenue.Tests
{
    public class PincodeServiceabilityTests
    {
        [Fact]
        public async Task CourierApi_ShouldReturnServiceable()
        {
            // Arrange: mock courier API response
            var handlerMock = new Mock<HttpMessageHandler>();
            handlerMock.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent("{\"serviceable\":true}")
                });

            var httpClient = new HttpClient(handlerMock.Object);
            var courierApi = new MockCourierApi(httpClient);

            // Act
            var result = await courierApi.CheckPincodeAsync("400001");

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task CourierApi_ShouldFallbackToLocalFile_WhenApiFails()
        {
            // Arrange: simulate API failure
            var handlerMock = new Mock<HttpMessageHandler>();
            handlerMock.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
                .ThrowsAsync(new HttpRequestException("API down"));

            var httpClient = new HttpClient(handlerMock.Object);
            var courierApi = new MockCourierApi(httpClient);

            // Local fallback
            var fallbackValidator = new PincodeValidator();

            // Act
            bool result;
            try
            {
                result = await courierApi.CheckPincodeAsync("400001");
            }
            catch
            {
                result = fallbackValidator.IsServiceable("400001");
            }

            // Assert
            Assert.True(result); // Should succeed via fallback
        }
    }

    // Mock courier API client
    public class MockCourierApi
    {
        private readonly HttpClient _httpClient;
        public MockCourierApi(HttpClient httpClient) => _httpClient = httpClient;

        public async Task<bool> CheckPincodeAsync(string pincode)
        {
            var response = await _httpClient.GetAsync($"https://mockcourier.com/api/pincode/{pincode}");
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync();
            return json.Contains("\"serviceable\":true");
        }
    }
}
