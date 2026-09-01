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

        public CCAvenuePaymentService(IConfiguration config, PincodeValidator pincodeValidator,
            CCAvenueChecksumValidator checksumValidator, CCAvenueTimestampValidator timestampValidator)
        {
            _config = config;
            _pincodeValidator = pincodeValidator;
            _checksumValidator = checksumValidator;
            _timestampValidator = timestampValidator;
        }

        public string BuildEncryptedRequest(Dictionary<string,string> data)
        {
            if (!_pincodeValidator.IsServiceable(data["delivery_zip"]))
                throw new InvalidOperationException($"Out of delivery area: {data["delivery_zip"]}");

            var workingKey = _config["CCAvenue:WorkingKey"];
            var plain = string.Join("&", data.Select(x => $"{x.Key}={x.Value}"));

            return Policy.Handle<Exception>()
                .Retry(3)
                .Execute(() => Encrypt(plain, workingKey));
        }

        private string Encrypt(string plainText, string key)
        {
            using var aes = Aes.Create();
            aes.Key = Encoding.UTF8.GetBytes(key);
            aes.Mode = CipherMode.CBC;
            aes.IV = new byte[16];
            var encryptor = aes.CreateEncryptor();
            var bytes = Encoding.UTF8.GetBytes(plainText);
            return Convert.ToBase64String(encryptor.TransformFinalBlock(bytes, 0, bytes.Length));
        }
    }
}
