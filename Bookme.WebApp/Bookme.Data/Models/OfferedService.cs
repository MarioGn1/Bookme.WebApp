using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using static Bookme.Data.DbConstants;

namespace Bookme.Data.Models
{
    public class OfferedService
    {
        public OfferedService()
        {
            this.VisitationPrice = DEFAULT_VISITATION_PRICE;
        }

        public int Id { get; init; }
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        [Required]
        [MaxLength(200)]
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Duration { get; set; }
        [Required]
        public string BusinessId { get; set; }
        public ApplicationUser Business { get; set; }
        public ICollection<ServiceVisitation> ServiceVisitations { get; set; } = new HashSet<ServiceVisitation>();
        public decimal VisitationPrice { get; set; }
    }
}
