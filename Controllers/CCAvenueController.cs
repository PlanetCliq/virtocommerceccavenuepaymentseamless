using Microsoft.AspNetCore.Mvc;
using VirtoCommerce.Payment.CCAvenue.Services;

namespace VirtoCommerce.Payment.CCAvenue.Controllers
{
    [Route("api/payment/ccavenue")]
    public class CCAvenueController : Controller
    {
        private readonly CCAvenuePaymentService _service;
        public CCAvenueController(CCAvenuePaymentService service) => _service = service;

        [HttpPost("create")]
        public IActionResult Create([FromBody] Dictionary<string,string> payload)
        {
            var enc = _service.BuildEncryptedRequest(payload);
            return Ok(new { encRequest = enc });
        }
    }
}
