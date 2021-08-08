using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Bookme.WebApp.Areas.Booking.Controllers
{
    [Area("Booking")]
    public class BusinessBookingsController : Controller
    {
        [Authorize]
        public IActionResult All()
        {
            return View();
        }
    }
}
