using System;
using System.ComponentModel.DataAnnotations;

namespace Bookme.ViewModels.BookingConfiguration
{
    public class BreaksViewModel
    {
        public PartialBreakViewModel FirstBreak { get; set; }
        public PartialBreakViewModel SecondBreak { get; set; }
        public PartialBreakViewModel ThirdBreak { get; set; }
    }
}
