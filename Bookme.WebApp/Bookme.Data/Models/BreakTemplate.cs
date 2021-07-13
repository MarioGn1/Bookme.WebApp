using System;

namespace Bookme.Data.Models
{
    public class BreakTemplate
    {
        public int Id { get; set; }
        public int BookingConfigurationId { get; set; }
        public BookingConfiguration BookingConfiguration { get; set; }
        public DateTime BreakStart { get; set; }
        public DateTime BreakEnd { get; set; }
    }
}
