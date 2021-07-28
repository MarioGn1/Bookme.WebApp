using Bookme.ViewModels.OfferedServices;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Bookme.ViewModels.Business
{
    public class BusinessDetailsViewModel
    {
        [Display(Name = "Business Name:")]
        public string BusinessName { get; init; }
        [Display(Name = "Description:")]
        public string Description { get; init; }
        [Display(Name = "Supported Locations:")]
        public string SupportedLocation { get; init; }
        [Display(Name = "Address:")]
        public string Address { get; init; }
        [Display(Name = "Phone Number:")]
        public string PhoneNumber { get; init; }
        public string ImageUrl { get; init; }
        public IEnumerable<GetOfferedServiceViewModel> offeredServices { get; set; }
    }
}
