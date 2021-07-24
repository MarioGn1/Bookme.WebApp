using Bookme.ViewModels.Business;

namespace Bookme.Services.Contracts
{
    public interface IBusinessService
    {
        public void CreateBusiness(CreateBusinessViewModel model, string userId);
    }
}
