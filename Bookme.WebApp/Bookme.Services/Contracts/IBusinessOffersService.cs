using Bookme.ViewModels.OfferedServices;
using System.Collections.Generic;

namespace Bookme.Services.Contracts
{
    public interface IBusinessOffersService
    {
        public IEnumerable<ServiceCategoryViewModel> GetAllCategories();
        public IEnumerable<VisitationTypeViewModel> GetAllVisitationTypes();
        public AddOfferedServiceViewModel GetAddViewModel();
        public void CreateOfferedService(AddOfferedServiceViewModel model, string userId);
        public bool CheckForCategory(int categoryId);
        public bool CheckForVisitationType(int visitationTypeId);
        public IEnumerable<MyOfferedServiceViewModel> GetAllBusinessServices(string userId);
    }
}
