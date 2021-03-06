using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using static Bookme.Data.DbConstants.OfferedService;

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
        [MaxLength(NAME_MAX_LENGTH)]
        public string Name { get; set; }

        [Required]
        [MaxLength(DESCRIPTION_MAX_LENGTH)]
        public string Description { get; set; }

        [Required]
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }

        [Required]
        public string ImageUrl { get; set; }

        public int Duration { get; set; }   
        
        public int ServiceCategoryId { get; set; }
        public ServiceCategory ServiceCategory { get; set; }

        public decimal Price { get; set; }
        public decimal VisitationPrice { get; set; }

        public ICollection<ServiceVisitation> ServiceVisitations { get; set; } = new HashSet<ServiceVisitation>();
    }
}
