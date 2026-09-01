namespace VirtoCommerce.Payment.CCAvenue.Services
{
    public class CCAvenueTimestampValidator
    {
        private readonly TimeSpan _tolerance = TimeSpan.FromMinutes(15);
        public bool IsValid(DateTime timestamp)
        {
            return Math.Abs((DateTime.UtcNow - timestamp).TotalMinutes) <= _tolerance.TotalMinutes;
        }
    }
}
