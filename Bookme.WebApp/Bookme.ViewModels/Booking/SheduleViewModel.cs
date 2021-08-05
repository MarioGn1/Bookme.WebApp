using System;
using System.Collections.Generic;

namespace Bookme.ViewModels.Booking
{
    public class SheduleViewModel
    {
        public bool IsWorkingDay { get; set; }
        public OwnerInfoViewModel OwnerInfo { get; set; }
        public ICollection<BookedHourViewModel> bookedHours { get; set; } = new List<BookedHourViewModel>();
    }
}
