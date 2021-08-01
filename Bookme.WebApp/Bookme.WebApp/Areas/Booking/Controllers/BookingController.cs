using Bookme.Services.Contracts;
using Bookme.ViewModels.Booking;
using Bookme.WebApp.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Bookme.WebApp.Areas.Booking.Controllers
{
    [Area("Booking")]
    public class BookingController : Controller
    {
        private readonly IBookingService bookingService;

        public BookingController(IBookingService bookingService)
        {
            this.bookingService = bookingService;
        }

        [Authorize]
        public IActionResult Index(int id)
        {
            var userId = this.User.GetId();
            var model = bookingService.GetServiceInfo(id, userId);

            if (model.OwnerId == model.ClientId)
            {
                return Unauthorized();
            }

            return View(model);
        }

        [Authorize]
        [HttpPost]
        public IActionResult Index(BookServiceViewModel model)
        {
            if (model.OwnerId == model.ClientId)
            {
                return Unauthorized();
            }

            bookingService.CreateBooking(model);
            return Redirect("/");
        }
    }
}
