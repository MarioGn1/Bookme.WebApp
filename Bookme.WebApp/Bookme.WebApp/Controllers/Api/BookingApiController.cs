using Bookme.Services.Contracts;
using Bookme.ViewModels.Booking;
using Microsoft.AspNetCore.Mvc;

namespace Bookme.WebApp.Controllers.Api
{
    [ApiController]
    [Route("api/bookings/{id}/{date}")]
    public class BookingApiController : ControllerBase
    {
        private readonly IBookingService bookingService;

        public BookingApiController(IBookingService bookingService)
        {
            this.bookingService = bookingService;
        }

        [HttpGet]
        public SheduleViewModel GetDaySchedule(int id, string date)
        {
            var data = this.bookingService.GetDaySchedule(id, date);

            return data;
        }
    }
}
