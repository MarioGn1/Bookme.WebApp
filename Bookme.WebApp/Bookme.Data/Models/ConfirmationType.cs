using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using static Bookme.Data.DbConstants.Confirmation;

namespace Bookme.Data.Models
{
    public class ConfirmationType
    {
        public int Id { get; init; }
        [Required]
        [MaxLength(TYPE_MAX_LENGTH)]
        public string Type { get; set; }
        public ICollection<Booking> Bookings { get; set; }
    }
}
