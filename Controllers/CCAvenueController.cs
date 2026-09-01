using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using VirtoCommerce.Payment.CCAvenue.Services;

namespace VirtoCommerce.Payment.CCAvenue.Controllers
{
    [ApiController]
    [Route("api/payment/ccavenue")]
    public class CCAvenueController : ControllerBase
    {
        private readonly CCAvenuePaymentService _service;

        public CCAvenueController(CCAvenuePaymentService service) => _service = service;

        [HttpPost("create")]
        public IActionResult Create([FromBody] Dictionary<string, string> payload)
        {
            if (payload == null || payload.Count == 0)
            {
                return BadRequest("Invalid payment request payload");
            }

            var enc = _service.BuildEncryptedRequest(payload);

            if (string.IsNullOrEmpty(enc))
            {
                return BadRequest("Missing WorkingKey configuration");
            }

            return Ok(new { encRequest = enc });
        }
    }
}
