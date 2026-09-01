using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Extensions.Options;

namespace VirtoCommerce.Payment.CCAvenue.Services
{
    public class CCAvenuePaymentService
    {
        private readonly CCAvenueOptions _options;
        private readonly PincodeValidator _pincodeValidator;
        private readonly CCAvenueChecksumValidator _checksumValidator;
        private readonly CCAvenueTimestampValidator _timestampValidator;

        public CCAvenuePaymentService(
            IOptions<CCAvenueOptions> options,
            PincodeValidator pincodeValidator,
            CCAvenueChecksumValidator checksumValidator,
            CCAvenueTimestampValidator timestampValidator)
        {
            _options = options.Value ?? throw new ArgumentNullException(nameof(options));
            _pincodeValidator = pincodeValidator;
            _checksumValidator = checksumValidator;
            _timestampValidator = timestampValidator;
        }

        public string BuildEncryptedRequest(Dictionary<string, string> payload)
        {
            if (payload == null || !payload.ContainsKey("delivery_zip"))
                throw new ArgumentException("Missing delivery_zip in payload");

            if (!_pincodeValidator.IsServiceable(payload["delivery_zip"]))
                throw new InvalidOperationException($"Out of delivery area: {payload["delivery_zip"]}");

            var workingKey = _options.WorkingKey;
            if (string.IsNullOrWhiteSpace(workingKey))
                throw new InvalidOperationException("Missing WorkingKey configuration");

            // Ensure merchant_id is present
            if (!payload.ContainsKey("merchant_id"))
                payload["merchant_id"] = _options.MerchantId;

            var plain = string.Join("&", payload.Select(x => $"{x.Key}={x.Value}"));
            return Encrypt(plain, workingKey);
        }

        private string Encrypt(string plainText, string key)
        {
            var keyBytes = Encoding.UTF8.GetBytes(key);
            if (keyBytes.Length != 16 && keyBytes.Length != 24 && keyBytes.Length != 32)
                throw new ArgumentException("WorkingKey must be 16, 24, or 32 bytes long");

            using var aes = Aes.Create();
            aes.Key = keyBytes;
            aes.Mode = CipherMode.CBC;
            aes.IV = new byte[16]; // zero IV; consider random IV for stronger security

            var encryptor = aes.CreateEncryptor();
            var bytes = Encoding.UTF8.GetBytes(plainText);
            var cipher = encryptor.TransformFinalBlock(bytes, 0, bytes.Length);

            return Convert.ToBase64String(cipher);
        }
    }
}
