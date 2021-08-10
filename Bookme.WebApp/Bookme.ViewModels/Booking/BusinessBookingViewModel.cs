using System;

namespace Bookme.ViewModels.Booking
{
    public class BusinessBookingViewModel
    {
        public string ClientFullName { get; set; }
        public string ClientPhoneNumber { get; set; }
        public DateTime Date { get; set; }
        public int Duration { get; set; } = 0;
        public string BookedService { get; set; }
        public string Notes { get; set; }
    }
}
