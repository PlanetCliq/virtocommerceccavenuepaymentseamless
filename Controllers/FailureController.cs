using Microsoft.AspNetCore.Mvc;

namespace VirtoCommerce.Payment.CCAvenue.Controllers
{
    [Route("payment/ccavenue/failure")]
    public class FailureController : Controller
    {
        [HttpGet]
        public IActionResult Index() => View("Failure");
    }
}
