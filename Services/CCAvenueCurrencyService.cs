namespace VirtoCommerce.Payment.CCAvenue.Services
{
    public class CCAvenueCurrencyService
    {
        private readonly HashSet<string> _allowed = new() { "INR", "USD", "AED", "SAR" };
        public bool IsSupported(string currency) => _allowed.Contains(currency.ToUpper());
        public string Normalize(decimal amount) => amount.ToString("F2");
    }
}
