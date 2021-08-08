using Bookme.ViewModels.Business;
using Bookme.ViewModels.OfferedServices;
using System.Collections.Generic;

namespace Bookme.Services.Contracts
{
    public interface IBusinessOffersService
    {
        public IEnumerable<ServiceCategoryViewModel> GetAllCategories();
        public IEnumerable<VisitationTypeViewModel> GetAllVisitationTypes();
        public AddOfferedServiceViewModel GetAddViewModel();
        public AddOfferedServiceViewModel GetEditViewModel(int serviceId);
        public bool CreateOfferedService(AddOfferedServiceViewModel model, string userId);
        public void EditOfferedService(AddOfferedServiceViewModel model, int serviceId);
        public bool CheckForCategory(int categoryId);
        public bool CheckForVisitationType(int visitationTypeId);
        public IEnumerable<GetOfferedServiceViewModel> GetAllBusinessServices(string userId);
        public BusinessDetailsViewModel GetBusinesDetails(string userId);
        public OfferedServiceDetailsViewModel GetServiceDetails(int serviceId);
        public bool CheckOwnership(int serviceId, string userId);
        public void DeleteService(int serviceId);
    }
}
