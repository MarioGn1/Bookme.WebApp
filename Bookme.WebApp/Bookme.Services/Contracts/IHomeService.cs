using Bookme.ViewModels.HomeModels;
using System.Collections.Generic;

namespace Bookme.Services.Contracts
{
    public interface IHomeService
    {
        public IEnumerable<ClientHomeViewModel> GetAllCategories();
    }
}
