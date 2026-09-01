using System;

namespace VirtoCommerce.Payment.CCAvenue.Services
{
    public class CCAvenueResponseHandler
    {
        /// <summary>
        /// Determines whether the given order status indicates success.
        /// </summary>
        /// <param name="status">The order status string returned by CCAvenue.</param>
        /// <returns>True if status equals "Success" (case-insensitive), otherwise false.</returns>
        public bool IsSuccess(string status)
        {
            if (string.IsNullOrWhiteSpace(status))
                return false;

            return status.Equals("Success", StringComparison.OrdinalIgnoreCase);
        }
    }

    public class CCAvenueTokenService
    {
        /// <summary>
        /// Generates a unique token string without hyphens.
        /// </summary>
        /// <returns>A 32-character hexadecimal token.</returns>
        public string GenerateToken()
        {
            return Guid.NewGuid().ToString("N");
        }
    }
}
