using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Bookme.ViewModels.Booking
{
    public class BookingsByDayViewModel
    {
        public DateTime FirstDay { get; set; } = DateTime.Now;

        public ICollection<BookingViewModel> Bookings { get; set; }
    }
}
