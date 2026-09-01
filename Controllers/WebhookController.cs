using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using VirtoCommerce.Payment.CCAvenue.Services;

namespace VirtoCommerce.Payment.CCAvenue.Controllers
{
    [ApiController]
    [Route("api/payment/ccavenue/webhook")]
    public class WebhookController : ControllerBase
    {
        private readonly CCAvenueResponseHandler _handler;

        public WebhookController(CCAvenueResponseHandler handler) => _handler = handler;

        [HttpPost]
        public IActionResult Notify([FromBody] Dictionary<string, string> response)
        {
            if (response == null || !response.ContainsKey("order_status"))
            {
                return BadRequest("Missing order_status in response");
            }

            var success = _handler.IsSuccess(response["order_status"]);
            return success ? Ok("Payment success") : BadRequest("Payment failed");
        }
    }
}
