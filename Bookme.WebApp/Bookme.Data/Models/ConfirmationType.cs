using System.Collections.Generic;

namespace Bookme.Data.Models
{
    public class ConfirmationType
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public ICollection<Booking> Bookings { get; set; }
    }
}
