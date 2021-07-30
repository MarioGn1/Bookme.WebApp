using System;
using System.Collections.Generic;

namespace Bookme.ViewModels.Booking
{
    public class SheduleViewModel
    {
        public OwnerInfoViewModel OwnerInfo { get; set; }
        public ICollection<DateTime> bookedHours { get; set; } = new List<DateTime>();
    }
}
