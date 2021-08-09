using System;
using System.ComponentModel.DataAnnotations;

namespace Bookme.ViewModels.Booking
{
    public class BookingViewModel
    {
        public int Id { get; init; }
        public string BusinessId { get; set; }

        [Display(Name ="Operated by: ")]
        public string BusinessName { get; set; }

        [Display(Name = "Company phone: ")]
        public string BusinessPhone { get; set; }
        
        [Display(Name = "On: ")]
        public DateTime Date { get; set; }

        [Display(Name = "Service duration: ")]
        public int Duration { get; set; }

        public string BookedService { get; set; }

        [Display(Name = "Your notes: ")]
        public string Notes { get; set; }
    }
}
