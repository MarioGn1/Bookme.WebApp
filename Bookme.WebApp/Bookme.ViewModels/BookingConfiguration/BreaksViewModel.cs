using System;
using System.ComponentModel.DataAnnotations;

namespace Bookme.ViewModels.BookingConfiguration
{
    public class BreaksViewModel
    {
        [Display(Name = "First Break")]
        public PartialBreakViewModel FirstBreak { get; set; }
        [Display(Name = "Second Break (optional)")]
        public PartialBreakViewModel SecondBreak { get; set; }
        [Display(Name = "Third Break (optional)")]
        public PartialBreakViewModel ThirdBreak { get; set; }
    }
}
