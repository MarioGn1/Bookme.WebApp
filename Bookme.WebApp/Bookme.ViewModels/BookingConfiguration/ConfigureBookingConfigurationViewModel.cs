using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Bookme.ViewModels.BookingConfiguration
{
    public class ConfigureBookingConfigurationViewModel
    {
        [Display(Name = "Shift Start")]
        public DateTime ShiftStart { get; set; }

        [Display(Name = "Shift End")]
        public DateTime ShiftEnd { get; set; }

        [Display(Name = "Service average duration (in minutes)")]
        public int ServiceInterval { get; set; }

        [Display(Name = "Working days")]
        public WeeklyScheduleViewModel WeeklySchedule { get; set; }
        public BreaksViewModel Breaks { get; set; }

    }
}
