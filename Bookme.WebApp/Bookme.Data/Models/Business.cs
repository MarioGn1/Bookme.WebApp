using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using static Bookme.Data.DbConstants.Business;

namespace Bookme.Data.Models
{
    public class Business
    {
        public int Id { get; init; }

        [Required]
        [MaxLength(NAME_MAX_LENGTH)]
        public string BusinessName { get; set; }

        [Required]
        [MaxLength(DESCRIPTION_MAX_LENGTH)]
        public string Description { get; set; }

        [Required]
        [MaxLength(SUPPORTED_LOCATION_MAX_LENGTH)]
        public string SupportedLocation { get; set; }

        [MaxLength(ADDRESS_MAX_LENGTH)]
        public string Address { get; set; }

        [Required]
        [MaxLength(PHONE_MAX_LENGTH)]
        public string PhoneNumber { get; set; }

        [Required]
        public string ImageUrl { get; set; }

        [Required]
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }

        public int? BookingConfigurationId { get; set; }
        public BookingConfiguration BookingConfiguration { get; set; }

        public ICollection<Comment> Comments { get; set; } = new HashSet<Comment>();
    }
}
