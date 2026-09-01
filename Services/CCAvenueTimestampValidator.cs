using System;

namespace VirtoCommerce.Payment.CCAvenue.Services
{
    public class CCAvenueTimestampValidator
    {
        private readonly TimeSpan _tolerance;

        public CCAvenueTimestampValidator(TimeSpan? tolerance = null)
        {
            // Default tolerance is 15 minutes if none provided
            _tolerance = tolerance ?? TimeSpan.FromMinutes(15);
        }

        public bool IsValid(DateTime timestampUtc)
        {
            // Defensive: ensure timestamp is treated as UTC
            var now = DateTime.UtcNow;
            var diff = Math.Abs((now - timestampUtc).TotalMinutes);
            return diff <= _tolerance.TotalMinutes;
        }
    }
}
