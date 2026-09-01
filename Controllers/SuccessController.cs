using Microsoft.AspNetCore.Mvc;

namespace VirtoCommerce.Payment.CCAvenue.Controllers
{
    [ApiController]
    [Route("payment/ccavenue/success")]
    public class SuccessController : ControllerBase
    {
        [HttpGet]
        public IActionResult Index()
        {
            // Return the Success view explicitly
            return new ViewResult { ViewName = "Success" };
        }
    }
}
