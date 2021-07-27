using Bookme.ViewModels.OfferedServices;
using System.Collections.Generic;

namespace Bookme.ViewModels.Business
{
    public class BusinessDetailsViewModel
    {
        public string BusinessName { get; init; }
        public string Description { get; init; }
        public string SupportedLocation { get; init; }
        public string Address { get; init; }
        public string PhoneNumber { get; init; }
        public string ImageUrl { get; init; }
        public IEnumerable<GetOfferedServiceViewModel> offeredServices { get; set; }
    }
}
