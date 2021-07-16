using System;
using System.Collections.Generic;

namespace Bookme.Data.Models
{
    public class BookingConfiguration
    {
        public int Id { get; init; }
        public int ServiceInterval { get; set; }
        public DateTime ShiftStart { get; set; }
        public DateTime ShiftEnd { get; set; }
        public ApplicationUser Business { get; set; }
        public WeeklySchedule WeeklySchedule { get; set; }
        public ICollection<BreakTemplate> Breaks { get; set; } = new HashSet<BreakTemplate>();
    }
}
