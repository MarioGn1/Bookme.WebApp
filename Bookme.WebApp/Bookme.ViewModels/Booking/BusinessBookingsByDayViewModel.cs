using System;
using System.Collections.Generic;

namespace Bookme.ViewModels.Booking
{
    public class BusinessBookingsByDayViewModel
    {
        public DateTime Date { get; set; } = DateTime.Now;
        public ICollection<BusinessBookingViewModel> Bookings { get; set; }
    }
}
