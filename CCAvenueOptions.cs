namespace VirtoCommerce.Payment.CCAvenue
{
    public class CCAvenueOptions
    {
        public string MerchantId { get; set; }
        public string WorkingKey { get; set; }
        public string AccessCode { get; set; }
        public string RedirectUrl { get; set; }
        public string CancelUrl { get; set; }
        public string Currency { get; set; }
        public bool IsTestMode { get; set; }
    }
}
