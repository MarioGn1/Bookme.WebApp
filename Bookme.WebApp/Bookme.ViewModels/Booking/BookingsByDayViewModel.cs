using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Bookme.ViewModels.Booking
{
    public class BookingsByDayViewModel
    {
        public DateTime Date { get; set; } = DateTime.Now;
        public ICollection<ClientBookingViewModel> Bookings { get; set; }
    }
}
