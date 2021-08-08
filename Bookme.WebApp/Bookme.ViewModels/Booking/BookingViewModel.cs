using System;

namespace Bookme.ViewModels.Booking
{
    public class BookingViewModel
    {
        public int Id { get; init; }
        public string BusinessId { get; set; }
        public string BusinessName { get; set; }
        public DateTime Date { get; set; }
        public int Duration { get; set; }
        public string BookedService { get; set; }
        public string Notes { get; set; }
    }
}
