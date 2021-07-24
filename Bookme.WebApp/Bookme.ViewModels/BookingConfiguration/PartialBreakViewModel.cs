using System;
using System.ComponentModel.DataAnnotations;

namespace Bookme.ViewModels.BookingConfiguration
{
    public class PartialBreakViewModel
    {
        [Display(Name = "Break Start")]
        public DateTime BreakStart { get; set; }

        [Display(Name = "Break End")]
        public DateTime BreakEnd { get; set; }
    }
}
