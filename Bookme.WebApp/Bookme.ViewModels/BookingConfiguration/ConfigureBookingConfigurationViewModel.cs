using System;
using System.ComponentModel.DataAnnotations;
using static Bookme.ViewModels.DataViewConstants.BookingConfiguration;

namespace Bookme.ViewModels.BookingConfiguration
{
    public class ConfigureBookingConfigurationViewModel
    {
        [Display(Name = "Shift Start")]
        [Range(typeof(TimeSpan), "00:00", "23:59")]
        public TimeSpan ShiftStart { get; set; }

        [Display(Name = "Shift End")]
        [Range(typeof(TimeSpan), "00:00", "23:59")]
        public TimeSpan ShiftEnd { get; set; }

        [Display(Name = "Service average duration (in minutes)")]
        [Range(SERVICE_MIN_DURATION, SERVICE_MAX_DURATION)]
        public int ServiceInterval { get; set; }

        [Display(Name = "Working days")]
        public WeeklyScheduleViewModel WeeklySchedule { get; set; }
        public BreaksViewModel Breaks { get; set; }

    }
}
