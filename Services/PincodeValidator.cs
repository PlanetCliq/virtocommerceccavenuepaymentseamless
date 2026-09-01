using System.Text.Json;

namespace VirtoCommerce.Payment.CCAvenue.Services
{
    public class PincodeValidator
    {
        private readonly HashSet<string> _pincodes;

        public PincodeValidator()
        {
            var dataPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data", "pincodes.json");

            if (!File.Exists(dataPath))
            {
                // Defensive: avoid runtime crash if file is missing
                _pincodes = new HashSet<string>();
                return;
            }

            var json = File.ReadAllText(dataPath);

            try
            {
                _pincodes = JsonSerializer.Deserialize<HashSet<string>>(json) ?? new HashSet<string>();
            }
            catch (JsonException)
            {
                // Defensive: avoid crash if JSON is malformed
                _pincodes = new HashSet<string>();
            }
        }

        public bool IsServiceable(string pincode)
        {
            if (string.IsNullOrWhiteSpace(pincode))
                return false;

            return _pincodes.Contains(pincode.Trim());
        }
    }
}
