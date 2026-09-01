using System;
using System.Collections.Generic;
using System.Globalization;

namespace VirtoCommerce.Payment.CCAvenue.Services
{
    public class CCAvenueCurrencyService
    {
        private readonly HashSet<string> _allowed = new(StringComparer.OrdinalIgnoreCase)
        {
            "INR", "USD", "AED", "SAR"
        };

        /// <summary>
        /// Checks if the given currency code is supported.
        /// </summary>
        public bool IsSupported(string currency)
        {
            if (string.IsNullOrWhiteSpace(currency))
                return false;

            return _allowed.Contains(currency.Trim());
        }

        /// <summary>
        /// Normalizes the amount to a fixed two-decimal string using invariant culture.
        /// </summary>
        public string Normalize(decimal amount)
        {
            return amount.ToString("F2", CultureInfo.InvariantCulture);
        }
    }
}
