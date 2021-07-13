using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Bookme.Data.Models
{
    public class OfferedService
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(20)]
        public string Name { get; set; }
        [Required]
        [MaxLength(200)]
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Duration { get; set; }
        [Required]
        public string BusinessId { get; set; }
        public ApplicationUser Business { get; set; }
        public int VisitationTypeId { get; set; }
        public VisitationType VisitationType { get; set; }
    }
}
