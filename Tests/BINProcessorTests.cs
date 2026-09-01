using VirtoCommerce.Payment.CCAvenue.Services;
using Xunit;

namespace VirtoCommerce.Payment.CCAvenue.Tests
{
    public class BINProcessorTests
    {
        private readonly CCAvenueBINProcessor _processor = new();

        [Theory]
        [InlineData("412345", "VISA")]
        [InlineData("512345", "MASTERCARD")]
        [InlineData("652345", "RUPAY")]
        [InlineData("371234", "AMEX")]
        [InlineData("999999", "UNKNOWN")]
        public void GetCardType_ShouldReturnExpected(string bin, string expected)
        {
            var result = _processor.GetCardType(bin);
            Assert.Equal(expected, result);
        }
    }
}
