using System;
using System.Collections.Generic;

namespace Bookme.Data.Models
{
    public class BookingConfiguration
    {
        public int Id { get; init; }
        public int ServiceInterval { get; set; }
        public TimeSpan ShiftStart { get; set; }
        public TimeSpan ShiftEnd { get; set; }
        public Business Business { get; set; }
        public WeeklySchedule WeeklySchedule { get; set; }
        public ICollection<BreakTemplate> Breaks { get; set; } = new HashSet<BreakTemplate>();
    }
}
