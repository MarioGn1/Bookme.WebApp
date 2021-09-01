using Bookme.Services.Contracts;
using Bookme.ViewModels.Booking;
using Bookme.WebApp.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using static Bookme.WebApp.Controllers.Constants.TempDataConstants;

namespace Bookme.WebApp.Areas.Booking.Controllers
{
    [Area("Booking")]
    public class BookingController : Controller
    {
        private readonly IBookingService bookingService;
        private readonly IEmailService emailService;

        public BookingController(IBookingService bookingService, IEmailService emailService)
        {
            this.bookingService = bookingService;
            this.emailService = emailService;
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
        public async Task<IActionResult> Index(BookServiceViewModel model)
        {
            if (model.OwnerId == model.ClientId)
            {
                return Unauthorized();
            }

            var isAbleToBook = bookingService.CreateBooking(model);

            if (!isAbleToBook)
            {
                TempData[GLOBAL_MESSAGE_WARNING_KEY] = $"You already booked service for {model.Date}! Pleas check all your bookings for more details.";
                return Redirect($"/Booking/Booking/Index/{model.ServiceId}");
            }            

            TempData[GLOBAL_MESSAGE_KEY] = $"You successfuly booked {model.Name} service for {model.Date}!";

            var sendConfirmation = await emailService.SendEmailBookingConfirmation(model);
            if (sendConfirmation)
            {
                TempData[GLOBAL_MESSAGE_KEY] += " Confirmation mail has been sent to both sides!";
            }
            else
            {
                TempData[GLOBAL_MESSAGE_WARNING_KEY] = "Failed to send confirmation mail to both sides! Please contact the supplier manually.";
            }

            return Redirect($"/Category/Details/{model.OwnerId}");
        }
    }
}
