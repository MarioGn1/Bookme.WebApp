using Bookme.Services.Contracts;
using Bookme.ViewModels.Booking;
using Bookme.WebApp.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Bookme.WebApp.Areas.Booking.Controllers
{
    [Area("Booking")]
    public class MyBookingsController : Controller
    {
        private readonly IBookingService bookingService;

        public MyBookingsController(IBookingService bookingService)
        {
            this.bookingService = bookingService;
        }

        [Authorize]
        public IActionResult All([FromQuery] BookingsByDayViewModel model,object date)
        {
            var userId = this.User.GetId();

            model = bookingService.GetAllMyBookings(userId, model.FirstDay);

            return View(model);
        }
    }
}
