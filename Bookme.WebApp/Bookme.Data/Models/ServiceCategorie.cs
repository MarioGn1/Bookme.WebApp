using System.Collections.Generic;

namespace Bookme.Data.Models
{
    public class ServiceCategorie
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<ApplicationUser> Businesses { get; set; } = new HashSet<ApplicationUser>();
    }
}
