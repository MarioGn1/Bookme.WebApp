using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Bookme.Data.Models
{
    public class ServiceCategory
    {
        public int Id { get; init; }
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        public ICollection<OfferedService> OfferedServices { get; set; } = new HashSet<OfferedService>();
    }
}
