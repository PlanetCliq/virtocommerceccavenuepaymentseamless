namespace VirtoCommerce.Payment.CCAvenue.Services
{
    public class CCAvenueResponseHandler
    {
        public bool IsSuccess(string status) => status.Equals("Success", StringComparison.OrdinalIgnoreCase);
    }

    public class CCAvenueTokenService
    {
        public string GenerateToken() => Guid.NewGuid().ToString("N");
    }
}
