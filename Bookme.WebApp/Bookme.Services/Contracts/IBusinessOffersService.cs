using Bookme.ViewModels.OfferedServices;
using System.Collections.Generic;

namespace Bookme.Services.Contracts
{
    public interface IBusinessOffersService
    {
        public IEnumerable<ServiceCategoryViewModel> GetAllCategories();
    }
}
