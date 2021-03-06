using Bookme.Services.Contracts;
using Bookme.ViewModels.Booking;
using Bookme.WebApp.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

using static Bookme.WebApp.Controllers.Constants.PaginationConstants;

namespace Bookme.WebApp.Areas.Booking.Controllers
{
    [Area("Booking")]
    public class BusinessBookingsController : Controller
    {
        private readonly IBookingService bookingService;

        public BusinessBookingsController(IBookingService bookingService)
        {
            this.bookingService = bookingService;
        }

        [Authorize]
        public IActionResult All(BusinessBookingsByDayViewModel model)
        {
            if (model.Date.Date < DateTime.Now.Date)
            {
                ModelState.AddModelError(nameof(model.Date), "Invalid Date, please reselect!");
                model.Date = DateTime.Now.Date;
            }

            var userId = this.User.GetId();

            model = bookingService.GetMyBusinessBookings(userId, model.Date, BOOKINGS_DAYS_PER_PAGE);

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            return View(model);
        }
    }
}
