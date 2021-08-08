using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using static Bookme.Data.DbConstants.VisitationType;

namespace Bookme.Data.Models
{
    public class VisitationType
    {      
        public int Id { get; init; }
        [Required]
        [MaxLength(TYPE_MAX_LENGTH)]
        public string Type { get; set; }
        public ICollection<ServiceVisitation> ServiceVisitations { get; set; } = new HashSet<ServiceVisitation>();
    }
}
