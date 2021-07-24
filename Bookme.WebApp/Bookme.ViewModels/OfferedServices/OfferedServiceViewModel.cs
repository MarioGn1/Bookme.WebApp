using System.ComponentModel.DataAnnotations;
using static Bookme.ViewModels.DataViewConstants.OfferedService;

namespace Bookme.ViewModels.OfferedServices
{
    public class OfferedServiceViewModel
    {
        [Required]
        [StringLength(SERVICE_NAME_MAX_LENGTH, MinimumLength = SERVICE_NAME_MIN_LENGTH)]
        public string Name { get; set; }

        [Required]
        [StringLength(SERVICE_DESCRIPTION_MAX_LENGTH, MinimumLength = SERVICE_DESCRIPTION_MIN_LENGTH)]
        public string Description { get; set; }

        [Range(SERVICE_MIN_PRICE, SERVICE_MAX_PRICE)]
        public double Price { get; set; }

        [Range(SERVICE_MIN_PRICE, SERVICE_MAX_PRICE)]
        [Display(Name = "Visitation Price")]
        public double VisitationPrice { get; set; }

        [Range(SERVICE_DURATION_MIN_TIME, SERVICE_DURATION_MAX_TIME)]
        [Display(Name = "Duration (optional)")]
        public int? Duration { get; set; }

        [Display(Name = "Service Category")]
        public int ServiceCategoryId { get; set; }

        [Display(Name = "Visitation Type")]
        public int ServiceVisitationId { get; set; }

        [Required]
        [Url]
        [Display(Name = "Image Url")]
        public string ImageUrl { get; set; }
    }
}
