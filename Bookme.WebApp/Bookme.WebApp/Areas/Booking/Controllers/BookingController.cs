using Microsoft.AspNetCore.Mvc;

namespace Bookme.WebApp.Areas.Booking.Controllers
{
    [Area("Booking")]
    public class BookingController : Controller
    {
        public IActionResult Index(int id)
        {
            return View(id);
        }
    }
}
