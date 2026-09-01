using System.Text.RegularExpressions;

namespace VirtoCommerce.Payment.CCAvenue.Services
{
    public class CCAvenueBINProcessor
    {
        /// <summary>
        /// Determines the card type based on BIN (first 6 digits).
        /// </summary>
        /// <param name="bin">Bank Identification Number string.</param>
        /// <returns>Card type (VISA, MASTERCARD, RUPAY, AMEX, UNKNOWN).</returns>
        public string GetCardType(string bin)
        {
            if (string.IsNullOrWhiteSpace(bin))
                return "UNKNOWN";

            bin = bin.Trim();

            if (Regex.IsMatch(bin, @"^4")) return "VISA";
            if (Regex.IsMatch(bin, @"^5[1-5]")) return "MASTERCARD";
            if (Regex.IsMatch(bin, @"^(60|65|35)")) return "RUPAY";
            if (Regex.IsMatch(bin, @"^3[47]")) return "AMEX";

            return "UNKNOWN";
        }
    }
}
