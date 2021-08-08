using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using static Bookme.Data.DbConstants.ServiceCategory;

namespace Bookme.Data.Models
{
    public class ServiceCategory
    {
        public int Id { get; init; }
        [Required]
        [MaxLength(NAME_MAX_LENGTH)]
        public string Name { get; set; }
        [Required]
        public string ImageUrl { get; set; }
        public ICollection<OfferedService> OfferedServices { get; set; } = new HashSet<OfferedService>();
    }
}
