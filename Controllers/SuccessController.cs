using Microsoft.AspNetCore.Mvc;

namespace VirtoCommerce.Payment.CCAvenue.Controllers
{
    [Route("payment/ccavenue/success")]
    public class SuccessController : Controller
    {
        [HttpGet]
        public IActionResult Index() => View("Success");
    }
}
