using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace Bookme.Data.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public Business Business { get; set; }

        public virtual ICollection<OfferedService> OfferedServices { get; set; } = new HashSet<OfferedService>();
        public virtual ICollection<Booking> Bookings { get; set; } = new HashSet<Booking>();
        public ICollection<Comment> Comments { get; set; } = new HashSet<Comment>();
        public ICollection<Raiting> Raitings { get; set; }
    }
}
