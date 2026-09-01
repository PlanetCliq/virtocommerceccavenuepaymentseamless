using System.Text.Json;

namespace VirtoCommerce.Payment.CCAvenue.Services
{
    public class PincodeValidator
    {
        private readonly HashSet<string> _pincodes;

        public PincodeValidator()
        {
            var json = File.ReadAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data", "pincodes.json"));
            _pincodes = JsonSerializer.Deserialize<HashSet<string>>(json) ?? new HashSet<string>();
        }

        public bool IsServiceable(string pincode) => _pincodes.Contains(pincode);
    }
}
