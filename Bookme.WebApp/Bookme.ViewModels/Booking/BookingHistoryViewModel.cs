using System;
using System.ComponentModel.DataAnnotations;

namespace Bookme.ViewModels.Booking
{
    public class BookingHistoryViewModel
    {
        public string BusinessId { get; set; }
        [Display(Name = "Business Name")]
        public string BusinessName { get; set; }
        public DateTime Date { get; set; }
        [Display(Name = "Booked Service")]
        public string BookedService { get; set; }
    }
}
