using System;
using System.ComponentModel.DataAnnotations;

namespace Bookme.ViewModels.BookingConfiguration
{
    public class PartialBreakViewModel
    {
        [Display(Name = "Break Start")]
        [Range(typeof(TimeSpan), "00:00", "23:59")]
        public TimeSpan BreakStart { get; set; }

        [Display(Name = "Break End")]
        [Range(typeof(TimeSpan), "00:00", "23:59")]
        public TimeSpan BreakEnd { get; set; }
    }
}
