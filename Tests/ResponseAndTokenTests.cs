using VirtoCommerce.Payment.CCAvenue.Services;
using Xunit;

namespace VirtoCommerce.Payment.CCAvenue.Tests
{
    public class ResponseAndTokenTests
    {
        [Fact]
        public void ResponseHandler_ShouldReturnTrue_ForSuccess()
        {
            var handler = new CCAvenueResponseHandler();
            Assert.True(handler.IsSuccess("Success"));
        }

        [Fact]
        public void TokenService_ShouldGenerateUniqueToken()
        {
            var tokenService = new CCAvenueTokenService();
            var token1 = tokenService.GenerateToken();
            var token2 = tokenService.GenerateToken();

            Assert.NotEqual(token1, token2);
            Assert.Equal(32, token1.Length);
        }
    }
}
