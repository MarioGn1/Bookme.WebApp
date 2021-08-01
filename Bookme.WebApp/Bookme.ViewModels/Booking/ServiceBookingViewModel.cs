using System.ComponentModel.DataAnnotations;
using static Bookme.ViewModels.DataViewConstants.Business;

namespace Bookme.ViewModels.Booking
{
    public class ServiceBookingViewModel
    {
        public int ServiceId { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public int Duration { get; set; }

        [Required]
        public string OwnerId { get; set; }

        [Display(Name = "Operated by:")]
        public string OperatorName { get; set; }

        [Required]
        public string ClientId { get; set; }

        [Required]
        [Display(Name = "First Name:")]
        [StringLength(NAME_MAX_LENGTH, MinimumLength = NAME_MIN_LENGTH)]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name:")]
        [StringLength(NAME_MAX_LENGTH, MinimumLength = NAME_MIN_LENGTH)]
        public string LastName { get; set; }

        [Required]
        [Display(Name = "Phone Number:")]
        [StringLength(PHONE_MAX_LENGTH, MinimumLength = PHONE_MIN_LENGTH)]
        public string PhoneNumber { get; set; }

        public string Notes { get; set; }
    }
}
