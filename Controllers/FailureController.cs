using Microsoft.AspNetCore.Mvc;

namespace VirtoCommerce.Payment.CCAvenue.Controllers
{
    [ApiController]
    [Route("payment/ccavenue/failure")]
    public class FailureController : ControllerBase
    {
        [HttpGet]
        public IActionResult Index()
        {
            // Return the Failure view explicitly
            return new ViewResult { ViewName = "Failure" };
        }
    }
}
