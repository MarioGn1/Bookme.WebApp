using System.Collections.Generic;

namespace Bookme.ViewModels.OfferedServices
{
    public class AddOfferedServiceViewModel
    {
        public CreateServiceViewModel OfferedService { get; set; }
        public IEnumerable<ServiceCategoryViewModel> Categories { get; set; }
        public IEnumerable<VisitationTypeViewModel> VisitationTypes { get; set; }
    }
}
