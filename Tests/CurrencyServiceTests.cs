using VirtoCommerce.Payment.CCAvenue.Services;
using Xunit;

namespace VirtoCommerce.Payment.CCAvenue.Tests
{
    public class CurrencyServiceTests
    {
        private readonly CCAvenueCurrencyService _service = new();

        [Theory]
        [InlineData("INR", true)]
        [InlineData("usd", true)]
        [InlineData("eur", false)]
        public void IsSupported_ShouldReturnExpected(string currency, bool expected)
        {
            Assert.Equal(expected, _service.IsSupported(currency));
        }

        [Fact]
        public void Normalize_ShouldReturnTwoDecimalPlaces()
        {
            var result = _service.Normalize(123.456m);
            Assert.Equal("123.46", result);
        }
    }
}
