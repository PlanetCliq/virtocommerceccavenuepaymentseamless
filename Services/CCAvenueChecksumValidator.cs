using System.Security.Cryptography;
using System.Text;

namespace VirtoCommerce.Payment.CCAvenue.Services
{
    public class CCAvenueChecksumValidator
    {
        public bool Validate(string data, string workingKey, string checksum)
        {
            using var sha = SHA256.Create();
            var hash = sha.ComputeHash(Encoding.UTF8.GetBytes(data + workingKey));
            var calculated = BitConverter.ToString(hash).Replace("-", "").ToLower();
            return calculated == checksum.ToLower();
        }
    }
}
