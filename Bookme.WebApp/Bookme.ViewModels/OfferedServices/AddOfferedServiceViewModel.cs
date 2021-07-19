using System.Collections.Generic;

namespace Bookme.ViewModels.OfferedServices
{
    public class AddOfferedServiceViewModel
    {
        public IEnumerable<ServiceCategoryViewModel> Categories { get; init; }
        public IEnumerable<VisitationTypeViewModel> VisitationTypes { get; init; }
    }
}
