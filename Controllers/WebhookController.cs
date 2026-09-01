using Microsoft.AspNetCore.Mvc;
using VirtoCommerce.Payment.CCAvenue.Services;

namespace VirtoCommerce.Payment.CCAvenue.Controllers
{
    [Route("api/payment/ccavenue/webhook")]
    public class WebhookController : Controller
    {
        private readonly CCAvenueResponseHandler _handler;
        public WebhookController(CCAvenueResponseHandler handler) => _handler = handler;

        [HttpPost]
        public IActionResult Notify([FromBody] Dictionary<string,string> response)
        {
            var success = _handler.IsSuccess(response["order_status"]);
            return success ? Ok("Payment success") : BadRequest("Payment failed");
        }
    }
}
