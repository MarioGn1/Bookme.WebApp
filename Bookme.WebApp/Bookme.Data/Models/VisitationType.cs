using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Bookme.Data.Models
{
    public class VisitationType
    {      
        public int Id { get; init; }
        [Required]
        [MaxLength(20)]
        public string Type { get; set; }
        public ICollection<ServiceVisitation> ServiceVisitations { get; set; } = new HashSet<ServiceVisitation>();
    }
}
