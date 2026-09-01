using System;
using System.Security.Cryptography;
using System.Text;

namespace VirtoCommerce.Payment.CCAvenue.Services
{
    public class CCAvenueChecksumValidator
    {
        /// <summary>
        /// Validates the checksum for the given data and working key.
        /// </summary>
        /// <param name="data">The raw data string to validate.</param>
        /// <param name="workingKey">The secret working key.</param>
        /// <param name="checksum">The checksum provided by CCAvenue.</param>
        /// <returns>True if the calculated checksum matches the provided one, otherwise false.</returns>
        public bool Validate(string data, string workingKey, string checksum)
        {
            if (string.IsNullOrWhiteSpace(data) || string.IsNullOrWhiteSpace(workingKey) || string.IsNullOrWhiteSpace(checksum))
                return false;

            using var sha = SHA256.Create();
            var input = Encoding.UTF8.GetBytes(data + workingKey);
            var hash = sha.ComputeHash(input);

            var calculated = BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant();
            return string.Equals(calculated, checksum.Trim(), StringComparison.OrdinalIgnoreCase);
        }
    }
}
