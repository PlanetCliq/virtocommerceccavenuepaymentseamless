using System.Text.RegularExpressions;

namespace VirtoCommerce.Payment.CCAvenue.Services
{
    public class CCAvenueBINProcessor
    {
        public string GetCardType(string bin)
        {
            if (Regex.IsMatch(bin, "^4")) return "VISA";
            if (Regex.IsMatch(bin, "^5[1-5]")) return "MASTERCARD";
            if (Regex.IsMatch(bin, "^60|^65|^35")) return "RUPAY";
            if (Regex.IsMatch(bin, "^3[47]")) return "AMEX";
            return "UNKNOWN";
        }
    }
}
