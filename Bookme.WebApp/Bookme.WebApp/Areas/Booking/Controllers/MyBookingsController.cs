using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Bookme.WebApp.Areas.Booking.Controllers
{
    [Area("Booking")]
    public class MyBookingsController : Controller
    {
        [Authorize]
        public IActionResult All()
        {
            return View();
        }
    }
}
