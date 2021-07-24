using System.ComponentModel.DataAnnotations;
using static Bookme.ViewModels.DataViewConstants.Business;

namespace Bookme.ViewModels.Business
{
    public class CreateBusinessViewModel
    {
        [Required]
        [Display(Name = "Business Name")]
        [StringLength(NAME_MAX_LENGTH, MinimumLength = NAME_MIN_LENGTH)]
        public string BusinessName { get; set; }

        [Required]
        [StringLength(DESCRIPTION_MAX_LENGTH, MinimumLength = DESCRIPTION_MIN_LENGTH)]
        public string Description { get; set; }

        [Required]
        [Display(Name = "Supported Location")]
        [StringLength(LOCATION_MAX_LENGTH, MinimumLength = LOCATION_MIN_LENGTH)]
        public string SupportedLocation { get; set; }

        [Required]
        [Display(Name = "Business Address")]
        [StringLength(ADDRESS_MAX_LENGTH, MinimumLength = ADDRESS_MIN_LENGTH)]
        public string Address { get; set; }

        [Required]
        [Display(Name = "Phone Number")]
        [StringLength(PHONE_MAX_LENGTH, MinimumLength = PHONE_MIN_LENGTH)]
        public string PhoneNumber { get; set; }

        [Required]
        [Url]
        [Display(Name = "Image Url")]
        public string ImageUrl { get; set; }
    }
}
