using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Bookme.WebApp.Controllers
{
    public class BookingConfigurationController : Controller
    {
        [Authorize(Roles = "Business, Admin")]
        public IActionResult Configure()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Business, Admin")]
        public IActionResult Configure(object model)
        {
            return View();
        }
    }
}
