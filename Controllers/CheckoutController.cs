using Microsoft.AspNetCore.Mvc;
using VirtoCommerce.Payment.CCAvenue.Services;

namespace VirtoCommerce.Payment.CCAvenue.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CheckoutController : ControllerBase
    {
        private readonly CCAvenuePaymentService _paymentService;

        public CheckoutController(CCAvenuePaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        [HttpPost("ccavenue")]
        public IActionResult ProcessPayment([FromBody] Dictionary<string, string> payload)
        {
            try
            {
                var encryptedRequest = _paymentService.BuildEncryptedRequest(payload);
                return Ok(new { Encrypted = encryptedRequest });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
        }
    }
}
