using System;
using System.Collections.Generic;

namespace Bookme.ViewModels.Booking
{
    public class OwnerInfoViewModel
    {
        public int ServiceInterval { get; set; }
        public TimeSpan ShiftStart { get; set; }
        public TimeSpan ShiftEnd { get; set; }
        public ICollection<BreakViewModel> Breaks { get; set; }
    }
}
