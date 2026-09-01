using System.Security.Cryptography;
using System.Text;
using Microsoft.Extensions.Configuration;
using Polly;

namespace VirtoCommerce.Payment.CCAvenue.Services
{
    public class CCAvenuePaymentService
    {
        private readonly IConfiguration _config;
        private readonly PincodeValidator _pincodeValidator;
        private readonly CCAvenueChecksumValidator _checksumValidator;
        private readonly CCAvenueTimestampValidator _timestampValidator;

        public CCAvenuePaymentService(
            IConfiguration config,
            PincodeValidator pincodeValidator,
            CCAvenueChecksumValidator checksumValidator,
            CCAvenueTimestampValidator timestampValidator)
        {
            _config = config;
            _pincodeValidator = pincodeValidator;
            _checksumValidator = checksumValidator;
            _timestampValidator = timestampValidator;
        }

        public string BuildEncryptedRequest(Dictionary<string, string> data)
        {
            if (data == null || !data.ContainsKey("delivery_zip"))
                throw new ArgumentException("Missing delivery_zip in payload");

            if (!_pincodeValidator.IsServiceable(data["delivery_zip"]))
                throw new InvalidOperationException($"Out of delivery area: {data["delivery_zip"]}");

            var workingKey = _config["CCAvenue:WorkingKey"];
            if (string.IsNullOrWhiteSpace(workingKey))
                throw new InvalidOperationException("Missing WorkingKey configuration");

            var plain = string.Join("&", data.Select(x => $"{x.Key}={x.Value}"));

            // Retry only makes sense for transient errors, but kept here for consistency
            return Policy.Handle<CryptographicException>()
                .Retry(3)
                .Execute(() => Encrypt(plain, workingKey));
        }

        private string Encrypt(string plainText, string key)
        {
            // Ensure key is valid AES length (16, 24, 32 bytes)
            var keyBytes = Encoding.UTF8.GetBytes(key);
            if (keyBytes.Length != 16 && keyBytes.Length != 24 && keyBytes.Length != 32)
                throw new ArgumentException("WorkingKey must be 16, 24, or 32 bytes long");

            using var aes = Aes.Create();
            aes.Key = keyBytes;
            aes.Mode = CipherMode.CBC;
            aes.IV = new byte[16]; // zero IV for simplicity; consider random IV for stronger security

            var encryptor = aes.CreateEncryptor();
            var bytes = Encoding.UTF8.GetBytes(plainText);
            var cipher = encryptor.TransformFinalBlock(bytes, 0, bytes.Length);

            return Convert.ToBase64String(cipher);
        }
    }
}
