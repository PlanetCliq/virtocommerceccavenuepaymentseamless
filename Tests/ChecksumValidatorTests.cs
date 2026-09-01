using VirtoCommerce.Payment.CCAvenue.Services;
using Xunit;

namespace VirtoCommerce.Payment.CCAvenue.Tests
{
    public class ChecksumValidatorTests
    {
        [Fact]
        public void Validate_ShouldReturnTrue_ForCorrectChecksum()
        {
            var validator = new CCAvenueChecksumValidator();
            var data = "order123";
            var key = "secret";
            var checksum = validator.Validate(data, key, 
                new CCAvenueChecksumValidator().Validate(data, key, "").ToString());

            Assert.True(checksum);
        }

        [Fact]
        public void Validate_ShouldReturnFalse_ForInvalidChecksum()
        {
            var validator = new CCAvenueChecksumValidator();
            Assert.False(validator.Validate("order123", "secret", "wrongchecksum"));
        }
    }
}
