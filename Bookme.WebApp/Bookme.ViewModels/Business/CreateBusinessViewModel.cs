using System.ComponentModel.DataAnnotations;
using static Bookme.ViewModels.DataViewConstants.Business;

namespace Bookme.ViewModels.Business
{
    public class CreateBusinessViewModel : BusinessDetailsAbstract
    {
        [Required]
        [Display(Name = "Business Name")]
        [StringLength(NAME_MAX_LENGTH, MinimumLength = NAME_MIN_LENGTH)]
        public override string BusinessName { get; init; }

        [Required]
        [StringLength(DESCRIPTION_MAX_LENGTH, MinimumLength = DESCRIPTION_MIN_LENGTH)]
        public override string Description { get; init; }

        [Required]
        [Display(Name = "Supported Location")]
        [StringLength(LOCATION_MAX_LENGTH, MinimumLength = LOCATION_MIN_LENGTH)]
        public override string SupportedLocation { get; init; }

        [Required]
        [Display(Name = "Business Address")]
        [StringLength(ADDRESS_MAX_LENGTH, MinimumLength = ADDRESS_MIN_LENGTH)]
        public override string Address { get; init; }

        [Required]
        [Display(Name = "Phone Number")]
        [StringLength(PHONE_MAX_LENGTH, MinimumLength = PHONE_MIN_LENGTH)]
        public override string PhoneNumber { get; init; }

        [Required]
        [Url]
        [Display(Name = "Image Url")]
        public override string ImageUrl { get; init; }
    }
}
