using Bookme.ViewModels.Business;
using System.Threading.Tasks;

namespace Bookme.Services.Contracts
{
    public interface IBusinessService
    {
        public Task CreateBusiness(CreateBusinessViewModel model, string userId);
        public Task EditBusinessInfo(CreateBusinessViewModel model, string userId);
    }
}
