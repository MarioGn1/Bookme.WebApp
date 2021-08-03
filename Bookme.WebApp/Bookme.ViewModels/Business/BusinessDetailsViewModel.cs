using Bookme.ViewModels.OfferedServices;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Bookme.ViewModels.Business
{
    public class BusinessDetailsViewModel : BusinessDetailsAbstract
    {
        [Display(Name = "Business Name:")]
        public override string BusinessName { get; init; }
        [Display(Name = "Description:")]
        public override string Description { get; init; }
        [Display(Name = "Supported Locations:")]
        public override string SupportedLocation { get; init; }
        [Display(Name = "Address:")]
        public override string Address { get; init; }
        [Display(Name = "Phone Number:")]
        public override string PhoneNumber { get; init; }
        public override string ImageUrl { get; init; }
        public IEnumerable<GetOfferedServiceViewModel> offeredServices { get; set; }
    }
}
